// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC;
using Genometric.MSPC.Model;
using System.Linq;
using Xunit;

namespace Core.Tests.Base
{
    public class MultipleConfirmDiscard
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        /// <summary>
        /// Assuming samples are biological replicates,
        /// this test asserts if MSPC correctly discards a weak peak
        /// from a sample that overlaps 2 weak peaks from another sample.
        /// 
        ///                        r11
        /// Sample 1: ---------███████████----------
        ///                  r21          r22
        /// Sample 2: ----████████-----████████-----
        ///
        /// </summary>
        [Fact]
        public void T1()
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            var p1 = new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-4, Name = "r11" };
            sA.Add(p1, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-4, Name = "r21" }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 18, Right = 25, Value = 1e-4, Name = "r22" }, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-3, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Discarded).Count() == 1);
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).Count() == 0);
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Discarded).ToList()[0].Source.CompareTo(p1) == 0);
        }
    }
}
