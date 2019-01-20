// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Core.Interfaces;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class Tree<I>
        where I : IPPeak
    {
        private Node<I> _head;
        private readonly List<I> _intervalList;
        private bool _inSync;

        public Tree()
        {
            _head = new Node<I>();
            _intervalList = new List<I>();
            _inSync = true;
        }

        public void Add(I interval)
        {
            _intervalList.Add(interval);
            _inSync = false;
        }

        public List<I> GetIntervals(I peak)
        {
            BuildAndFinalize();
            return _head.Query(peak);
        }

        public void BuildAndFinalize()
        {
            if (!_inSync)
            {
                _head = new Node<I>(_intervalList);
                /// If it is intended to only build this tree
                /// without finalizing it, then remove the
                /// following line.
                _intervalList.Clear();
                _inSync = true;
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
