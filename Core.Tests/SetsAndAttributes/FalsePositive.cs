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
    public class FalsePositive
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private ReadOnlyDictionary<uint, Result<ChIPSeqPeak>> GenerateAndProcessBackgroundPeaks()
        {
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-6 }, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-8 }, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 5e-7F, MultipleIntersections.UseLowestPValue);

            // Act
            return mspc.Run(config);
        }

        [Fact]
        public void AssignFalsePositiveAttributeWhenThereIsOnlyOnePeak()
        {
            // Arrange & Act
            var res = GenerateAndProcessBackgroundPeaks();

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.FalsePositive).Count() == 1);
        }

        [Fact]
        public void AssignFalsePositive()
        {
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-6, Name = "r11", HashKey = 1 }, _chr, _strand);
            sA.Add(new ChIPSeqPeak() { Left = 100, Right = 200, Value = 1e-8, Name = "r12", HashKey = 2 }, _chr, _strand);
            sA.Add(new ChIPSeqPeak() { Left = 1000, Right = 2000, Value = 1e-10, Name = "r13", HashKey = 3 }, _chr, _strand);
            sA.Add(new ChIPSeqPeak() { Left = 10000, Right = 20000, Value = 1e-12, Name = "r14", HashKey = 4 }, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-7, Name = "21", HashKey = 5 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 50, Right = 120, Value = 1e-9, Name = "22", HashKey = 6 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 500, Right = 1200, Value = 1e-11, Name = "23", HashKey = 7 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 5000, Right = 12000, Value = 1e-13, Name = "24", HashKey = 8 }, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-6, 1e-6, 2, 5e-10F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            foreach (var sample in res)
                Assert.True(sample.Value.Chromosomes[_chr].Get(Attributes.FalsePositive).Count() == 2);
        }

        [Fact]
        public void AssertCorrectPeakIsTaggedAsFalsePositive()
        {
            var sA = new BED<ChIPSeqPeak>();
            var r11 = new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-6, Name = "r11", HashKey = 1 };
            sA.Add(r11, _chr, _strand);
            sA.Add(new ChIPSeqPeak() { Left = 100, Right = 200, Value = 1e-8, Name = "r12", HashKey = 2 }, _chr, _strand);
            sA.Add(new ChIPSeqPeak() { Left = 1000, Right = 2000, Value = 1e-10, Name = "r13", HashKey = 3 }, _chr, _strand);
            sA.Add(new ChIPSeqPeak() { Left = 10000, Right = 20000, Value = 1e-12, Name = "r14", HashKey = 4 }, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-7, Name = "21", HashKey = 5 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 50, Right = 120, Value = 1e-9, Name = "22", HashKey = 6 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 500, Right = 1200, Value = 1e-11, Name = "23", HashKey = 7 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 5000, Right = 12000, Value = 1e-13, Name = "24", HashKey = 8 }, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-6, 1e-6, 2, 5e-10F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.FalsePositive).First().Source.Equals(r11));
        }

        [Fact]
        public void CorrectlyIdentifyAllPeaksAsFalsePositive()
        {
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-6, Name = "r11", HashKey = 1 }, _chr, _strand);
            sA.Add(new ChIPSeqPeak() { Left = 100, Right = 200, Value = 1e-8, Name = "r12", HashKey = 2 }, _chr, _strand);
            sA.Add(new ChIPSeqPeak() { Left = 1000, Right = 2000, Value = 1e-10, Name = "r13", HashKey = 3 }, _chr, _strand);
            sA.Add(new ChIPSeqPeak() { Left = 10000, Right = 20000, Value = 1e-12, Name = "r14", HashKey = 4 }, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-7, Name = "21", HashKey = 5 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 50, Right = 120, Value = 1e-9, Name = "22", HashKey = 6 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 500, Right = 1200, Value = 1e-11, Name = "23", HashKey = 7 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 5000, Right = 12000, Value = 1e-13, Name = "24", HashKey = 8 }, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-6, 1e-6, 2, 5e-14F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            foreach (var sample in res)
                Assert.True(sample.Value.Chromosomes[_chr].Get(Attributes.FalsePositive).Count() == 4);
        }
    }
}
