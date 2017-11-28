/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Genometric.IGenomics;
using System;
using System.Collections.Generic;
using Genometric.MSPC.Model;
using System.Collections.ObjectModel;
using Genometric.MSPC.Core.Model;
using System.ComponentModel;
using System.Threading;

namespace Genometric.MSPC.Core
{
    public class MSPC<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        public event EventHandler<ValueEventArgs> StatusChanged;
        private void OnStatusValueChaned(ProgressReport value)
        {
            StatusChanged?.Invoke(this, new ValueEventArgs(value));
        }

        public AutoResetEvent done;
        public AutoResetEvent canceled;

        private Processor<Peak, Metadata> _processor { set; get; }
        private BackgroundWorker _backgroundProcessor { set; get; }

        private ReadOnlyDictionary<uint, AnalysisResult<Peak, Metadata>> _results { set; get; }

        public MSPC()
        {
            _processor = new Processor<Peak, Metadata>();
            _processor.OnProgressUpdate += _processorOnProgressUpdate;
            _backgroundProcessor = new BackgroundWorker();
            _backgroundProcessor.DoWork += _doWork;
            _backgroundProcessor.RunWorkerCompleted += _runWorkerCompleted;
            done = new AutoResetEvent(false);
            canceled = new AutoResetEvent(false);
        }

        public void AddSample(uint id, Dictionary<string, Dictionary<char, List<Peak>>> sample)
        {
            _processor.AddSample(id, sample);
        }

        public ReadOnlyDictionary<uint, AnalysisResult<Peak, Metadata>> Run(Config config)
        {
            if (_processor.SamplesCount < 2)
                throw new InvalidOperationException(String.Format("Minimum two samples are required; {} is given.", _processor.SamplesCount));

            _processor.cancel = false;
            _results = _processor.Run(config);
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

        public ReadOnlyDictionary<uint, AnalysisResult<Peak, Metadata>> GetResults()
        {
            return _results;
        }

        public ReadOnlyDictionary<string, SortedList<Peak, Peak>> GetMergedReplicates()
        {
            return _processor.MergedReplicates;
        }

        private void _doWork(object sender, DoWorkEventArgs e)
        {
            _results = _processor.Run((Config)e.Argument);
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
