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
    /// <summary>
    /// This class asserts MSPC's functionality on processing 
    /// and categorizing ChIP-seq peaks regardless of any parameters
    /// (e.g., strong and weak thresholds, C, and etc.) and only
    /// based on their coordinate attributes.
    /// </summary>
    public class PeakReason
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        private List<Bed<PPeak>> RunMSPCAndReturnResult(Config config)
        {
            var sA = new Bed<PPeak>() { FileHashKey = 0 };
            sA.Add(new PPeak(left: 10, right: 20, value: 1E-5), _chr, _strand);

            var sB = new Bed<PPeak>() { FileHashKey = 1 };
            sB.Add(new PPeak(left: 12, right: 18, value: 1E-5), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { sA, sB };

            var mspc = new Mspc();
            mspc.Run(samples, config);
            return samples;
        }

        [Fact]
        public void NoMessageForConfirmedPeaks()
        {
            // Arrange & Act
            var res = RunMSPCAndReturnResult(
                new Config(ReplicateType.Biological, 1E-2, 1E-4, 1E-4, 1, 1F, MultipleIntersections.UseLowestPValue));

            // Assert
            Assert.Equal("", Helpers<PPeak>.Get(res[0], _chr, _strand, Attributes.Confirmed).First().Reason);
        }


        [Fact]
        public void MessageOfDiscardedForGamma()
        {
            // Arrange & Act
            var res = RunMSPCAndReturnResult(
                new Config(ReplicateType.Biological, 1E-2, 1E-4, 1E-50, 1, 1F, MultipleIntersections.UseLowestPValue));

            // Assert
            Assert.Equal(
                "X-squared is below chi-squared of Gamma.",
                Helpers<PPeak>.Get(res[0], _chr, _strand, Attributes.Discarded).First().Reason);
        }

        [Fact]
        public void MessageOfDiscardedForC()
        {
            // Arrange & Act
            var res = RunMSPCAndReturnResult(
                new Config(ReplicateType.Biological, 1E-2, 1E-4, 1E-50, 3, 1F, MultipleIntersections.UseLowestPValue));

            // Assert
            Assert.Equal(
                "Intersecting peaks count doesn't comply minimum C requirement.",
                Helpers<PPeak>.Get(res[0], _chr, _strand, Attributes.Discarded).First().Reason);
        }
    }
}
