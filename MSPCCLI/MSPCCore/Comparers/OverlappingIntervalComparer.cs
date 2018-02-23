using Polimi.DEIB.VahidJalili.IGenomics;
using Polimi.DEIB.VahidJalili.MSPC.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polimi.DEIB.VahidJalili.MSPC.Analyzer.Comparers
{
    class OverlappingIntervalComparer<C, M> : IComparer<Interval<C, M>>
        where M : IExtendedMetadata, IComparable<M>, new()
    {
        public int Compare(Interval<C, M> x, Interval<C, M> y)
        {
            if (x.right <= y.left) return -1;
            if (x.left >= y.right) return 1;
            return 0;
        }
    }
}
