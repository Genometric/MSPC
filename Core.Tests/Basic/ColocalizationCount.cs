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
    public class ColocalizationCount
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '.';

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        public void SingleNonOverlappingPeak(int c, int expected)
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 0.01), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 30, right: 40, value: 0.01), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(new[]
            {
                Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.Confirmed)
            }.All(x => x == expected));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        public void TwoOverlappingPeak(int c, int expected)
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 0.01), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 40, value: 0.01), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(new[]
            {
                Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.Confirmed)
            }.All(x => x == expected));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void ThreePeaksTwoOverlapping(int c, int expected)
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 0.01), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 12, value: 0.01), _chr, _strand);

            var sC = new Bed<PPeak>() { FileHashKey = 2 };
            sC.Add(new PPeak(left: 18, right: 25, value: 0.01), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB, sC };
            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(new[]
            {
                Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[2], _chr, _strand, Attributes.Confirmed)
            }.All(x => x == expected));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void ThreePeaksThreeOverlapping(int c, int expected)
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 0.01), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 18, value: 0.01), _chr, _strand);

            var sC = new Bed<PPeak>() { FileHashKey = 2 };
            sC.Add(new PPeak(left: 14, right: 25, value: 0.01), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB, sC };
            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(new[]
            {
                Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[2], _chr, _strand, Attributes.Confirmed)
            }.All(x => x == expected));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        public void ThreePeaksNoneOverlapping(int c, int expected)
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 0.01), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 8, value: 0.01), _chr, _strand);

            var sC = new Bed<PPeak>() { FileHashKey = 2 };
            sC.Add(new PPeak(left: 24, right: 25, value: 0.01), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB, sC };
            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(new[]
            {
                Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[2], _chr, _strand, Attributes.Confirmed)
            }.All(x => x == expected));
        }

        [Fact]
        public void FivePeaksThreeOverlapping()
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 0.01), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 18, value: 0.01), _chr, _strand);

            var sC = new Bed<PPeak>() { FileHashKey = 2 };
            sC.Add(new PPeak(left: 14, right: 25, value: 0.01), _chr, _strand);

            var sD = new Bed<PPeak>() { FileHashKey = 3 };
            sD.Add(new PPeak(left: 35, right: 40, value: 0.01), _chr, _strand);

            var sE = new Bed<PPeak>() { FileHashKey = 4 };
            sE.Add(new PPeak(left: 43, right: 50, value: 0.01), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB, sC, sD, sE };
            var config = new Config(ReplicateType.Biological, 1, 1, 1, 1, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(new[]
            {
                Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[2], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[3], _chr, _strand, Attributes.Confirmed),
                Helpers<PPeak>.Count(samples[4], _chr, _strand, Attributes.Confirmed)
            }.All(x => x == 1));
        }

        [Fact]
        public void OnlyOnePeakPerSampleIsConsideredForC()
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 0.01), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 12, value: 0.01), _chr, _strand);
            sB.Add(new PPeak(left: 14, right: 22, value: 0.01), _chr, _strand);

            var sC = new Bed<PPeak>() { FileHashKey = 2 };
            sC.Add(new PPeak(left: 24, right: 25, value: 0.01), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB, sC };
            var config = new Config(ReplicateType.Biological, 1, 1, 1, 3, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed) == 0);
        }
    }
}
