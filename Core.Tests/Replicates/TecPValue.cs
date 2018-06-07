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
    public class TecPValue
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        /// <summary>
        /// This test asserts if MSPC correctly discards a weak peak
        /// from a sample that overlaps 2 weak peaks from another sample.
        /// 
        ///                        r11
        /// Sample 1: ---------▒▒▒▒▒▒▒▒▒▒▒----------
        ///                  r21          r22
        /// Sample 2: ----▒▒▒▒▒▒▒▒-----▒▒▒▒▒▒▒▒-----
        ///
        /// 
        /// Legend: [▒▒ Weak peak], [██ Stringent peak]
        /// </summary>
        [Fact]
        public void T1()
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            var r11 = new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-4, Name = "r11", HashKey = 0 };
            sA.Add(r11, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-4, Name = "r21", HashKey = 1 }, _chr, _strand);
            sB.Add(new ChIPSeqPeak() { Left = 18, Right = 25, Value = 1e-4, Name = "r22", HashKey = 2 }, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Technical, 1e-3, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Discarded).Count() == 1);
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).Count() == 0);
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Discarded).ToList()[0].Source.CompareTo(r11) == 0);
        }

        /// <summary>
        /// This test asserts if MSPC correctly discards a weak peak, which
        /// is confirmed by at least one test, and discarded in multiple tests.
        /// 
        ///                           r11
        /// Sample 1: ---------▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒---------
        ///                  r21     r22     r23
        /// Sample 2: ----▒▒▒▒▒▒▒▒---▒▒▒---▒▒▒▒▒▒▒-------
        ///
        /// 
        /// Legend: [▒▒ Weak peak], [██ Stringent peak]
        /// </summary>
        [Fact]
        public void T2()
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            var r11 = new ChIPSeqPeak() { Left = 10, Right = 26, Value = 1e-4, Name = "r11", HashKey = 0 };
            sA.Add(r11, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            var r21 = new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-4, Name = "r21", HashKey = 1 };
            var r22 = new ChIPSeqPeak() { Left = 16, Right = 18, Value = 1e-7, Name = "r22", HashKey = 2 };
            var r23 = new ChIPSeqPeak() { Left = 22, Right = 28, Value = 1e-4, Name = "r23", HashKey = 3 };
            sB.Add(r21, _chr, _strand);
            sB.Add(r22, _chr, _strand);
            sB.Add(r23, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Technical, 1e-3, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).Count() == 0);
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Discarded).Count() == 1);
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Discarded).ToList()[0].Source.CompareTo(r11) == 0);

            Assert.True(res[1].Chromosomes[_chr].Get(Attributes.Confirmed).Count() == 1);
            Assert.True(res[1].Chromosomes[_chr].Get(Attributes.Discarded).Count() == 2);
            Assert.True(res[1].Chromosomes[_chr].Get(Attributes.Confirmed).ToList()[0].Source.CompareTo(r22) == 0);
            Assert.True(res[1].Chromosomes[_chr].Get(Attributes.Discarded).ToList()[0].Source.CompareTo(r21) == 0);
            Assert.True(res[1].Chromosomes[_chr].Get(Attributes.Discarded).ToList()[1].Source.CompareTo(r23) == 0);
        }
    }
}
