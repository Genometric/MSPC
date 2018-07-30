// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC;
using Genometric.MSPC.Model;
using System.Linq;
using Xunit;

namespace Core.Tests
{
    public class Core
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';

        [Fact]
        public void CancelCurrentAsyncRun()
        {
            // Arrange
            var sA = new BED<ChIPSeqPeak>();
            var sB = new BED<ChIPSeqPeak>();
            for (int i = 0; i < 10000; i++)
            {
                sA.Add(new ChIPSeqPeak() { Left = (10 * i) + 1, Right = (10 * i) + 4, Value = 1E-4, Name = "r1" + i, HashKey = (uint)i }, _chr, _strand);
                sB.Add(new ChIPSeqPeak() { Left = (10 * i) + 6, Right = (10 * i) + 9, Value = 1E-5, Name = "r1" + i, HashKey = (uint)i * 10000 }, _chr, _strand);
            }
            
            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            // Act
            // MSPC is expected to confirm peaks using the following configuration.
            mspc.RunAsync(new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 2, 0.05F, MultipleIntersections.UseLowestPValue));
            System.Threading.Thread.Sleep(1000);
            // However, with the following configuration, it is expected to discard 
            // all the peaks. This can help asserting if the asynchronous process of 
            // the previous execution is canceled, and instead the following asynchronous 
            // execution is completed.
            mspc.RunAsync(new Config(ReplicateType.Biological, 1e-10, 1e-20, 1e-200, 2, 0.05F, MultipleIntersections.UseLowestPValue));
            mspc.done.WaitOne();

            // Assert
            Assert.True(!mspc.GetResults()[0].Chromosomes[_chr].Get(Attributes.Confirmed).Any());
        }
    }
}
