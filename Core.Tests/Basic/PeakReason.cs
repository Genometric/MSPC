// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.ObjectModel;
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

        private ReadOnlyDictionary<uint, Result<Peak>> RunMSPCAndReturnResult(Config config)
        {
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 1E-5), _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 12, right: 18, value: 1E-5), _chr, _strand);

            var mspc = new MSPC<Peak>(new PeakConstructor());
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            return mspc.Run(config);
        }

        [Fact]
        public void NoMessageForConfirmedPeaks()
        {
            // Arrange & Act
            var res = RunMSPCAndReturnResult(
                new Config(ReplicateType.Biological, 1E-2, 1E-4, 1E-4, 1, 1F, MultipleIntersections.UseLowestPValue));

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).First().Reason == "");
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
                res[0].Chromosomes[_chr].Get(Attributes.Discarded).First().Reason);
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
                res[0].Chromosomes[_chr].Get(Attributes.Discarded).First().Reason);
        }
    }
}
