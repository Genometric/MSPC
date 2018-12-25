// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Comparers;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Functions
{
    internal static class ConsensusPeaks<I>
        where I : IPeak
    {
        private static IPeakConstructor<I> _peakConstructor;
        private static Dictionary<string, HashSet<ProcessedPeak<I>>> _consensusPeaks { set; get; }

        public static Dictionary<string, HashSet<ProcessedPeak<I>>> Compute(Dictionary<uint, Result<I>> analysisResults, IPeakConstructor<I> peakConstructor)
        {
            _peakConstructor = peakConstructor;
            _consensusPeaks = new Dictionary<string, HashSet<ProcessedPeak<I>>>();
            foreach (var result in analysisResults)
            {
                foreach (var chr in result.Value.Chromosomes)
                {
                    if (!_consensusPeaks.ContainsKey(chr.Key))
                        _consensusPeaks.Add(chr.Key, new HashSet<ProcessedPeak<I>>(comparer: new EqualOverlappingPeaks<I>()));

                    int c = 0;
                    foreach (var confirmedPeak in chr.Value.Get(Attributes.Confirmed))
                    {
                        var interval = _peakConstructor.Construct(
                            left: confirmedPeak.Source.Left,
                            right: confirmedPeak.Source.Right,
                            name: "MSPC_Peak_" + (c++),
                            summit: (confirmedPeak.Source.Right - confirmedPeak.Source.Left) / 2,
                            value: (-2) * Math.Log((confirmedPeak.Source.Value == 0 ? Config.default0PValue : confirmedPeak.Source.Value), Math.E));
                        var pp = new ProcessedPeak<I>(interval, double.NaN, new List<SupportingPeak<I>>());

                        while (_consensusPeaks[chr.Key].TryGetValue(pp, out ProcessedPeak<I> mergedPeak))
                        {
                            _consensusPeaks[chr.Key].Remove(pp);
                            interval = _peakConstructor.Construct(
                                left: Math.Min(interval.Left, mergedPeak.Source.Left),
                                right: Math.Max(interval.Right, mergedPeak.Source.Right),
                                name: interval.Name,
                                summit: interval.Summit,
                                value: interval.Value + mergedPeak.Source.Value);
                            pp = new ProcessedPeak<I>(interval, double.NaN, new List<SupportingPeak<I>>());
                        }

                        _consensusPeaks[chr.Key].Add(pp);
                    }
                }
            }

            return _consensusPeaks;
        }
    }
}
