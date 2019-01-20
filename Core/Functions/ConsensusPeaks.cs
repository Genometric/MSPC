// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Interfaces;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Genometric.MSPC.Core.Functions
{
    internal class ConsensusPeaks<I>
        where I : IPPeak
    {
        private Dictionary<string, SortedDictionary<Interval, Interval>> _consensusPeaks { set; get; }

        public Dictionary<string, List<I>> Compute(
            List<Bed<I>> samples,
            IPeakConstructor<I> peakConstructor,
            int degreeOfParallelisim,
            float alpha)
        {
            // Initialize data structures.
            _consensusPeaks = new Dictionary<string, SortedDictionary<Interval, Interval>>();
            foreach (var sample in samples)
                foreach (var chr in sample.Chromosomes)
                    if (!_consensusPeaks.ContainsKey(chr.Key))
                        _consensusPeaks.Add(chr.Key, new SortedDictionary<Interval, Interval>());

            // Determin consensus peaks; stored in mutable and light-weight types.
            foreach (var result in samples)
            {
                Parallel.ForEach(
                    result.Chromosomes,
                    new ParallelOptions { MaxDegreeOfParallelism = degreeOfParallelisim },
                    chr =>
                    {
                        if (chr.Value.Strands.Count > 1)
                        {
                            int size = 0;
                            foreach (var strand in chr.Value.Strands)
                                size += strand.Value.Intervals.Count;

                            var peaks = new List<I>(size);
                            foreach (var strand in chr.Value.Strands)
                                peaks.AddRange(strand.Value.Intervals.Where(x => x.HasAttribute(Attributes.Confirmed)));
                            DetermineConsensusPeaks(chr.Key, peaks);
                        }
                        else
                            DetermineConsensusPeaks(chr.Key, chr.Value.Strands.First().Value.Intervals.Where(x => x.HasAttribute(Attributes.Confirmed)));
                    });
            }

            // Convert the type of determined consensus peaks.
            var processedPeaks = ConvertToListOfProcessedPeaks(peakConstructor, degreeOfParallelisim);
            var fdr = new FalseDiscoveryRate<I>();
            fdr.PerformMultipleTestingCorrection(processedPeaks, alpha);
            return processedPeaks;
        }

        private void DetermineConsensusPeaks(string chr, IEnumerable<I> peaks)
        {
            foreach (var confirmedPeak in peaks)
            {
                var interval = new Interval()
                {
                    left = confirmedPeak.Left,
                    right = confirmedPeak.Right,
                    involvedPeaksCount = 1,
                    xSquard = (-2) * Math.Log(confirmedPeak.Value == 0 ? Config.default0PValue : confirmedPeak.Value, Math.E)
                };

                while (_consensusPeaks[chr].TryGetValue(interval, out Interval mergedInterval))
                {
                    _consensusPeaks[chr].Remove(interval);
                    interval.left = Math.Min(interval.left, mergedInterval.left);
                    interval.right = Math.Max(interval.right, mergedInterval.right);
                    interval.involvedPeaksCount += mergedInterval.involvedPeaksCount;
                    interval.xSquard += mergedInterval.xSquard;
                }
                _consensusPeaks[chr].Add(interval, interval);
            }
        }

        private Dictionary<string, List<I>> ConvertToListOfProcessedPeaks(IPeakConstructor<I> peakConstructor, int degreeOfParallelism)
        {
            var rtv = new Dictionary<string, List<I>>();
            foreach (var chr in _consensusPeaks)
                rtv.Add(chr.Key, new List<I>(capacity: chr.Value.Count));

            int counter = 0;
            Parallel.ForEach(
                _consensusPeaks,
                new ParallelOptions { MaxDegreeOfParallelism = degreeOfParallelism },
                chr =>
                {
                    foreach (var peak in chr.Value)
                    {
                        var p = peakConstructor.Construct(
                            peak.Key.left,
                            peak.Key.right,
                            ChiSqrd.ChiSqrdDistRTP(peak.Key.xSquard, peak.Key.involvedPeaksCount * 2),
                            "MSPC_Peak_" + Interlocked.Increment(ref counter),
                            (peak.Key.right - peak.Key.left) / 2);

                        p.XSquared = peak.Key.xSquard;
                        p.SupportingPeaksCount = peak.Key.involvedPeaksCount - 1;

                        rtv[chr.Key].Add(p);
                    }
                });

            return rtv;
        }
    }
}
