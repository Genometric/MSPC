// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.Core.Functions;
using Genometric.MSPC.Core.Interfaces;
using System.Collections.Generic;
using static Genometric.MSPC.Core.Model.Messages;

namespace Genometric.MSPC.Core.Model
{
    public class PPeak: Peak, IPPeak
    {
        public double XSquared { set; get; }
        /// <summary>
        /// Sets and gets adjusted p-value (false discovery rate) using the 
        /// Benjamini and Hochberg Multiple Testing Correction multiple 
        /// testing correction.
        /// </summary>
        public double AdjPValue { set; get; }
        /// <summary>
        /// Right tailed probability of x-squared.
        /// </summary>
        public double RTP
        {
            get
            {
                if (double.IsNaN(_rtp) && !double.IsNaN(XSquared))
                    _rtp = ChiSqrd.ChiSqrdDistRTP(XSquared, 2 + (SupportingPeaksCount * 2));
                return _rtp;
            }
        }
        private double _rtp;
        public int SupportingPeaksCount { set; get; }
        public List<SupportingPeak<IPPeak>> SupportingPeaks { set; get; }

        /// <summary>
        /// Gets the reason of discarding this ER. It returns an empty string if 
        /// this ER is confirmed. 
        /// </summary>
        public string Reason { get { return Decode(_reason); } }
        private Codes _reason = Codes.M000;

        private byte _classification;

        public PPeak(int left, int right, double value, string name = null, int summit = -1, string hashSeed = "")
            : base(left, right, value, name, summit, hashSeed)
        {
            _rtp = double.NaN;
            AdjPValue = double.NaN;
            XSquared = double.NaN;
        }

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

        public void SetReason(Codes code)
        {
            _reason = code;
        }


        public override bool Equals(object obj)
        {
            return 
                obj is PPeak peak &&
                base.Equals(peak) && 
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

        public new int CompareTo(object obj)
        {
            return base.CompareTo(obj);
        }

        public new int CompareTo(IPeak other)
        {
            if (other == null) return 1;

            int c = Left.CompareTo(other.Left);
            if (c != 0) return c;
            c = Right.CompareTo(other.Right);
            if (c != 0) return c;
            c = Value.CompareTo(other.Value);
            if (c != 0) return c;
            return GetHashCode().CompareTo(other.GetHashCode());
        }

        public static bool operator >(PPeak operand1, PPeak operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        public static bool operator <(PPeak operand1, PPeak operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        public static bool operator >=(PPeak operand1, PPeak operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        public static bool operator <=(PPeak operand1, PPeak operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }

        public static bool operator ==(PPeak operand1, PPeak operand2)
        {
            if (operand1 is null)
                return operand2 is null;
            return operand1.Equals(operand2);
        }

        public static bool operator !=(PPeak operand1, PPeak operand2)
        {
            return !(operand1 == operand2);
        }
    }
}
