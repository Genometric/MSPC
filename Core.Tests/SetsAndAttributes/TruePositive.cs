// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.SetsAndAttributes
{
    public class TruePositive
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private readonly string[] _chrs = new string[] { "chr1", "chr1", "chr5", "chrx" };

        private readonly List<PPeak> _setA = new List<PPeak>()
        {
            new PPeak(left: 10, right: 20, value: 1e-6),
            new PPeak(left: 100, right: 200, value: 1e-8),
            new PPeak(left: 1000, right: 2000, value: 1e-10),
            new PPeak(left: 10000, right: 20000, value: 1e-12)
        };
        private readonly List<PPeak> _setB = new List<PPeak>()
        {
            new PPeak(left: 5, right: 12, value: 1e-7),
            new PPeak(left: 50, right: 120, value: 1e-9),
            new PPeak(left: 500, right: 1200, value: 1e-11),
            new PPeak(left: 5000, right: 12000, value: 1e-13)
        };

        private List<Bed<PPeak>> SetupMSPC(int peakCount = 4, float alpha = 5e-10F)
        {
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            for (int i = 0; i < peakCount; i++)
                sA.Add(_setA[i], _chrs[i], _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            for (int i = 0; i < peakCount; i++)
                sB.Add(_setB[i], _chrs[i], _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, alpha, MultipleIntersections.UseLowestPValue);

            var mspc = new Mspc();
            mspc.Run(samples, config);
            return samples;
        }

        [Fact]
        public void AssignTruePositiveAttributeWhenThereIsOnlyOnePeak()
        {
            // Arrange & Act
            var samples = SetupMSPC(peakCount: 1, alpha: 5e-7F);

            // Assert
            Assert.True(Helpers<PPeak>.Count(samples[1], _chr, _strand, Attributes.TruePositive) == 1);
        }

        [Fact]
        public void AssignTruePositive()
        {
            // Arrange & Act
            var samples = SetupMSPC();

            // Assert
            foreach (var sample in samples)
            {
                int count = 0;
                foreach (var chr in sample.Chromosomes)
                    count += Helpers<PPeak>.Count(sample, chr.Key, _strand, Attributes.TruePositive);

                Assert.True(count == 2);
            }
        }

        [Fact]
        public void AssertCorrectPeakIsTaggedAsFalsePositive()
        {
            // Arrange & Act
            var samples = SetupMSPC();

            // Assert
            Assert.True(Helpers<PPeak>.Get(samples[0], _chrs[2], _strand, Attributes.TruePositive).First().Equals(_setA[2]));
        }

        [Fact]
        public void CorrectlyIdentifyAllPeaksAsTruePositive()
        {
            // Arrange & Act
            var samples = SetupMSPC(alpha: 5e-6F);

            // Assert
            foreach (var sample in samples)
            {
                int count = 0;
                foreach (var chr in sample.Chromosomes)
                    count += Helpers<PPeak>.Count(sample, chr.Key, _strand, Attributes.TruePositive);

                Assert.True(count == 4);
            }
        }
    }
}
