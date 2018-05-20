/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Model
{
    public class ProcessedPeak<I> : IComparable<ProcessedPeak<I>>
            where I : IChIPSeqPeak, new()
    {
        public ProcessedPeak()
        {
            StatisticalClassification = Attributes.TruePositive;
            Classification = new HashSet<Attributes>();
        }

        /// <summary>
        /// Sets and gets the Confirmed I. Is a reference to the original er in cached data.
        /// </summary>
        public I Peak { set; get; }

        /// <summary>
        /// Sets and gets X-squared of test
        /// </summary>
        public double XSquared { set; get; }

        /// <summary>
        /// Right tailed probability of x-squared.
        /// </summary>
        public double RTP { set; get; }

        /// <summary>
        /// Sets and gets the set of peaks intersecting with confirmed er
        /// </summary>
        public List<SupportingPeak<I>> SupportingPeaks { set; get; }

        /// <summary>
        /// Sets and gets the reason of discarding the er. It points to an index of
        /// predefined messages.
        /// </summary>
        public byte Reason { set; get; }

        /// <summary>
        /// Sets and gets classification type. 
        /// </summary>
        public HashSet<Attributes> Classification { set; get; }

        /// <summary>
        /// Sets and gets adjusted p-value using the multiple testing correction method of choice.
        /// </summary>
        public double AdjPValue { set; get; }

        /// <summary>
        /// Set and gets whether the peaks is identified as false-positive or true-positive 
        /// based on multiple testing correction threshold. 
        /// </summary>
        public Attributes StatisticalClassification { set; get; }

        /// <summary>
        /// Contains different classification types.
        /// </summary>
        int IComparable<ProcessedPeak<I>>.CompareTo(ProcessedPeak<I> other)
        {
            if (other == null) return 1;

            return Peak.CompareTo(other.Peak);
        }
    }
}
