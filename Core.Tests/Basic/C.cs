// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class C
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

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
        private static IEnumerable<PPeak> GetPeaks(int count)
        {
            var peaks = new List<PPeak>() {
                new PPeak(left: 10, right: 20, name: "r11", value: 1e-18),
                new PPeak(left: 05, right: 12, name: "r21", value: 1e-22),
                new PPeak(left: 08, right: 22, name: "r31", value: 1e-47),
                new PPeak(left: 18, right: 26, name: "r41", value: 1e-55),
                new PPeak(left: 14, right: 16, name: "r51", value: 1e-61)};
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
        public void AssertCRequirement(int samplesCount, int c, int confirmedPeaksCount, int discardedPeaksCount)
        {
            // Arrange
            uint counter = 0;
            var samples = new List<Bed<PPeak>>();
            foreach (var peak in GetPeaks(samplesCount))
            {
                var sample = new Bed<PPeak>() { FileHashKey = counter++ };
                sample.Add(peak, _chr, _strand);
                samples.Add(sample);
            }

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, c, 1F, MultipleIntersections.UseLowestPValue));

            // Assert
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed) == confirmedPeaksCount);
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Discarded) == discardedPeaksCount);
        }
    }
}
