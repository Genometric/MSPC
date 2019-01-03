// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Comparers;
using Genometric.MSPC.Core.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace Genometric.MSPC.Core.Functions
{
    internal class FalseDiscoveryRate<I>
        where I : IPeak
    {
        /// <summary>
        /// Benjamini–Hochberg (step-up) procedure.
        /// </summary>
        public void PerformMultipleTestingCorrection(ReadOnlyDictionary<uint, Result<I>> results, float alpha)
        {
            foreach (var result in results)
            {
                foreach (var chr in result.Value.Chromosomes)
                {
                    var confirmedPeaks = chr.Value.Get(Attributes.Confirmed).ToList();
                    int m = confirmedPeaks.Count;

                    // Sorts confirmed peaks set based on their p-values.
                    confirmedPeaks.Sort(new CompareProcessedPeaksByValue<I>());

                    for (int i = 0; i < m; i++)
                    {
                        if (confirmedPeaks[i].Source.Value <= (i + 1) / (double)m * alpha)
                            confirmedPeaks[i].Classification.Add(Attributes.TruePositive);
                        else
                            confirmedPeaks[i].Classification.Add(Attributes.FalsePositive);

                        // False discovery rate based on Benjamini and Hochberg Multiple Testing Correction.
                        confirmedPeaks[i].AdjPValue = confirmedPeaks[i].Source.Value * (m / (i + 1));
                    }
                    // Sorts confirmed peaks set based on coordinates using default comparer.
                    confirmedPeaks.Sort();
                }
            }
        }
    }
}
