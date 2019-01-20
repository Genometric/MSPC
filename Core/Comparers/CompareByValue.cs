// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Core.Interfaces;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Comparers
{
    public class CompareProcessedPeaksByValue<I> : IComparer<I>
        where I : IPPeak
    {
        public int Compare(I x, I y)
        {
            if (x == null)
            {
                if (y == null) return 0; // they are equal
                else return -1; // B is greater
            }
            else
            {
                if (y == null) return 1; // A is greater
                else
                {
                    if (x.Value != y.Value)
                        return x.Value.CompareTo(y.Value);
                    else if (x.Left != y.Left)
                        return x.Left.CompareTo(y.Left);
                    else
                        return x.Right.CompareTo(y.Right);
                }
            }
        }
    }
}