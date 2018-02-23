
using System;
using System.Collections.Generic;
using System.Text;
using Genometric.GeUtilities.IGenomics;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class IntervalOperations<I>
        where I : IChIPSeqPeak, new()
    {
        public static bool Intersects(I a, I b)
        {
            return b.Right.CompareTo(a.Left) > 0 && b.Left.CompareTo(a.Right) < 0;
        }

        public static bool Contains(I interval, int point, ContainConstrains constraint)
        {
            bool isContained;

            switch (constraint)
            {
                case ContainConstrains.None:
                    isContained = Contains(interval, point);
                    break;
                case ContainConstrains.IncludeStart:
                    isContained = ContainsWithStart(interval, point);
                    break;
                case ContainConstrains.IncludeEnd:
                    isContained = ContainsWithEnd(interval, point);
                    break;
                case ContainConstrains.IncludeStartAndEnd:
                    isContained = ContainsWithStartEnd(interval, point);
                    break;
                default:
                    throw new ArgumentException("Invalid constraint " + constraint);
            }

            return isContained;
        }

        public static bool Contains(I interval, int point)
        {
            //return time < end && time > start;
            return point.CompareTo(interval.Right) < 0 && point.CompareTo(interval.Left) > 0;
        }

        public static bool ContainsWithStart(I interval, int point)
        {
            return point.CompareTo(interval.Right) < 0 && point.CompareTo(interval.Left) >= 0;
        }

        public static bool ContainsWithEnd(I interval, int point)
        {
            return point.CompareTo(interval.Right) <= 0 && point.CompareTo(interval.Left) > 0;
        }

        public static bool ContainsWithStartEnd(I interval, int point)
        {
            return point.CompareTo(interval.Right) <= 0 && point.CompareTo(interval.Left) >= 0;
        }
    }

    public enum ContainConstrains
    {
        None,
        IncludeStart,
        IncludeEnd,
        IncludeStartAndEnd
    }
}
