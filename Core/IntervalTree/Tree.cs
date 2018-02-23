
using Polimi.DEIB.VahidJalili.IGenomics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class Tree<P, M>
        where P : IInterval<int, M>, IComparable<P>, new()
        where M : IChIPSeqPeak, IComparable<M>, new()
    {
        private Node<P, M> _head;
        private List<P> _intervalList;
        private bool _inSync;
        private int _size;

        public Tree()
        {
            _head = new Node<P, M>();
            _intervalList = new List<P>();
            _inSync = true;
            _size = 0;
        }

        public Tree(List<P> intervalList)
        {
            _head = new Node<P, M>(intervalList);
            _intervalList = new List<P>();
            _intervalList.AddRange(intervalList);
            _inSync = true;
            _size = intervalList.Count;
        }

        public void Add(P interval)
        {
            _intervalList.Add(interval);
            _inSync = false;
        }

        public List<P> GetIntervals(P peak)
        {
            Build();
            return _head.Query(peak);
        }

        public void Build()
        {
            if (!_inSync)
            {
                _head = new Node<P, M>(_intervalList);
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
