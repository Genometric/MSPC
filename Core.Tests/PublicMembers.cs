// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.Core.Model;
using Genometric.MSPC.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Xunit;

namespace Genometric.MSPC.Core.Tests
{
    public class PublicMembers
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';
        private string _cancelOnMessage;
        private string status;
        private AutoResetEvent _continue;

        private ReadOnlyDictionary<uint, Result<ChIPSeqPeak>> RunThenCancelMSPC(int iCount)
        {
            _continue = new AutoResetEvent(false);
            var sA = new BED<ChIPSeqPeak>();
            var sB = new BED<ChIPSeqPeak>();
            for (int i = 0; i < iCount; i++)
            {
                sA.Add(new ChIPSeqPeak() { Left = (10 * i) + 1, Right = (10 * i) + 4, Value = 1E-4, Name = "r1" + i, HashKey = (uint)i }, _chr, _strand);
                sB.Add(new ChIPSeqPeak() { Left = (10 * i) + 6, Right = (10 * i) + 9, Value = 1E-5, Name = "r1" + i, HashKey = (uint)i * 10000 }, _chr, _strand);
            }

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.StatusChanged += Mspc_StatusChanged;
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            _continue.Reset();
            // MSPC is expected to confirm peaks using the following configuration.
            mspc.RunAsync(new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 2, 0.05F, MultipleIntersections.UseLowestPValue));
            _continue.WaitOne();
            // However, with the following configuration, it is expected to discard 
            // all the peaks. This can help asserting if the asynchronous process of 
            // the previous execution is canceled, and instead the following asynchronous 
            // execution is completed.
            mspc.RunAsync(new Config(ReplicateType.Biological, 1e-10, 1e-20, 1e-200, 2, 0.05F, MultipleIntersections.UseLowestPValue));
            mspc.done.WaitOne();

            return mspc.GetResults();
        }

        private void Mspc_StatusChanged(object sender, ValueEventArgs e)
        {
            if (e.Value.Message == _cancelOnMessage)
                _continue.Set();
            status += e.Value.Message;
        }

        [Theory]
        [InlineData("Initializing")]
        [InlineData("Processing samples")]
        [InlineData("Performing Multiple testing correction")]
        [InlineData("Creating consensus peaks set")]
        public void CancelCurrentAsyncRun(string cancelOnMessage)
        {
            // Arrange & Act
            int c = 10000;
            _cancelOnMessage = cancelOnMessage;
            var results = RunThenCancelMSPC(c);

            // Assert
            Assert.True(!results[0].Chromosomes[_chr].Get(Attributes.Confirmed).Any());
            Assert.True(results[0].Chromosomes[_chr].Get(Attributes.Background).Count() == c);
        }

        [Fact]
        public void ReportProcessIsCanceled()
        {
            // Arrange & Act
            _cancelOnMessage = "Initializing";
            RunThenCancelMSPC(10000);

            // Assert
            Assert.Contains("Canceled current task.", status);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RunIfAtLeastTwoInputIsGiven(int inputCount)
        {
            // Arrange
            var mspc = new MSPC<ChIPSeqPeak>();
            if (inputCount == 1)
                mspc.AddSample(0, new BED<ChIPSeqPeak>());
            var config = new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 2, 0.05F, MultipleIntersections.UseLowestPValue);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => mspc.Run(config));
            Assert.Equal(String.Format("Minimum two samples are required; {0} is given.", inputCount), exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RunAsyncIfAtLeastTwoInputIsGiven(int inputCount)
        {
            // Arrange
            var mspc = new MSPC<ChIPSeqPeak>();
            if (inputCount == 1)
                mspc.AddSample(0, new BED<ChIPSeqPeak>());
            var config = new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 2, 0.05F, MultipleIntersections.UseLowestPValue);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => mspc.RunAsync(config));
            Assert.Equal(String.Format("Minimum two samples are required; {0} is given.", inputCount), exception.Message);
        }
    }
}
