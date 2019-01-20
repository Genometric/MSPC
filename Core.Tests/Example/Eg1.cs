// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Example
{
    public class Eg1
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        public static IEnumerable<object[]> ExpectedAttributes =>
            new List<object[]>
            {
                /// Discard a stringent peak that does not satisfy C.
                /// r_12 overlaps with two peaks, but both of the peaks belong to 
                /// sample_2, hence only one of the peaks is _considereded_. Therefore, 
                /// r_12 is overlapping with only one peak, hence does not satisfy C. 
                new object[] { ReplicateType.Technical, 3, 0, "r11", Attributes.Weak, Attributes.Confirmed },

                /// Discard a stringent peak that does not satisfy C.
                /// r_12 overlaps with two peaks, but both of the peaks belong to 
                /// sample_2, hence only one of the peaks is _considereded_. Therefore, 
                /// r_12 is overlapping with only one peak, hence does not satisfy C. 
                new object[] { ReplicateType.Technical, 3, 0, "r12", Attributes.Stringent, Attributes.Discarded },

                new object[] { ReplicateType.Technical, 3, 1, "r21", Attributes.Weak, Attributes.Confirmed },
                new object[] { ReplicateType.Technical, 3, 1, "r22", Attributes.Weak, Attributes.Discarded },
                new object[] { ReplicateType.Technical, 3, 1, "r23", Attributes.Weak, Attributes.Discarded },
                new object[] { ReplicateType.Technical, 3, 2, "r31", Attributes.Weak, Attributes.Discarded },
                new object[] { ReplicateType.Technical, 3, 2, "r32", Attributes.Stringent, Attributes.Confirmed },
                new object[] { ReplicateType.Technical, 3, 2, "r33", Attributes.Stringent, Attributes.Discarded },

                new object[] { ReplicateType.Biological, 2, 0, "r11", Attributes.Weak, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 0, "r12", Attributes.Stringent, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 1, "r21", Attributes.Weak, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 1, "r22", Attributes.Weak, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 1, "r23", Attributes.Weak, Attributes.Discarded },
                new object[] { ReplicateType.Biological, 2, 2, "r31", Attributes.Weak, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 2, "r32", Attributes.Stringent, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 2, "r33", Attributes.Stringent, Attributes.Discarded },
            };

        private List<PPeak> GetPeaks()
        {
            //                 r11                r12
            // Sample 1: --███████████-------████████████----------------------------
            //                           r21             r22        r23
            // Sample 2: ---------████████████████----████████---████████------------
            //           r31       r32                                       r33
            // Sample 3: ████---██████████---------------------------------████████--
            var r11 = new PPeak(left: 3, right: 13, name: "r11", value: 1e-6);
            var r12 = new PPeak(left: 21, right: 32, name: "r12", value: 1e-12);
            var r21 = new PPeak(left: 10, right: 25, name: "r21", value: 1e-7);
            var r22 = new PPeak(left: 30, right: 37, name: "r22", value: 1e-5);
            var r23 = new PPeak(left: 41, right: 48, name: "r23", value: 1e-6);
            var r31 = new PPeak(left: 0, right: 4, name: "r31", value: 1e-6);
            var r32 = new PPeak(left: 8, right: 17, name: "r32", value: 1e-12);
            var r33 = new PPeak(left: 51, right: 58, name: "r33", value: 1e-18);

            return new List<PPeak>() { r11, r12, r21, r22, r23, r31, r32, r33 };
        }

        private List<Bed<PPeak>> GetSamples(string peakNameToOut, out PPeak peak)
        {
            var peaks = GetPeaks();
            peak = peaks.FirstOrDefault(x => x.Name == peakNameToOut);

            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(peaks[0], _chr, _strand);
            sA.Add(peaks[1], _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(peaks[2], _chr, _strand);
            sB.Add(peaks[3], _chr, _strand);
            sB.Add(peaks[4], _chr, _strand);

            var sC = new Bed<PPeak>() { FileHashKey = 2 };
            sC.Add(peaks[5], _chr, _strand);
            sC.Add(peaks[6], _chr, _strand);
            sC.Add(peaks[7], _chr, _strand);

            return new List<Bed<PPeak>>() { sA, sB, sC };
        }

        [Theory]
        [MemberData(nameof(ExpectedAttributes))]
        public void AssertAttributeAssignment(ReplicateType replicateType, int c, int sampleIndex, string peakName, Attributes initial, Attributes processed)
        {
            // Arrange
            var samples = GetSamples(peakName, out PPeak peak);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, new Config(replicateType, 1e-4, 1e-8, 1e-4, c, 1F, MultipleIntersections.UseLowestPValue));
            var qres = Helpers<PPeak>.Get(samples[sampleIndex], _chr, _strand, processed).FirstOrDefault(x => x.CompareTo(peak) == 0);

            // Assert
            Assert.NotNull(qres);
            Assert.True(qres.HasAttribute(initial));
            Assert.True(qres.HasAttribute(processed));
        }

        [Theory]
        [InlineData(ReplicateType.Technical, 3, 0, Attributes.Background, 0)]
        [InlineData(ReplicateType.Technical, 3, 0, Attributes.Weak, 1)]
        [InlineData(ReplicateType.Technical, 3, 0, Attributes.Stringent, 1)]
        [InlineData(ReplicateType.Technical, 3, 1, Attributes.Background, 0)]
        [InlineData(ReplicateType.Technical, 3, 1, Attributes.Weak, 3)]
        [InlineData(ReplicateType.Technical, 3, 1, Attributes.Stringent, 0)]
        [InlineData(ReplicateType.Technical, 3, 2, Attributes.Background, 0)]
        [InlineData(ReplicateType.Technical, 3, 2, Attributes.Weak, 1)]
        [InlineData(ReplicateType.Technical, 3, 2, Attributes.Stringent, 2)]
        [InlineData(ReplicateType.Technical, 3, 0, Attributes.Confirmed, 1)]
        [InlineData(ReplicateType.Technical, 3, 0, Attributes.Discarded, 1)]
        [InlineData(ReplicateType.Technical, 3, 1, Attributes.Confirmed, 1)]
        [InlineData(ReplicateType.Technical, 3, 1, Attributes.Discarded, 2)]
        [InlineData(ReplicateType.Technical, 3, 2, Attributes.Confirmed, 1)]
        [InlineData(ReplicateType.Technical, 3, 2, Attributes.Discarded, 2)]
        [InlineData(ReplicateType.Biological, 2, 0, Attributes.Confirmed, 2)]
        [InlineData(ReplicateType.Biological, 2, 0, Attributes.Discarded, 0)]
        [InlineData(ReplicateType.Biological, 2, 1, Attributes.Confirmed, 2)]
        [InlineData(ReplicateType.Biological, 2, 1, Attributes.Discarded, 1)]
        [InlineData(ReplicateType.Biological, 2, 2, Attributes.Confirmed, 2)]
        [InlineData(ReplicateType.Biological, 2, 2, Attributes.Discarded, 1)]
        public void AssertSetsCount(ReplicateType replicateType, int c, int sampleIndex, Attributes attribute, int expectedCount)
        {
            // Arrange
            var samples = GetSamples("", out PPeak peak);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, new Config(replicateType, 1e-4, 1e-8, 1e-4, c, 1F, MultipleIntersections.UseLowestPValue));
            var qres = Helpers<PPeak>.Get(samples[sampleIndex], _chr, _strand, attribute);

            // Assert
            Assert.True(qres.Count() == expectedCount);
        }
    }
}
