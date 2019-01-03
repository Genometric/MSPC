// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class TConsensusPeaks
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private ReadOnlyDictionary<string, List<ProcessedPeak<Peak>>> GetSampleConsensusPeaks(float alpha= 1e-15F)
        {
            ///                 r11                 r12
            /// Sample 0: ----▓▓▓▓▓▓--------------▓▓▓▓▓▓-----------------------
            ///               r21         r22             r23    r24
            /// Sample 1: --▓▓▓▓▓▓--------▓▓▓-------------░░░---▓▓▓▓▓----------
            ///                 r31                 r32             r33
            /// Sample 2: -▓▓▓▓▓▓▓▓▓▓▓▓-----------▓▓▓▓▓▓----------▓▓▓▓▓▓▓▓-----
            ///
            var s0 = new Bed<Peak>();
            s0.Add(new Peak(10, 20, 1.23E-8), _chr, _strand);
            s0.Add(new Peak(50, 60, 1.56E-80), _chr, _strand);

            var s1 = new Bed<Peak>();
            s1.Add(new Peak(6, 16, 4.56E-8), _chr, _strand);
            s1.Add(new Peak(36, 40, 1.31E-23), _chr, _strand);
            s1.Add(new Peak(64, 68, 1.02E-2), _chr, _strand);
            s1.Add(new Peak(70, 80, 8.76E-9), _chr, _strand);

            var s2 = new Bed<Peak>();
            s2.Add(new Peak(2, 26, 7.89E-10), _chr, _strand);
            s2.Add(new Peak(50, 60, 9.9E-200), _chr, _strand);
            s2.Add(new Peak(76, 90, 1.1E-8), _chr, _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, s0);
            mspc.AddSample(1, s1);
            mspc.AddSample(2, s2);

            mspc.Run(new Config(ReplicateType.Biological, 1E-4, 1E-6, 1E-6, 1, alpha, MultipleIntersections.UseLowestPValue));

            return mspc.GetConsensusPeaks();
        }

        [Theory]
        [InlineData(10, 20, 5, 15, 5, 20)]
        [InlineData(10, 20, 12, 18, 10, 20)]
        [InlineData(10, 20, 5, 25, 5, 25)]
        [InlineData(10, 20, 19, 25, 10, 25)]
        [InlineData(19, 25, 10, 20, 10, 25)]
        public void MergeTwoConsensusPeaks(int xLeft, int xRight, int yLeft, int yRight, int cLeft, int cRight)
        {
            // Arrange
            var mspc = new Mspc();
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: xLeft, right: xRight, value: 0.01), _chr, _strand);
            mspc.AddSample(0, sA);

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: yLeft, right: yRight, value: 0.01), _chr, _strand);
            mspc.AddSample(1, sB);

            // Act
            mspc.Run(new Config(ReplicateType.Biological, 1, 1, 1, 1, 1F, MultipleIntersections.UseLowestPValue));
            var cp = mspc.GetConsensusPeaks()[_chr].First();

            // Assert
            Assert.True(cp.Source.Left == cLeft && cp.Source.Right == cRight);
        }

        [Fact]
        public void CoordinatesOfConsensusPeaks()
        {
            // Arrange and Act
            var cPeaks = GetSampleConsensusPeaks();

            // Assert
            foreach (var peak in cPeaks[_chr])
                Assert.True(
                    (peak.Source.Left == 2 && peak.Source.Right == 26) ||
                    (peak.Source.Left == 36 && peak.Source.Right == 40) ||
                    (peak.Source.Left == 50 && peak.Source.Right == 60) ||
                    (peak.Source.Left == 70 && peak.Source.Right == 90));
        }

        [Fact]
        public void XSqrd()
        {
            // Arrange and Act
            var cPeaks = GetSampleConsensusPeaks();
            var cPeak = cPeaks[_chr].First(x => x.Source.Left == 2 && x.Source.Right == 26);

            // Assert
            Assert.True(Math.Round(cPeak.XSquared, 6) == 112.154559);
        }

        [Fact]
        public void RTP()
        {
            // Arrange and Act
            var cPeaks = GetSampleConsensusPeaks();
            var cPeak = cPeaks[_chr].First(x => x.Source.Left == 2 && x.Source.Right == 26);

            // Assert
            Assert.True(cPeak.RTP.ToString("E5") == "7.21069E-022");
        }

        [Fact]
        public void AdjPValue()
        {
            // Arrange & Act
            var cPeaks = GetSampleConsensusPeaks()[_chr];
            var r1 = cPeaks.First(x => x.Source.Left == 2);
            var r2 = cPeaks.First(x => x.Source.Left == 36);
            var r3 = cPeaks.First(x => x.Source.Left == 50);
            var r4 = cPeaks.First(x => x.Source.Left == 70);

            // Assert
            Assert.Equal("9.614E-022", r1.AdjPValue.ToString("E3"));
            Assert.Equal("2.620E-023", r2.AdjPValue.ToString("E3"));
            Assert.Equal("3.972E-276", r3.AdjPValue.ToString("E3"));
            Assert.Equal("3.650E-015", r4.AdjPValue.ToString("E3"));
        }

        [Fact]
        public void FalseDiscovery()
        {
            // Arrange & Act
            var cPeaks = GetSampleConsensusPeaks()[_chr];
            var r1 = cPeaks.First(x => x.Source.Left == 2);
            var r2 = cPeaks.First(x => x.Source.Left == 36);
            var r3 = cPeaks.First(x => x.Source.Left == 50);
            var r4 = cPeaks.First(x => x.Source.Left == 70);

            // Assert
            Assert.Contains(Attributes.TruePositive, r1.Classification);
            Assert.Contains(Attributes.TruePositive, r2.Classification);
            Assert.Contains(Attributes.TruePositive, r3.Classification);
            Assert.Contains(Attributes.FalsePositive, r4.Classification);
        }
    }
}
