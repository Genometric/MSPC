// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.IntervalParsers;
using Genometric.MSPC.Core.Model;
using Genometric.MSPC.IntervalTree;
using Genometric.MSPC.XSquaredData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Genometric.MSPC.Model
{
    internal class Processor<I>
        where I : IChIPSeqPeak, new()
    {
        private BackgroundWorker _worker;
        private DoWorkEventArgs _workerEventArgs;

        public delegate void ProgressUpdate(ProgressReport value);
        public event ProgressUpdate OnProgressUpdate;

        private double _xsqrd { set; get; }

        private Dictionary<uint, Result<I>> _analysisResults { set; get; }
        public ReadOnlyDictionary<uint, Result<I>> AnalysisResults
        {
            get { return new ReadOnlyDictionary<uint, Result<I>>(_analysisResults); }
        }

        private Dictionary<uint, Dictionary<string, Tree<I>>> _trees { set; get; }

        private Dictionary<string, SortedList<I, I>> _mergedReplicates { set; get; }
        public ReadOnlyDictionary<string, SortedList<I, I>> MergedReplicates
        {
            get { return new ReadOnlyDictionary<string, SortedList<I, I>>(_mergedReplicates); }
        }

        private List<double> _cachedChiSqrd { set; get; }

        private Config _config { set; get; }

        private Dictionary<uint, BED<I>> _samples { set; get; }

        internal int SamplesCount { get { return _samples.Count; } }

        internal Processor()
        {
            _samples = new Dictionary<uint, BED<I>>();
        }

        internal void AddSample(uint id, BED<I> peaks)
        {
            _samples.Add(id, peaks);
        }

        internal void Run(Config config, BackgroundWorker worker, DoWorkEventArgs e)
        {
            _config = config;
            _worker = worker;
            _workerEventArgs = e;
            
            int step = 1, stepCount = 4;

            if (CheckCancellationPending()) return;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Initializing"));
            BuildDataStructures();

            if (CheckCancellationPending()) return;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Processing samples"));
            ProcessSamples();

            if (CheckCancellationPending()) return;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Performing Multiple testing correction"));
            PerformMultipleTestingCorrection();

            if (CheckCancellationPending()) return;
            OnProgressUpdate(new ProgressReport(step, stepCount, "Creating consensus peaks set"));
            CreateConsensusPeaks();
        }

        private void BuildDataStructures()
        {
            _cachedChiSqrd = new List<double>();
            for (int i = 1; i <= _samples.Count; i++)
                _cachedChiSqrd.Add(Math.Round(ChiSquaredCache.ChiSqrdINVRTP(_config.Gamma, (byte)(i * 2)), 3));

            _trees = new Dictionary<uint, Dictionary<string, Tree<I>>>();
            _analysisResults = new Dictionary<uint, Result<I>>();
            foreach (var sample in _samples)
            {
                _trees.Add(sample.Key, new Dictionary<string, Tree<I>>());
                _analysisResults.Add(sample.Key, new Result<I>(_config.ReplicateType));
                foreach (var chr in sample.Value.Chromosomes)
                {
                    _trees[sample.Key].Add(chr.Key, new Tree<I>());
                    _analysisResults[sample.Key].AddChromosome(chr.Key, chr.Value.Statistics.Count);
                    foreach (var strand in chr.Value.Strands)
                        foreach (I p in strand.Value.Intervals)
                            if (p.Value < _config.TauW)
                                _trees[sample.Key][chr.Key].Add(p);
                }
            }
        }

        private void ProcessSamples()
        {
            Attributes attribute;
            foreach (var sample in _samples)
                foreach (var chr in sample.Value.Chromosomes)
                    foreach (var strand in chr.Value.Strands)
                        foreach (I peak in strand.Value.Intervals)
                        {
                            if (_worker.CancellationPending)
                                return;

                            _xsqrd = 0;
                            if (peak.Value < _config.TauS)
                                attribute = Attributes.Stringent;
                            else if (peak.Value < _config.TauW)
                                attribute = Attributes.Weak;
                            else
                            {
                                var pp = new ProcessedPeak<I>(peak, double.NaN, new List<SupportingPeak<I>>());
                                pp.Classification.Add(Attributes.Background);
                                _analysisResults[sample.Key].Chromosomes[chr.Key].AddOrUpdate(pp);
                                continue;
                            }

                            var supportingPeaks = FindSupportingPeaks(sample.Key, chr.Key, peak);
                            if (supportingPeaks.Count + 1 >= _config.C)
                            {
                                CalculateXsqrd(peak, supportingPeaks);
                                var pp = new ProcessedPeak<I>(peak, _xsqrd, supportingPeaks);
                                pp.Classification.Add(attribute);
                                if (_xsqrd >= _cachedChiSqrd[supportingPeaks.Count])
                                {
                                    pp.Classification.Add(Attributes.Confirmed);
                                    _analysisResults[sample.Key].Chromosomes[chr.Key].AddOrUpdate(pp);
                                    ProcessSupportingPeaks(sample.Key, chr.Key, peak, supportingPeaks, Attributes.Confirmed, Messages.Codes.M000);
                                }
                                else
                                {
                                    pp.reason = Messages.Codes.M001;
                                    pp.Classification.Add(Attributes.Discarded);
                                    _analysisResults[sample.Key].Chromosomes[chr.Key].AddOrUpdate(pp);
                                    ProcessSupportingPeaks(sample.Key, chr.Key, peak, supportingPeaks, Attributes.Discarded, Messages.Codes.M001);
                                }
                            }
                            else
                            {
                                var pp = new ProcessedPeak<I>(peak, _xsqrd, supportingPeaks);
                                pp.Classification.Add(attribute);
                                pp.Classification.Add(Attributes.Discarded);
                                pp.reason = Messages.Codes.M002;
                                _analysisResults[sample.Key].Chromosomes[chr.Key].AddOrUpdate(pp);
                            }
                        }
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
                            if ((_config.MultipleIntersections == MultipleIntersections.UseLowestPValue && sp.Value < chosenPeak.Value) ||
                                (_config.MultipleIntersections == MultipleIntersections.UseHighestPValue && sp.Value > chosenPeak.Value))
                                chosenPeak = sp;

                        supportingPeaks.Add(new SupportingPeak<I>(chosenPeak, tree.Key));
                        break;
                }
            }

            return supportingPeaks;
        }

        private void ProcessSupportingPeaks(uint id, string chr, I p, List<SupportingPeak<I>> supportingPeaks, Attributes attribute, Messages.Codes message)
        {
            foreach (var supPeak in supportingPeaks)
            {
                var tSupPeak = new List<SupportingPeak<I>>();
                tSupPeak.Add(new SupportingPeak<I>(p, id));
                foreach (var sP in supportingPeaks)
                    if (supPeak.CompareTo(sP) != 0)
                        tSupPeak.Add(sP);

                var pp = new ProcessedPeak<I>(supPeak.Source, _xsqrd, tSupPeak);
                pp.Classification.Add(attribute);
                pp.reason = message;

                if (supPeak.Source.Value <= _config.TauS)
                    pp.Classification.Add(Attributes.Stringent);
                else
                    pp.Classification.Add(Attributes.Weak);

                _analysisResults[supPeak.SampleID].Chromosomes[chr].AddOrUpdate(pp);
            }
        }

        private void CalculateXsqrd(I p, List<SupportingPeak<I>> supportingPeaks)
        {
            if (p.Value != 0)
                _xsqrd = Math.Log(p.Value, Math.E);
            else
                _xsqrd = Math.Log(Config.default0PValue, Math.E);

            foreach (var supPeak in supportingPeaks)
                if (supPeak.Source.Value != 0)
                    _xsqrd += Math.Log(supPeak.Source.Value, Math.E);
                else
                    _xsqrd += Math.Log(Config.default0PValue, Math.E);

            _xsqrd = _xsqrd * (-2);

            if (_xsqrd >= Math.Abs(Config.defaultMaxLogOfPVvalue))
                _xsqrd = Math.Abs(Config.defaultMaxLogOfPVvalue);
        }

        /// <summary>
        /// Benjamini–Hochberg procedure (step-up procedure)
        /// </summary>
        private void PerformMultipleTestingCorrection()
        {
            foreach (var result in _analysisResults)
            {
                foreach (var chr in result.Value.Chromosomes)
                {
                    var confirmedPeaks = chr.Value.Get(Attributes.Confirmed).ToList();
                    chr.Value.SetTruePositiveCount(confirmedPeaks.Count);
                    chr.Value.SetFalsePositiveCount(0);
                    int m = confirmedPeaks.Count;

                    // Sorts confirmed peaks set based on their p-values.
                    confirmedPeaks.Sort(new Comparers.CompareProcessedPeakByValue<I>());

                    for (int i = 0; i < m; i++)
                    {
                        if (confirmedPeaks[i].Source.Value <= ((i + 1) / (double)m) * _config.Alpha)
                            confirmedPeaks[i].Classification.Add(Attributes.TruePositive);
                        else
                            confirmedPeaks[i].Classification.Add(Attributes.FalsePositive);
                        confirmedPeaks[i].AdjPValue = confirmedPeaks[i].Source.Value * (m / (i + 1));
                    }
                    // Sorts confirmed peaks set based on coordinates using default comparer.
                    confirmedPeaks.Sort();
                }
            }
        }

        private void CreateConsensusPeaks()
        {
            _mergedReplicates = new Dictionary<string, SortedList<I, I>>();
            foreach (var result in _analysisResults)
            {
                foreach (var chr in result.Value.Chromosomes)
                {
                    if (!_mergedReplicates.ContainsKey(chr.Key))
                        _mergedReplicates.Add(chr.Key, new SortedList<I, I>());

                    foreach (var confirmedPeak in chr.Value.Get(Attributes.Confirmed))
                    {
                        var peak = confirmedPeak.Source;
                        var interval = new I
                        {
                            Left = peak.Left,
                            Right = peak.Right,
                            Name = "MSPC"
                        };

                        I mergingPeak = new I
                        {
                            Left = peak.Left,
                            Right = peak.Right,
                            Name = "MSPC",
                            Value =
                            (-2) * Math.Log((peak.Value == 0 ? Config.default0PValue : peak.Value), Math.E)
                        };

                        while (_mergedReplicates[chr.Key].TryGetValue(interval, out I mergedPeak))
                        {
                            _mergedReplicates[chr.Key].Remove(interval);
                            interval.Left = Math.Min(interval.Left, mergedPeak.Left);
                            interval.Right = Math.Max(interval.Right, mergedPeak.Right);
                            mergingPeak.Left = interval.Left;
                            mergingPeak.Right = interval.Right;
                            mergingPeak.Value += mergedPeak.Value;
                        }

                        if (mergingPeak.Value >= Math.Abs(Config.defaultMaxLogOfPVvalue))
                            mergingPeak.Value = Math.Abs(Config.defaultMaxLogOfPVvalue);

                        _mergedReplicates[chr.Key].Add(interval, mergingPeak);
                    }
                }
            }

            int c = 0;
            foreach (var chr in _mergedReplicates)
                foreach (var peak in chr.Value)
                    peak.Value.Name = "MSPC_peak_" + (c++);
        }

        private bool CheckCancellationPending()
        {
            if (_worker.CancellationPending)
            {
                _analysisResults = new Dictionary<uint, Result<I>>();
                OnProgressUpdate(new ProgressReport(-1, -1, "Canceled current task."));
                _workerEventArgs.Cancel = true;
                return true;
            }
            return false;
        }
    }
}
