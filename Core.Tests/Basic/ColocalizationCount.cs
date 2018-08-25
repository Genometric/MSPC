// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.Core.Model;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class ColocalizationCount
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        public void SingleNonOverlappingPeak(byte c, byte expected)
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20 }, "chr1", '*');

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 30, Right = 40 }, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(new[]
            {
                res[0].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[1].Chromosomes["chr1"].Get(Attributes.Confirmed).Count()
            }.All(x => x == expected));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        public void TwoOverlappingPeak(byte c, byte expected)
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20 }, "chr1", '*');

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 40 }, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(new[] 
            {
                res[0].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[1].Chromosomes["chr1"].Get(Attributes.Confirmed).Count()
            }.All(x => x == expected));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void ThreePeaksTwoOverlapping(byte c, byte expected)
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20 }, "chr1", '*');

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12 }, "chr1", '*');

            var sC = new BED<ChIPSeqPeak>();
            sC.Add(new ChIPSeqPeak() { Left = 18, Right = 25 }, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);
            mspc.AddSample(2, sC);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(new[] 
            {
                res[0].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[1].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[2].Chromosomes["chr1"].Get(Attributes.Confirmed).Count()
            }.All(x => x == expected));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void ThreePeaksThreeOverlapping(byte c, byte expected)
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20 }, "chr1", '*');

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 18 }, "chr1", '*');

            var sC = new BED<ChIPSeqPeak>();
            sC.Add(new ChIPSeqPeak() { Left = 14, Right = 25 }, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);
            mspc.AddSample(2, sC);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(new[]
            {
                res[0].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[1].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[2].Chromosomes["chr1"].Get(Attributes.Confirmed).Count()
            }.All(x => x == expected));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        public void ThreePeaksNoneOverlapping(byte c, byte expected)
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20 }, "chr1", '*');

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 8 }, "chr1", '*');

            var sC = new BED<ChIPSeqPeak>();
            sC.Add(new ChIPSeqPeak() { Left = 24, Right = 25 }, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);
            mspc.AddSample(2, sC);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(new[]
            {
                res[0].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[1].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[2].Chromosomes["chr1"].Get(Attributes.Confirmed).Count()
            }.All(x => x == expected));
        }

        [Fact]
        public void OnlyOnePeakPerSampleIsConsideredForC()
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20 }, "chr1", '*');

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12 }, "chr1", '*');
            sB.Add(new ChIPSeqPeak() { Left = 14, Right = 22 }, "chr1", '*');

            var sC = new BED<ChIPSeqPeak>();
            sC.Add(new ChIPSeqPeak() { Left = 24, Right = 25 }, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);
            mspc.AddSample(2, sC);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, 3, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(res[0].Chromosomes["chr1"].Get(Attributes.Confirmed).Any());
        }
    }
}
