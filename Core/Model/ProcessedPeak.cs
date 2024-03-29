﻿using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Functions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Genometric.MSPC.Core.Model.Messages;

namespace Genometric.MSPC.Core.Model
{
    public class ProcessedPeak<I> : Peak<I>, IComparable<ProcessedPeak<I>>
            where I : IPeak
    {
        public ProcessedPeak(I source, double xSquared, List<SupportingPeak<I>> supportingPeaks) :
            this(source, xSquared, supportingPeaks.AsReadOnly())
        { }

        public ProcessedPeak(I source, double xSquared, ReadOnlyCollection<SupportingPeak<I>> supportingPeaks) :
            this(source, xSquared, supportingPeaks.Count)
        {
            SupportingPeaks = supportingPeaks;
        }

        public ProcessedPeak(I source, double xSquared, int supportingPeaksCount = 0) :
            base(source)
        {
            AdjPValue = double.NaN;
            XSquared = xSquared;
            if (double.IsNaN(xSquared))
                RTP = double.NaN;
            else
                RTP = ChiSqrd.ChiSqrdDistRTP(XSquared, 2 + (supportingPeaksCount * 2));
        }


        /// <summary>
        /// Sets and gets X-squared of test
        /// </summary>
        public double XSquared { private set; get; }

        /// <summary>
        /// Right tailed probability of x-squared.
        /// </summary>
        public double RTP { private set; get; }

        public ReadOnlyCollection<SupportingPeak<I>> SupportingPeaks { private set; get; }

        /// <summary>
        /// Gets the reason of discarding this ER. It returns an empty string if 
        /// this ER is confirmed. 
        /// </summary>
        public string Reason { get { return Decode(reason); } }
        internal Codes reason = Codes.M000;

        private byte _classification;

        public void AddClassification(Attributes attribute)
        {
            if (!HasAttribute(attribute))
                _classification += (byte)attribute;
        }

        public bool HasAttribute(Attributes attribute)
        {
            return (_classification & (byte)attribute) == (byte)attribute;
        }

        public bool Different(Attributes attribute)
        {
            return _classification != (byte)attribute;
        }

        public bool Different(byte attribute)
        {
            return _classification != attribute;
        }

        /// <summary>
        /// Sets and gets adjusted p-value (false discovery rate) using the 
        /// Benjamini and Hochberg Multiple Testing Correction multiple 
        /// testing correction.
        /// </summary>
        public double AdjPValue { internal set; get; }

        public int CompareTo(ProcessedPeak<I> other)
        {
            if (other == null) return 1;
            return base.CompareTo(other);
        }

        public override bool Equals(object obj)
        {
            return obj is ProcessedPeak<I> peak &&
                   EqualityComparer<I>.Default.Equals(Source, peak.Source) &&
                   ((double.IsNaN(XSquared) && double.IsNaN(peak.XSquared)) || XSquared == peak.XSquared) &&
                   ((double.IsNaN(RTP) && double.IsNaN(peak.RTP)) || RTP == peak.RTP) &&
                   Reason == peak.Reason &&
                   !peak.Different(_classification) &&
                   ((double.IsNaN(AdjPValue) && double.IsNaN(peak.AdjPValue)) || AdjPValue == peak.AdjPValue);
        }

        public override int GetHashCode()
        {
            string key = base.GetHashCode() + "_" + XSquared + "_" + RTP;
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
            return !(operand1 == operand2);
        }
    }
}
