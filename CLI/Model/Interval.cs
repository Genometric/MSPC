// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

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
