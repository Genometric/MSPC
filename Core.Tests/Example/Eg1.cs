// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Example
{
    public class Eg1
    {
        //                 r11                r12
        // Sample 1: --███████████-------████████████----------------------------
        //                           r21             r22        r23
        // Sample 2: ---------████████████████----████████---████████------------
        //           r31       r32                                       r33
        // Sample 3: ████---██████████---------------------------------████████--

        private readonly static ChIPSeqPeak r11 = new ChIPSeqPeak() { Left = 3, Right = 13, Name = "r11", Value = 1e-6, HashKey = 1 };
        private readonly static ChIPSeqPeak r12 = new ChIPSeqPeak() { Left = 21, Right = 32, Name = "r12", Value = 1e-12, HashKey = 2 };
        private readonly static ChIPSeqPeak r21 = new ChIPSeqPeak() { Left = 10, Right = 25, Name = "r21", Value = 1e-7, HashKey = 3 };
        private readonly static ChIPSeqPeak r22 = new ChIPSeqPeak() { Left = 30, Right = 37, Name = "r22", Value = 1e-5, HashKey = 4 };
        private readonly static ChIPSeqPeak r23 = new ChIPSeqPeak() { Left = 41, Right = 48, Name = "r23", Value = 1e-6, HashKey = 5 };
        private readonly static ChIPSeqPeak r31 = new ChIPSeqPeak() { Left = 0, Right = 4, Name = "r31", Value = 1e-6, HashKey = 6 };
        private readonly static ChIPSeqPeak r32 = new ChIPSeqPeak() { Left = 8, Right = 17, Name = "r32", Value = 1e-12, HashKey = 7 };
        private readonly static ChIPSeqPeak r33 = new ChIPSeqPeak() { Left = 51, Right = 58, Name = "r33", Value = 1e-18, HashKey = 8 };

        public static IEnumerable<object[]> ExpectedAttributes =>
            new List<object[]>
            {
                /// Discard a stringent peak that does not satisfy C.
                /// r_12 overlaps with two peaks, but both of the peaks belong to 
                /// sample_2, hence only one of the peaks is _considereded_. Therefore, 
                /// r_12 is overlapping with only one peak, hence does not satisfy C. 
                new object[] { ReplicateType.Technical, 3, 0, r11, Attributes.Weak, Attributes.Confirmed },

                /// Discard a stringent peak that does not satisfy C.
                /// r_12 overlaps with two peaks, but both of the peaks belong to 
                /// sample_2, hence only one of the peaks is _considereded_. Therefore, 
                /// r_12 is overlapping with only one peak, hence does not satisfy C. 
                new object[] { ReplicateType.Technical, 3, 0, r12, Attributes.Stringent, Attributes.Discarded },

                new object[] { ReplicateType.Technical, 3, 1, r21, Attributes.Weak, Attributes.Confirmed },
                new object[] { ReplicateType.Technical, 3, 1, r22, Attributes.Weak, Attributes.Discarded },
                new object[] { ReplicateType.Technical, 3, 1, r23, Attributes.Weak, Attributes.Discarded },
                new object[] { ReplicateType.Technical, 3, 2, r31, Attributes.Weak, Attributes.Discarded },
                new object[] { ReplicateType.Technical, 3, 2, r32, Attributes.Stringent, Attributes.Confirmed },
                new object[] { ReplicateType.Technical, 3, 2, r33, Attributes.Stringent, Attributes.Discarded },

                new object[] { ReplicateType.Biological, 2, 0, r11, Attributes.Weak, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 0, r12, Attributes.Stringent, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 1, r21, Attributes.Weak, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 1, r22, Attributes.Weak, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 1, r23, Attributes.Weak, Attributes.Discarded },
                new object[] { ReplicateType.Biological, 2, 2, r31, Attributes.Weak, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 2, r32, Attributes.Stringent, Attributes.Confirmed },
                new object[] { ReplicateType.Biological, 2, 2, r33, Attributes.Stringent, Attributes.Discarded },
            };

        private MSPC<ChIPSeqPeak> InitializeMSPC()
        {
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(r11, "chr1", '*');
            sA.Add(r12, "chr1", '*');

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(r21, "chr1", '*');
            sB.Add(r22, "chr1", '*');
            sB.Add(r23, "chr1", '*');

            var sC = new BED<ChIPSeqPeak>();
            sC.Add(r31, "chr1", '*');
            sC.Add(r32, "chr1", '*');
            sC.Add(r33, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);
            mspc.AddSample(2, sC);
            return mspc;
        }

        [Theory]
        [MemberData(nameof(ExpectedAttributes))]
        public void AssertAttributeAssignment(
            ReplicateType replicateType, byte c, uint sampleIndex,
            ChIPSeqPeak peak, Attributes initial, Attributes processed)
        {
            // Arrange
            var mspc = InitializeMSPC();

            // Act
            var res = mspc.Run(new Config(replicateType, 1e-4, 1e-8, 1e-4, c, 1F, MultipleIntersections.UseLowestPValue));
            var qres = res[sampleIndex].Chromosomes["chr1"].Get(processed).FirstOrDefault(x => x.Source.CompareTo(peak) == 0);

            // Assert
            Assert.NotNull(qres);
            Assert.Contains(initial, qres.Classification);
            Assert.Contains(processed, qres.Classification);
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
        public void AssertSetsCount(ReplicateType replicateType, byte c, uint sampleIndex, Attributes attribute, int expectedCount)
        {
            // Arrange
            var mspc = InitializeMSPC();

            // Act
            var res = mspc.Run(new Config(replicateType, 1e-4, 1e-8, 1e-4, c, 1F, MultipleIntersections.UseLowestPValue));
            var qres = res[sampleIndex].Chromosomes["chr1"].Get(attribute);

            // Assert
            Assert.True(qres.Count() == expectedCount);
        }
    }
}
