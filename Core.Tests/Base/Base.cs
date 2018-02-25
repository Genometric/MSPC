using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.IntervalTree;

namespace Genometric.MSPC.Core.Tests.Base
{
    public class Base
    {
        [Fact]
        public void ConfirmAll()
        {
            // Arrange
            var bed1 = new GeUtilities.IntervalParsers.BED<ChIPSeqPeak>();
            bed1.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-8, Name = "1" }, "chr1", '*');

            var bed2 = new GeUtilities.IntervalParsers.BED<ChIPSeqPeak>();
            bed2.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-8, Name = "2" }, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(1, bed1);
            mspc.AddSample(2, bed2);

            var config = new Model.Config(Model.ReplicateType.Biological, 1e-8, 1e-8, 1e-8, 1, 0.05F, Model.MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            foreach (var rep in res)
                rep.Value.ReadOverallStats();

            // Assert
            foreach (var rep in res.Values)
                Assert.True(rep.total____o == 1);
        }
    }
}
