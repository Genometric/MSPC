// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.Core.Model;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.SetsAndAttributes
{
    public class Stringent
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private ReadOnlyDictionary<uint, Result<ChIPSeqPeak>> GenerateAndProcessStringentPeaks()
        {
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-9 }, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-12 }, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            return mspc.Run(config);
        }

        [Fact]
        public void AssignStringentAttribute()
        {
            // Arrange & Act
            var res = GenerateAndProcessStringentPeaks();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Get(Attributes.Stringent).Count() == 1);
        }

        [Fact]
        public void StringentPeaksShouldNotHaveWeakAttribute()
        {
            // Arrange & Act
            var res = GenerateAndProcessStringentPeaks();

            // Assert
            foreach (var s in res)
                Assert.False(s.Value.Chromosomes[_chr].Get(Attributes.Weak).Any());
        }

        [Fact]
        public void StringentPeaksShouldNotHaveBackgroundAttribute()
        {
            // Arrange & Act
            var res = GenerateAndProcessStringentPeaks();

            // Assert
            foreach (var s in res)
                Assert.False(s.Value.Chromosomes[_chr].Get(Attributes.Background).Any());
        }

        [Fact]
        public void StringentNonOverlappingPeaks()
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-9 }, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 50, Right = 60, Value = 1e-12 }, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Get(Attributes.Stringent).Count() == 1);
        }

        [Fact]
        public void ProcessedStringentPeakEqualsInput()
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            var sAP = new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-9 };
            sA.Add(sAP, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            var sBP = new ChIPSeqPeak() { Left = 50, Right = 60, Value = 1e-12 };
            sB.Add(sBP, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert

            Assert.True(
                res[0].Chromosomes[_chr].Get(Attributes.Stringent).ToList()[0].Source.Equals(sAP) &&
                res[1].Chromosomes[_chr].Get(Attributes.Stringent).ToList()[0].Source.Equals(sBP));
        }
    }
}
