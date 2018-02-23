
using Polimi.DEIB.VahidJalili.IGenomics;
using System;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Comparers
{
    internal class OverlappingIntervalComparer<P, M> : IComparer<P>
        where P : IInterval<int, M>, IComparable<P>, new()
        where M : IChIPSeqPeak, IComparable<M>, new()
    {
        public int Compare(P x, P y)
        {
            if (x.right <= y.left) return -1;
            if (x.left >= y.right) return 1;
            return 0;
        }
    }
}
