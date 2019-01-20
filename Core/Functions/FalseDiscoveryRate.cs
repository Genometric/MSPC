// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Comparers;
using Genometric.MSPC.Core.Interfaces;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genometric.MSPC.Core.Functions
{
    internal class FalseDiscoveryRate<I>
        where I : IPPeak
    {
        /// <summary>
        /// Benjamini–Hochberg (step-up) procedure.
        /// </summary>
        public void PerformMultipleTestingCorrection(List<Bed<I>> samples, float alpha, int degreeOfParallelism)
        {
            Parallel.ForEach(
                samples,
                new ParallelOptions { MaxDegreeOfParallelism = degreeOfParallelism },
                sample =>
                {
                    PerformMultipleTestingCorrection(UnionChrs(sample), alpha);
                });
        }

        private List<I> UnionChrs(Bed<I> sample)
        {
            IEnumerable<I> peaks = new List<I>();
            foreach (var chr in sample.Chromosomes)
                foreach (var strand in chr.Value.Strands)
                    peaks = peaks.Union(strand.Value.Intervals.Where(i => i.HasAttribute(Attributes.Confirmed)));
            return peaks.ToList();
        }

        /// <summary>
        /// Benjamini–Hochberg (step-up) procedure.
        /// </summary>
        public void PerformMultipleTestingCorrection(Dictionary<string, List<I>> peaks, float alpha)
        {
            IEnumerable<I> ps = new List<I>();
            foreach (var chr in peaks)
                ps = ps.Union(chr.Value);
            PerformMultipleTestingCorrection(ps.ToList(), alpha);
        }

        private void PerformMultipleTestingCorrection(List<I> peaks, float alpha)
        {
            int m = peaks.Count;

            // Sorts the set of confirmed peaks based on their p-values.
            peaks.Sort(new CompareProcessedPeaksByValue<I>());

            for (int i = 0; i < m; i++)
            {
                if (peaks[i].Value <= (i + 1) / (double)m * alpha)
                    peaks[i].AddClassification(Attributes.TruePositive);
                else
                    peaks[i].AddClassification(Attributes.FalsePositive);

                // False discovery rate based on Benjamini and Hochberg Multiple Testing Correction.
                peaks[i].AdjPValue = peaks[i].Value * (m / (i + 1.0));
            }
        }
    }
}
