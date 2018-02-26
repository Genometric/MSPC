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
    public class ColocalizationCount
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        public void SingleNonOverlappingPeak(byte c, byte expected)
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20 }, "chr1", '*');

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 30, Right = 40 }, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1, 1, 1, c, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // TODO: this step should not be necessary; remove it after the Results class is updated.
            foreach (var rep in res)
                rep.Value.ReadOverallStats();

            // Assert
            Assert.True(res[0].R_j__o["chr1"].Count == expected);
        }
    }
}
