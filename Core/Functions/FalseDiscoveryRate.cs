// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Comparers;
using Genometric.MSPC.Core.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genometric.MSPC.Core.Functions
{
    internal class FalseDiscoveryRate<I>
        where I : IPeak
    {
        /// <summary>
        /// Benjamini–Hochberg (step-up) procedure.
        /// </summary>
        public void PerformMultipleTestingCorrection(Dictionary<uint, Result<I>> results, float alpha, int degreeOfParallelism)
        {
            Parallel.ForEach(
                results,
                new ParallelOptions { MaxDegreeOfParallelism = degreeOfParallelism },
                result =>
                {
                    PerformMultipleTestingCorrection(UnionChrs(result.Value.Chromosomes), alpha);
                });
        }

        private List<ProcessedPeak<I>> UnionChrs(ConcurrentDictionary<string, Sets<I>> chrs)
        {
            IEnumerable<ProcessedPeak<I>> peaks = new List<ProcessedPeak<I>>();
            foreach (var chr in chrs)
                peaks = peaks.Union(chr.Value.Get(Attributes.Confirmed));
            return peaks.ToList();
        }

        /// <summary>
        /// Benjamini–Hochberg (step-up) procedure.
        /// </summary>
        public void PerformMultipleTestingCorrection(Dictionary<string, List<ProcessedPeak<I>>> peaks, float alpha)
        {
            IEnumerable<ProcessedPeak<I>> ps = new List<ProcessedPeak<I>>();
            foreach (var chr in peaks)
                ps = ps.Union(chr.Value);
            PerformMultipleTestingCorrection(ps.ToList(), alpha);
        }

        private void PerformMultipleTestingCorrection(List<ProcessedPeak<I>> peaks, float alpha)
        {
            int m = peaks.Count;

            // Sorts the set of confirmed peaks based on their p-values.
            peaks.Sort(new CompareProcessedPeaksByValue<I>());

            for (int i = 0; i < m; i++)
            {
                if (peaks[i].Source.Value <= (i + 1) / (double)m * alpha)
                    peaks[i].Classification.Add(Attributes.TruePositive);
                else
                    peaks[i].Classification.Add(Attributes.FalsePositive);

                // False discovery rate based on Benjamini and Hochberg Multiple Testing Correction.
                peaks[i].AdjPValue = peaks[i].Source.Value * (m / (i + 1.0));
            }
            // Sorts confirmed peaks set based on coordinates using default comparer.
            peaks.Sort();
        }
    }
}
