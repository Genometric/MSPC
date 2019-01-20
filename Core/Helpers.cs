// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Interfaces;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Genometric.MSPC.Core
{
    public static class Helpers<I>
        where I: IPPeak
    {
        public static IEnumerable<I> Get(IReadOnlyCollection<I> peaks, Attributes attribute)
        {
            return peaks.Where(kvp => kvp.HasAttribute(attribute));
        }

        public static IEnumerable<I> Get(Bed<I> sample, string chr, char strand, Attributes attribute)
        {
            return Get(sample.Chromosomes[chr].Strands[strand].Intervals, attribute);
        }

        public static int Count(IReadOnlyCollection<I> peaks, Attributes attribute)
        {
            return peaks.Count(kvp => kvp.HasAttribute(attribute));
        }

        public static int Count(Bed<I> sample, string chr, char strand, Attributes attribute)
        {
            return Count(sample.Chromosomes[chr].Strands[strand].Intervals, attribute);
        }
    }
}
