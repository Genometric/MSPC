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
    public class FalsePositive
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private List<Bed<PPeak>> GenerateAndProcessBackgroundPeaks()
        {
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 1e-6), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 12, value: 1e-8), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 5e-7F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);
            return samples;
        }

        [Fact]
        public void AssignFalsePositiveAttributeWhenThereIsOnlyOnePeak()
        {
            // Arrange & Act
            var samples = GenerateAndProcessBackgroundPeaks();

            // Assert
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.FalsePositive) == 1);
        }

        [Fact]
        public void AssignFalsePositive()
        {
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 1e-6), _chr, _strand);
            sA.Add(new PPeak(left: 100, right: 200, value: 1e-8), _chr, _strand);
            sA.Add(new PPeak(left: 1000, right: 2000, value: 1e-10), _chr, _strand);
            sA.Add(new PPeak(left: 10000, right: 20000, value: 1e-12), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 12, value: 1e-7), _chr, _strand);
            sB.Add(new PPeak(left: 50, right: 120, value: 1e-9), _chr, _strand);
            sB.Add(new PPeak(left: 500, right: 1200, value: 1e-11), _chr, _strand);
            sB.Add(new PPeak(left: 5000, right: 12000, value: 1e-13), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-6, 1e-6, 2, 5e-10F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            foreach (var sample in samples)
                Assert.True(Helpers<PPeak>.Count(sample, _chr, _strand, Attributes.FalsePositive) == 2);
        }

        [Fact]
        public void AssertCorrectPeakIsTaggedAsFalsePositive()
        {
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            var r11 = new PPeak(left: 10, right: 20, value: 1e-6);
            sA.Add(r11, _chr, _strand);
            sA.Add(new PPeak(left: 100, right: 200, value: 1e-8), _chr, _strand);
            sA.Add(new PPeak(left: 1000, right: 2000, value: 1e-10), _chr, _strand);
            sA.Add(new PPeak(left: 10000, right: 20000, value: 1e-12), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 12, value: 1e-7), _chr, _strand);
            sB.Add(new PPeak(left: 50, right: 120, value: 1e-9), _chr, _strand);
            sB.Add(new PPeak(left: 500, right: 1200, value: 1e-11), _chr, _strand);
            sB.Add(new PPeak(left: 5000, right: 12000, value: 1e-13), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-6, 1e-6, 2, 5e-10F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            Assert.True(Helpers<PPeak>.Get(samples[0], _chr, _strand, Attributes.FalsePositive).First().Equals(r11));
        }

        [Fact]
        public void CorrectlyIdentifyAllPeaksAsFalsePositive()
        {
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 1e-6), _chr, _strand);
            sA.Add(new PPeak(left: 100, right: 200, value: 1e-8), _chr, _strand);
            sA.Add(new PPeak(left: 1000, right: 2000, value: 1e-10), _chr, _strand);
            sA.Add(new PPeak(left: 10000, right: 20000, value: 1e-12), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 5, right: 12, value: 1e-7), _chr, _strand);
            sB.Add(new PPeak(left: 50, right: 120, value: 1e-9), _chr, _strand);
            sB.Add(new PPeak(left: 500, right: 1200, value: 1e-11), _chr, _strand);
            sB.Add(new PPeak(left: 5000, right: 12000, value: 1e-13), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };
            var config = new Config(ReplicateType.Biological, 1e-4, 1e-6, 1e-6, 2, 5e-14F, MultipleIntersections.UseLowestPValue);

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, config);

            // Assert
            foreach (var sample in samples)
                Assert.True(Helpers<PPeak>.Count(sample, _chr, _strand, Attributes.FalsePositive) == 4);
        }
    }
}
