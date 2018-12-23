// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Comparers
{
    public class EqualOverlappingPeaks<I> : IEqualityComparer<I>
        where I : IPeak
    {
        public bool Equals(I x, I y)
        {
            if (x.Right < y.Left || x.Left > y.Right) return false;
            return true;
        }

        public int GetHashCode(I obj)
        {
            // This function always returns
            // `0` so that HashSet used for 
            // consensus peaks, relies on
            // the `Equals` function to 
            // determin if two intervals
            // are "equal" (aka, overlapping). 
            return 0;
        }
    }
}
