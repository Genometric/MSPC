using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC;
using Genometric.MSPC.Core.Model;
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xunit;

namespace Core.Tests.Base
{
    public class WeakDiscarded
    {
        private ReadOnlyDictionary<uint, Result<ChIPSeqPeak>> CreateWeakPeaksAndDiscardThem()
        {
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-4 }, "chr1", '*');

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-4 }, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-3, 1e-8, 1e-8, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // TODO: this step should not be necessary; remove it after the Results class is updated.
            ///foreach (var rep in res)
            ///    rep.Value.ReadOverallStats();

            return res;
        }

        [Fact]
        public void Separate()
        {
            // Arrange & Act
            var res = CreateWeakPeaksAndDiscardThem();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes["chr1"].Stats(Attributes.WeakDiscardedT) == 1);
        }

        [Fact]
        public void Separate2()
        {
            // Arrange & Act
            var res = CreateWeakPeaksAndDiscardThem();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes["chr1"].R_j__d.Count == 1);
            foreach (var s in res)
                foreach (var p in s.Value.Chromosomes["chr1"].R_j__d)
                    Assert.True(p.Value.classification == Attributes.WeakDiscardedT);
        }
    }
}
