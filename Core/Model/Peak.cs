// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Model
{
    /// <summary>
    /// A base type of MSPC-processed peak. 
    /// </summary>
    /// <typeparam name="I"></typeparam>
    public class Peak<I> : IComparable<Peak<I>>
            where I : IPeak
    {
        public Peak(I source)
        {
            Source = source;
        }

        public I Source { private set; get; }

        public int CompareTo(Peak<I> other)
        {
            if (other == null) return 1;

            int c = Source.Left.CompareTo(other.Source.Left);
            if (c != 0) return c;
            c = Source.Right.CompareTo(other.Source.Right);
            if (c != 0) return c;
            c = Source.Value.CompareTo(other.Source.Value);
            if (c != 0) return c;
            return Source.GetHashCode().CompareTo(other.Source.GetHashCode());
        }

        public static bool operator >(Peak<I> operand1, Peak<I> operand2)
        {
            return operand1.Source.CompareTo(operand2.Source) == 1;
        }

        public static bool operator <(Peak<I> operand1, Peak<I> operand2)
        {
            return operand1.Source.CompareTo(operand2.Source) == -1;
        }

        public static bool operator >=(Peak<I> operand1, Peak<I> operand2)
        {
            return operand1.Source.CompareTo(operand2.Source) >= 0;
        }

        public static bool operator <=(Peak<I> operand1, Peak<I> operand2)
        {
            return operand1.Source.CompareTo(operand2.Source) <= 0;
        }

        public static bool operator ==(Peak<I> operand1, Peak<I> operand2)
        {
            if (operand1 is null)
                return operand2 is null;
            return operand1.Equals(operand2);
        }

        public static bool operator !=(Peak<I> operand1, Peak<I> operand2)
        {
            return !operand1.Equals(operand2);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return EqualityComparer<I>.Default.Equals(Source, (obj as Peak<I>).Source);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode();
        }

        public override string ToString()
        {
            return Source.ToString();
        }
    }
}
