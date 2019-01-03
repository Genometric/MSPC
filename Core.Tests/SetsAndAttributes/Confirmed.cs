// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.SetsAndAttributes
{
    public class Confirmed
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private ReadOnlyDictionary<uint, Result<Peak>> CreateStringentPeaksAndConfirmThem()
        {
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 1e-9), _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 12, value: 1e-12), _chr, _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            return mspc.Run(config);
        }

        [Fact]
        public void XSqrd()
        {
            // Arrange & Act
            var results = CreateStringentPeaksAndConfirmThem();

            // Assert
            foreach (var result in results)
                Assert.True(
                    Math.Round(result.Value.Chromosomes[_chr].Get(Attributes.Confirmed).First().XSquared, 8) == 96.70857391);
        }

        [Fact]
        public void RTP()
        {
            // Arrange & Act
            var results = CreateStringentPeaksAndConfirmThem();

            // Assert
            foreach (var result in results)
                Assert.True(result.Value.Chromosomes[_chr].Get(Attributes.Confirmed).First().RTP.ToString("E5") == "4.93543E-020");
        }

        [Fact]
        public void AssignConfirmedAttribute()
        {
            // Arrange & Act
            var res = CreateStringentPeaksAndConfirmThem();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Count(Attributes.Confirmed) == 1);
        }

        [Fact]
        public void ConfirmTwoOverlappingWeakPeaks()
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 7e-5), _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 12, value: 7e-5), _chr, _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-7, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Count(Attributes.Confirmed) == 1);
        }

        [Fact]
        public void ConfirmTwoNonOverlappingWeakPeaks()
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 1e-6), _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 50, right: 60, value: 1e-6), _chr, _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-6, 1, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Count(Attributes.Confirmed) == 1);
        }

        [Fact]
        public void NoDiscardedAttributeForConfirmedPeaks()
        {
            // Arrange & Act
            var res = CreateStringentPeaksAndConfirmThem();

            // Assert
            foreach (var s in res)
                Assert.False(s.Value.Chromosomes[_chr].Get(Attributes.Discarded).Any());
        }

        [Fact]
        public void SourceOfConfirmedPeakEqualsInput()
        {
            // Arrange
            var sA = new Bed<Peak>();
            var sAP = new Peak(left: 10, right: 20, value: 1e-9);
            sA.Add(sAP, _chr, _strand);

            var sB = new Bed<Peak>();
            var sBP = new Peak(left: 5, right: 15, value: 1e-12);
            sB.Add(sBP, _chr, _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert

            Assert.True(
                res[0].Chromosomes[_chr].Get(Attributes.Confirmed).ToList()[0].Source.Equals(sAP) &&
                res[1].Chromosomes[_chr].Get(Attributes.Confirmed).ToList()[0].Source.Equals(sBP));
        }

        [Fact]
        public void ConfirmPeakWithZeroPValue()
        {
            // Arrange
            var sA = new Bed<Peak>();
            var sAP = new Peak(left: 10, right: 20, value: 0);
            sA.Add(sAP, _chr, _strand);

            var sB = new Bed<Peak>();
            var sBP = new Peak(left: 5, right: 15, value: 1e-12);
            sB.Add(sBP, _chr, _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).Any());
        }

        [Fact]
        public void ConfirmTwoPeaksWithVeryLowPValue()
        {
            // Arrange
            var sA = new Bed<Peak>();
            var sAP = new Peak(left: 10, right: 20, value: 5e-321);
            sA.Add(sAP, _chr, _strand);

            var sB = new Bed<Peak>();
            var sBP = new Peak(left: 5, right: 15, value: 5e-323);
            sB.Add(sBP, _chr, _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(
                res[0].Chromosomes[_chr].Get(Attributes.Confirmed).Any() &&
                res[1].Chromosomes[_chr].Get(Attributes.Confirmed).Any());
        }
    }
}
