// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;

namespace Genometric.MSPC.Comparers
{
    public class CompareProcessedPeaksByValue<I> : IComparer<ProcessedPeak<I>>
        where I : IChIPSeqPeak, new()
    {
        public int Compare(ProcessedPeak<I> x, ProcessedPeak<I> y)
        {
            if (x == null)
            {
                if (y == null) return 0; // they are equal
                else return -1; // B is greater
            }
            else
            {
                if (y == null) return 1; // A is greater
                else
                {
                    if (x.Source.Value != y.Source.Value)
                        return x.Source.Value.CompareTo(y.Source.Value);
                    else if (x.Source.Left != y.Source.Left)
                        return x.Source.Left.CompareTo(y.Source.Left);
                    else
                        return x.Source.Right.CompareTo(y.Source.Right);
                }
            }
        }
    }
}