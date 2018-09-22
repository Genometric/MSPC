// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.SetsAndAttributes
{
    public class Discarded
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private ReadOnlyDictionary<uint, Result<Peak>> CreateStringentPeaksAndDiscardThem()
        {
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 1e-9), _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 8, value: 1e-12), _chr, _strand);

            var mspc = new Mspc<Peak>(new PeakConstructor());
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            return mspc.Run(config);
        }

        [Fact]
        public void AssignDiscardedAttribute()
        {
            // Arrange & Act
            var res = CreateStringentPeaksAndDiscardThem();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Get(Attributes.Discarded).Count() == 1);
        }

        [Fact]
        public void DiscardTwoOverlappingWeakPeaks()
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 7e-5), _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 12, value: 7e-5), _chr, _strand);

            var mspc = new Mspc<Peak>(new PeakConstructor());
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Get(Attributes.Discarded).Count() == 1);
        }

        [Fact]
        public void DiscardTwoNonOverlappingWeakPeaks()
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 1e-6), _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 50, right: 60, value: 1e-6), _chr, _strand);

            var mspc = new Mspc<Peak>(new PeakConstructor());
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-7, 1, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Get(Attributes.Discarded).Count() == 1);
        }

        [Fact]
        public void NoConfirmedAttributeForDiscardedPeaks()
        {
            // Arrange & Act
            var res = CreateStringentPeaksAndDiscardThem();

            // Assert
            foreach (var s in res)
                Assert.False(s.Value.Chromosomes[_chr].Get(Attributes.Confirmed).Any());
        }

        [Fact]
        public void SourceOfDiscardedPeakEqualsInput()
        {
            // Arrange
            var sA = new Bed<Peak>();
            var sAP = new Peak(left: 10, right: 20, value: 1e-9);
            sA.Add(sAP, _chr, _strand);

            var sB = new Bed<Peak>();
            var sBP = new Peak(left: 5, right: 8, value: 1e-12);
            sB.Add(sBP, _chr, _strand);

            var mspc = new Mspc<Peak>(new PeakConstructor());
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert

            Assert.True(
                res[0].Chromosomes[_chr].Get(Attributes.Discarded).ToList()[0].Source.Equals(sAP) &&
                res[1].Chromosomes[_chr].Get(Attributes.Discarded).ToList()[0].Source.Equals(sBP));
        }
    }
}
