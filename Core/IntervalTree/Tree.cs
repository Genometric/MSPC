// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class Tree<I>
        where I : IPeak
    {
        private Node<I, NodeData<I>> _head;
        private readonly List<NodeData<I>> _intervalList;
        private bool _inSync;

        public Tree()
        {
            _head = new Node<I, NodeData<I>>();
            _intervalList = new List<NodeData<I>>();
            _inSync = true;
        }

        public void Add(I interval, uint sampleID)
        {
            _intervalList.Add(new NodeData<I>(interval, sampleID));
            _inSync = false;
        }

        public List<NodeData<I>> GetIntervals(I peak, uint skipID)
        {
            return _head.Query(new NodeData<I>(peak, 0), skipID);
        }

        public void BuildAndFinalize()
        {
            if (!_inSync)
            {
                _head = new Node<I, NodeData<I>>(_intervalList);
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
