// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Core.Interfaces;
using System;

namespace Genometric.MSPC.Core.Model
{
    // TODO: maybe can replace IComparable with IEqual
    public class SupportingPeak<I> : IComparable<SupportingPeak<I>>
            where I : IPPeak
    {
        public SupportingPeak(I source, uint sampleID)
        {
            SampleID = sampleID;
            Source = source;
        }

        public uint SampleID { get; }
        public I Source { get; }

        public int CompareTo(SupportingPeak<I> other)
        {
            if (other == null) return 1;
            int c = SampleID.CompareTo(other.SampleID);
            if (c != 0) return c;
            return Source.CompareTo(other.Source);
        }
    }
}
