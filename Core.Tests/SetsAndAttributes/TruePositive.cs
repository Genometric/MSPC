// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.SetsAndAttributes
{
    public class TruePositive
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';
        private readonly List<Peak> setA = new List<Peak>()
        {
            new Peak(left: 10, right: 20, value: 1e-6),
            new Peak(left: 100, right: 200, value: 1e-8),
            new Peak(left: 1000, right: 2000, value: 1e-10),
            new Peak(left: 10000, right: 20000, value: 1e-12)
        };
        private readonly List<Peak> setB = new List<Peak>()
        {
            new Peak(left: 5, right: 12, value: 1e-7),
            new Peak(left: 50, right: 120, value: 1e-9),
            new Peak(left: 500, right: 1200, value: 1e-11),
            new Peak(left: 5000, right: 12000, value: 1e-13)
        };

        private ReadOnlyDictionary<uint, Result<Peak>> GenerateAndProcessBackgroundPeaks(int peakCount = 4, float alpha= 5e-10F)
        {
            var sA = new Bed<Peak>();
            for (int i = 0; i < peakCount; i++)
                sA.Add(setA[i], _chr, _strand);

            var sB = new Bed<Peak>();
            for (int i = 0; i < peakCount; i++)
                sB.Add(setB[i], _chr, _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, alpha, MultipleIntersections.UseLowestPValue);

            // Act
            return mspc.Run(config);
        }

        [Fact]
        public void AssignTruePositiveAttributeWhenThereIsOnlyOnePeak()
        {
            // Arrange & Act
            var res = GenerateAndProcessBackgroundPeaks(peakCount: 1, alpha: 5e-7F);

            // Assert
            Assert.True(res[1].Chromosomes[_chr].Count(Attributes.TruePositive) == 1);
        }

        [Fact]
        public void AssignTruePositive()
        {
            // Arrange & Act
            var results = GenerateAndProcessBackgroundPeaks();

            // Assert
            foreach (var sample in results)
                Assert.True(sample.Value.Chromosomes[_chr].Count(Attributes.TruePositive) == 2);
        }

        [Fact]
        public void AssertCorrectPeakIsTaggedAsFalsePositive()
        {
            // Arrange & Act
            var results = GenerateAndProcessBackgroundPeaks();

            // Assert
            Assert.True(results[0].Chromosomes[_chr].Get(Attributes.TruePositive).First().Source.Equals(setA[2]));
        }

        [Fact]
        public void CorrectlyIdentifyAllPeaksAsTruePositive()
        {
            // Act
            var results = GenerateAndProcessBackgroundPeaks(alpha: 5e-6F);

            // Assert
            foreach (var sample in results)
                Assert.True(sample.Value.Chromosomes[_chr].Count(Attributes.TruePositive) == 4);
        }
    }
}
