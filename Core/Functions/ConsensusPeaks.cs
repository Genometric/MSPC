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
        private Dictionary<string, SortedDictionary<Interval, Interval>> _consensusPeaks { set; get; }

        public Dictionary<string, List<ProcessedPeak<I>>> Compute(
            Dictionary<uint, Result<I>> analysisResults,
            IPeakConstructor<I> peakConstructor,
            int degreeOfParallelisim)
        {
            // Initialize data structures.
            _consensusPeaks = new Dictionary<string, SortedDictionary<Interval, Interval>>();
            foreach (var result in analysisResults)
                foreach (var chr in result.Value.Chromosomes)
                    if (!_consensusPeaks.ContainsKey(chr.Key))
                        _consensusPeaks.Add(chr.Key, new SortedDictionary<Interval, Interval>());

            // Determin consensus peaks; stored in mutable and light-weight types.
            foreach (var result in analysisResults)
            {
                Parallel.ForEach(
                    result.Value.Chromosomes,
                    new ParallelOptions { MaxDegreeOfParallelism = degreeOfParallelisim },
                    chr =>
                    {
                        DetermineConsensusPeaks(chr.Key, chr.Value.Get(Attributes.Confirmed));
                    });
            }

            // Convert the type of determined consensus peaks.
            return ConvertToListOfProcessedPeaks(peakConstructor, degreeOfParallelisim);
        }

        private void DetermineConsensusPeaks(string chr, IEnumerable<ProcessedPeak<I>> peaks)
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

                while (_consensusPeaks[chr].TryGetValue(interval, out Interval mergedInterval))
                {
                    _consensusPeaks[chr].Remove(interval);
                    interval.left = Math.Min(interval.left, mergedInterval.left);
                    interval.right = Math.Max(interval.right, mergedInterval.right);
                    interval.involvedPeaksCount++;
                    interval.xSquard += mergedInterval.xSquard;
                }
                _consensusPeaks[chr].Add(interval, interval);
            }
        }

        private Dictionary<string, List<ProcessedPeak<I>>> ConvertToListOfProcessedPeaks(IPeakConstructor<I> peakConstructor, int degreeOfParallelism)
        {
            var rtv = new Dictionary<string, List<ProcessedPeak<I>>>();
            foreach (var chr in _consensusPeaks)
                rtv.Add(chr.Key, new List<ProcessedPeak<I>>(capacity: chr.Value.Count));

            int counter = 0;
            Parallel.ForEach(
                _consensusPeaks,
                new ParallelOptions { MaxDegreeOfParallelism = degreeOfParallelism },
                chr =>
                {
                    foreach (var peak in chr.Value)
                    {
                        rtv[chr.Key].Add(new ProcessedPeak<I>(
                            peakConstructor.Construct(
                                peak.Key.left,
                                peak.Key.right,
                                peak.Key.xSquard,
                                "MSPC_Peak_" + Interlocked.Increment(ref counter),
                                (peak.Key.right - peak.Key.left) / 2),
                            peak.Key.xSquard,
                            peak.Key.involvedPeaksCount));
                    }
                });

            return rtv;
        }
    }
}
