﻿using Genometric.GeUtilities.Intervals.Model;
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
        private readonly char _strand = '.';

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
        private static IEnumerable<Peak> GetPeaks(int count)
        {
            var peaks = new List<Peak>() {
                new Peak(left: 10, right: 20, name: "r11", value: 1e-18),
                new Peak(left: 05, right: 12, name: "r21", value: 1e-22),
                new Peak(left: 08, right: 22, name: "r31", value: 1e-47),
                new Peak(left: 18, right: 26, name: "r41", value: 1e-55),
                new Peak(left: 14, right: 16, name: "r51", value: 1e-61)};
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
            uint id = 0;
            var mspc = new Mspc();
            foreach (var peak in GetPeaks(samplesCount))
            {
                var sample = new Bed<Peak>();
                sample.Add(peak, _chr, _strand);
                mspc.AddSample(id++, sample);
            }

            // Act
            var res = mspc.Run(new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, c, 1F, MultipleIntersections.UseLowestPValue));

            // Assert
            foreach (var strand in res[0].Chromosomes[_chr])
            {
                var x = strand.Value;
                Assert.True(x.Get(Attributes.Confirmed).Count() == confirmedPeaksCount);
                Assert.True(x.Get(Attributes.Discarded).Count() == discardedPeaksCount);
            }
        }
    }
}
