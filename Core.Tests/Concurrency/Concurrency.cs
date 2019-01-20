// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Concurrency
{
    public class Concurrency
    {
        private readonly char _strand = '.';

        private Bed<PPeak> CreateSample(uint id, int offset, int chrCount, int iCount)
        {
            var rtv = new Bed<PPeak>() { FileHashKey = id };
            for (int c = 0; c < chrCount; c++)
            {
                for (int i = 0; i < iCount; i++)
                {
                    rtv.Add(
                        new PPeak(
                            left: (10 * i) + 1 + offset,
                            right: (10 * i) + 4 + offset,
                            value: 1E-6,
                            name: "r1" + i,
                            hashSeed: id.ToString()
                        ),
                        "chr" + c,
                        _strand);
                }
            }
            return rtv;
        }

        [Fact]
        public void NumberOfPeaks()
        {
            // Arrange
            var samples = new List<Bed<PPeak>>() { CreateSample(0, 0, 20, 1000), CreateSample(1, 2, 20, 2000) };

            // Act
            var mspc = new Mspc();
            mspc.Run(samples, new Config(ReplicateType.Biological, 1e-4, 1e-5, 1e-5, 1, 0.05F, MultipleIntersections.UseLowestPValue));

            // Assert
            for (int c = 0; c < 20; c++)
            {
                Assert.True(Helpers<PPeak>.Count(samples[0], "chr" + c, _strand, Attributes.Confirmed) == 1000);
                Assert.True(Helpers<PPeak>.Count(samples[1], "chr" + c, _strand, Attributes.Confirmed) == 2000);
            }
        }

        [Fact]
        public void HighDegreeOfParallelisim()
        {
            // Arrange
            var samples = new List<Bed<PPeak>>() { CreateSample(0, 0, 20, 10000), CreateSample(1, 2, 20, 20000) };

            // Act
            var mspc = new Mspc() { DegreeOfParallelism = 20 };
            mspc.Run(samples, new Config(ReplicateType.Biological, 1e-4, 1e-5, 1e-5, 1, 0.05F, MultipleIntersections.UseLowestPValue));

            // Assert
            for (int c = 0; c < 20; c++)
            {
                Assert.True(Helpers<PPeak>.Count(samples[0], "chr" + c, _strand, Attributes.Confirmed) == 10000);
                Assert.True(Helpers<PPeak>.Count(samples[1], "chr" + c, _strand, Attributes.Confirmed) == 20000);
            }
        }
    }
}
