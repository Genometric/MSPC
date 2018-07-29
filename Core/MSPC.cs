// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.IntervalParsers;
using Genometric.MSPC.Core.Model;
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace Genometric.MSPC
{
    public class MSPC<I>
        where I : IChIPSeqPeak, new()
    {
        public event EventHandler<ValueEventArgs> StatusChanged;
        private void OnStatusValueChaned(ProgressReport value)
        {
            StatusChanged?.Invoke(this, new ValueEventArgs(value));
        }

        public AutoResetEvent done;
        public AutoResetEvent canceled;

        private Processor<I> _processor { set; get; }
        private BackgroundWorker _backgroundProcessor { set; get; }

        private ReadOnlyDictionary<uint, Result<I>> _results { set; get; }

        public MSPC()
        {
            _processor = new Processor<I>();
            _processor.OnProgressUpdate += _processorOnProgressUpdate;
            _backgroundProcessor = new BackgroundWorker();
            _backgroundProcessor.DoWork += _doWork;
            _backgroundProcessor.RunWorkerCompleted += _runWorkerCompleted;
            done = new AutoResetEvent(false);
            canceled = new AutoResetEvent(false);
        }

        public void AddSample(uint id, BED<I> sample)
        {
            _processor.AddSample(id, sample);
        }

        public ReadOnlyDictionary<uint, Result<I>> Run(Config config)
        {
            if (_processor.SamplesCount < 2)
                throw new InvalidOperationException(String.Format("Minimum two samples are required; {0} is given.", _processor.SamplesCount));

            _processor.cancel = false;
            _processor.Run(config);
            _results = _processor.AnalysisResults;
            return GetResults();
        }

        public void RunAsync(Config config)
        {
            if (_processor.SamplesCount < 2)
                throw new InvalidOperationException(String.Format("Minimum two samples are required; {} is given.", _processor.SamplesCount));

            done.Reset();
            canceled.Reset();
            _processor.cancel = false;
            if (_backgroundProcessor.IsBusy)
                Cancel();
            _backgroundProcessor.RunWorkerAsync(config);
        }

        public void Cancel()
        {
            canceled.Reset();
            _processor.cancel = true;
            canceled.WaitOne();
        }

        public void CancelAsync()
        {
            canceled.Reset();
            _processor.cancel = true;
        }

        public ReadOnlyDictionary<uint, Result<I>> GetResults()
        {
            return _results;
        }

        public ReadOnlyDictionary<string, SortedList<I, I>> GetMergedReplicates()
        {
            return _processor.MergedReplicates;
        }

        private void _doWork(object sender, DoWorkEventArgs e)
        {
            _processor.Run((Config)e.Argument);
            _results = _processor.AnalysisResults;
        }

        private void _runWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            canceled.Set();
            done.Set();
        }

        private void _processorOnProgressUpdate(ProgressReport value)
        {
            OnStatusValueChaned(value);
        }
    }
}
