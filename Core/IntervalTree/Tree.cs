
using System;
using System.Collections.Generic;
using System.Text;
using Genometric.GeUtilities.IGenomics;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class Tree<I>
        where I : IChIPSeqPeak, new()
    {
        private Node<I> _head;
        private List<I> _intervalList;
        private bool _inSync;
        private int _size;

        public Tree()
        {
            _head = new Node<I>();
            _intervalList = new List<I>();
            _inSync = true;
            _size = 0;
        }

        public Tree(List<I> intervalList)
        {
            _head = new Node<I>(intervalList);
            _intervalList = new List<I>();
            _intervalList.AddRange(intervalList);
            _inSync = true;
            _size = intervalList.Count;
        }

        public void Add(I interval)
        {
            _intervalList.Add(interval);
            _inSync = false;
        }

        public List<I> GetIntervals(I peak)
        {
            Build();
            return _head.Query(peak);
        }

        public void Build()
        {
            if (!_inSync)
            {
                _head = new Node<I>(_intervalList);
                _inSync = true;
                _size = _intervalList.Count;
            }
        }

        public enum StubMode
        {
            Contains,
            ContainsStart,
            ContainsStartThenEnd
        }
    }
}
