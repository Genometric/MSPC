using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC;
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Core.Tests.Base
{
    /// <summary>
    /// This class asserts MSPC's functionality on processing 
    /// and categorizing ChIP-seq peaks regardless of any parameters
    /// (e.g., strong and weak thresholds, C, and etc.) and only
    /// based on their coordinate attributes.
    /// </summary>
    public class Overlap
    {
        public static IEnumerable<object[]> GetData(bool overlap)
        {
            var rtv = new List<BED<ChIPSeqPeak>[]>();

            string chr = "chr1";
            char strand = '*';

            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20 }, chr, strand);

            if(overlap)
            {
                int peakCount = 6;
                while (peakCount-- > 0)
                    rtv.Add(new BED<ChIPSeqPeak>[] { sA, new BED<ChIPSeqPeak>() });

                // rtv[0] is intentionally left empty (i.e., with not ChIP-seq peak) so we could
                // assert confirming a peak that is not co-localized with another peak.

                rtv[1][1].Add(new ChIPSeqPeak() { Left = 5, Right = 15 }, chr, strand);
                rtv[2][1].Add(new ChIPSeqPeak() { Left = 10, Right = 15 }, chr, strand);
                rtv[3][1].Add(new ChIPSeqPeak() { Left = 15, Right = 18 }, chr, strand);
                rtv[4][1].Add(new ChIPSeqPeak() { Left = 15, Right = 25 }, chr, strand);
                rtv[5][1].Add(new ChIPSeqPeak() { Left = 5, Right = 25 }, chr, strand);
            }
            else
            {
                int peakCount = 5;
                while (peakCount-- > 0)
                    rtv.Add(new BED<ChIPSeqPeak>[] { sA, new BED<ChIPSeqPeak>() });

                // rtv[0] is intentionally left empty (i.e., with not ChIP-seq peak) so we could
                // assert confirming a peak that is not co-localized with another peak.

                rtv[1][1].Add(new ChIPSeqPeak() { Left = 1, Right = 8 }, chr, strand);
                rtv[2][1].Add(new ChIPSeqPeak() { Left = 1, Right = 10 }, chr, strand);
                rtv[3][1].Add(new ChIPSeqPeak() { Left = 20, Right = 30 }, chr, strand);
                rtv[4][1].Add(new ChIPSeqPeak() { Left = 25, Right = 30 }, chr, strand);
            }

            return rtv;
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: true)]
        public void ConfirmTwoOverlappingPeaks(BED<ChIPSeqPeak> sA, BED<ChIPSeqPeak> sB)
        {
            // Arrange
            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, 1, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // TODO: this step should not be necessary; remove it after the Results class is updated.
            foreach (var rep in res)
                rep.Value.ReadOverallStats();

            // Assert
            foreach (var rep in res.Values)
                Assert.True(rep.total____o == 1);
        }
    }
}
