// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using System;

namespace Genometric.MSPC.Core.Model
{
    internal class Interval : IComparable<Interval>
    {
        public int left;
        public int right;
        public double xSquard;
        public int involvedPeaksCount;

        public int CompareTo(Interval other)
        {
            if (right < other.left) return 1;
            if (left > other.right) return -1;
            return 0;
        }
    }
}
