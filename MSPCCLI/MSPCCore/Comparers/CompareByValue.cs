using Polimi.DEIB.VahidJalili.IGenomics;
using Polimi.DEIB.VahidJalili.MSPC.Warehouse;
using System;
using System.Collections.Generic;

namespace Polimi.DEIB.VahidJalili.MSPC.Analyzer.Comparers
{
    class CompareProcessedPeakByValue<Peak, Metadata> : IComparer<AnalysisResult<Peak, Metadata>.ProcessedPeak>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        public int Compare(AnalysisResult<Peak, Metadata>.ProcessedPeak A, AnalysisResult<Peak, Metadata>.ProcessedPeak B)
        {
            if (A == null)
            {
                if (B == null) return 0;// then they are equal
                else return -1; // then B is greater
            }
            else
            {
                if (B == null) return 1;// A is greater
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