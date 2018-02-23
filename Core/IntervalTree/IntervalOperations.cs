
using Polimi.DEIB.VahidJalili.IGenomics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class IntervalOperations<P, M>
        where P : IInterval<int, M>, IComparable<P>, new()
        where M : IChIPSeqPeak, IComparable<M>, new()
    {
        public static bool Intersects(P a, P b)
        {
            return b.right.CompareTo(a.left) > 0 && b.left.CompareTo(a.right) < 0;
        }

        public static bool Contains(P interval, int point, ContainConstrains constraint)
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

        public static bool Contains(P interval, int point)
        {
            //return time < end && time > start;
            return point.CompareTo(interval.right) < 0 && point.CompareTo(interval.left) > 0;
        }

        public static bool ContainsWithStart(P interval, int point)
        {
            return point.CompareTo(interval.right) < 0 && point.CompareTo(interval.left) >= 0;
        }

        public static bool ContainsWithEnd(P interval, int point)
        {
            return point.CompareTo(interval.right) <= 0 && point.CompareTo(interval.left) > 0;
        }

        public static bool ContainsWithStartEnd(P interval, int point)
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
