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
using Genometric.MSPC.XSquaredData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Genometric.MSPC.Core.Model.Messages;

namespace Genometric.MSPC.Core.Model
{
    public class ProcessedPeak<I> : Peak<I>, IComparable<ProcessedPeak<I>>
            where I : IChIPSeqPeak, new()
    {
        internal ProcessedPeak(I peak, double xSquared, List<SupportingPeak<I>> supportingPeaks, Codes reason = Codes.M000) :
            this(peak, xSquared, supportingPeaks.AsReadOnly(), reason)
        { }

        internal ProcessedPeak(I source, double xSquared, ReadOnlyCollection<SupportingPeak<I>> supportingPeaks, Codes reason = Codes.M000):
            base(source)
        {
            XSquared = xSquared;
            RTP = ChiSquaredCache.ChiSqrdDistRTP(xSquared, 2 + (supportingPeaks.Count * 2));
            SupportingPeaks = supportingPeaks;
            _reason = reason;
            Classification = new HashSet<Attributes>
            {
                Attributes.TruePositive
            };
        }

        /// <summary>
        /// Sets and gets X-squared of test
        /// </summary>
        public double XSquared { private set; get; }

        /// <summary>
        /// Right tailed probability of x-squared.
        /// </summary>
        public double RTP { private set; get; }

        /// <summary>
        /// Sets and gets the set of peaks intersecting with confirmed er
        /// </summary>
        public ReadOnlyCollection<SupportingPeak<I>> SupportingPeaks { private set; get; }

        /// <summary>
        /// Gets the reason of discarding this ER. It returns an empty string if 
        /// this ER is confirmed. 
        /// </summary>
        public string Reason { get { return Decode(_reason); } }
        private readonly Codes _reason;

        /// <summary>
        /// Sets and gets classification type. 
        /// </summary>
        public HashSet<Attributes> Classification { internal set; get; }

        /// <summary>
        /// Sets and gets adjusted p-value using the multiple testing correction method of choice.
        /// </summary>
        public double AdjPValue { internal set; get; }

        internal void SetStatisticalClassification(Attributes attribute)
        {
            if (attribute != Attributes.TruePositive && attribute != Attributes.FalsePositive)
                throw new ArgumentException(
                    String.Format("Invalid attribute; accepted values are: {0} and {1}.",
                    Attributes.TruePositive.ToString(), Attributes.FalsePositive.ToString()));

            if (!Classification.Remove(attribute))
                Classification.Add(attribute);
        }

        int IComparable<ProcessedPeak<I>>.CompareTo(ProcessedPeak<I> other)
        {
            if (other == null) return 1;
            return CompareTo(other);
        }

        public override bool Equals(object obj)
        {
            return obj is ProcessedPeak<I> peak &&
                   EqualityComparer<I>.Default.Equals(Source, peak.Source) &&
                   XSquared == peak.XSquared &&
                   RTP == peak.RTP &&
                   EqualityComparer<ReadOnlyCollection<SupportingPeak<I>>>.Default.Equals(SupportingPeaks, peak.SupportingPeaks) &&
                   Reason == peak.Reason &&
                   EqualityComparer<HashSet<Attributes>>.Default.Equals(Classification, peak.Classification) &&
                   AdjPValue == peak.AdjPValue;
        }

        public override int GetHashCode()
        {
            string key = base.GetHashCode() + "_" + XSquared + "_" + RTP + "_" + SupportingPeaks.Count;
            int l = key.Length;

            int hashKey = 0;
            for (int i = 0; i < l; i++)
            {
                hashKey += key[i];
                hashKey += (hashKey << 10);
                hashKey ^= (hashKey >> 6);
            }

            hashKey += (hashKey << 3);
            hashKey ^= (hashKey >> 11);
            hashKey += (hashKey << 15);

            return hashKey;
        }

        public static bool operator >(ProcessedPeak<I> operand1, ProcessedPeak<I> operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        public static bool operator <(ProcessedPeak<I> operand1, ProcessedPeak<I> operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        public static bool operator >=(ProcessedPeak<I> operand1, ProcessedPeak<I> operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        public static bool operator <=(ProcessedPeak<I> operand1, ProcessedPeak<I> operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }

        public static bool operator ==(ProcessedPeak<I> operand1, ProcessedPeak<I> operand2)
        {
            if (operand1 is null)
                return operand2 is null;
            return operand1.Equals(operand2);
        }

        public static bool operator !=(ProcessedPeak<I> operand1, ProcessedPeak<I> operand2)
        {
            if (operand1 is null)
                return operand2 is null;
            return !operand1.Equals(operand2);
        }
    }
}
