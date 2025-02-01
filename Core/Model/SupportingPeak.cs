using Genometric.GeUtilities.IGenomics;
using System;

namespace Genometric.MSPC.Core.Model
{
    public class SupportingPeak<I> : Peak<I>, IComparable<SupportingPeak<I>>
            where I : IPeak
    {
        public SupportingPeak(I source, uint sampleID) :
            base(source)
        {
            SampleID = sampleID;
        }

        public uint SampleID { private set; get; }

        public int CompareTo(SupportingPeak<I> other)
        {
            if (other == null) return 1;
            if (SampleID != other.SampleID)
                return SampleID.CompareTo(other.SampleID);
            return base.CompareTo(other);
        }
    }
}
