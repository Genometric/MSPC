// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.Core.Model;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class TConsensusPeaks
    {
        [Theory]
        [InlineData(10, 20, 5, 15, 5, 20)]
        [InlineData(10, 20, 12, 18, 10, 20)]
        [InlineData(10, 20, 5, 25, 5, 25)]
        [InlineData(10, 20, 19, 25, 10, 25)]
        [InlineData(19, 25, 10, 20, 10, 25)]
        public void MergeTwoConsensusPeaks(int xLeft, int xRight, int yLeft, int yRight, int cLeft, int cRight)
        {
            // Arrange
            var mspc = new MSPC<ChIPSeqPeak>();
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = xLeft, Right = xRight }, "chr1", '*');
            mspc.AddSample(0, sA);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = yLeft, Right = yRight }, "chr1", '*');
            mspc.AddSample(1, sB);

            // Act
            mspc.Run(new Config(ReplicateType.Biological, 1, 1, 1, 1, 1F, MultipleIntersections.UseLowestPValue));
            var cp = mspc.GetMergedReplicates()["chr1"].First().Value;

            // Assert
            Assert.True(cp.Left == cLeft && cp.Right == cRight);
        }
    }
}
