// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class AdjPValue
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '.';

        [Fact]
        public void ComputeAdjustedPValue()
        {
            var sA = new Bed<Peak>();
            sA.Add(new Peak(left: 10, right: 20, value: 0.01), _chr, _strand);
            sA.Add(new Peak(left: 100, right: 200, value: 0.001), _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(new Peak(left: 5, right: 12, value: 0.01), _chr, _strand);
            sB.Add(new Peak(left: 50, right: 120, value: 0.001), _chr, _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 2, 0.05F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // Assert

            foreach (var strand in res[0].Chromosomes[_chr])
            {
                var x = strand.Value;
                Assert.True(x.Get(Attributes.TruePositive).First().AdjPValue == 0.01);
                Assert.True(x.Get(Attributes.TruePositive).Last().AdjPValue == 0.002);
            }
        }
    }
}
