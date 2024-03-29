﻿using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Genometric.MSPC.Core.Tests
{
    public class PublicMembers
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '.';
        private string _status;
        private AutoResetEvent _continueIni;
        private AutoResetEvent _continuePrc;
        private AutoResetEvent _continueMtc;
        private AutoResetEvent _continueCon;

        public enum Status { Init, Process, MTC, Consensu };

        private Dictionary<uint, Result<Peak>> RunThenCancelMSPC(int iCount, Status status)
        {
            var sA = new Bed<Peak>();
            var sB = new Bed<Peak>();
            for (int i = 0; i < iCount; i++)
            {
                sA.Add(new Peak(
                    left: (10 * i) + 1,
                    right: (10 * i) + 4,
                    value: 1E-4,
                    summit: 0,
                    name: "r1" + i),
                    _chr, _strand);

                sB.Add(new Peak(
                    left: (10 * i) + 6,
                    right: (10 * i) + 9,
                    value: 1E-5,
                    summit: 0,
                    name: "r1" + i),
                _chr, _strand);
            }

            var mspc = new Mspc();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 1, 0.05F, MultipleIntersections.UseLowestPValue);

            switch (status)
            {
                case Status.Init:
                    _continueIni = new AutoResetEvent(false);
                    _continueIni.Reset();
                    mspc.StatusChanged += WaitingIni;
                    // MSPC is expected to confirm peaks using the following configuration.
                    mspc.RunAsync(config);
                    _continueIni.WaitOne();
                    break;

                case Status.Process:
                    _continuePrc = new AutoResetEvent(false);
                    _continuePrc.Reset();
                    mspc.StatusChanged += WaitingPrc;
                    // MSPC is expected to confirm peaks using the following configuration.
                    mspc.RunAsync(config);
                    _continuePrc.WaitOne();
                    break;

                case Status.MTC:
                    _continueMtc = new AutoResetEvent(false);
                    _continueMtc.Reset();
                    mspc.StatusChanged += WaitingMtc;
                    // MSPC is expected to confirm peaks using the following configuration.
                    mspc.RunAsync(config);
                    _continueMtc.WaitOne();
                    break;

                case Status.Consensu:
                    _continueCon = new AutoResetEvent(false);
                    _continueCon.Reset();
                    mspc.StatusChanged += WaitingCon;
                    // MSPC is expected to confirm peaks using the following configuration.
                    mspc.RunAsync(config);
                    _continueCon.WaitOne();
                    break;
            }

            // With the following configuration, MSPC discards all the peaks. 
            // This can help asserting if the asynchronous process of 
            // the previous execution is canceled, and instead the following asynchronous 
            // execution is completed.
            mspc.RunAsync(new Config(ReplicateType.Biological, 1e-10, 1e-20, 1e-200, 1, 0.05F, MultipleIntersections.UseLowestPValue));
            mspc.Done.WaitOne();

            return mspc.GetResults();
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
            Dictionary<uint, Result<Peak>> results = null;
            for (int i = 0; i < tries; i++)
            {
                // Act
                results = RunThenCancelMSPC(c, status);
                if (results.Count > 0 &&
                    results[0].Chromosomes.Count > 0 &&
                    results[0].Chromosomes.ContainsKey(_chr) &&
                    !results[0].Chromosomes[_chr][_strand].Get(Attributes.Confirmed).Any() &&
                    results[0].Chromosomes[_chr][_strand].Get(Attributes.Background).Count() == c)
                    break;
                Thread.Sleep(10000);
            }

            if (results == null)
                throw new InvalidOperationException(
                    string.Format("Tried CancelCurrentAsyncRun unit test on {0} status for {1} times, all tries failed.", status, tries));

            // Assert
            Assert.True(!results[0].Chromosomes[_chr][_strand].Get(Attributes.Confirmed).Any());
            Assert.True(results[0].Chromosomes[_chr][_strand].Get(Attributes.Background).Count() == c);
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
            var mspc = new Mspc();
            if (inputCount == 1)
                mspc.AddSample(0, new Bed<Peak>());
            var config = new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 2, 0.05F, MultipleIntersections.UseLowestPValue);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => mspc.Run(config));
            Assert.Equal(string.Format("Minimum two samples are required; {0} is given.", inputCount), exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void RunAsyncIfAtLeastTwoInputIsGiven(int inputCount)
        {
            // Arrange
            var mspc = new Mspc();
            if (inputCount == 1)
                mspc.AddSample(0, new Bed<Peak>());
            var config = new Config(ReplicateType.Biological, 1e-1, 1e-2, 1e-2, 2, 0.05F, MultipleIntersections.UseLowestPValue);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => mspc.RunAsync(config));
            Assert.Equal(string.Format("Minimum two samples are required; {0} is given.", inputCount), exception.Message);
        }

        [Fact]
        public void GetDegreeOfParallelism()
        {
            // Arrange && Act
            int dp = 123;
            var mspc = new Mspc(maxDegreeOfParallelism: dp);

            // Assert
            Assert.Equal(mspc.DegreeOfParallelism, dp);
        }

        [Theory]
        [InlineData(1e-1)] // No background peaks.
        [InlineData(1e-6)] // Background should not be counted
        public void StepNotMoreThanStepCount(double tauW)
        {
            // Arrange
            var s1 = new Bed<Peak>();
            s1.Add(new Peak(10, 20, 1E-4), _chr, _strand);
            s1.Add(new Peak(30, 40, 1E-5), _chr, _strand);
            s1.Add(new Peak(50, 60, 1E-6), _chr, _strand);
            s1.Add(new Peak(70, 80, 1E-7), _chr, _strand);
            s1.Add(new Peak(90, 99, 1E-8), _chr, _strand);

            var s2 = new Bed<Peak>();
            s2.Add(new Peak(11, 18, 1E-4), _chr, _strand);
            s2.Add(new Peak(33, 38, 1E-5), _chr, _strand);
            s2.Add(new Peak(55, 58, 1E-6), _chr, _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, s1);
            mspc.AddSample(1, s2);

            var messages = new List<ProgressReport>();
            mspc.StatusChanged += (object sender, ValueEventArgs e) => messages.Add(e.Value);

            // Act
            mspc.RunAsync(new Config(ReplicateType.Biological, tauW, tauW, tauW, 2, 0.05F, MultipleIntersections.UseLowestPValue));
            mspc.Done.WaitOne();

            // Assert
            Assert.DoesNotContain(messages, x => x.Step > x.StepCount);
        }
    }
}
