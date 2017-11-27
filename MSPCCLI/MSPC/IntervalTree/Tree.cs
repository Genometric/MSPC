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
        private Node<Peak, Metadata> head;
        private List<Peak> intervalList;
        private bool inSync;
        private int size;

        public Tree()
        {
            this.head = new Node<Peak, Metadata>();
            this.intervalList = new List<Peak>();
            this.inSync = true;
            this.size = 0;
        }

        public Tree(List<Peak> intervalList)
        {
            this.head = new Node<Peak, Metadata>(intervalList);
            this.intervalList = new List<Peak>();
            this.intervalList.AddRange(intervalList);
            this.inSync = true;
            this.size = intervalList.Count;
        }

        public void Add(Peak interval)
        {
            intervalList.Add(interval);
            inSync = false;
        }

        public List<Peak> GetIntervals(Peak peak)
        {
            Build();
            return head.Query(peak);
        }

        public void Build()
        {
            if (!inSync)
            {
                head = new Node<Peak, Metadata>(intervalList);
                inSync = true;
                size = intervalList.Count;
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
