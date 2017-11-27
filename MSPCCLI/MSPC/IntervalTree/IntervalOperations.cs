using Genometric.IGenomics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class IntervalOperations<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        public static bool Intersects(Peak a, Peak b)
        {
            return b.right.CompareTo(a.left) > 0 && b.left.CompareTo(a.right) < 0;
        }

        public static bool Contains(Peak interval, int point, ContainConstrains constraint)
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
                    throw new ArgumentException("Ivnalid constraint " + constraint);
            }

            return isContained;
        }

        public static bool Contains(Peak interval, int point)
        {
            //return time < end && time > start;
            return point.CompareTo(interval.right) < 0 && point.CompareTo(interval.left) > 0;
        }

        public static bool ContainsWithStart(Peak interval, int point)
        {
            return point.CompareTo(interval.right) < 0 && point.CompareTo(interval.left) >= 0;
        }

        public static bool ContainsWithEnd(Peak interval, int point)
        {
            return point.CompareTo(interval.right) <= 0 && point.CompareTo(interval.left) > 0;
        }

        public static bool ContainsWithStartEnd(Peak interval, int point)
        {
            return point.CompareTo(interval.right) <= 0 && point.CompareTo(interval.left) >= 0;
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
