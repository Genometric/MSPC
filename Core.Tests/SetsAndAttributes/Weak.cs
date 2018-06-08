// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC;
using Genometric.MSPC.Core.Model;
using Genometric.MSPC.Model;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Core.Tests.Base
{
    public class Weak
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private ReadOnlyDictionary<uint, Result<ChIPSeqPeak>> GenerateAndProcessWeakPeaks()
        {
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-5 }, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-6 }, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            return mspc.Run(config);
        }

        [Fact]
        public void AssignWeakAttribute()
        {
            // Arrange & Act
            var res = GenerateAndProcessWeakPeaks();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Get(Attributes.Weak).Count() == 1);
        }

        [Fact]
        public void WeakPeaksShouldNotHaveStringentAttribute()
        {
            // Arrange & Act
            var res = GenerateAndProcessWeakPeaks();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Get(Attributes.Stringent).Count() == 0);
        }

        [Fact]
        public void WeakPeaksShouldNotHaveBackgroundAttribute()
        {
            // Arrange & Act
            var res = GenerateAndProcessWeakPeaks();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Get(Attributes.Background).Count() == 0);
        }

        [Fact]
        public void WeakNonOverlappingPeaks()
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-5 }, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 50, Right = 60, Value = 1e-6 }, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes[_chr].Get(Attributes.Weak).Count() == 1);
        }

        [Fact]
        public void ProcessedWeakPeakEqualsInput()
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            var sAP = new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-5 };
            sA.Add(sAP, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            var sBP = new ChIPSeqPeak() { Left = 50, Right = 60, Value = 1e-6 };
            sB.Add(sBP, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert

            Assert.True(
                res[0].Chromosomes[_chr].Get(Attributes.Weak).ToList()[0].Source.Equals(sAP) &&
                res[1].Chromosomes[_chr].Get(Attributes.Weak).ToList()[0].Source.Equals(sBP));
        }
    }
}
