// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Genometric.MSPC.Core.Functions
{
    internal class ConsensusPeaks<I>
        where I : IPeak
    {
        private Dictionary<string, Dictionary<char, SortedDictionary<Interval, Interval>>> _consensusPeaks { set; get; }

        public Dictionary<string, Dictionary<char, List<ProcessedPeak<I>>>> Compute(
            Dictionary<uint, Result<I>> analysisResults,
            IPeakConstructor<I> peakConstructor,
            int? degreeOfParallelisim,
            float alpha)
        {
            // Initialize data structures.
            _consensusPeaks = new Dictionary<string, Dictionary<char, SortedDictionary<Interval, Interval>>>();
            foreach (var result in analysisResults)
            {
                foreach (var chr in result.Value.Chromosomes)
                {
                    if (!_consensusPeaks.ContainsKey(chr.Key))
                        _consensusPeaks.Add(
                            chr.Key,
                            new Dictionary<char, SortedDictionary<Interval, Interval>>());

                    foreach (var strand in chr.Value)
                    {
                        if (!_consensusPeaks[chr.Key].ContainsKey(strand.Key))
                            _consensusPeaks[chr.Key].Add(
                                strand.Key,
                                new SortedDictionary<Interval, Interval>());
                    }
                }
            }

            // Determin consensus peaks; stored in mutable and light-weight types.
            foreach (var result in analysisResults)
            {
                var options = new ParallelOptions();
                if (degreeOfParallelisim is not null)
                    options.MaxDegreeOfParallelism = (int)degreeOfParallelisim;
                Parallel.ForEach(
                    result.Value.Chromosomes,
                    options,
                    chr =>
                    {
                        foreach (var strand in chr.Value)
                            DetermineConsensusPeaks(
                                chr.Key,
                                strand.Key,
                                strand.Value.Get(Attributes.Confirmed));

                    });
            }

            // Convert the type of determined consensus peaks.
            var processedPeaks = ConvertToListOfProcessedPeaks(peakConstructor, degreeOfParallelisim);
            var fdr = new FalseDiscoveryRate<I>();
            fdr.PerformMultipleTestingCorrection(processedPeaks, alpha);
            return processedPeaks;
        }

        private void DetermineConsensusPeaks(string chr, char strand, IEnumerable<ProcessedPeak<I>> peaks)
        {
            foreach (var confirmedPeak in peaks)
            {
                var interval = new Interval()
                {
                    left = confirmedPeak.Source.Left,
                    right = confirmedPeak.Source.Right,
                    involvedPeaksCount = 1,
                    xSquard = (-2) * Math.Log(confirmedPeak.Source.Value == 0 ? Config.default0PValue : confirmedPeak.Source.Value, Math.E)
                };

                while (_consensusPeaks[chr][strand].TryGetValue(interval, out Interval mergedInterval))
                {
                    _consensusPeaks[chr][strand].Remove(interval);
                    interval.left = Math.Min(interval.left, mergedInterval.left);
                    interval.right = Math.Max(interval.right, mergedInterval.right);
                    interval.involvedPeaksCount += mergedInterval.involvedPeaksCount;
                    interval.xSquard += mergedInterval.xSquard;
                }
                _consensusPeaks[chr][strand].Add(interval, interval);
            }
        }

        private Dictionary<string, Dictionary<char, List<ProcessedPeak<I>>>> ConvertToListOfProcessedPeaks(IPeakConstructor<I> peakConstructor, int? degreeOfParallelism)
        {
            var rtv = new Dictionary<string, Dictionary<char, List<ProcessedPeak<I>>>>();
            foreach (var chr in _consensusPeaks)
            {
                rtv.Add(chr.Key, new Dictionary<char, List<ProcessedPeak<I>>>());
                foreach (var strand in chr.Value)
                    rtv[chr.Key].Add(strand.Key, new List<ProcessedPeak<I>>(capacity: chr.Value.Count));
            }

            int counter = 0;
            var options = new ParallelOptions();
            if (degreeOfParallelism is not null)
                options.MaxDegreeOfParallelism = (int)degreeOfParallelism;
            Parallel.ForEach(
                _consensusPeaks,
                options,
                chr =>
                {
                    foreach (var strand in chr.Value)
                    {
                        foreach (var peak in strand.Value)
                        {
                            rtv[chr.Key][strand.Key].Add(new ProcessedPeak<I>(
                                peakConstructor.Construct(
                                    peak.Key.left,
                                    peak.Key.right,
                                    ChiSqrd.ChiSqrdDistRTP(peak.Key.xSquard, peak.Key.involvedPeaksCount * 2),
                                    "MSPC_Peak_" + Interlocked.Increment(ref counter),
                                    (peak.Key.right - peak.Key.left) / 2),
                                peak.Key.xSquard,
                                peak.Key.involvedPeaksCount - 1));
                        }
                    }
                });

            return rtv;
        }
    }
}
