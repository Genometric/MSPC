// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

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
            return Peak.CompareTo(other.Peak);    
        }
    }
}
