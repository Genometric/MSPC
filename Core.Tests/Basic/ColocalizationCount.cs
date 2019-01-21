// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
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
        public void SingleNonOverlappingPeak(int c, int expected)
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 30, right: 40, value: 0.01), "chr1", '*');

            var mspc = new Mspc();
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
        public void TwoOverlappingPeak(int c, int expected)
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 40, value: 0.01), "chr1", '*');

            var mspc = new Mspc();
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
        public void ThreePeaksTwoOverlapping(int c, int expected)
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 12, value: 0.01), "chr1", '*');

            var sC = new Bed<Peak>();
            sC.Add(new Peak(left: 18, right: 25, value: 0.01), "chr1", '*');

            var mspc = new Mspc();
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
        public void ThreePeaksThreeOverlapping(int c, int expected)
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 18, value: 0.01), "chr1", '*');

            var sC = new Bed<Peak>();
            sC.Add(new Peak(left: 14, right: 25, value: 0.01), "chr1", '*');

            var mspc = new Mspc();
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
        public void ThreePeaksNoneOverlapping(int c, int expected)
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 8, value: 0.01), "chr1", '*');

            var sC = new Bed<Peak>();
            sC.Add(new Peak(left: 24, right: 25, value: 0.01), "chr1", '*');

            var mspc = new Mspc();
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
        public void FivePeaksThreeOverlapping()
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 18, value: 0.01), "chr1", '*');

            var sC = new Bed<Peak>();
            sC.Add(new Peak(left: 14, right: 25, value: 0.01), "chr1", '*');

            var sD = new Bed<Peak>();
            sD.Add(new Peak(left: 35, right: 40, value: 0.01), "chr1", '*');

            var sE = new Bed<Peak>();
            sE.Add(new Peak(left: 43, right: 50, value: 0.01), "chr1", '*');

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);
            mspc.AddSample(2, sC);
            mspc.AddSample(3, sD);
            mspc.AddSample(4, sE);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, 1, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(new[]
            {
                res[0].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[1].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[2].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[3].Chromosomes["chr1"].Get(Attributes.Confirmed).Count(),
                res[4].Chromosomes["chr1"].Get(Attributes.Confirmed).Count()
            }.All(x => x == 1));
        }

        [Fact]
        public void OnlyOnePeakPerSampleIsConsideredForC()
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 12, value: 0.01), "chr1", '*');
            sB.Add(new Peak(left: 14, right: 22, value: 0.01), "chr1", '*');

            var sC = new Bed<Peak>();
            sC.Add(new Peak(left: 24, right: 25, value: 0.01), "chr1", '*');

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);
            mspc.AddSample(2, sC);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, 3, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.False(res[0].Chromosomes["chr1"].Get(Attributes.Confirmed).Any());
        }

        [Fact]
        public void DoNotCountOverlapsFromSameSample()
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), "chr1", '*');

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 12, value: 0.01), "chr1", '*');

            var sC = new Bed<Peak>();
            sC.Add(new Peak(left: 18, right: 25, value: 0.01), "chr1", '*');

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);
            mspc.AddSample(2, sC);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, 5, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(res[0].Chromosomes["chr1"].Count(Attributes.Confirmed) == 0);
        }

        [Fact]
        public void IntervalsWithExactSameCoordinates()
        {
            // Arrange
            int sampleCount = 22;
            var mspc = new Mspc();
            for (uint i = 0; i < sampleCount - 2; i++)
            {
                var bed = new Bed<Peak>();
                bed.Add(new Peak(10, 20, 0.01), "chr1", '*');
                mspc.AddSample(i, bed);
            }

            var bedB = new Bed<Peak>();
            bedB.Add(new Peak(8, 12, 0.01), "chr1", '*');
            mspc.AddSample((uint)sampleCount - 2, bedB);

            var bedC = new Bed<Peak>();
            bedC.Add(new Peak(18, 25, 0.01), "chr1", '*');
            mspc.AddSample((uint)sampleCount - 1, bedC);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, sampleCount, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            for (uint i = 0; i < sampleCount; i++)
                Assert.True(res[i].Chromosomes["chr1"].Count(Attributes.Confirmed) == 1);
        }
    }
}
