﻿using Genometric.GeUtilities.IGenomics;
using System.Collections.Generic;
using System.Linq;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class Node<I, D>
        where I : IPeak
        where D : NodeData<I>
    {
        private readonly SortedDictionary<D, uint> _intervals;
        private readonly int _center;
        private readonly Node<I, D> _leftNode;
        private readonly Node<I, D> _rightNode;

        public Node()
        {
            _intervals = new SortedDictionary<D, uint>();
            _center = 0;
            _leftNode = null;
            _rightNode = null;
        }

        public Node(List<D> intervalsList)
        {
            _intervals = new SortedDictionary<D, uint>();
            var endpoints = new SortedSet<int>();
            foreach (var interval in intervalsList)
            {
                endpoints.Add(interval.Peak.Left);
                endpoints.Add(interval.Peak.Right);
            }
            _center = endpoints.ElementAt(endpoints.Count / 2);

            var left = new List<D>();
            var right = new List<D>();

            foreach (D interval in intervalsList)
            {
                if (interval.Peak.Right.CompareTo(_center) < 0)
                    left.Add(interval);
                else if (interval.Peak.Left.CompareTo(_center) > 0)
                    right.Add(interval);
                else
                {
                    _intervals.Add(interval, interval.SampleID);
                }
            }

            if (left.Count > 0)
                _leftNode = new Node<I, D>(left);
            if (right.Count > 0)
                _rightNode = new Node<I, D>(right);
        }

        public List<NodeData<I>> Query(D target, uint skipID)
        {
            var result = new List<NodeData<I>>();

            foreach (var entry in _intervals)
                if (target.Peak.Right.CompareTo(entry.Key.Peak.Left) > 0 &&
                    target.Peak.Left.CompareTo(entry.Key.Peak.Right) < 0)
                {
                    if (entry.Value != skipID)
                        result.Add(entry.Key);
                }
                else if (entry.Key.Peak.Left.CompareTo(target.Peak.Right) > 0)
                    break;

            if (target.Peak.Left.CompareTo(_center) < 0 && _leftNode != null)
                result.AddRange(_leftNode.Query(target, skipID));
            if (target.Peak.Right.CompareTo(_center) > 0 && _rightNode != null)
                result.AddRange(_rightNode.Query(target, skipID));
            return result;
        }
    }
}
