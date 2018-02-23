
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Comparers
{
    class CompareProcessedPeakByValue<P, M> : IComparer<AnalysisResult<P, M>.ProcessedPeak>
        where P : IInterval<int, M>, IComparable<P>, new()
        where M : IChIPSeqPeak, IComparable<M>, new()
    {
        public int Compare(AnalysisResult<P, M>.ProcessedPeak A, AnalysisResult<P, M>.ProcessedPeak B)
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
                    if (A.peak.metadata.value != B.peak.metadata.value)
                        return A.peak.metadata.value.CompareTo(B.peak.metadata.value);
                    else if (A.peak.left != B.peak.left)
                        return A.peak.left.CompareTo(B.peak.left);
                    else
                        return A.peak.right.CompareTo(B.peak.right);
                }
            }
        }
    }
}