// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.Core.Model;
using Genometric.MSPC.Model;
using System.Collections.ObjectModel;
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
        private readonly static ChIPSeqPeak r11 = new ChIPSeqPeak() { Left = 10, Right = 30, Name = "r11", Value = 1e-8, HashKey = 1 };
        private readonly static ChIPSeqPeak r21 = new ChIPSeqPeak() { Left = 05, Right = 12, Name = "r21", Value = 1e-5, HashKey = 1 };
        private readonly static ChIPSeqPeak r22 = new ChIPSeqPeak() { Left = 16, Right = 20, Name = "r22", Value = 1e-6, HashKey = 1 };
        private readonly static ChIPSeqPeak r23 = new ChIPSeqPeak() { Left = 26, Right = 32, Name = "r23", Value = 1e-9, HashKey = 1 };

        private ReadOnlyDictionary<uint, Result<ChIPSeqPeak>> InitializeAndRun(MultipleIntersections miChoice)
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(r11, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(r21, _chr, _strand);
            sB.Add(r22, _chr, _strand);
            sB.Add(r23, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            // Act
            return mspc.Run(new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-8, 2, 1F, miChoice));
        }

        [Fact]
        public void UseMostStringentPeak()
        {
            // Arrange & Act
            var res = InitializeAndRun(MultipleIntersections.UseLowestPValue);

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).ToList()[0].SupportingPeaks[0].Source.CompareTo(r23) == 0);
        }

        [Fact]
        public void UseLeastStringentPeak()
        {
            // Arrange & Act
            var res = InitializeAndRun(MultipleIntersections.UseHighestPValue);

            // Assert
            Assert.True(res[0].Chromosomes[_chr].Get(Attributes.Confirmed).ToList()[0].SupportingPeaks[0].Source.CompareTo(r21) == 0);
        }
    }
}
