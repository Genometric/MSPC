using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Functions;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace Genometric.MSPC.Core
{
    public class Mspc<I>
        where I : IPeak
    {
        public event EventHandler<ValueEventArgs> StatusChanged;
        private void OnStatusValueChaned(ProgressReport value)
        {
            StatusChanged?.Invoke(this, new ValueEventArgs(value));
        }

        public AutoResetEvent Done { set; get; }
        public AutoResetEvent Canceled { set; get; }

        private readonly Processor<I> _processor;
        private readonly BackgroundWorker _backgroundProcessor;

        private Dictionary<uint, Result<I>> _results;

        public int? DegreeOfParallelism
        {
            get { return _processor.DegreeOfParallelism; }
        }

        public Mspc(
            IPeakConstructor<I> peakConstructor,
            bool trackSupportingRegions = false,
            int? maxDegreeOfParallelism = null)
        {
            _processor = new Processor<I>(
                peakConstructor,
                trackSupportingRegions,
                maxDegreeOfParallelism);

            _processor.OnProgressUpdate += ProcessorOnProgressUpdate;
            _backgroundProcessor = new BackgroundWorker();
            _backgroundProcessor.DoWork += DoWork;
            _backgroundProcessor.RunWorkerCompleted += RunWorkerCompleted;
            _backgroundProcessor.WorkerSupportsCancellation = true;
            Done = new AutoResetEvent(false);
            Canceled = new AutoResetEvent(false);
        }

        public void AddSample(uint id, Bed<I> sample)
        {
            _processor.AddSample(id, sample);
        }

        public Dictionary<uint, Result<I>> Run(Config config)
        {
            if (_processor.SamplesCount < 2)
                throw new InvalidOperationException(string.Format("Minimum two samples are required; {0} is given.", _processor.SamplesCount));

            _processor.Run(config, new BackgroundWorker(), new DoWorkEventArgs(null));
            _results = _processor.AnalysisResults;
            return GetResults();
        }

        public void RunAsync(Config config)
        {
            Done.Reset();
            Canceled.Reset();

            if (_processor.SamplesCount < 2)
                throw new InvalidOperationException(string.Format("Minimum two samples are required; {0} is given.", _processor.SamplesCount));

            if (_backgroundProcessor.IsBusy)
                Cancel();
            _backgroundProcessor.RunWorkerAsync(config);
        }

        public void Cancel()
        {
            Canceled.Reset();
            _backgroundProcessor.CancelAsync();
            Canceled.WaitOne();
            Done.Reset();
            Canceled.Reset();
        }

        public Dictionary<uint, Result<I>> GetResults()
        {
            return _results;
        }

        public Dictionary<string, Dictionary<char, List<ProcessedPeak<I>>>> GetConsensusPeaks()
        {
            return _processor.ConsensusPeaks;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            _processor.Run((Config)e.Argument, sender as BackgroundWorker, e);
            _results = _processor.AnalysisResults;
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Canceled.Set();
            Done.Set();
        }

        private void ProcessorOnProgressUpdate(ProgressReport value)
        {
            OnStatusValueChaned(value);
        }
    }
}
