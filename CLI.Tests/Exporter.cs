// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class Exporter
    {
        private readonly string _chr = "chr1";
        private readonly char _strand = '*';
        private readonly List<Attributes> _attributes;

        // Sample ID Filename Mapping
        private readonly Dictionary<uint, string> _sidfm;

        public Exporter()
        {
            _attributes = new List<Attributes>();
            foreach (var att in Enum.GetValues(typeof(Attributes)).Cast<Attributes>())
                _attributes.Add(att);

            _sidfm = new Dictionary<uint, string>
            {
                { 0, "sA" },
                { 1, "sB" },
                { 2, "sC" }
            };
        }

        private MSPC<ChIPSeqPeak> InitializeMSPC()
        {
            ///                 r11                r12
            /// Sample 1: --░░░░░░░░░░░-------████████████----------------------------
            ///                           r21             r22        r23
            /// Sample 2: ---------████████████████----▒▒▒▒▒▒▒▒---▒▒▒▒▒▒▒▒------------
            ///           r31       r32                                       r33
            /// Sample 3: ▒▒▒▒---██████████---------------------------------████████--
            ///
            /// Legend: [░░ Background peak], [▒▒ Weak peak], [██ Stringent peak]

            var r11 = new ChIPSeqPeak() { Left = 3, Right = 13, Name = "r11", Value = 1e-2, HashKey = 1 };
            var r12 = new ChIPSeqPeak() { Left = 21, Right = 32, Name = "r12", Value = 1e-12, HashKey = 2 };
            var r21 = new ChIPSeqPeak() { Left = 10, Right = 25, Name = "r21", Value = 1e-8, HashKey = 3 };
            var r22 = new ChIPSeqPeak() { Left = 30, Right = 37, Name = "r22", Value = 1e-5, HashKey = 4 };
            var r23 = new ChIPSeqPeak() { Left = 41, Right = 48, Name = "r23", Value = 1e-6, HashKey = 5 };
            var r31 = new ChIPSeqPeak() { Left = 0, Right = 4, Name = "r31", Value = 1e-6, HashKey = 6 };
            var r32 = new ChIPSeqPeak() { Left = 8, Right = 17, Name = "r32", Value = 1e-12, HashKey = 7 };
            var r33 = new ChIPSeqPeak() { Left = 51, Right = 58, Name = "r33", Value = 1e-18, HashKey = 8 };

            var sA = new BED<ChIPSeqPeak>();
            sA.Add(r11, _chr, _strand);
            sA.Add(r12, _chr, _strand);

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(r21, _chr, _strand);
            sB.Add(r22, _chr, _strand);
            sB.Add(r23, _chr, _strand);

            var sC = new BED<ChIPSeqPeak>();
            sC.Add(r31, _chr, _strand);
            sC.Add(r32, _chr, _strand);
            sC.Add(r33, _chr, _strand);

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);
            mspc.AddSample(2, sC);

            return mspc;
        }

        private string RunMSPCAndExportResults(bool includeHeader = false)
        {
            var mspc = InitializeMSPC();
            mspc.Run(new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 0.05F, MultipleIntersections.UseLowestPValue));

            var path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "MSPCTests_" + new Random().NextDouble().ToString();

            var exporter = new Exporter<ChIPSeqPeak>();
            var options = new Options(path, includeHeader, _attributes);

            exporter.Export(_sidfm, mspc.GetResults(), mspc.GetMergedReplicates(), options);

            return path;
        }

        [Fact]
        public void CreateOneAndOnlyOneFilePerAttribute()
        {
            // Arrange & Act
            string path = RunMSPCAndExportResults();
            foreach (var sampleFolder in Directory.GetDirectories(path))
            {
                var files = new List<string>();
                foreach (var file in Directory.GetFiles(sampleFolder))
                    files.Add(Path.GetFileName(file));

                // Assert
                foreach (var att in _attributes)
                {
                    Assert.Contains(files, (string p) => { return p.Equals(att + ".bed"); });
                    Assert.True(files.Count(x => x.Equals(att + ".bed")) == 1);
                }
            }

            // Clean up
            Directory.Delete(path, true);
        }

        [Theory]
        [InlineData(0, Attributes.Background, 1)]
        [InlineData(1, Attributes.Background, 0)]
        [InlineData(2, Attributes.Background, 0)]
        [InlineData(0, Attributes.Confirmed, 1)]
        [InlineData(1, Attributes.Confirmed, 2)]
        [InlineData(2, Attributes.Confirmed, 1)]
        [InlineData(0, Attributes.Discarded, 0)]
        [InlineData(1, Attributes.Discarded, 1)]
        [InlineData(2, Attributes.Discarded, 2)]
        [InlineData(0, Attributes.FalsePositive, 0)]
        [InlineData(1, Attributes.FalsePositive, 0)]
        [InlineData(2, Attributes.FalsePositive, 0)]
        [InlineData(0, Attributes.Stringent, 1)]
        [InlineData(1, Attributes.Stringent, 1)]
        [InlineData(2, Attributes.Stringent, 2)]
        [InlineData(0, Attributes.TruePositive, 1)]
        [InlineData(1, Attributes.TruePositive, 2)]
        [InlineData(2, Attributes.TruePositive, 1)]
        [InlineData(0, Attributes.Weak, 0)]
        [InlineData(1, Attributes.Weak, 2)]
        [InlineData(2, Attributes.Weak, 1)]
        public void CountNumberOfExportedPeaksInEachSet(uint sampleID, Attributes attribute, int count)
        {
            // Arrange & Act
            string path = RunMSPCAndExportResults();
            var sampleFolder = Array.Find(Directory.GetDirectories(path), (string f) => { return f.Contains(_sidfm[sampleID]); });
            var file = Array.Find(Directory.GetFiles(sampleFolder), (string f) => { return Path.GetFileNameWithoutExtension(f).Equals(attribute.ToString()); });
            var bedParser = new BEDParser();
            var parsedSample = bedParser.Parse(file);

            // Assert
            if (count == 0)
                Assert.False(parsedSample.Chromosomes.ContainsKey(_chr));
            else
                Assert.True(parsedSample.Chromosomes[_chr].Strands[_strand].Intervals.Count == count);
            
            // Clean up
            Directory.Delete(path, true);
        }

        [Fact]
        public void WriteHeaderFileToAllExportedData()
        {
            // Arrange & Act
            string path = RunMSPCAndExportResults(true);
            foreach (var sampleFolder in Directory.GetDirectories(path))
                foreach (var file in Directory.GetFiles(sampleFolder))
                    using (StreamReader reader = new StreamReader(file))
                        Assert.Equal("chr\tstart\tstop\tname\t-1xlog10(p-value)\txSqrd\t-1xlog10(Right-Tail Probability)", reader.ReadLine());

            // Clean up
            Directory.Delete(path, true);
        }

        [Fact]
        public void NotWriteHeaderFileToAllExportedData()
        {
            // Arrange & Act
            string path = RunMSPCAndExportResults();
            foreach (var sampleFolder in Directory.GetDirectories(path))
                foreach (var file in Directory.GetFiles(sampleFolder))
                    using (StreamReader reader = new StreamReader(file))
                        Assert.NotEqual("chr\tstart\tstop\tname\t-1xlog10(p-value)\txSqrd\t-1xlog10(Right-Tail Probability)", reader.ReadLine());

            // Clean up
            Directory.Delete(path, true);
        }
    }
}
