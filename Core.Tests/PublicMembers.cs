// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Genometric.MSPC.Core.Tests
{
    public class PublicMembers
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';
        private string _status;
        private AutoResetEvent _continueIni;
        private AutoResetEvent _continuePrc;
        private AutoResetEvent _continueMtc;
        private AutoResetEvent _continueCon;

        public enum Status { Init, Process, MTC, Consensu }; 

        private List<Bed<PPeak>> RunThenCancelMSPC(int iCount, Status status)
        {
            /// TODO:
            /// -----------------------------------------------------------
            /// Assertion of whether MSPC has successfully canceled a 
            /// process and responded to the new invocation, based on 
            /// the analysis results, was a valid test when MSPC was 
            /// creating a separate list of peaks for its results without 
            /// modifying the input. However, with the new method 
            /// where MSPC is modifying the input instead, this assertion 
            /// cannot be valid anymore, because, if canceled, MSPC does 
            /// not undo the changes, hence the re-analysis of the modified 
            /// collection does not have the same result as the re-analysis 
            /// of an immutable input. Therefore, this assertion should be 
            /// updated to leverage a different property to assert if MSPC 
            /// has successfully canceled a process, and responded to a new 
            /// invocation.
            /// -----------------------------------------------------------

            var sABefore = new Bed<PPeak>() { FileHashKey = 0 };
            var sBBefore = new Bed<PPeak>() { FileHashKey = 1 };
            for (int i = 0; i < iCount; i++)
            {
                sABefore.Add(new PPeak(
                    left: (10 * i) + 1,
                    right: (10 * i) + 4,
                    value: 1E-4,
                    summit: 0,
                    name: "r1" + i,
                    hashSeed: "0"),
                    _chr, _strand);

                sBBefore.Add(new PPeak(
                    left: (10 * i) + 6,
                    right: (10 * i) + 9,
                    value: 1E-5,
                    summit: 0,
                    name: "r1" + i,
                    hashSeed: "1"),
                _chr, _strand);
            }

            var sAAfter = new Bed<PPeak>() { FileHashKey = 0 };
            var sBAfter = new Bed<PPeak>() { FileHashKey = 1 };
            for (int i = 0; i < iCount; i++)
            {
                sAAfter.Add(new PPeak(
                    left: (10 * i) + 1,
                    right: (10 * i) + 4,
                    value: 1E-4,
                    summit: 0,
                    name: "r1" + i,
                    hashSeed: "0"),
                    _chr, _strand);

                sBAfter.Add(new PPeak(
                    left: (10 * i) + 6,
                    right: (10 * i) + 9,
                    value: 1E-5,
                    summit: 0,
                    name: "r1" + i,
                    hashSeed: "1"),
                _chr, _strand);
            }

            var samplesBefore = new List<Bed<PPeak>>() { sABefore, sBBefore };
            var samplesAfter = new List<Bed<PPeak>>() { sAAfter, sBAfter };

            var config = new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 1, 0.05F, MultipleIntersections.UseLowestPValue);

            var mspc = new Mspc();
            switch (status)
            {
                case Status.Init:
                    _continueIni = new AutoResetEvent(false);
                    _continueIni.Reset();
                    mspc.StatusChanged += WaitingIni;
                    // MSPC is expected to confirm peaks using the following configuration.
                    mspc.RunAsync(samplesBefore, config);
                    _continueIni.WaitOne();
                    break;

                case Status.Process:
                    _continuePrc = new AutoResetEvent(false);
                    _continuePrc.Reset();
                    mspc.StatusChanged += WaitingPrc;
                    // MSPC is expected to confirm peaks using the following configuration.
                    mspc.RunAsync(samplesBefore, config);
                    _continuePrc.WaitOne();
                    break;

                case Status.MTC:
                    _continueMtc = new AutoResetEvent(false);
                    _continueMtc.Reset();
                    mspc.StatusChanged += WaitingMtc;
                    // MSPC is expected to confirm peaks using the following configuration.
                    mspc.RunAsync(samplesBefore, config);
                    _continueMtc.WaitOne();
                    break;

                case Status.Consensu:
                    _continueCon = new AutoResetEvent(false);
                    _continueCon.Reset();
                    mspc.StatusChanged += WaitingCon;
                    // MSPC is expected to confirm peaks using the following configuration.
                    mspc.RunAsync(samplesBefore, config);
                    _continueCon.WaitOne();
                    break;
            }

            // With the following configuration, MSPC discards all the peaks. 
            // This can help asserting if the asynchronous process of 
            // the previous execution is canceled, and instead the following asynchronous 
            // execution is completed.
            mspc.RunAsync(samplesAfter, new Config(ReplicateType.Biological, 1e-10, 1e-20, 1e-200, 1, 0.05F, MultipleIntersections.UseLowestPValue));
            mspc.Done.WaitOne();

            return samplesAfter;
        }

        private void WaitingIni(object sender, ValueEventArgs e)
        {
            if (e.Value.Message == "Initializing")
                _continueIni.Set();
            _status += e.Value.Message;
        }
        private void WaitingPrc(object sender, ValueEventArgs e)
        {
            if (e.Value.Message == "Processing samples")
                _continuePrc.Set();
            _status += e.Value.Message;
        }
        private void WaitingMtc(object sender, ValueEventArgs e)
        {
            if (e.Value.Message == "Performing Multiple testing correction")
                _continueMtc.Set();
            _status += e.Value.Message;
        }
        private void WaitingCon(object sender, ValueEventArgs e)
        {
            if (e.Value.Message == "Creating consensus peaks set")
                _continueCon.Set();
            _status += e.Value.Message;
        }

        [Theory]
        [InlineData(Status.Init)]
        [InlineData(Status.Process)]
        [InlineData(Status.MTC)]
        [InlineData(Status.Consensu)]
        public void CancelCurrentAsyncRun(Status status)
        {
            /// NOTE
            /// 
            /// This test (for all InlineData) always passes successfully.
            /// However, it sometimes fails on Appveyor for unknown reasons.
            /// When it fails on Appveyor, a re-build of the PR always passes
            /// the failing test. Therefore, to prevent such fails, this unit
            /// test re-tries it for a number of time before failing it.
            /// ______________________________________________________________

            // Arrange
            int c = 10000;
            int tries = 10;
            List<Bed<PPeak>> samples = null;
            for (int i = 0; i < tries; i++)
            {
                // Act
                samples = RunThenCancelMSPC(c, status);
                if (samples.Count > 0 &&
                    samples[0].Chromosomes.Count > 0 &&
                    samples[0].Chromosomes.ContainsKey(_chr) &&
                    Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed) == 0 &&
                    Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Background) == c)
                    break;
                Thread.Sleep(1000);
            }

            if (samples == null)
                throw new InvalidOperationException(
                    string.Format("Tried CancelCurrentAsyncRun unit test on {0} status for {1} times, all tries failed.", status, tries));

            // Assert
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Confirmed) == 0);
            Assert.True(Helpers<PPeak>.Count(samples[0], _chr, _strand, Attributes.Background) == c);
        }

        [Fact]
        public void ReportProcessIsCanceled()
        {
            // Arrange & Act
            RunThenCancelMSPC(10000, Status.Init);

            // Assert
            Assert.Contains("Canceled current task.", _status);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RunIfAtLeastTwoInputIsGiven(int inputCount)
        {
            // Arrange
            var samples = new List<Bed<PPeak>>();
            if (inputCount == 1)
                samples.Add(new Bed<PPeak>());
            var config = new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 2, 0.05F, MultipleIntersections.UseLowestPValue);

            // Act & Assert
            var mspc = new Mspc();
            var exception = Assert.Throws<InvalidOperationException>(() => mspc.Run(samples, config));
            Assert.Equal(string.Format("Minimum two samples are required; {0} is given.", inputCount), exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RunAsyncIfAtLeastTwoInputIsGiven(int inputCount)
        {
            // Arrange
            var samples = new List<Bed<PPeak>>();
            if (inputCount == 1)
                samples.Add(new Bed<PPeak>());
            var config = new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 2, 0.05F, MultipleIntersections.UseLowestPValue);

            // Act & Assert
            var mspc = new Mspc();
            var exception = Assert.Throws<InvalidOperationException>(() => mspc.RunAsync(samples, config));
            Assert.Equal(string.Format("Minimum two samples are required; {0} is given.", inputCount), exception.Message);
        }

        [Fact]
        public void GetDegreeOfParallelism()
        {
            // Arrange && Act
            int dp = 123;
            var mspc = new Mspc()
            {
                DegreeOfParallelism = dp
            };

            // Assert
            Assert.Equal(mspc.DegreeOfParallelism, dp);
        }

        [Theory]
        [InlineData(1e-1)] // No background peaks.
        [InlineData(1e-6)] // Background should not be counted
        public void StepNotMoreThanStepCount(double tauW)
        {
            // Arrange
            var s1 = new Bed<PPeak>();
            s1.Add(new PPeak(10, 20, 1E-4), _chr, _strand);
            s1.Add(new PPeak(30, 40, 1E-5), _chr, _strand);
            s1.Add(new PPeak(50, 60, 1E-6), _chr, _strand);
            s1.Add(new PPeak(70, 80, 1E-7), _chr, _strand);
            s1.Add(new PPeak(90, 99, 1E-8), _chr, _strand);

            var s2 = new Bed<PPeak>();
            s2.Add(new PPeak(11, 18, 1E-4), _chr, _strand);
            s2.Add(new PPeak(33, 38, 1E-5), _chr, _strand);
            s2.Add(new PPeak(55, 58, 1E-6), _chr, _strand);

            var samples = new List<Bed<PPeak>>() { s1, s2 };
            var messages = new List<ProgressReport>();
            var mspc = new Mspc();
            mspc.StatusChanged += (object sender, ValueEventArgs e) => messages.Add(e.Value);

            // Act
            mspc.RunAsync(samples, new Config(ReplicateType.Biological, tauW, tauW, tauW, 2, 0.05F, MultipleIntersections.UseLowestPValue));
            mspc.Done.WaitOne();

            // Assert
            Assert.DoesNotContain(messages, x => x.Step > x.StepCount);
        }
    }
}
