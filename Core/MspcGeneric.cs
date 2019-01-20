// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Functions;
using Genometric.MSPC.Core.Interfaces;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace Genometric.MSPC.Core
{
    public class Mspc<I>
        where I : IPPeak
    {
        public event EventHandler<ValueEventArgs> StatusChanged;
        private void OnStatusValueChaned(ProgressReport value)
        {
            StatusChanged?.Invoke(this, new ValueEventArgs(value));
        }

        public AutoResetEvent Done { set; get; }
        public AutoResetEvent Canceled { set; get; }

        private Processor<I> _processor { set; get; }
        private BackgroundWorker _backgroundProcessor { set; get; }

        public int DegreeOfParallelism
        {
            set { _processor.DegreeOfParallelism = value; }
            get { return _processor.DegreeOfParallelism; }
        }

        public Mspc(IPeakConstructor<I> peakConstructor, bool trackSupportingRegions = false)
        {
            _processor = new Processor<I>(peakConstructor, trackSupportingRegions);
            _processor.OnProgressUpdate += _processorOnProgressUpdate;
            _backgroundProcessor = new BackgroundWorker();
            _backgroundProcessor.DoWork += _doWork;
            _backgroundProcessor.RunWorkerCompleted += _runWorkerCompleted;
            _backgroundProcessor.WorkerSupportsCancellation = true;
            Done = new AutoResetEvent(false);
            Canceled = new AutoResetEvent(false);
        }

        public void Run(List<Bed<I>> samples, Config config)
        {
            if (samples.Count < 2)
                throw new InvalidOperationException(string.Format("Minimum two samples are required; {0} is given.", samples.Count));

            _processor.Run(samples, config, new BackgroundWorker(), new DoWorkEventArgs(null));
        }

        public void RunAsync(List<Bed<I>> samples, Config config)
        {
            Done.Reset();
            Canceled.Reset();

            if (samples.Count < 2)
                throw new InvalidOperationException(string.Format("Minimum two samples are required; {0} is given.", samples.Count));

            if (_backgroundProcessor.IsBusy)
                Cancel();
            _backgroundProcessor.RunWorkerAsync(new List<object>() { samples, config });
        }

        public void Cancel()
        {
            Canceled.Reset();
            _backgroundProcessor.CancelAsync();
            Canceled.WaitOne();
            Done.Reset();
            Canceled.Reset();
        }

        public Dictionary<string, List<I>> GetConsensusPeaks()
        {
            return _processor.ConsensusPeaks;
        }

        private void _doWork(object sender, DoWorkEventArgs e)
        {
            var args = (List<object>)e.Argument;
            _processor.Run((List<Bed<I>>)args[0], (Config)args[1], sender as BackgroundWorker, e);
        }

        private void _runWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Canceled.Set();
            Done.Set();
        }

        private void _processorOnProgressUpdate(ProgressReport value)
        {
            OnStatusValueChaned(value);
        }
    }
}
