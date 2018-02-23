/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Genometric.IGenomics;
using System;

namespace Genometric.MSPC.CLI.Model
{
    public class Interval<C, M> : IInterval<int, M>, IComparable<Interval<C, M>>
        where M : IExtendedMetadata, IComparable<M>, new()
    {
        public Interval()
        {
            metadata = new M();
        }

        /// <summary>
        /// Sets and gets the left-end of the interval.
        /// </summary>
        public int left { set; get; }

        /// <summary>
        /// Sets and gets the right-end of the interval.
        /// </summary>
        public int right { set; get; }

        /// <summary>
        /// Sets and gets the descriptive metadata
        /// of the interval. It could be a reference
        /// to a memory object, or a pointer, or 
        /// an entry ID on database, or etc. 
        /// </summary>
        public M metadata { set; get; }

        public uint hashKey { set; get; }

        public void Merge(int left, int right)
        {
            this.left = Math.Min(this.left, left);
            this.right = Math.Max(this.right, right);
        }

        public int CompareTo(Interval<C, M> that)
        {
            /*if (that == null) return 1;
            if (this.left != that.left) return this.left.CompareTo(that.left);
            if (this.right != that.right) return this.right.CompareTo(that.right);

            //if (this.metadata != that.metadata)
            return this.metadata.CompareTo(that.metadata);*/
            if (this.right <= that.left) return -1;
            if (this.left >= that.right) return 1;
            return 0;
        }
    }
}
