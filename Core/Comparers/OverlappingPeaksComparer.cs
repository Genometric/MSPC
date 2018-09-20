// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Comparers
{
    public class OverlappingPeaksComparer<I> : IComparer<I>
        where I : IPeak
    {
        public int Compare(I x, I y)
        {
            if (x.Right < y.Left) return 1;
            if (x.Left > y.Right) return -1;
            return 0;
        }
    }
}
