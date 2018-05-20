
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;

namespace Genometric.MSPC.Comparers
{
    class CompareProcessedPeakByValue<I> : IComparer<ProcessedPeak<I>>
        where I : IChIPSeqPeak, new()
    {
        public int Compare(ProcessedPeak<I> A, ProcessedPeak<I> B)
        {
            if (A == null)
            {
                if (B == null) return 0; // they are equal
                else return -1; // B is greater
            }
            else
            {
                if (B == null) return 1; // A is greater
                else
                {
                    if (A.Peak.Value != B.Peak.Value)
                        return A.Peak.Value.CompareTo(B.Peak.Value);
                    else if (A.Peak.Left != B.Peak.Left)
                        return A.Peak.Left.CompareTo(B.Peak.Left);
                    else
                        return A.Peak.Right.CompareTo(B.Peak.Right);
                }
            }
        }
    }
}