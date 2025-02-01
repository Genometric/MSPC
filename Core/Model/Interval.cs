using System;

namespace Genometric.MSPC.Core.Model
{
#pragma warning disable S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"
    internal class Interval : IComparable<Interval>
#pragma warning restore S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"
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
