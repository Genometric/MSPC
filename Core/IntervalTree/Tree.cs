// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System.Collections.Generic;

namespace Genometric.MSPC.IntervalTree
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
