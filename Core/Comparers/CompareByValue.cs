// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;
using System.Runtime.CompilerServices;

namespace Genometric.MSPC.Comparers
{
    public class CompareProcessedPeakByValue<I> : IComparer<ProcessedPeak<I>>
        where I : IChIPSeqPeak, new()
    {
        public int Compare(ProcessedPeak<I> A, ProcessedPeak<I> B)
        {
            if (A == null)
            {
                if (B == null) return 0; // they are equal
                else return -1; // B is greater
            }
            else
            {
                if (B == null) return 1; // A is greater
                else
                {
                    if (A.Source.Value != B.Source.Value)
                        return A.Source.Value.CompareTo(B.Source.Value);
                    else if (A.Source.Left != B.Source.Left)
                        return A.Source.Left.CompareTo(B.Source.Left);
                    else
                        return A.Source.Right.CompareTo(B.Source.Right);
                }
            }
        }
    }
}