using Genometric.GeUtilities.IGenomics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.Model
{
    public class SupportingPeak<I> : IComparable<SupportingPeak<I>>
            where I : IChIPSeqPeak, new()
    {
        /// <summary>
        /// Sets and gets the source of the er in cached data. 
        /// </summary>
        public UInt32 sampleID { set; get; }

        /// <summary>
        /// Sets and gets the supporting er.
        /// </summary>
        public I peak { set; get; }

        int IComparable<SupportingPeak<I>>.CompareTo(SupportingPeak<I> that)
        {
            return CompareWithThis(that);
        }

        public int CompareTo(object obj)
        {
            return CompareWithThis((SupportingPeak<I>)obj);
        }

        private int CompareWithThis(SupportingPeak<I> that)
        {
            if (that == null) return 1;

            if (this.sampleID != that.sampleID)
                return this.sampleID.CompareTo(that.sampleID);

            if (this.peak.Left != that.peak.Left)
                return this.peak.Left.CompareTo(that.peak.Left);

            if (this.peak.Right != that.peak.Right)
                return this.peak.Right.CompareTo(that.peak.Right);

            /*if (this.er.metadata.strand != that.er.metadata.strand)
                return this.er.metadata.strand.CompareTo(that.er.metadata.strand);*/

            if (this.peak.Value != that.peak.Value)
                return this.peak.Value.CompareTo(that.peak.Value);

            /*if (this.er.metadata.name != that.er.metadata.name)
                return this.er.metadata.name.CompareTo(that.er.metadata.name);*/

            return 0;
        }
    }
}
