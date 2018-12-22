// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.Core;
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

        private Mspc InitializeMSPC()
        {
            ///                 r11                r12
            /// Sample 1: --░░░░░░░░░░░-------████████████----------------------------
            ///                           r21             r22        r23
            /// Sample 2: ---------████████████████----▒▒▒▒▒▒▒▒---▒▒▒▒▒▒▒▒------------
            ///           r31       r32                                       r33
            /// Sample 3: ▒▒▒▒---██████████---------------------------------████████--
            ///
            /// Legend: [░░ Background peak], [▒▒ Weak peak], [██ Stringent peak]

            var r11 = new Peak(left: 3, right: 13, value: 1e-2, summit: 10, name: "r11");
            var r12 = new Peak(left: 21, right: 32, value: 1e-12, summit: 25, name: "r12");
            var r21 = new Peak(left: 10, right: 25, value: 1e-8, summit: 20, name: "r21");
            var r22 = new Peak(left: 30, right: 37, value: 1e-5, summit: 35, name: "r22");
            var r23 = new Peak(left: 41, right: 48, value: 1e-6, summit: 45, name: "r23");
            var r31 = new Peak(left: 0, right: 4, value: 1e-6, summit: 2, name: "r31");
            var r32 = new Peak(left: 8, right: 17, value: 1e-12, summit: 12, name: "r32");
            var r33 = new Peak(left: 51, right: 58, value: 1e-18, summit: 55, name: "r33");

            var sA = new Bed<Peak>();
            sA.Add(r11, _chr, _strand);
            sA.Add(r12, _chr, _strand);

            var sB = new Bed<Peak>();
            sB.Add(r21, _chr, _strand);
            sB.Add(r22, _chr, _strand);
            sB.Add(r23, _chr, _strand);

            var sC = new Bed<Peak>();
            sC.Add(r31, _chr, _strand);
            sC.Add(r32, _chr, _strand);
            sC.Add(r33, _chr, _strand);

            var mspc = new Mspc();
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

            var exporter = new Exporter<Peak>();
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
            var bedParser = new BedParser
            {
                PValueFormat = PValueFormats.minus1_Log10_pValue
            };
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
                        Assert.Equal("chr\tstart\tstop\tname\t-1xlog10(p-value)\txSqrd\t-1xlog10(Right-Tail Probability)\t-1xlog10(AdjustedP-value)", reader.ReadLine());

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
                        Assert.NotEqual("chr\tstart\tstop\tname\t-1xlog10(p-value)\txSqrd\t-1xlog10(Right-Tail Probability)\t-1xlog10(AdjustedP-value)", reader.ReadLine());

            // Clean up
            Directory.Delete(path, true);
        }

        [Theory]
        [InlineData(0, Attributes.Background, "chr1", 3, 13, "r11", 2, double.NaN, double.NaN, double.NaN)]
        public void CorrectValuesForEachPropertyOfExportedPeak(
            uint sampleID, Attributes attribute,
            string chr, int left, int right, string name, double value, double xSqrd, double rtp, double adjustedPValue)
        {
            // Arrange
            string path = RunMSPCAndExportResults();
            string expectedLine = chr + "\t" + left + "\t" + right + "\t" + name + "\t" + value + "\t" + xSqrd + "\t" + rtp + "\t" + adjustedPValue;
            string readLine = "";
            var sampleFolder = Array.Find(Directory.GetDirectories(path), (string f) => { return f.Contains(_sidfm[sampleID]); });
            var file = Array.Find(Directory.GetFiles(sampleFolder), (string f) => { return Path.GetFileNameWithoutExtension(f).Equals(attribute.ToString()); });

            // Act
            using (StreamReader reader = new StreamReader(file))
                readLine = reader.ReadLine();

            // Assert
            Assert.Equal(expectedLine, readLine);

            // Clean up
            Directory.Delete(path, true);
        }

        [Fact]
        public void OutputIsSorted()
        {
            // Arrange
            string path = RunMSPCAndExportResults();
            var sampleFolder = Array.Find(
                Directory.GetDirectories(path),
                (string f) => { return f.Contains(_sidfm[1]); });

            var file = Array.Find(
                Directory.GetFiles(sampleFolder),
                (string f) => { return Path.GetFileNameWithoutExtension(f).Equals(Attributes.Confirmed.ToString()); });

            // Act
            string line1, line2;
            using (var reader = new StreamReader(file))
            {
                line1 = reader.ReadLine();
                line2 = reader.ReadLine();
            }

            // Assert
            Assert.Equal("chr1\t10\t25\tr21\t8\t92.103\t0\t7.699", line1);
            Assert.Equal("chr1\t30\t37\tr22\t5\t78.288\t0\t5", line2);

            // Clean up
            Directory.Delete(path, true);
        }
    }
}
