
using System;
using System.Collections.Generic;
using Genometric.GeUtilities.IGenomics;

namespace Genometric.MSPC.Comparers
{
    internal class OverlappingIntervalComparer<I> : IComparer<I>
        where I : IChIPSeqPeak, new()
    {
        public int Compare(I x, I y)
        {
            if (x.Right <= y.Left) return -1;
            if (x.Left >= y.Right) return 1;
            return 0;
        }
    }
}
