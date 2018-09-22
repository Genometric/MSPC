// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Replicates
{
    public class BioPValue
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
            var sA = new Bed<Peak>();
            var r11 = new Peak(left: 10, right: 20, value: 1e-4, name: "r11");
            sA.Add(r11, _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 12, value: 1e-4, name: "r21"), _chr, _strand);
            sB.Add(new Peak(left: 18, right: 25, value: 1e-4, name: "r22"), _chr, _strand);

            var mspc = new Mspc<Peak>(new PeakConstructor());
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-3, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Discarded).Count() == 1);
            Assert.False(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).Any());
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Discarded).ToList()[0].Source.CompareTo(r11) == 0);
        }

        /// <summary>
        /// This test asserts if MSPC correctly confirms a weak peak, which
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
            var sA = new Bed<Peak>();
            var r11 = new Peak(left: 10, right: 26, value: 1e-4, name: "r11");
            sA.Add(r11, _chr, _strand);

            var sB = new Bed<Peak>();
            var r21 = new Peak(left: 5, right: 12, value: 1e-4, name: "r21");
            var r22 = new Peak(left: 16, right: 18, value: 1e-7, name: "r22");
            var r23 = new Peak(left: 22, right: 28, value: 1e-4, name: "r23");
            sB.Add(r21, _chr, _strand);
            sB.Add(r22, _chr, _strand);
            sB.Add(r23, _chr, _strand);

            var mspc = new Mspc<Peak>(new PeakConstructor());
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-3, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).Count() == 1);
            Assert.False(res[0].Chromosomes[_chr].Get(Attributes.Discarded).Any());
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).ToList()[0].Source.CompareTo(r11) == 0);

            Assert.True(res[1].Chromosomes[_chr].Get(Attributes.Confirmed).Count() == 1);
            Assert.True(res[1].Chromosomes[_chr].Get(Attributes.Discarded).Count() == 2);
            Assert.True(res[1].Chromosomes[_chr].Get(Attributes.Confirmed).ToList()[0].Source.CompareTo(r22) == 0);
            Assert.True(res[1].Chromosomes[_chr].Get(Attributes.Discarded).ToList()[0].Source.CompareTo(r21) == 0);
            Assert.True(res[1].Chromosomes[_chr].Get(Attributes.Discarded).ToList()[1].Source.CompareTo(r23) == 0);
        }
    }
}
