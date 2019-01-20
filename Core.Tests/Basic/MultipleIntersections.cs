// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class ChooseOnePeakFromManyOverlappingPeaks
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        //                            r11
        // Sample 1: ---------█████████████████████------
        //                  r21      r22        r23
        // Sample 2: ----████████---█████-----███████----
        //
        private readonly static PPeak r11 = new PPeak(left: 10, right: 30, name: "r11", value: 1e-8);
        private readonly static PPeak r21 = new PPeak(left: 05, right: 12, name: "r21", value: 1e-5);
        private readonly static PPeak r22 = new PPeak(left: 16, right: 20, name: "r22", value: 1e-6);
        private readonly static PPeak r23 = new PPeak(left: 26, right: 32, name: "r23", value: 1e-9);

        private List<Bed<PPeak>> InitializeAndRun(MultipleIntersections miChoice, bool trackSupportingPeaks = false)
        {
            // Arrange
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(r11, _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(r21, _chr, _strand);
            sB.Add(r22, _chr, _strand);
            sB.Add(r23, _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };

            // Act
            var mspc = new Mspc(trackSupportingPeaks);
            mspc.Run(samples, new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, miChoice));
            return samples;
        }

        /*
        [Fact]
        public void UseMostStringentPeak()
        {
            // Arrange & Act
            var res = InitializeAndRun(MultipleIntersections.UseLowestPValue, true);

            // Assert
            Assert.True(Helpers<PPeak>.Get(res[0], _chr, _strand, Attributes.Confirmed).ToList()[0].SupportingPeaks[0].CompareTo(r23) == 0);
        }

        [Fact]
        public void UseLeastStringentPeak()
        {
            // Arrange & Act
            var res = InitializeAndRun(MultipleIntersections.UseHighestPValue, true);

            // Assert
            Assert.True(Helpers<PPeak>.Get(res[0], _chr, _strand, Attributes.Confirmed).ToList()[0].SupportingPeaks[0].CompareTo(r21) == 0);
        }*/
    }
}
