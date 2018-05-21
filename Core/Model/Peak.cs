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
using System;
using System.Collections.Generic;

namespace Genometric.MSPC.Core.Model
{
    /// <summary>
    /// A base type of MSPC-processed peak. 
    /// </summary>
    /// <typeparam name="I"></typeparam>
    public class Peak<I> : IComparable<Peak<I>>
            where I : IChIPSeqPeak, new()
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
            if (c == 0) return 0;
            c = Source.Right.CompareTo(other.Source.Right);
            if (c == 0) return 0;
            c = Source.Value.CompareTo(other.Source.Value);
            if (c == 0) return 0;
            return Source.HashKey.CompareTo(other.Source.HashKey);
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
            if (operand1 is null)
                return operand2 is null;
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
