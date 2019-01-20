// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.SetsAndAttributes
{
    public class Confirmed
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private List<Bed<PPeak>> CreateStringentPeaksAndConfirmThem()
        {
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 1e-9), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 12, value: 1e-12), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);
            return samples;
        }

        [Fact]
        public void XSqrd()
        {
            // Arrange & Act
            var samples = CreateStringentPeaksAndConfirmThem();

            // Assert
            foreach (var sample in samples)
                Assert.True(
                    Math.Round(Helpers<PPeak>.Get(sample, _chr, _strand, Attributes.Confirmed).First().XSquared, 8) == 96.70857391);
        }

        [Fact]
        public void RTP()
        {
            // Arrange & Act
            var samples = CreateStringentPeaksAndConfirmThem();

            // Assert
            foreach (var sample in samples)
                Assert.True(Helpers<PPeak>.Get(sample, _chr, _strand, Attributes.Confirmed).First().RTP.ToString("E5") == "4.93543E-020");
        }

        [Fact]
        public void AssignConfirmedAttribute()
        {
            // Arrange & Act
            var samples = CreateStringentPeaksAndConfirmThem();

            // Assert
            foreach (var sample in samples)
                Assert.True(Helpers<PPeak>.Count(sample, _chr, _strand, Attributes.Confirmed) == 1);
        }

        [Fact]
        public void ConfirmTwoOverlappingWeakPeaks()
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 7e-5), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 12, value: 7e-5), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-7, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            foreach (var sample in samples)
                Assert.True(Helpers<PPeak>.Count(sample, _chr, _strand, Attributes.Confirmed) == 1);
        }

        [Fact]
        public void ConfirmTwoNonOverlappingWeakPeaks()
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 1e-6), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 50, right: 60, value: 1e-6), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-6, 1, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            foreach (var sample in samples)
                Assert.True(Helpers<PPeak>.Count(sample, _chr, _strand, Attributes.Confirmed) == 1);
        }

        [Fact]
        public void NoDiscardedAttributeForConfirmedPeaks()
        {
            // Arrange & Act
            var samples = CreateStringentPeaksAndConfirmThem();

            // Assert
            foreach (var sample in samples)
                Assert.True(Helpers<PPeak>.Count(sample, _chr, _strand, Attributes.Discarded) == 0);
        }

        [Fact]
        public void SourceOfConfirmedPeakEqualsInput()
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            var sAP = new PPeak(left: 10, right: 20, value: 1e-9);
            sA.Add(sAP, _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            var sBP = new PPeak(left: 5, right: 15, value: 1e-12);
            sB.Add(sBP, _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert

            Assert.True(
                Helpers<PPeak>.Get(samples[0], _chr, _strand, Attributes.Confirmed).ToList()[0].Equals(sAP) &&
                Helpers<PPeak>.Get(samples[1], _chr, _strand, Attributes.Confirmed).ToList()[0].Equals(sBP));
        }

        [Fact]
        public void ConfirmPeakWithZeroPValue()
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            var sAP = new PPeak(left: 10, right: 20, value: 0);
            sA.Add(sAP, _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            var sBP = new PPeak(left: 5, right: 15, value: 1e-12);
            sB.Add(sBP, _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed) > 0);
        }

        [Fact]
        public void ConfirmTwoPeaksWithVeryLowPValue()
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            var sAP = new PPeak(left: 10, right: 20, value: 5e-321);
            sA.Add(sAP, _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            var sBP = new PPeak(left: 5, right: 15, value: 5e-323);
            sB.Add(sBP, _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(
                Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed) > 0 &&
                Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.Confirmed) > 0);
        }
    }
}
