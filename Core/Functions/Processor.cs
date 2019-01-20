// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.Intervals.Genome;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Interfaces;
using Genometric.MSPC.Core.IntervalTree;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Genometric.MSPC.Core.Functions
{
    internal class Processor<I>
        where I : IPPeak
    {
        private int _processedPeaks;
        private int _peaksToBeProcessed;

        private BackgroundWorker _worker;
        private DoWorkEventArgs _workerEventArgs;

        public delegate void ProgressUpdate(ProgressReport value);
        public event ProgressUpdate OnProgressUpdate;

        private Dictionary<uint, Dictionary<string, Tree<I>>> _trees { set; get; }

        public Dictionary<string, List<I>> ConsensusPeaks { set; get; }

        public int DegreeOfParallelism { set; get; }

        private List<double> _cachedChiSqrd { set; get; }

        private readonly bool _trackSupportingRegions;

        private Config _config { set; get; }

        private readonly IPeakConstructor<I> _peakConstructor;

        private ReplicateType _replicateType;

        internal Processor(IPeakConstructor<I> peakConstructor, bool trackSupportingRegions)
        {
            _peakConstructor = peakConstructor;
            _trackSupportingRegions = trackSupportingRegions;
            DegreeOfParallelism = Environment.ProcessorCount;
        }

        private List<Bed<I>> _samples;

        internal void Run(List<Bed<I>> samples, Config config, BackgroundWorker worker, DoWorkEventArgs e)
        {
            _samples = samples;
            _config = config;
            _worker = worker;
            _workerEventArgs = e;
            _processedPeaks = 0;
            _peaksToBeProcessed = 0;
            _replicateType = config.ReplicateType;

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
            var fdr = new FalseDiscoveryRate<I>();
            fdr.PerformMultipleTestingCorrection(samples, _config.Alpha, DegreeOfParallelism);

            if (CheckCancellationPending()) return;
            OnProgressUpdate(new ProgressReport(step, stepCount, false, false, "Creating consensus peaks set"));
            ConsensusPeaks = new ConsensusPeaks<I>().Compute(samples, _peakConstructor, DegreeOfParallelism, _config.Alpha);
        }

        private void CacheChiSqrdData()
        {
            _cachedChiSqrd = new List<double>();
            for (int i = 1; i <= _samples.Count; i++)
                _cachedChiSqrd.Add(Math.Round(ChiSqrd.ChiSqrdINVRTP(_config.Gamma, i * 2), 3));
        }

        private void BuildDataStructures()
        {
            _trees = new Dictionary<uint, Dictionary<string, Tree<I>>>();
            foreach (var sample in _samples)
            {
                _trees.Add(sample.FileHashKey, new Dictionary<string, Tree<I>>());
                foreach (var chr in sample.Chromosomes)
                {
                    _trees[sample.FileHashKey].Add(chr.Key, new Tree<I>());
                    foreach (var strand in chr.Value.Strands)
                        foreach (I p in strand.Value.Intervals)
                            if (p.Value < _config.TauW)
                            {
                                _trees[sample.FileHashKey][chr.Key].Add(p);
                                _peaksToBeProcessed++;
                            }
                }
            }

            foreach (var sampleTree in _trees)
                Parallel.ForEach(
                    sampleTree.Value,
                    new ParallelOptions { MaxDegreeOfParallelism = DegreeOfParallelism },
                    tree => { tree.Value.BuildAndFinalize(); });
        }

        private void ProcessSamples()
        {
            void processChr(uint sampleKey, KeyValuePair<string, Chromosome<I, BedStats>> chr)
            {
                ProcessChr(sampleKey, chr);
            }

            foreach (var sample in _samples)
                Parallel.ForEach(
                    sample.Chromosomes,
                    new ParallelOptions { MaxDegreeOfParallelism = DegreeOfParallelism },
                    chr =>
                    {
                        processChr(sample.FileHashKey, chr);
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
                        if (ShouldUpdate(peak))
                            peak.AddClassification(Attributes.Background);
                        continue;
                    }

                    var supportingPeaks = FindSupportingPeaks(sampleKey, chr.Key, peak);
                    if (supportingPeaks.Count + 1 >= _config.C)
                    {
                        double xsqrd = CalculateXsqrd(peak, supportingPeaks);
                        if (xsqrd >= _cachedChiSqrd[supportingPeaks.Count])
                        {
                            if (ShouldUpdate(peak))
                            {
                                peak.AddClassification(attribute);
                                peak.AddClassification(Attributes.Confirmed);
                                peak.XSquared = xsqrd;
                                if (_trackSupportingRegions)
                                    peak.SupportingPeaks = null; //supportingPeaks
                                else
                                    peak.SupportingPeaksCount = supportingPeaks.Count;
                            }
                            ProcessSupportingPeaks(
                                sampleKey,
                                peak, supportingPeaks,
                                xsqrd, Attributes.Confirmed, Messages.Codes.M000);
                        }
                        else
                        {
                            if (ShouldUpdate(peak))
                            {
                                peak.AddClassification(attribute);
                                peak.AddClassification(Attributes.Discarded);
                                peak.SetReason(Messages.Codes.M001);
                                peak.XSquared = xsqrd;
                                if (_trackSupportingRegions)
                                    peak.SupportingPeaks = null; //supportingPeaks
                                else
                                    peak.SupportingPeaksCount = supportingPeaks.Count;
                            }
                            ProcessSupportingPeaks(
                                sampleKey,
                                peak, supportingPeaks,
                                xsqrd, Attributes.Discarded, Messages.Codes.M001);
                        }
                    }
                    else
                    {
                        if (ShouldUpdate(peak))
                        {
                            peak.AddClassification(attribute);
                            peak.AddClassification(Attributes.Discarded);
                            peak.SetReason(Messages.Codes.M002);
                            peak.SupportingPeaksCount = supportingPeaks.Count;
                        }
                    }

                    Interlocked.Increment(ref _processedPeaks);
                }
                OnProgressUpdate(new ProgressReport(_processedPeaks, _peaksToBeProcessed, true, true, "peaks processed"));
            }
        }

        private bool ShouldUpdate(I peak)
        {
            if (_replicateType == ReplicateType.Biological)
            {
                if ((peak.HasAttribute(Attributes.Discarded) && peak.HasAttribute(Attributes.Confirmed)) ||
                    (!peak.HasAttribute(Attributes.Confirmed) && !peak.HasAttribute(Attributes.Discarded)))
                    return true;
            }
            else
            {
                if (peak.HasAttribute(Attributes.Confirmed) && peak.HasAttribute(Attributes.Discarded) ||
                    (!peak.HasAttribute(Attributes.Confirmed) && !peak.HasAttribute(Attributes.Discarded)))
                    return true;
            }
            return false;
        }

        private bool ShouldUpdate(SupportingPeak<I> peak)
        {
            return ShouldUpdate(peak.Source);
        }

        private List<SupportingPeak<I>> FindSupportingPeaks(uint id, string chr, I p)
        {
            var supportingPeaks = new List<SupportingPeak<I>>();
            foreach (var tree in _trees)
            {
                if (tree.Key == id)
                    continue;

                var sps = new List<I>();
                if (_trees[tree.Key].ContainsKey(chr))
                    sps = _trees[tree.Key][chr].GetIntervals(p);

                switch (sps.Count)
                {
                    case 0: break;

                    case 1:
                        supportingPeaks.Add(new SupportingPeak<I>(sps[0], tree.Key));
                        break;

                    default:
                        var chosenPeak = sps[0];
                        foreach (var sp in sps.Skip(1))
                            if ((_config.MultipleIntersections == MultipleIntersections.UseLowestPValue
                                && sp.Value < chosenPeak.Value) ||
                                (_config.MultipleIntersections == MultipleIntersections.UseHighestPValue
                                && sp.Value > chosenPeak.Value))
                                chosenPeak = sp;

                        supportingPeaks.Add(new SupportingPeak<I>(chosenPeak, tree.Key));
                        break;
                }
            }

            return supportingPeaks;
        }
        // TODO: There could be two type of processing supporting peaks:
        // 1) takes Supporting peaks and tracks to which sample they belong to, if trackSupporting Regions is true.
        // 2) takes I and does not track to which sample peaks belong to, and returns only their count if _trackSupportingRegions is false.
        private void ProcessSupportingPeaks
            (uint id, I p, List<SupportingPeak<I>> supportingPeaks,
            double xsqrd, Attributes attribute, Messages.Codes message)
        {
            foreach (var supPeak in supportingPeaks)
            {
                if (!ShouldUpdate(supPeak))
                    continue;

                var tSupPeaks = new List<SupportingPeak<I>> { new SupportingPeak<I>(p, id) };
                foreach (var sP in supportingPeaks)
                    if (supPeak.CompareTo(sP) != 0)
                        tSupPeaks.Add(sP);

                supPeak.Source.XSquared = xsqrd;
                if (_trackSupportingRegions)
                    supPeak.Source.SupportingPeaks = null; // tSupPeaks
                else
                    supPeak.Source.SupportingPeaksCount = tSupPeaks.Count;

                supPeak.Source.AddClassification(attribute);
                supPeak.Source.SetReason(message);

                if (supPeak.Source.Value <= _config.TauS)
                    supPeak.Source.AddClassification(Attributes.Stringent);
                else
                    supPeak.Source.AddClassification(Attributes.Weak);
            }
        }

        private double CalculateXsqrd(I p, List<SupportingPeak<I>> supportingPeaks)
        {
            double xsqrd;
            if (p.Value != 0)
                xsqrd = Math.Log(p.Value, Math.E);
            else
                xsqrd = Math.Log(Config.default0PValue, Math.E);

            foreach (var suI in supportingPeaks)
                if (suI.Source.Value != 0)
                    xsqrd += Math.Log(suI.Source.Value, Math.E);
                else
                    xsqrd += Math.Log(Config.default0PValue, Math.E);

            xsqrd = xsqrd * (-2);

            return xsqrd;
        }

        private bool CheckCancellationPending()
        {
            if (_worker.CancellationPending)
            {
                OnProgressUpdate(new ProgressReport(0, 0, false, false, "Canceled current task."));
                _workerEventArgs.Cancel = true;
                return true;
            }
            return false;
        }
    }
}
