using Genometric.IGenomics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class Node<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        private SortedDictionary<Peak, List<Peak>> intervals { set; get; }
        private int center { set; get; }
        private Node<Peak, Metadata> leftNode { set; get; }
        private Node<Peak, Metadata> rightNode { set; get; }

        public Node()
        {
            intervals = new SortedDictionary<Peak, List<Peak>>();
            center = 0;
            leftNode = null;
            rightNode = null;
        }

        public Node(List<Peak> intervalsList)
        {
            intervals = new SortedDictionary<Peak, List<Peak>>();
            var endpoints = new SortedSet<int>();
            foreach (var interval in intervalsList)
            {
                endpoints.Add(interval.left);
                endpoints.Add(interval.right);
            }

            int? median = GetMedian(endpoints);
            center = median.GetValueOrDefault();

            var left = new List<Peak>();
            var right = new List<Peak>();

            foreach (Peak interval in intervalsList)
            {
                if (interval.right.CompareTo(center) < 0)
                    left.Add(interval);
                else if (interval.left.CompareTo(center) > 0)
                    right.Add(interval);
                else
                {
                    List<Peak> posting;
                    if (!intervals.TryGetValue(interval, out posting))
                    {
                        posting = new List<Peak>();
                        intervals.Add(interval, posting);
                    }
                    posting.Add(interval);
                }
            }

            if (left.Count > 0)
                leftNode = new Node<Peak, Metadata>(left);
            if (right.Count > 0)
                rightNode = new Node<Peak, Metadata>(right);
        }

        private int? GetMedian(SortedSet<int> set)
        {
            int i = 0;
            int middle = set.Count / 2;
            foreach (var point in set)
            {
                if (i == middle)
                    return point;
                i++;
            }
            return null;
        }

        private List<Peak> GetIntervalsOfKeys(List<Peak> intervalKeys)
        {
            var allIntervals =
              from k in intervalKeys
              select intervals[k];

            return allIntervals.SelectMany(x => x).ToList();
        }

        public IEnumerable<IList<Peak>> Intersections
        {
            get
            {
                if (intervals.Count == 0) yield break;
                else if (intervals.Count == 1)
                {
                    if (intervals.First().Value.Count > 1)
                    {
                        yield return intervals.First().Value;
                    }
                }
                else
                {
                    var keys = intervals.Keys.ToArray();

                    int lastIntervalIndex = 0;
                    var intersectionsKeys = new List<Peak>();
                    for (int index = 1; index < intervals.Count; index++)
                    {
                        var intervalKey = keys[index];
                        if (IntervalOperations<Peak, Metadata>.Intersects(intervalKey, keys[lastIntervalIndex]))
                        {
                            if (intersectionsKeys.Count == 0)
                            {
                                intersectionsKeys.Add(keys[lastIntervalIndex]);
                            }
                            intersectionsKeys.Add(intervalKey);
                        }
                        else
                        {
                            if (intersectionsKeys.Count > 0)
                            {
                                yield return GetIntervalsOfKeys(intersectionsKeys);
                                intersectionsKeys = new List<Peak>();
                                index--;
                            }
                            else
                            {
                                if (intervals[intervalKey].Count > 1)
                                {
                                    yield return intervals[intervalKey];
                                }
                            }

                            lastIntervalIndex = index;
                        }
                    }

                    if (intersectionsKeys.Count > 0) yield return GetIntervalsOfKeys(intersectionsKeys);
                }
            }
        }

        public List<Peak> Stab(int time, ContainConstrains constraint)
        {
            List<Peak> result = new List<Peak>();

            foreach (var entry in intervals)
            {
                if (IntervalOperations<Peak, Metadata>.Contains(entry.Key, time, constraint))
                    foreach (var interval in entry.Value)
                        result.Add(interval);
                else if (entry.Key.left.CompareTo(time) > 0)
                    break;
            }

            if (time.CompareTo(center) < 0 && leftNode != null)
                result.AddRange(leftNode.Stab(time, constraint));
            else if (time.CompareTo(center) > 0 && rightNode != null)
                result.AddRange(rightNode.Stab(time, constraint));
            return result;
        }

        public List<Peak> Query(Peak target)
        {
            List<Peak> result = new List<Peak>();

            foreach (var entry in intervals)
            {
                if (IntervalOperations<Peak, Metadata>.Intersects(entry.Key, target))
                    foreach (Peak interval in entry.Value)
                        result.Add(interval);
                else if (entry.Key.left.CompareTo(target.right) > 0)
                    break;
            }

            if (target.left.CompareTo(center) < 0 && leftNode != null)
                result.AddRange(leftNode.Query(target));
            if (target.right.CompareTo(center) > 0 && rightNode != null)
                result.AddRange(rightNode.Query(target));
            return result;
        }


        public int Center
        {
            get { return center; }
            set { center = value; }
        }

        public Node<Peak, Metadata> Left
        {
            get { return leftNode; }
            set { leftNode = value; }
        }

        public Node<Peak, Metadata> Right
        {
            get { return rightNode; }
            set { rightNode = value; }
        }
    }
}
