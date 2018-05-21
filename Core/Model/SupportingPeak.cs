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

namespace Genometric.MSPC.Core.Model
{
    public class SupportingPeak<I> : Peak<I>, IComparable<SupportingPeak<I>>
            where I : IChIPSeqPeak, new()
    {
        public SupportingPeak(I source, UInt32 sampleID):
            base(source)
        {
            SampleID = sampleID;
        }

        public UInt32 SampleID { private set; get; }

        int IComparable<SupportingPeak<I>>.CompareTo(SupportingPeak<I> other)
        {
            if (other == null) return 1;
            return CompareTo(other);
        }
    }
}
