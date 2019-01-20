// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.SetsAndAttributes
{
    public class BackgroundPeaks
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private List<Bed<PPeak>> GenerateAndProcessBackgroundPeaks()
        {
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 1e-2), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 12, value: 1e-4), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);
            return samples;
        }

        [Fact]
        public void AssignBackgroundAttribute()
        {
            // Arrange & Act
            var res = GenerateAndProcessBackgroundPeaks();

            // Assert
            foreach (var s in res)
                Assert.True(Helpers<PPeak>.Count(s, _chr, _strand, Attributes.Background) == 1);
        }

        [Fact]
        public void BackgroundPeaksShouldNotHaveAnyOtherAttributes()
        {
            // Arrange & Act
            var res = GenerateAndProcessBackgroundPeaks();

            // Assert
            foreach (var s in res)
                Assert.True(
                    Helpers<PPeak>.Count(s, _chr, _strand, Attributes.Weak) == 0 &&
                    Helpers<PPeak>.Count(s, _chr, _strand, Attributes.Stringent) == 0 &&
                    Helpers<PPeak>.Count(s, _chr, _strand, Attributes.Confirmed) == 0 &&
                    Helpers<PPeak>.Count(s, _chr, _strand, Attributes.Discarded) == 0 &&
                    Helpers<PPeak>.Count(s, _chr, _strand, Attributes.TruePositive) == 0 &&
                    Helpers<PPeak>.Count(s, _chr, _strand, Attributes.FalsePositive) == 0);
        }

        [Fact]
        public void NonOverlappingBackgroundPeaks()
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 1e-2), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 50, right: 60, value: 1e-4), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            foreach (var s in samples)
                Assert.True(Helpers<PPeak>.Count(s, _chr, _strand, Attributes.Background) == 1);
        }

        [Fact]
        public void BackgroundOverlappingNonBackground()
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 1e-2), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 50, right: 60, value: 1e-8), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(
                Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Background) == 1 &&
                Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.Background) == 0);
        }

        [Fact]
        public void ProcessedBackgroundPeakEqualsInput()
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            var sAP = new PPeak(left: 10, right: 20, value: 1e-2);
            sA.Add(sAP, _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            var sBP = new PPeak(left: 50, right: 60, value: 1e-4);
            sB.Add(sBP, _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(
                Helpers<PPeak>.Get(samples[0], _chr, _strand, Attributes.Background).ToList()[0].Equals(sAP) &&
                Helpers<PPeak>.Get(samples[1], _chr, _strand, Attributes.Background).ToList()[0].Equals(sBP));
        }
    }
}
