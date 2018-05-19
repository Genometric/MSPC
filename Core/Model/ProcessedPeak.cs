using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.Model
{
    public class ProcessedPeak<I> : IComparable<ProcessedPeak<I>>
            where I : IChIPSeqPeak, new()
    {
        public ProcessedPeak()
        {
            statisticalClassification = Attributes.TruePositive;
            classification = new HashSet<Attributes>();
        }

        /// <summary>
        /// Sets and gets the Confirmed I. Is a reference to the original er in cached data.
        /// </summary>
        public I peak { set; get; }

        /// <summary>
        /// Sets and gets X-squared of test
        /// </summary>
        public double xSquared { set; get; }

        /// <summary>
        /// Right tailed probability of x-squared.
        /// </summary>
        public double rtp { set; get; }

        /// <summary>
        /// Sets and gets the set of peaks intersecting with confirmed er
        /// </summary>
        public List<SupportingPeak<I>> supportingPeaks { set; get; }

        /// <summary>
        /// Sets and gets the reason of discarding the er. It points to an index of
        /// predefined messages.
        /// </summary>
        public byte reason { set; get; }

        /// <summary>
        /// Sets and gets classification type. 
        /// </summary>
        public HashSet<Attributes> classification { set; get; }

        /// <summary>
        /// Sets and gets adjusted p-value using the multiple testing correction method of choice.
        /// </summary>
        public double adjPValue { set; get; }

        /// <summary>
        /// Set and gets whether the peaks is identified as false-positive or true-positive 
        /// based on multiple testing correction threshold. 
        /// </summary>
        public Attributes statisticalClassification { set; get; }

        /// <summary>
        /// Contains different classification types.
        /// </summary>
        int IComparable<ProcessedPeak<I>>.CompareTo(ProcessedPeak<I> that)
        {
            if (that == null) return 1;

            return this.peak.CompareTo(that.peak);
        }
    }
}
