// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Replicates
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
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            var r11 = new PPeak(left: 10, right: 20, value: 1e-4, name: "r11");
            sA.Add(r11, _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 12, value: 1e-4, name: "r21"), _chr, _strand);
            sB.Add(new PPeak(left: 18, right: 25, value: 1e-4, name: "r22"), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Technical, 1e-3, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Discarded) == 1);
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed) == 0);
            Assert.True(Helpers<PPeak>.Get(samples[0], _chr, _strand, Attributes.Discarded).ToList()[0].CompareTo(r11) == 0);
        }

        /// <summary>
        /// This test asserts if MSPC correctly discards a weak peak, which
        /// is confirmed by multiple tests, and discarded in at least one test.
        /// In other words, for a peak to be confirmed in technical replicates,
        /// it must pass all the tests, and do not fail any test.
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
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            var r11 = new PPeak(left: 10, right: 26, value: 1e-4, name: "r11");
            sA.Add(r11, _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            var r21 = new PPeak(left: 5, right: 12, value: 1e-4, name: "r21");
            var r22 = new PPeak(left: 16, right: 18, value: 1e-7, name: "r22");
            var r23 = new PPeak(left: 22, right: 28, value: 1e-4, name: "r23");
            sB.Add(r21, _chr, _strand);
            sB.Add(r22, _chr, _strand);
            sB.Add(r23, _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Technical, 1e-3, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed) == 1);
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Discarded) == 0);
            Assert.True(Helpers<PPeak>.Get(samples[0], _chr, _strand, Attributes.Confirmed).ToList()[0].CompareTo(r11) == 0);

            Assert.True(Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.Confirmed) == 1);
            Assert.True(Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.Discarded) == 2);
            Assert.True(Helpers<PPeak>.Get(samples[1], _chr, _strand, Attributes.Confirmed).ToList()[0].CompareTo(r22) == 0);
            Assert.True(Helpers<PPeak>.Get(samples[1], _chr, _strand, Attributes.Discarded).ToList()[0].CompareTo(r21) == 0);
            Assert.True(Helpers<PPeak>.Get(samples[1], _chr, _strand, Attributes.Discarded).ToList()[1].CompareTo(r23) == 0);
        }
    }
}
