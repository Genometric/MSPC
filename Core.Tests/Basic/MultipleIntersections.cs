﻿using Genometric.GeUtilities.Intervals.Model;
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
        private readonly char _strand = '.';

        //                            r11
        // Sample 1: ---------█████████████████████------
        //                  r21      r22        r23
        // Sample 2: ----████████---█████-----███████----
        //
        private readonly static Peak r11 = new Peak(left: 10, right: 30, name: "r11", value: 1e-8);
        private readonly static Peak r21 = new Peak(left: 05, right: 12, name: "r21", value: 1e-5);
        private readonly static Peak r22 = new Peak(left: 16, right: 20, name: "r22", value: 1e-6);
        private readonly static Peak r23 = new Peak(left: 26, right: 32, name: "r23", value: 1e-9);

        private Dictionary<uint, Result<Peak>> InitializeAndRun(MultipleIntersections miChoice, bool trackSupportingPeaks = false)
        {
            // Arrange
            var sA = new Bed<Peak>();
            sA.Add(r11, _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(r21, _chr, _strand);
            sB.Add(r22, _chr, _strand);
            sB.Add(r23, _chr, _strand);

            var mspc = new Mspc(trackSupportingPeaks);
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            // Act
            return mspc.Run(new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, miChoice));
        }

        [Fact]
        public void UseMostStringentPeak()
        {
            // Arrange & Act
            var res = InitializeAndRun(MultipleIntersections.UseLowestPValue, true);

            // Assert
            Assert.True(res[0].Chromosomes[_chr][_strand].Get(Attributes.Confirmed).ToList()[0].SupportingPeaks[0].Source.CompareTo(r23) == 0);
        }

        [Fact]
        public void UseLeastStringentPeak()
        {
            // Arrange & Act
            var res = InitializeAndRun(MultipleIntersections.UseHighestPValue, true);

            // Assert
            Assert.True(res[0].Chromosomes[_chr][_strand].Get(Attributes.Confirmed).ToList()[0].SupportingPeaks[0].Source.CompareTo(r21) == 0);
        }
    }
}
