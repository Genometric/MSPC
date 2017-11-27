using Genometric.IGenomics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class Tree<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        private Node<Peak, Metadata> _head;
        private List<Peak> _intervalList;
        private bool _inSync;
        private int _size;

        public Tree()
        {
            _head = new Node<Peak, Metadata>();
            _intervalList = new List<Peak>();
            _inSync = true;
            _size = 0;
        }

        public Tree(List<Peak> intervalList)
        {
            _head = new Node<Peak, Metadata>(intervalList);
            _intervalList = new List<Peak>();
            _intervalList.AddRange(intervalList);
            _inSync = true;
            _size = intervalList.Count;
        }

        public void Add(Peak interval)
        {
            _intervalList.Add(interval);
            _inSync = false;
        }

        public List<Peak> GetIntervals(Peak peak)
        {
            Build();
            return _head.Query(peak);
        }

        public void Build()
        {
            if (!_inSync)
            {
                _head = new Node<Peak, Metadata>(_intervalList);
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
