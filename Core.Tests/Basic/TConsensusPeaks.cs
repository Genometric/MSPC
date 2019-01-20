// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class TConsensusPeaks
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';
        private readonly string[] _chrs = new string[] { "chr1", "chr2", "chr3", "chr9", "chrx" };

        private Dictionary<string, List<PPeak>> GetSampleConsensusPeaks(float alpha = 1e-15F)
        {
            ///                 r11                 r12
            /// Sample 0: ----▓▓▓▓▓▓--------------▓▓▓▓▓▓-----------------------
            ///               r21         r22             r23    r24
            /// Sample 1: --▓▓▓▓▓▓--------▓▓▓-------------░░░---▓▓▓▓▓----------
            ///                 r31                 r32             r33
            /// Sample 2: -▓▓▓▓▓▓▓▓▓▓▓▓-----------▓▓▓▓▓▓----------▓▓▓▓▓▓▓▓-----
            ///
            var s0 = new Bed<PPeak>() { FileHashKey = 0 };
            s0.Add(new PPeak(10, 20, 1.23E-8), _chrs[0], _strand);
            s0.Add(new PPeak(50, 60, 1.56E-80), _chrs[2], _strand);

            var s1 = new Bed<PPeak>() { FileHashKey = 1 };
            s1.Add(new PPeak(6, 16, 4.56E-8), _chrs[0], _strand);
            s1.Add(new PPeak(36, 40, 1.31E-23), _chrs[1], _strand);
            s1.Add(new PPeak(64, 68, 1.02E-2), _chrs[3], _strand);
            s1.Add(new PPeak(70, 80, 8.76E-9), _chrs[4], _strand);

            var s2 = new Bed<PPeak>() { FileHashKey = 2 };
            s2.Add(new PPeak(2, 26, 7.89E-10), _chrs[0], _strand);
            s2.Add(new PPeak(50, 60, 9.9E-200), _chrs[2], _strand);
            s2.Add(new PPeak(76, 90, 1.1E-8), _chrs[4], _strand);

            var mspc = new Mspc();
            mspc.Run(
                new List<Bed<PPeak>>() { s0, s1, s2 },
                new Config(ReplicateType.Biological, 1E-4, 1E-6, 1E-6, 1, alpha, MultipleIntersections.UseLowestPValue));

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
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: xLeft, right: xRight, value: 0.01), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: yLeft, right: yRight, value: 0.01), _chr, _strand);

            // Act
            mspc.Run(new List<Bed<PPeak>> { sA, sB }, new Config(ReplicateType.Biological, 1, 1, 1, 1, 1F, MultipleIntersections.UseLowestPValue));
            var cp = mspc.GetConsensusPeaks()[_chr].First();

            // Assert
            Assert.True(cp.Left == cLeft && cp.Right == cRight);
        }

        [Fact]
        public void CoordinatesOfConsensusPeaks()
        {
            // Arrange and Act
            var cPeaks = GetSampleConsensusPeaks();

            // Assert
            Assert.True(cPeaks[_chrs[0]][0].Left == 2 && cPeaks[_chrs[0]][0].Right == 26);
            Assert.True(cPeaks[_chrs[1]][0].Left == 36 && cPeaks[_chrs[1]][0].Right == 40);
            Assert.True(cPeaks[_chrs[2]][0].Left == 50 && cPeaks[_chrs[2]][0].Right == 60);
            Assert.True(cPeaks[_chrs[4]][0].Left == 70 && cPeaks[_chrs[4]][0].Right == 90);
        }

        [Fact]
        public void XSqrd()
        {
            // Arrange and Act
            var cPeaks = GetSampleConsensusPeaks();
            var cPeak = cPeaks[_chrs[0]].First(x => x.Left == 2 && x.Right == 26);

            // Assert
            Assert.True(Math.Round(cPeak.XSquared, 6) == 112.154559);
        }

        [Fact]
        public void RTP()
        {
            // Arrange and Act
            var cPeaks = GetSampleConsensusPeaks();
            var cPeak = cPeaks[_chrs[0]].First(x => x.Left == 2 && x.Right == 26);

            // Assert
            Assert.True(cPeak.RTP.ToString("E5") == "7.21069E-022");
        }

        [Fact]
        public void AdjPValue()
        {
            // Arrange & Act
            var cPeaks = GetSampleConsensusPeaks();
            var r1 = cPeaks[_chrs[0]].First(x => x.Left == 2);
            var r2 = cPeaks[_chrs[1]].First(x => x.Left == 36);
            var r3 = cPeaks[_chrs[2]].First(x => x.Left == 50);
            var r4 = cPeaks[_chrs[4]].First(x => x.Left == 70);

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
            var cPeaks = GetSampleConsensusPeaks();
            var r1 = cPeaks[_chrs[0]].First(x => x.Left == 2);
            var r2 = cPeaks[_chrs[1]].First(x => x.Left == 36);
            var r3 = cPeaks[_chrs[2]].First(x => x.Left == 50);
            var r4 = cPeaks[_chrs[4]].First(x => x.Left == 70);

            // Assert
            Assert.True(r1.HasAttribute(Attributes.TruePositive));
            Assert.True(r2.HasAttribute(Attributes.TruePositive));
            Assert.True(r3.HasAttribute(Attributes.TruePositive));
            Assert.True(r4.HasAttribute(Attributes.FalsePositive));
        }
    }
}
