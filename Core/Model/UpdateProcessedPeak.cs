// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.Model
{
    internal static class UpdateProcessedPeak<I>
        where I : IChIPSeqPeak, new()
    {
        public static ProcessedPeak<I> AsTecRep(ProcessedPeak<I> input)
        {
            return input;
        }

        public static ProcessedPeak<I> AsBioRep(ProcessedPeak<I> input)
        {
            return input;
        }
    }
}
