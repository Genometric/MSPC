// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Base
{
    public class C
    {
        private string _chr = "chr1";
        private char _strand = '*';

        //                        r11
        // Sample 1: ---------███████████----------
        //                  r21
        // Sample 2: ----████████------------------
        //                        r31
        // Sample 3: -------███████████████--------
        //                               r41
        // Sample 4: -----------------█████████----
        //                        r51
        // Sample 4: -------------███--------------
        private static IEnumerable<ChIPSeqPeak> GetPeaks(int count)
        {
            var peaks = new List<ChIPSeqPeak>() {
                new ChIPSeqPeak() { Left = 10, Right = 20, Name = "r11", Value = 1e-18, HashKey = 1 },
                new ChIPSeqPeak() { Left = 05, Right = 12, Name = "r21", Value = 1e-22, HashKey = 2 },
                new ChIPSeqPeak() { Left = 08, Right = 22, Name = "r31", Value = 1e-47, HashKey = 3 },
                new ChIPSeqPeak() { Left = 18, Right = 26, Name = "r41", Value = 1e-55, HashKey = 4 },
                new ChIPSeqPeak() { Left = 14, Right = 16, Name = "r51", Value = 1e-61, HashKey = 5 }};
            return peaks.Take(count);
        }

        [Theory]
        [InlineData(2, 0, 1, 0)]
        [InlineData(2, 1, 1, 0)]
        [InlineData(2, 2, 1, 0)]
        [InlineData(2, 3, 0, 1)]
        [InlineData(2, 4, 0, 1)]
        [InlineData(3, 3, 1, 0)]
        [InlineData(3, 4, 0, 1)]
        [InlineData(5, 0, 1, 0)]
        [InlineData(5, 1, 1, 0)]
        [InlineData(5, 2, 1, 0)]
        [InlineData(5, 3, 1, 0)]
        [InlineData(5, 4, 1, 0)]
        [InlineData(5, 5, 1, 0)]
        [InlineData(5, 6, 0, 1)]
        [InlineData(5, 7, 0, 1)]
        [InlineData(5, 8, 0, 1)]
        public void AssertCRequirement(int samplesCount, byte c, int confirmedPeaksCount, int discardedPeaksCount)
        {
            // Arrange
            uint id = 0;
            var mspc = new MSPC<ChIPSeqPeak>();
            foreach(var peak in GetPeaks(samplesCount))
            {
                var sample = new BED<ChIPSeqPeak>();
                sample.Add(peak, _chr, _strand);
                mspc.AddSample(id++, sample);
            }

            // Act
            var res = mspc.Run(new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, c, 1F, MultipleIntersections.UseLowestPValue));

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).Count() == confirmedPeaksCount);
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Discarded).Count() == discardedPeaksCount);
        }
    }
}
