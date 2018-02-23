
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using Genometric.GeUtilities.IGenomics;

namespace Genometric.MSPC.Core.Comparers
{
    class CompareProcessedPeakByValue<I> : IComparer<AnalysisResult<I>.ProcessedPeak>
        where I : IChIPSeqPeak, new()
    {
        public int Compare(AnalysisResult<I>.ProcessedPeak A, AnalysisResult<I>.ProcessedPeak B)
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
                    if (A.peak.metadata.Value != B.peak.metadata.Value)
                        return A.peak.metadata.Value.CompareTo(B.peak.metadata.Value);
                    else if (A.peak.Left != B.peak.Left)
                        return A.peak.Left.CompareTo(B.peak.Left);
                    else
                        return A.peak.Right.CompareTo(B.peak.Right);
                }
            }
        }
    }
}