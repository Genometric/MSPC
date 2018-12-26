// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Functions
{
    internal static class ConsensusPeaks<I>
        where I : IPeak
    {
        public static Dictionary<string, List<ProcessedPeak<I>>> Compute(Dictionary<uint, Result<I>> analysisResults, IPeakConstructor<I> peakConstructor)
        {
            var _consensusPeaks = new Dictionary<string, SortedDictionary<Interval, Interval>>();
            foreach (var result in analysisResults)
            {
                foreach (var chr in result.Value.Chromosomes)
                {
                    if (!_consensusPeaks.ContainsKey(chr.Key))
                        _consensusPeaks.Add(chr.Key, new SortedDictionary<Interval, Interval>());

                    int c = 0;
                    foreach (var confirmedPeak in chr.Value.Get(Attributes.Confirmed))
                    {
                        var interval = new Interval()
                        {
                            left = confirmedPeak.Source.Left,
                            right = confirmedPeak.Source.Right,
                            involvedPeaksCount = 1,
                            xSquard = (-2) * Math.Log((confirmedPeak.Source.Value == 0 ? Config.default0PValue : confirmedPeak.Source.Value), Math.E)
                        };

                        while (_consensusPeaks[chr.Key].TryGetValue(interval, out Interval mergedInterval))
                        {
                            _consensusPeaks[chr.Key].Remove(interval);
                            interval.left = Math.Min(interval.left, mergedInterval.left);
                            interval.right = Math.Max(interval.right, mergedInterval.right);
                            interval.involvedPeaksCount++;
                            interval.xSquard += mergedInterval.xSquard;
                        }
                        _consensusPeaks[chr.Key].Add(interval, interval);
                    }
                }
            }

            var rtv = new Dictionary<string, List<ProcessedPeak<I>>>();
            foreach(var chr in _consensusPeaks)
            {
                rtv.Add(chr.Key, new List<ProcessedPeak<I>>(capacity: chr.Value.Count));
                foreach(var peak in  chr.Value)
                {
                    rtv[chr.Key].Add(new ProcessedPeak<I>(
                        peakConstructor.Construct(
                            peak.Key.left,
                            peak.Key.right,
                            peak.Key.xSquard,
                            "MSPC_Peak",
                            (peak.Key.right - peak.Key.left) / 2),
                        peak.Key.xSquard,
                        new List<SupportingPeak<I>>()));
                }
            }

            return rtv;
        }
    }
}
