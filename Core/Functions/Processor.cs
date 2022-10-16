using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.Intervals.Genome;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.IntervalTree;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Genometric.MSPC.Core.Functions
{
    internal class Processor<I>
        where I : IPeak
    {
        private int _processedPeaks;
        private int _peaksToBeProcessed;

        private BackgroundWorker _worker;
        private DoWorkEventArgs _workerEventArgs;

        public delegate void ProgressUpdate(ProgressReport value);
        public event ProgressUpdate OnProgressUpdate;

        private Dictionary<uint, Result<I>> _analysisResults;
        public Dictionary<uint, Result<I>> AnalysisResults
        {
            get { return new Dictionary<uint, Result<I>>(_analysisResults); }
        }

        private Dictionary<string, Dictionary<char, Tree<I>>> _tree;

        private Dictionary<string, Dictionary<char, List<ProcessedPeak<I>>>> _consensusPeaks;
        public Dictionary<string, Dictionary<char, List<ProcessedPeak<I>>>> ConsensusPeaks
        {
            get
            {
                return new
                    Dictionary<string,
                    Dictionary<char,
                    List<ProcessedPeak<I>>>>(_consensusPeaks);
            }
        }

        public int? DegreeOfParallelism { get; }

        private List<double> _cachedChiSqrd;

        private readonly bool _trackSupportingRegions;

        private Config _config;

        private readonly Dictionary<uint, Bed<I>> _samples;

        private readonly IPeakConstructor<I> _peakConstructor;

        internal int SamplesCount { get { return _samples.Count; } }

        internal Processor(
            IPeakConstructor<I> peakConstructor,
            bool trackSupportingRegions,
            int? maxDegreeOfParallelism = null)
        {
            _samples = new Dictionary<uint, Bed<I>>();
            _peakConstructor = peakConstructor;
            _trackSupportingRegions = trackSupportingRegions;
            DegreeOfParallelism = maxDegreeOfParallelism;
        }

        internal void AddSample(uint id, Bed<I> peaks)
        {
            _samples.Add(id, peaks);
        }

        internal void Run(Config config, BackgroundWorker worker, DoWorkEventArgs e)
        {
            _config = config;
            _worker = worker;
            _workerEventArgs = e;
            _processedPeaks = 0;
            _peaksToBeProcessed = 0;

            int step = 1, stepCount = 4;

            OnProgressUpdate(new ProgressReport(step++, stepCount, false, false, "Initializing"));
            CacheChiSqrdData();
            BuildDataStructures();

            if (CheckCancellationPending()) return;
            OnProgressUpdate(new ProgressReport(step++, stepCount, false, false, "Processing samples"));
            OnProgressUpdate(new ProgressReport(_processedPeaks, _peaksToBeProcessed, true, true, "peaks"));
            ProcessSamples();

            if (CheckCancellationPending()) return;
            OnProgressUpdate(new ProgressReport(step++, stepCount, false, false, "Performing Multiple testing correction"));
            FalseDiscoveryRate<I>.PerformMultipleTestingCorrection(_analysisResults, _config.Alpha, DegreeOfParallelism);

            if (CheckCancellationPending()) return;
            OnProgressUpdate(new ProgressReport(step, stepCount, false, false, "Creating consensus peaks set"));
            _consensusPeaks = new ConsensusPeaks<I>().Compute(_analysisResults, _peakConstructor, DegreeOfParallelism, _config.Alpha);
        }

        private void CacheChiSqrdData()
        {
            _cachedChiSqrd = new List<double>();
            for (int i = 1; i <= _samples.Count; i++)
                _cachedChiSqrd.Add(Math.Round(ChiSqrd.ChiSqrdINVRTP(_config.Gamma, i * 2), 3));
        }

        private void BuildDataStructures()
        {
            _tree = new Dictionary<string, Dictionary<char, Tree<I>>>();
            _analysisResults = new Dictionary<uint, Result<I>>();
            foreach (var sample in _samples)
            {
                _analysisResults.Add(sample.Key, new Result<I>(_config.ReplicateType));
                foreach (var chr in sample.Value.Chromosomes)
                {
                    if (!_tree.ContainsKey(chr.Key))
                        _tree.Add(chr.Key, new Dictionary<char, Tree<I>>());
                    _analysisResults[sample.Key].AddChromosome(chr.Key);
                    foreach (var strand in chr.Value.Strands)
                    {
                        _analysisResults[sample.Key].AddStrand(
                            chr.Key, strand.Key, chr.Value.Statistics.Count);
                        if (!_tree[chr.Key].ContainsKey(strand.Key))
                            _tree[chr.Key].Add(strand.Key, new Tree<I>());

                        foreach (I p in strand.Value.Intervals)
                        {
                            if (p.Value < _config.TauW)
                            {
                                _tree[chr.Key][strand.Key].Add(p, sample.Key);
                                _peaksToBeProcessed++;
                            }
                        }
                    }
                }
            }

            var options = new ParallelOptions();
            if (DegreeOfParallelism is not null)
                options.MaxDegreeOfParallelism = (int)DegreeOfParallelism;
            Parallel.ForEach(
                _tree,
                options,
                tree =>
                {
                    foreach (var strand in tree.Value.Values)
                        strand.BuildAndFinalize();
                });
        }

        private void ProcessSamples()
        {
            var options = new ParallelOptions();
            if (DegreeOfParallelism is not null)
                options.MaxDegreeOfParallelism = (int)DegreeOfParallelism;

            foreach (var sample in _samples)
                Parallel.ForEach(
                    sample.Value.Chromosomes,
                    options,
                    chr =>
                    {
                        ProcessChr(sample.Key, chr);
                    });
        }

        private void ProcessChr(uint sampleKey, KeyValuePair<string, Chromosome<I, BedStats>> chr)
        {
            Attributes attribute;
            foreach (var strand in chr.Value.Strands)
            {
                foreach (I peak in strand.Value.Intervals)
                {
                    if (_worker.CancellationPending)
                        return;

                    if (peak.Value < _config.TauS)
                        attribute = Attributes.Stringent;
                    else if (peak.Value < _config.TauW)
                        attribute = Attributes.Weak;
                    else
                    {
                        var pp = new ProcessedPeak<I>(peak, double.NaN);
                        pp.AddClassification(Attributes.Background);
                        _analysisResults[sampleKey].Chromosomes[chr.Key][strand.Key].AddOrUpdate(pp);
                        continue;
                    }

                    var supportingPeaks = FindSupportingPeaks(sampleKey, chr.Key, strand.Key, peak);
                    if (supportingPeaks.Count + 1 >= _config.C)
                    {
                        double xsqrd = CalculateXsqrd(peak, supportingPeaks);
                        ProcessedPeak<I> pp;
                        if (_trackSupportingRegions)
                            pp = new ProcessedPeak<I>(peak, xsqrd, supportingPeaks);
                        else
                            pp = new ProcessedPeak<I>(peak, xsqrd, supportingPeaks.Count);
                        pp.AddClassification(attribute);
                        if (xsqrd >= _cachedChiSqrd[supportingPeaks.Count])
                        {
                            pp.AddClassification(Attributes.Confirmed);
                            _analysisResults[sampleKey].Chromosomes[chr.Key][strand.Key].AddOrUpdate(pp);
                            ProcessSupportingPeaks(
                                sampleKey, chr.Key, strand.Key, peak, supportingPeaks,
                                xsqrd, Attributes.Confirmed, Messages.Codes.M000);
                        }
                        else
                        {
                            pp.reason = Messages.Codes.M001;
                            pp.AddClassification(Attributes.Discarded);
                            _analysisResults[sampleKey].Chromosomes[chr.Key][strand.Key].AddOrUpdate(pp);
                            ProcessSupportingPeaks(
                                sampleKey, chr.Key, strand.Key, peak, supportingPeaks,
                                xsqrd, Attributes.Discarded, Messages.Codes.M001);
                        }
                    }
                    else
                    {
                        var pp = new ProcessedPeak<I>(peak, 0, supportingPeaks.Count);
                        pp.AddClassification(attribute);
                        pp.AddClassification(Attributes.Discarded);
                        pp.reason = Messages.Codes.M002;
                        _analysisResults[sampleKey].Chromosomes[chr.Key][strand.Key].AddOrUpdate(pp);
                    }

                    Interlocked.Increment(ref _processedPeaks);
                }
                OnProgressUpdate(new ProgressReport(_processedPeaks, _peaksToBeProcessed, true, true, "peaks processed"));
            }
        }

        private List<SupportingPeak<I>> FindSupportingPeaks(uint id, string chr, char strand, I queryPeak)
        {
            var supportingPeaks = new List<SupportingPeak<I>>();
            if (!_tree.ContainsKey(chr))
                return supportingPeaks;

            var overlappingPeaks = new List<NodeData<I>>();
            overlappingPeaks = _tree[chr][strand].GetIntervals(queryPeak, id);

            switch (overlappingPeaks.Count)
            {
                case 0: break;

                case 1:
                    if (overlappingPeaks[0].SampleID != id)
                        supportingPeaks.Add(new SupportingPeak<I>(overlappingPeaks[0].Peak, overlappingPeaks[0].SampleID));
                    break;

                default:
                    var _supPeakFilterHelper = new Dictionary<uint, SupportingPeak<I>>(overlappingPeaks.Count);
                    foreach (var p in overlappingPeaks)
                    {
                        if (_supPeakFilterHelper.TryGetValue(p.SampleID, out SupportingPeak<I> chosenPeak))
                        {
                            if ((_config.MultipleIntersections == MultipleIntersections.UseLowestPValue
                            && p.Peak.Value < chosenPeak.Source.Value) ||
                            (_config.MultipleIntersections == MultipleIntersections.UseHighestPValue
                            && p.Peak.Value > chosenPeak.Source.Value))
                                chosenPeak.Source = p.Peak;
                        }
                        else
                        {
                            _supPeakFilterHelper.Add(p.SampleID, new SupportingPeak<I>(p.Peak, p.SampleID));
                        }
                    }

                    supportingPeaks.AddRange(_supPeakFilterHelper.Values);
                    break;
            }

            return supportingPeaks;
        }

        private void ProcessSupportingPeaks
            (uint id, string chr, char strand, I p, List<SupportingPeak<I>> supportingPeaks,
            double xsqrd, Attributes attribute, Messages.Codes message)
        {
            foreach (var supPeak in supportingPeaks)
            {
                ProcessedPeak<I> pp;
                if (_trackSupportingRegions)
                {
                    var tSupPeak = new List<SupportingPeak<I>> { new SupportingPeak<I>(p, id) };
                    foreach (var sP in supportingPeaks)
                        if (supPeak.CompareTo(sP) != 0)
                            tSupPeak.Add(sP);
                    pp = new ProcessedPeak<I>(supPeak.Source, xsqrd, tSupPeak);
                }
                else
                {
                    pp = new ProcessedPeak<I>(supPeak.Source, xsqrd, supportingPeaks.Count);
                }

                pp.AddClassification(attribute);
                pp.reason = message;

                if (supPeak.Source.Value <= _config.TauS)
                    pp.AddClassification(Attributes.Stringent);
                else
                    pp.AddClassification(Attributes.Weak);

                _analysisResults[supPeak.SampleID].Chromosomes[chr][strand].AddOrUpdate(pp);
            }
        }

        private static double CalculateXsqrd(I p, List<SupportingPeak<I>> supportingPeaks)
        {
            double xsqrd;
            if (p.Value != 0)
                xsqrd = Math.Log(p.Value, Math.E);
            else
                xsqrd = Math.Log(Config.default0PValue, Math.E);

            foreach (var supPeak in supportingPeaks)
                if (supPeak.Source.Value != 0)
                    xsqrd += Math.Log(supPeak.Source.Value, Math.E);
                else
                    xsqrd += Math.Log(Config.default0PValue, Math.E);

            xsqrd *= (-2);

            return xsqrd;
        }

        private bool CheckCancellationPending()
        {
            if (_worker.CancellationPending)
            {
                _analysisResults = new Dictionary<uint, Result<I>>();
                OnProgressUpdate(new ProgressReport(0, 0, false, false, "Canceled current task."));
                _workerEventArgs.Cancel = true;
                return true;
            }
            return false;
        }
    }
}
