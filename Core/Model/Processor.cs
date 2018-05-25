/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.GeUtilities.IntervalParsers;
using Genometric.MSPC.XSquaredData;
using Genometric.MSPC.IntervalTree;
using Genometric.MSPC.Core.Model;

namespace Genometric.MSPC.Model
{
    internal class Processor<I>
        where I : IChIPSeqPeak, new()
    {
        public delegate void ProgressUpdate(ProgressReport value);
        public event ProgressUpdate OnProgressUpdate;

        internal bool cancel;

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

        /// <summary>
        /// <list type="bullet">
        /// <item><description>
        ///     uint: sample ID.
        /// </description></item>
        /// <item><description>
        ///     string: chromosome label.
        /// </description></item>
        /// <item><description>
        ///     char: chromosome strand.
        /// </description></item>
        /// </list>
        /// </summary>
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

        internal ReadOnlyDictionary<uint, Result<I>> Run(Config config)
        {
            int step = 1, stepCount = 5;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Initializing"));

            _config = config;
            _cachedChiSqrd = new List<double>();
            for (int i = 1; i <= _samples.Count; i++)
                _cachedChiSqrd.Add(Math.Round(ChiSquaredCache.ChiSqrdINVRTP(config.Gamma, (byte)(i * 2)), 3));

            _trees = new Dictionary<uint, Dictionary<string, Tree<I>>>();
            _analysisResults = new Dictionary<uint, Result<I>>();
            foreach (var sample in _samples)
            {
                if (cancel) return null;
                _trees.Add(sample.Key, new Dictionary<string, Tree<I>>());
                _analysisResults.Add(sample.Key, new Result<I>());
                foreach (var chr in sample.Value.Chromosomes)
                {
                    if (cancel) return null;
                    _trees[sample.Key].Add(chr.Key, new Tree<I>());
                    _analysisResults[sample.Key].AddChromosome(chr.Key);
                    foreach (var strand in chr.Value.Strands)
                    {
                        foreach (I p in strand.Value.Intervals)
                        {
                            if (cancel) return null;
                            if (p.Value < _config.TauW)
                                _trees[sample.Key][chr.Key].Add(p);
                        }
                    }
                }
            }

            OnProgressUpdate(new ProgressReport(step++, stepCount, "Processing samples"));
            Attributes attribute;
            foreach (var sample in _samples)
                foreach (var chr in sample.Value.Chromosomes)
                    foreach (var strand in chr.Value.Strands)
                        foreach (I peak in strand.Value.Intervals)
                        {
                            if (cancel) return null;
                            _xsqrd = 0;
                            if (peak.Value < _config.TauS)
                                attribute = Attributes.Stringent;
                            else if (peak.Value < _config.TauW)
                                attribute = Attributes.Weak;
                            else
                            {
                                var pp = new ProcessedPeak<I>(peak, _xsqrd, new List<SupportingPeak<I>>());
                                pp.Classification.Add(Attributes.Background);
                                _analysisResults[sample.Key].Chromosomes[chr.Key].Add(pp);
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
                                    _analysisResults[sample.Key].Chromosomes[chr.Key].Add(pp);
                                    ProcessSupportingPeaks(sample.Key, chr.Key, pp.Source, supportingPeaks, Attributes.Confirmed, Messages.Codes.M000);
                                }
                                else
                                {
                                    pp.reason = Messages.Codes.M001;
                                    pp.Classification.Add(Attributes.Discarded);
                                    _analysisResults[sample.Key].Chromosomes[chr.Key].Add(pp);
                                    ProcessSupportingPeaks(sample.Key, chr.Key, pp.Source, supportingPeaks, Attributes.Discarded, Messages.Codes.M001);
                                }
                            }
                            else
                            {
                                var pp = new ProcessedPeak<I>(peak, _xsqrd, supportingPeaks);
                                pp.Classification.Add(attribute);
                                pp.Classification.Add(Attributes.Discarded);
                                pp.reason = Messages.Codes.M002;
                                _analysisResults[sample.Key].Chromosomes[chr.Key].Add(pp);
                            }
                        }

            if (cancel) return null;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Processing intermediate sets"));
            ProcessIntermediaSets();

            if (cancel) return null;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Performing Multiple testing correction"));
            PerformMultipleTestingCorrection();

            if (cancel) return null;
            OnProgressUpdate(new ProgressReport(step, stepCount, "Creating consensus peaks set"));
            CreateConsensusPeaks();

            return AnalysisResults;
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
                if (!_analysisResults[supPeak.SampleID].Chromosomes[chr].Contains(attribute, supPeak.Source.HashKey))
                {
                    var tSupPeak = new List<SupportingPeak<I>>();
                    var targetSample = _analysisResults[supPeak.SampleID];
                    tSupPeak.Add(new SupportingPeak<I>(p, id));

                    foreach (var sP in supportingPeaks)
                        if (supPeak.CompareTo(sP) != 0)
                            tSupPeak.Add(sP);

                    var anRe = new ProcessedPeak<I>(supPeak.Source, _xsqrd, tSupPeak);
                    anRe.Classification.Add(attribute);
                    anRe.reason = message;

                    if (supPeak.Source.Value <= _config.TauS)
                        anRe.Classification.Add(Attributes.Stringent);
                    else
                        anRe.Classification.Add(Attributes.Weak);

                    targetSample.Chromosomes[chr].Add(anRe);
                }
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

        private void ProcessIntermediaSets()
        {
            if (_config.ReplicateType == ReplicateType.Biological)
                /// Performe : R_j__d = R_j__d \ { R_j__d intersection R_j__c }
                foreach (var result in _analysisResults)
                    foreach (var chr in result.Value.Chromosomes)
                        foreach (var confirmedPeak in chr.Value.Get(Attributes.Confirmed))
                            chr.Value.Remove(Attributes.Discarded, confirmedPeak.Source.HashKey);
            else
                /// Performe : R_j__c = R_j__c \ { R_j__c intersection R_j__d }
                foreach (var result in _analysisResults)
                    foreach (var chr in result.Value.Chromosomes)
                        foreach (var discardedPeak in chr.Value.Get(Attributes.Discarded))
                            chr.Value.Remove(Attributes.Confirmed, discardedPeak.Source.HashKey);
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
                    chr.Value.SetTruePositiveCount((uint)chr.Value.Get(Attributes.Confirmed).Count);
                    chr.Value.SetFalsePositiveCount(0);
                    var confirmedPeaks = chr.Value.Get(Attributes.Confirmed);
                    int m = confirmedPeaks.Count;

                    // Sorts confirmed peaks set based on their p-values.
                    confirmedPeaks.Sort(new Comparers.CompareProcessedPeakByValue<I>());

                    for (int k = 1; k <= m; k++)
                    {
                        if (confirmedPeaks[k - 1].Source.Value > (k / (double)m) * _config.Alpha)
                        {
                            k--;
                            for (int l = 1; l < k; l++)
                            {
                                confirmedPeaks[l].AdjPValue = ((k * confirmedPeaks[l].Source.Value) / m) * _config.Alpha;
                                confirmedPeaks[l].Classification.Add(Attributes.TruePositive);
                            }
                            for (int l = k; l < m; l++)
                            {
                                confirmedPeaks[l].AdjPValue = ((k * confirmedPeaks[l].Source.Value) / m) * _config.Alpha;
                                confirmedPeaks[l].Classification.Add(Attributes.FalsePositive);
                            }

                            chr.Value.SetTruePositiveCount((uint)k);
                            chr.Value.SetFalsePositiveCount((uint)(m - k));
                            break;
                        }
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
    }
}
