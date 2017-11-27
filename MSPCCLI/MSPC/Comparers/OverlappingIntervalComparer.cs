using Genometric.IGenomics;
using System;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Comparers
{
    internal class OverlappingIntervalComparer<Peak, Metadata> : IComparer<Peak>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        public int Compare(Peak x, Peak y)
        {
            if (x.right <= y.left) return -1;
            if (x.left >= y.right) return 1;
            return 0;
        }
    }
}
