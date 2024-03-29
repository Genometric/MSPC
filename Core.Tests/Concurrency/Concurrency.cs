﻿using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Concurrency
{
    public class Concurrency
    {
        private const char _strand = '.';

        private Bed<Peak> CreateSample(int offset, int chrCount, int iCount)
        {
            var rtv = new Bed<Peak>();
            for (int c = 0; c < chrCount; c++)
                for (int i = 0; i < iCount; i++)
                    rtv.Add(
                        new Peak(
                            left: (10 * i) + 1 + offset,
                            right: (10 * i) + 4 + offset,
                            value: 1E-6,
                            name: "r1" + i
                        ),
                        "chr" + c,
                        _strand);

            return rtv;
        }

        [Fact]
        public void NumberOfPeaks()
        {
            // Arrange
            var mspc = new Mspc();
            mspc.AddSample(0, CreateSample(0, 20, 1000));
            mspc.AddSample(1, CreateSample(2, 20, 2000));

            // Act
            var res = mspc.Run(new Config(
                ReplicateType.Biological, 1e-4, 1e-5, 1e-5, 1, 0.05F, MultipleIntersections.UseLowestPValue));

            // Assert
            for (int c = 0; c < 20; c++)
            {
                Assert.True(res[0].Chromosomes["chr" + c][_strand].Get(Attributes.Confirmed).Count() == 1000);
                Assert.True(res[1].Chromosomes["chr" + c][_strand].Get(Attributes.Confirmed).Count() == 2000);
            }
        }

        [Fact]
        public void HighDegreeOfParallelisim()
        {
            // Arrange
            var mspc = new Mspc(maxDegreeOfParallelism: 20);
            mspc.AddSample(0, CreateSample(0, 20, 10000));
            mspc.AddSample(1, CreateSample(2, 20, 20000));

            // Act
            var res = mspc.Run(new Config(
                ReplicateType.Biological, 1e-4, 1e-5, 1e-5, 1, 0.05F, MultipleIntersections.UseLowestPValue));

            // Assert
            for (int c = 0; c < 20; c++)
            {
                Assert.True(res[0].Chromosomes["chr" + c][_strand].Get(Attributes.Confirmed).Count() == 10000);
                Assert.True(res[1].Chromosomes["chr" + c][_strand].Get(Attributes.Confirmed).Count() == 20000);
            }
        }
    }
}
