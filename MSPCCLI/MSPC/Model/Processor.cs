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
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Genometric.MSPC.Core.IntervalTree;
using System.Collections.ObjectModel;
using Genometric.MSPC.Core.XSquaredData;
using System.Threading;

namespace Genometric.MSPC.Core.Model
{
    internal class Processor<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        public delegate void ProgressUpdate(ProgressReport value);
        public event ProgressUpdate OnProgressUpdate;

        internal bool cancel;

        private double _xsqrd { set; get; }

        private Dictionary<uint, AnalysisResult<Peak, Metadata>> _analysisResults { set; get; }
        public ReadOnlyDictionary<uint, AnalysisResult<Peak, Metadata>> AnalysisResults
        {
            get { return new ReadOnlyDictionary<uint, AnalysisResult<Peak, Metadata>>(_analysisResults); }
        }

        private Dictionary<uint, Dictionary<string, Tree<Peak, Metadata>>> _trees { set; get; }

        private Dictionary<string, SortedList<Peak, Peak>> _mergedReplicates { set; get; }
        public ReadOnlyDictionary<string, SortedList<Peak, Peak>> MergedReplicates
        {
            get { return new ReadOnlyDictionary<string, SortedList<Peak, Peak>>(_mergedReplicates); }
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
        private Dictionary<uint, Dictionary<string, Dictionary<char, List<Peak>>>> _samples { set; get; }

        internal int SamplesCount { get { return _samples.Count; } }

        internal Processor()
        {
            _samples = new Dictionary<uint, Dictionary<string, Dictionary<char, List<Peak>>>>();
        }

        internal void AddSample(uint id, Dictionary<string, Dictionary<char, List<Peak>>> peaks)
        {
            _samples.Add(id, peaks);
        }

        internal ReadOnlyDictionary<uint, AnalysisResult<Peak, Metadata>> Run(Config config)
        {
            int step = 1, stepCount = 6;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Initializing"));

            _config = config;
            _cachedChiSqrd = new List<double>();
            for (int i = 1; i <= _samples.Count; i++)
                _cachedChiSqrd.Add(Math.Round(ChiSquaredCache.ChiSqrdINVRTP(config.Gamma, (byte)(i * 2)), 3));

            _trees = new Dictionary<uint, Dictionary<string, Tree<Peak, Metadata>>>();
            _analysisResults = new Dictionary<uint, AnalysisResult<Peak, Metadata>>();
            foreach (var sample in _samples)
            {
                if (cancel) return null;
                _trees.Add(sample.Key, new Dictionary<string, Tree<Peak, Metadata>>());
                _analysisResults.Add(sample.Key, new AnalysisResult<Peak, Metadata>());
                foreach (var chr in sample.Value)
                {
                    if (cancel) return null;
                    _trees[sample.Key].Add(chr.Key, new Tree<Peak, Metadata>());
                    _analysisResults[sample.Key].AddChromosome(chr.Key);
                    foreach (var strand in chr.Value)
                    {
                        if (cancel) return null;
                        foreach (Peak p in strand.Value)
                        {
                            if (cancel) return null;
                            if (p.metadata.value <= _config.TauW)
                                _trees[sample.Key][chr.Key].Add(p);
                            else
                                _analysisResults[sample.Key].R_j__b[chr.Key].Add(p);
                        }
                    }
                }
            }

            OnProgressUpdate(new ProgressReport(step++, stepCount, "Processing samples"));
            foreach (var sample in _samples)
                foreach (var chr in sample.Value)
                    foreach (var strand in chr.Value)
                        foreach (Peak peak in strand.Value)
                        {
                            if (cancel) return null;
                            _xsqrd = 0;

                            // Initial assessment: classifying peak as strong or weak based on p-value.
                            if (peak.metadata.value <= _config.TauS)
                                _analysisResults[sample.Key].R_j__s[chr.Key].Add(peak);
                            else if (peak.metadata.value <= _config.TauW)
                                _analysisResults[sample.Key].R_j__w[chr.Key].Add(peak);
                            else
                                continue;

                            // Combined stringency assessment: confirming or discarding a peak based on
                            // (a) the number of peaks it overlaps with, and (b) the combined p-value of
                            // the overlapping peaks calculated using Fisher's combined probability test.
                            var supportingPeaks = FindSupportingPeaks(sample.Key, chr.Key, peak);
                            if (supportingPeaks.Count + 1 >= _config.C)
                            {
                                CalculateXsqrd(peak, supportingPeaks);
                                if (_xsqrd >= _cachedChiSqrd[supportingPeaks.Count])
                                    ConfirmPeak(sample.Key, chr.Key, peak, supportingPeaks);
                                else
                                    DiscardPeak(sample.Key, chr.Key, peak, supportingPeaks, 0);
                            }
                            else
                            {
                                DiscardPeak(sample.Key, chr.Key, peak, supportingPeaks, 1);
                            }
                        }

            if (cancel) return null;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Processing intermediate sets"));
            ProcessIntermediaSets();

            if (cancel) return null;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Creating output set"));
            CreateOuputSet();

            if (cancel) return null;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Performing Multiple testing correction"));
            PerformMultipleTestingCorrection();

            if (cancel) return null;
            OnProgressUpdate(new ProgressReport(step++, stepCount, "Creating consensus peaks set"));
            CreateConsensusPeaks();

            return AnalysisResults;
        }

        private List<AnalysisResult<Peak, Metadata>.SupportingPeak> FindSupportingPeaks(uint id, string chr, Peak p)
        {
            var supportingPeaks = new List<AnalysisResult<Peak, Metadata>.SupportingPeak>();
            foreach(var tree in _trees)
            {
                if (tree.Key == id)
                    continue;

                var sps = new List<Peak>();
                if (_trees[tree.Key].ContainsKey(chr))
                    sps = _trees[tree.Key][chr].GetIntervals(p);

                switch (sps.Count)
                {
                    case 0: break;

                    case 1:
                        supportingPeaks.Add(new AnalysisResult<Peak, Metadata>.SupportingPeak()
                        {
                            peak = sps[0],
                            sampleID = tree.Key
                        });
                        break;

                    default:
                        var chosenPeak = sps[0];
                        foreach (var sp in sps.Skip(1))
                            if ((_config.MultipleIntersections == MultipleIntersections.UseLowestPValue && sp.metadata.value < chosenPeak.metadata.value) ||
                                (_config.MultipleIntersections == MultipleIntersections.UseHighestPValue && sp.metadata.value > chosenPeak.metadata.value))
                                chosenPeak = sp;

                        supportingPeaks.Add(new AnalysisResult<Peak, Metadata>.SupportingPeak()
                        {
                            peak = chosenPeak,
                            sampleID = tree.Key
                        });
                        break;
                }
            }

            return supportingPeaks;
        }

        private void ConfirmPeak(uint id, string chr, Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks)
        {
            var anRe = new AnalysisResult<Peak, Metadata>.ProcessedPeak()
            {
                peak = p,
                xSquared = _xsqrd,
                rtp = ChiSquaredCache.ChiSqrdDistRTP(_xsqrd, 2 + (supportingPeaks.Count * 2)),
                supportingPeaks = supportingPeaks
            };

            if (p.metadata.value <= _config.TauS)
            {
                _analysisResults[id].R_j___sc[chr]++;
                anRe.classification = PeakClassificationType.StringentConfirmed;
            }
            else
            {
                _analysisResults[id].R_j___wc[chr]++;
                anRe.classification = PeakClassificationType.WeakConfirmed;
            }

            if (!_analysisResults[id].R_j__c[chr].ContainsKey(p.metadata.hashKey))
                _analysisResults[id].R_j__c[chr].Add(p.metadata.hashKey, anRe);

            ConfirmSupportingPeaks(id, chr, p, supportingPeaks);
        }

        private void ConfirmSupportingPeaks(uint id, string chr, Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks)
        {
            foreach (var supPeak in supportingPeaks)
            {
                if (!_analysisResults[supPeak.sampleID].R_j__c[chr].ContainsKey(supPeak.peak.metadata.hashKey))
                {
                    var tSupPeak = new List<AnalysisResult<Peak, Metadata>.SupportingPeak>();
                    var targetSample = _analysisResults[supPeak.sampleID];
                    tSupPeak.Add(new AnalysisResult<Peak, Metadata>.SupportingPeak() { peak = p, sampleID = id });

                    foreach (var sP in supportingPeaks)
                        if (supPeak.CompareTo(sP) != 0)
                            tSupPeak.Add(sP);

                    var anRe = new AnalysisResult<Peak, Metadata>.ProcessedPeak()
                    {
                        peak = supPeak.peak,
                        xSquared = _xsqrd,
                        rtp = ChiSquaredCache.ChiSqrdDistRTP(_xsqrd, 2 + (supportingPeaks.Count * 2)),
                        supportingPeaks = tSupPeak
                    };

                    if (supPeak.peak.metadata.value <= _config.TauS)
                    {
                        targetSample.R_j___sc[chr]++;
                        anRe.classification = PeakClassificationType.StringentConfirmed;
                    }
                    else
                    {
                        targetSample.R_j___wc[chr]++;
                        anRe.classification = PeakClassificationType.WeakConfirmed;
                    }

                    targetSample.R_j__c[chr].Add(supPeak.peak.metadata.hashKey, anRe);
                }
            }
        }

        private void DiscardPeak(uint id, string chr, Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks, byte discardReason)
        {
            var anRe = new AnalysisResult<Peak, Metadata>.ProcessedPeak
            {
                peak = p,
                xSquared = _xsqrd,
                reason = discardReason,
                supportingPeaks = supportingPeaks
            };

            if (p.metadata.value <= _config.TauS)
            {
                // The cause of discarding the region is :
                if (supportingPeaks.Count + 1 >= _config.C)
                    _analysisResults[id].R_j__sdt[chr]++;  // - Test failure
                else _analysisResults[id].R_j__sdc[chr]++; // - insufficient intersecting regions count

                anRe.classification = PeakClassificationType.StringentDiscarded;
            }
            else
            {
                // The cause of discarding the region is :
                if (supportingPeaks.Count + 1 >= _config.C)
                    _analysisResults[id].R_j__wdt[chr]++;  // - Test failure
                else _analysisResults[id].R_j__wdc[chr]++; // - insufficient intersecting regions count

                anRe.classification = PeakClassificationType.WeakDiscarded;
            }

            if (!_analysisResults[id].R_j__d[chr].ContainsKey(p.metadata.hashKey))
                _analysisResults[id].R_j__d[chr].Add(p.metadata.hashKey, anRe);

            if (supportingPeaks.Count + 1 >= _config.C)
                DiscardSupportingPeaks(id, chr, p, supportingPeaks, discardReason);
        }

        private void DiscardSupportingPeaks(uint id, string chr, Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks, byte discardReason)
        {
            foreach (var supPeak in supportingPeaks)
            {
                if (!_analysisResults[supPeak.sampleID].R_j__d[chr].ContainsKey(supPeak.peak.metadata.hashKey))
                {
                    var tSupPeak = new List<AnalysisResult<Peak, Metadata>.SupportingPeak>();
                    var targetSample = _analysisResults[supPeak.sampleID];
                    tSupPeak.Add(new AnalysisResult<Peak, Metadata>.SupportingPeak() { peak = p, sampleID = id });

                    foreach (var sP in supportingPeaks)
                        if (supPeak.CompareTo(sP) != 0)
                            tSupPeak.Add(sP);

                    var anRe = new AnalysisResult<Peak, Metadata>.ProcessedPeak()
                    {
                        peak = supPeak.peak,
                        xSquared = _xsqrd,
                        reason = discardReason,
                        rtp = ChiSquaredCache.ChiSqrdDistRTP(_xsqrd, 2 + (supportingPeaks.Count * 2)),
                        supportingPeaks = tSupPeak
                    };

                    if (supPeak.peak.metadata.value <= _config.TauS)
                    {
                        targetSample.R_j__sdt[chr]++;
                        anRe.classification = PeakClassificationType.StringentDiscarded;
                    }
                    else
                    {
                        targetSample.R_j__wdt[chr]++;
                        anRe.classification = PeakClassificationType.WeakDiscarded;
                    }

                    targetSample.R_j__d[chr].Add(supPeak.peak.metadata.hashKey, anRe);
                }
            }
        }

        private void CalculateXsqrd(Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks)
        {
            if (p.metadata.value != 0)
                _xsqrd = Math.Log(p.metadata.value, Math.E);
            else
                _xsqrd = Math.Log(Config.default0PValue, Math.E);

            foreach (var supPeak in supportingPeaks)
                if (supPeak.peak.metadata.value != 0)
                    _xsqrd += Math.Log(supPeak.peak.metadata.value, Math.E);
                else
                    _xsqrd += Math.Log(Config.default0PValue, Math.E);

            _xsqrd = _xsqrd * (-2);

            if (_xsqrd >= Math.Abs(Config.defaultMaxLogOfPVvalue))
                _xsqrd = Math.Abs(Config.defaultMaxLogOfPVvalue);
        }

        private void ProcessIntermediaSets()
        {
            if (_config.ReplicateType == ReplicateType.Biological)
            {
                // Performe : R_j__d = R_j__d \ { R_j__d intersection R_j__c }

                foreach(var result in _analysisResults)
                {
                    foreach(var chr in result.Value.R_j__c)
                    {
                        foreach (var confirmedPeak in chr.Value)
                        {
                            if (result.Value.R_j__d[chr.Key].ContainsKey(confirmedPeak.Key))
                            {
                                if (confirmedPeak.Value.peak.metadata.value <= _config.TauS)
                                    result.Value.total_scom++;
                                else if (confirmedPeak.Value.peak.metadata.value <= _config.TauW)
                                    result.Value.total_wcom++;

                                result.Value.R_j__d[chr.Key].Remove(confirmedPeak.Key);
                            }
                        }
                    }
                }
            }
            else
            {
                // Performe : R_j__c = R_j__c \ { R_j__c intersection R_j__d }

                foreach(var result in _analysisResults)
                {
                    foreach(var chr in result.Value.R_j__d)
                    {
                        foreach (var discardedPeak in chr.Value)
                        {
                            if (result.Value.R_j__c[chr.Key].ContainsKey(discardedPeak.Key))
                            {
                                if (discardedPeak.Value.peak.metadata.value <= _config.TauS)
                                    result.Value.total_scom++;
                                else if (discardedPeak.Value.peak.metadata.value <= _config.TauW)
                                    result.Value.total_wcom++;

                                result.Value.R_j__c[chr.Key].Remove(discardedPeak.Key);
                            }
                        }
                    }
                }
            }
        }

        private void CreateOuputSet()
        {
            foreach(var result in _analysisResults)
            {
                foreach(var chr in result.Value.R_j__c)
                {
                    foreach (var confirmedPeak in chr.Value)
                    {
                        var outputPeak = new AnalysisResult<Peak, Metadata>.ProcessedPeak()
                        {
                            peak = confirmedPeak.Value.peak,
                            rtp = confirmedPeak.Value.rtp,
                            xSquared = confirmedPeak.Value.xSquared,
                            classification = PeakClassificationType.TruePositive,
                            supportingPeaks = confirmedPeak.Value.supportingPeaks,
                        };

                        if (confirmedPeak.Value.peak.metadata.value <= _config.TauS)
                        {
                            outputPeak.classification = PeakClassificationType.StringentConfirmed;
                            result.Value.R_j___so[chr.Key]++;
                        }
                        else if (confirmedPeak.Value.peak.metadata.value <= _config.TauW)
                        {
                            outputPeak.classification = PeakClassificationType.WeakConfirmed;
                            result.Value.R_j___wo[chr.Key]++;
                        }

                        result.Value.R_j__o[chr.Key].Add(outputPeak);
                    }
                }
            }
        }

        /// <summary>
        /// Benjamini–Hochberg procedure (step-up procedure)
        /// </summary>
        private void PerformMultipleTestingCorrection()
        {
            foreach(var result in _analysisResults)
            {
                foreach(var chr in result.Value.R_j__o)
                {
                    result.Value.R_j_TP[chr.Key] = (uint)chr.Value.Count;
                    result.Value.R_j_FP[chr.Key] = 0;
                    var outputSet = result.Value.R_j__o[chr.Key];
                    int m = outputSet.Count();

                    // Sorts output set based on the values of peaks. 
                    outputSet.Sort(new Comparers.CompareProcessedPeakByValue<Peak, Metadata>());

                    for (int k = 1; k <= m; k++)
                    {
                        if (outputSet[k - 1].peak.metadata.value > ((double)k / (double)m) * _config.Alpha)
                        {
                            k--;
                            for (int l = 1; l < k; l++)
                            {
                                outputSet[l].adjPValue = (((double)k * outputSet[l].peak.metadata.value) / (double)m) * _config.Alpha;
                                outputSet[l].statisticalClassification = PeakClassificationType.TruePositive;
                            }
                            for (int l = k; l < m; l++)
                            {
                                outputSet[l].adjPValue = (((double)k * outputSet[l].peak.metadata.value) / (double)m) * _config.Alpha;
                                outputSet[l].statisticalClassification = PeakClassificationType.FalsePositive;
                            }

                            result.Value.R_j_TP[chr.Key] = (uint)k;
                            result.Value.R_j_FP[chr.Key] = (uint)(m - k);
                            break;
                        }
                    }

                    // Sorts output set using default comparer. 
                    // The default sorter gives higher priority to two ends than values. 
                    outputSet.Sort();
                }
            }
        }

        private void CreateConsensusPeaks()
        {
            _mergedReplicates = new Dictionary<string, SortedList<Peak, Peak>>();
            foreach (var result in _analysisResults)
            {
                foreach (var chr in result.Value.R_j__o)
                {
                    if (!_mergedReplicates.ContainsKey(chr.Key))
                        _mergedReplicates.Add(chr.Key, new SortedList<Peak, Peak>());

                    foreach (var outputER in chr.Value)
                    {
                        var peak = outputER.peak;
                        var interval = new Peak();
                        interval.left = peak.left;
                        interval.right = peak.right;

                        Peak mergedPeak;
                        Peak mergingPeak = new Peak();
                        mergingPeak.left = peak.left;
                        mergingPeak.right = peak.right;
                        mergingPeak.metadata.value =
                            (-2) * Math.Log((peak.metadata.value == 0 ? Config.default0PValue : peak.metadata.value), Math.E);

                        while (_mergedReplicates[chr.Key].TryGetValue(interval, out mergedPeak))
                        {
                            _mergedReplicates[chr.Key].Remove(interval);
                            interval.left = Math.Min(interval.left, mergedPeak.left);
                            interval.right = Math.Max(interval.right, mergedPeak.right);
                            mergingPeak.left = interval.left;
                            mergingPeak.right = interval.right;
                            mergingPeak.metadata.value += mergedPeak.metadata.value;
                        }

                        if (mergingPeak.metadata.value >= Math.Abs(Config.defaultMaxLogOfPVvalue))
                            mergingPeak.metadata.value = Math.Abs(Config.defaultMaxLogOfPVvalue);
                        
                        _mergedReplicates[chr.Key].Add(interval, mergingPeak);
                    }
                }
            }

            int c = 0;
            foreach (var chr in _mergedReplicates)
                foreach (var peak in chr.Value)
                    peak.Value.metadata.name = "MSPC_peak_" + (c++);
        }
    }
}
