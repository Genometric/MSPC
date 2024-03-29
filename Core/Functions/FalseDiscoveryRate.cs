﻿using Genometric.GeUtilities.IGenomics;
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
        public static void PerformMultipleTestingCorrection(Dictionary<uint, Result<I>> results, float alpha, int? degreeOfParallelism)
        {
            var options = new ParallelOptions();
            if (degreeOfParallelism is not null)
                options.MaxDegreeOfParallelism = (int)degreeOfParallelism;

            Parallel.ForEach(
                results,
                options,
                result =>
                {
                    PerformMultipleTestingCorrection(UnionChrs(result.Value.Chromosomes), alpha);
                });
        }

        private static List<ProcessedPeak<I>> UnionChrs(ConcurrentDictionary<string, ConcurrentDictionary<char, Sets<I>>> chrs)
        {
            IEnumerable<ProcessedPeak<I>> peaks = new List<ProcessedPeak<I>>();
            foreach (var chr in chrs)
                foreach (var strand in chr.Value)
                    peaks = peaks.Union(strand.Value.Get(Attributes.Confirmed));
            return peaks.ToList();
        }

        /// <summary>
        /// Benjamini–Hochberg (step-up) procedure.
        /// </summary>
        public static void PerformMultipleTestingCorrection(Dictionary<string, Dictionary<char, List<ProcessedPeak<I>>>> peaks, float alpha)
        {
            IEnumerable<ProcessedPeak<I>> ps = new List<ProcessedPeak<I>>();
            foreach (var chr in peaks)
                foreach (var strand in chr.Value)
                    ps = ps.Union(strand.Value);
            PerformMultipleTestingCorrection(ps.ToList(), alpha);
        }

        private static void PerformMultipleTestingCorrection(List<ProcessedPeak<I>> peaks, float alpha)
        {
            int m = peaks.Count;

            // Sorts the set of confirmed peaks based on their p-values.
            peaks.Sort(new CompareProcessedPeaksByValue<I>());

            for (int i = 0; i < m; i++)
            {
                if (peaks[i].Source.Value <= (i + 1) / (double)m * alpha)
                    peaks[i].AddClassification(Attributes.TruePositive);
                else
                    peaks[i].AddClassification(Attributes.FalsePositive);

                // False discovery rate based on Benjamini and Hochberg Multiple Testing Correction.
                peaks[i].AdjPValue = peaks[i].Source.Value * (m / (i + 1.0));
            }
        }
    }
}
