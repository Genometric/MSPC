/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using System;
using Genometric.GeUtilities.IGenomics;

namespace Genometric.MSPC.CLI.Model
{
    public class Interval : IInterval<int>
    {
        public Interval() { }

        /// <summary>
        /// Sets and gets the left-end of the interval.
        /// </summary>
        public int Left { set; get; }

        /// <summary>
        /// Sets and gets the right-end of the interval.
        /// </summary>
        public int Right { set; get; }

        public uint HashKey { set; get; }

        public void Merge(int left, int right)
        {
            Left = Math.Min(Left, left);
            Right = Math.Max(Right, right);
        }

        public int CompareTo(Interval that)
        {
            /*if (that == null) return 1;
            if (this.left != that.left) return this.left.CompareTo(that.left);
            if (this.right != that.right) return this.right.CompareTo(that.right);

            //if (this.metadata != that.metadata)
            return this.metadata.CompareTo(that.metadata);*/
            if (Right <= that.Left) return -1;
            if (Left >= that.Right) return 1;
            return 0;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
