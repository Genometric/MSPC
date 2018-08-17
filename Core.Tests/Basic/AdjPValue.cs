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
    public class AdjPValue
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        [Fact]
        public void ComputeAdjustedPValue()
        {
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 0.01, Name = "r11", HashKey = 1 }, _chr, _strand);
            sA.Add(new ChIPSeqPeak() { Left = 100, Right = 200, Value = 0.001, Name = "r12", HashKey = 2 }, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 0.01, Name = "21", HashKey = 5 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 50, Right = 120, Value = 0.001, Name = "22", HashKey = 6 }, _chr, _strand);
            
            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 2, 0.05F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.TruePositive).First().AdjPValue == 0.01);
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.TruePositive).Last().AdjPValue == 0.002);
        }
    }
}
