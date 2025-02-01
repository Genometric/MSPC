using Genometric.GeUtilities.IGenomics;
using System;

namespace Genometric.MSPC.Core.IntervalTree
{
    internal class NodeData<I> : IComparable<NodeData<I>>
        where I : IPeak
    {
        public I Peak { get; }
        public uint SampleID { get; }

        public NodeData(I peak, uint sampleID)
        {
            Peak = peak;
            SampleID = sampleID;
        }

        public int CompareTo(NodeData<I> other)
        {
            int c = Peak.CompareTo(other.Peak);
            if (c != 0) return c;
            c = SampleID.CompareTo(other.SampleID);
            if (c != 0) return c;
            /// At this point two NodeData are
            /// exactly same and does not matter
            /// which one proceedds the other,
            /// hence return -1 or 1. However, 
            /// should not return 0, because 
            /// that would fail adding them to
            /// the intervals list (two items 
            /// with same key). 
            return -1;
        }
    }
}
