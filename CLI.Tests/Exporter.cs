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
        private readonly char _strand = '.';
        private readonly List<Attributes> _attributes;

        readonly List<Peak> peaks = new List<Peak>()
            {
                new Peak(10, 11, 1E-8),
                new Peak(100, 110, 1E-8),
                new Peak(1000, 1100, 1E-8),
                new Peak(10000, 11000, 1E-8),
                new Peak(100000, 110000, 1E-8),
                new Peak(120000, 130000, 1E-8),
                new Peak(150000, 160000, 1E-8),
                new Peak(1000000, 1100000, 1E-8),
                new Peak(1200000, 1300000, 1E-8),
                new Peak(110000000, 120000000, 1E-8),
                new Peak(130000000, 140000000, 1E-8),
                new Peak(910000000, 920000000, 1E-8),
                new Peak(930000000, 940000000, 1E-8)
            };
        readonly List<string> chrs = new List<string>()
            {
                "chr1", "chr3", "chr5", "chr8", "chr9", "chr9", "chr9", "chr18", "chr18", "chrX", "chrX", "chrY", "chrY"
            };

        private string Header(bool mspcFormat)
        {
            return mspcFormat ?
                "chr\tstart\tstop\tname\t-1xlog10(p-value)\txSqrd\t-1xlog10(Right-Tail Probability)\t-1xlog10(AdjustedP-value)" :
                "chr\tstart\tstop\tname\t-1xlog10(p-value)";
        }

        private string RunMSPCAndExportResultsWithMultiChr()
        {
            var s0 = new Bed<Peak>();
            for (int i = 0; i < peaks.Count; i++)
                s0.Add(peaks[i], chrs[i], _strand);

            var s1 = new Bed<Peak>();
            s1.Add(new Peak(800, 900, 1E-2), "chr5", _strand);

            var mspc = new Mspc();
            mspc.AddSample(0, s0);
            mspc.AddSample(1, s1);

            mspc.Run(new Config(ReplicateType.Biological, 1e-4, 1e-5, 1e-5, 1, 0.05F, MultipleIntersections.UseLowestPValue));
            var path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "MSPCTests_" + new Random().NextDouble().ToString();
            new Exporter<Peak>().Export(_sidfm, mspc.GetResults(), mspc.GetConsensusPeaks(), new Options(path, false, _attributes));
            return path;
        }

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
            /// Sample 0: --░░░░░░░░░░░-------████████████----------------------------
            ///                           r21             r22        r23
            /// Sample 1: ---------████████████████----▒▒▒▒▒▒▒▒---▒▒▒▒▒▒▒▒------------
            ///           r31       r32                                       r33
            /// Sample 2: ▒▒▒▒---██████████---------------------------------████████--
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

            exporter.Export(_sidfm, mspc.GetResults(), mspc.GetConsensusPeaks(), options);

            return path;
        }

        [Fact]
        public void CreateOneAndOnlyOneFilePerAttributeInMSPCPeaksFormat()
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
                    Assert.Contains(files, (string p) => { return p.Equals(att + "_mspc_peaks.txt"); });
                    Assert.True(files.Count(x => x.Equals(att + "_mspc_peaks.txt")) == 1);
                }
            }

            // Clean up
            Directory.Delete(path, true);
        }

        [Theory]
        [InlineData(true, 0, Attributes.Background, 1)]
        [InlineData(true, 1, Attributes.Background, 0)]
        [InlineData(true, 2, Attributes.Background, 0)]
        [InlineData(true, 0, Attributes.Confirmed, 1)]
        [InlineData(true, 1, Attributes.Confirmed, 2)]
        [InlineData(true, 2, Attributes.Confirmed, 1)]
        [InlineData(true, 0, Attributes.Discarded, 0)]
        [InlineData(true, 1, Attributes.Discarded, 1)]
        [InlineData(true, 2, Attributes.Discarded, 2)]
        [InlineData(true, 0, Attributes.FalsePositive, 0)]
        [InlineData(true, 1, Attributes.FalsePositive, 0)]
        [InlineData(true, 2, Attributes.FalsePositive, 0)]
        [InlineData(true, 0, Attributes.Stringent, 1)]
        [InlineData(true, 1, Attributes.Stringent, 1)]
        [InlineData(true, 2, Attributes.Stringent, 2)]
        [InlineData(true, 0, Attributes.TruePositive, 1)]
        [InlineData(true, 1, Attributes.TruePositive, 2)]
        [InlineData(true, 2, Attributes.TruePositive, 1)]
        [InlineData(true, 0, Attributes.Weak, 0)]
        [InlineData(true, 1, Attributes.Weak, 2)]
        [InlineData(true, 2, Attributes.Weak, 1)]
        [InlineData(false, 0, Attributes.Background, 1)]
        [InlineData(false, 1, Attributes.Background, 0)]
        [InlineData(false, 2, Attributes.Background, 0)]
        [InlineData(false, 0, Attributes.Confirmed, 1)]
        [InlineData(false, 1, Attributes.Confirmed, 2)]
        [InlineData(false, 2, Attributes.Confirmed, 1)]
        [InlineData(false, 0, Attributes.Discarded, 0)]
        [InlineData(false, 1, Attributes.Discarded, 1)]
        [InlineData(false, 2, Attributes.Discarded, 2)]
        [InlineData(false, 0, Attributes.FalsePositive, 0)]
        [InlineData(false, 1, Attributes.FalsePositive, 0)]
        [InlineData(false, 2, Attributes.FalsePositive, 0)]
        [InlineData(false, 0, Attributes.Stringent, 1)]
        [InlineData(false, 1, Attributes.Stringent, 1)]
        [InlineData(false, 2, Attributes.Stringent, 2)]
        [InlineData(false, 0, Attributes.TruePositive, 1)]
        [InlineData(false, 1, Attributes.TruePositive, 2)]
        [InlineData(false, 2, Attributes.TruePositive, 1)]
        [InlineData(false, 0, Attributes.Weak, 0)]
        [InlineData(false, 1, Attributes.Weak, 2)]
        [InlineData(false, 2, Attributes.Weak, 1)]
        public void CountNumberOfExportedPeaksInEachSet(bool mspcFormat, uint sampleID, Attributes attribute, int count)
        {
            // Arrange & Act
            string path = RunMSPCAndExportResults();
            string filename = mspcFormat ? attribute.ToString() + "_mspc_peaks" : attribute.ToString();
            var sampleFolder = Array.Find(Directory.GetDirectories(path), (string f) => { return f.Contains(_sidfm[sampleID]); });
            var file = Array.Find(Directory.GetFiles(sampleFolder), (string f) => { return Path.GetFileNameWithoutExtension(f).Equals(filename); });
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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WriteHeaderToAllExportedData(bool write)
        {
            // Arrange & Act & Assert
            string path = RunMSPCAndExportResults(write);
            foreach (var sampleFolder in Directory.GetDirectories(path))
                foreach (var file in Directory.GetFiles(sampleFolder))
                    using (StreamReader reader = new StreamReader(file))
                        if (Path.GetFileNameWithoutExtension(file).Contains("_mspc_peaks"))
                            Assert.True(Header(true).Equals(reader.ReadLine()) == write);
                        else
                            Assert.True(Header(false).Equals(reader.ReadLine()) == write);

            // Clean up
            Directory.Delete(path, true);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void WriteHeaderToConsensusPeaksFile(bool mspcFormat, bool write)
        {
            // Arrange & Act
            string line;
            string expectedHeader = Header(mspcFormat);
            string path = RunMSPCAndExportResults(write);
            string filename = mspcFormat ? "ConsensusPeaks_mspc_peaks.txt" : "ConsensusPeaks.bed";
            using (StreamReader reader = new StreamReader(path + Path.DirectorySeparatorChar + filename))
                line = reader.ReadLine();

            // Assert
            Assert.True(line.Equals(expectedHeader) == write);

            // Clean up
            Directory.Delete(path, true);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AddHeaderBasedOnGivenArg(bool include)
        {
            // Arrange
            static string GetFirstLine(string filename)
            {
                var reader = new StreamReader(filename);
                var line = reader.ReadLine();
                reader.Close();
                return line;
            }

            // Act
            string path = RunMSPCAndExportResults(include);

            foreach (var dir in Directory.GetDirectories(path))
            {
                foreach (var file in Directory.GetFiles(dir))
                {
                    string potentialHeader;
                    if (Path.GetFileNameWithoutExtension(file).EndsWith("_mspc_peaks"))
                        potentialHeader = Header(true);
                    else if (Path.GetExtension(file) == ".bed")
                        potentialHeader = Header(false);
                    else
                        continue;

                    // Assert
                    Assert.True(include == (potentialHeader == GetFirstLine(file)));
                }
            }

            // Clean up
            Directory.Delete(path, true);
        }

        [Theory]
        [InlineData(0, Attributes.Background, "chr1", 3, 13, "r11", 2, double.NaN, double.NaN, double.NaN)]
        public void CorrectValuesForEachPropertyOfExportedPeakInMSPCPeakFile(
            uint sampleID, Attributes attribute,
            string chr, int left, int right, string name, double value, double xSqrd, double rtp, double adjustedPValue)
        {
            // Arrange
            string path = RunMSPCAndExportResults();
            string expectedLine = chr + "\t" + left + "\t" + right + "\t" + name + "\t" + value + "\t" + xSqrd + "\t" + rtp + "\t" + adjustedPValue;
            string readLine = "";
            var sampleFolder = Array.Find(Directory.GetDirectories(path), (string f) => { return f.Contains(_sidfm[sampleID]); });
            var file = Array.Find(Directory.GetFiles(sampleFolder), (string f) => { return Path.GetFileNameWithoutExtension(f).Equals(attribute.ToString() + "_mspc_peaks"); });

            // Act
            using (StreamReader reader = new StreamReader(file))
                readLine = reader.ReadLine();

            // Assert
            Assert.Equal(expectedLine, readLine);

            // Clean up
            Directory.Delete(path, true);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void OutputIsSortedInMSPCPeaksFormat(bool mspcFormat)
        {
            // Arrange & Act
            var path = RunMSPCAndExportResultsWithMultiChr();
            string name = mspcFormat ? Attributes.Confirmed.ToString() + "_mspc_peaks" : Attributes.Confirmed.ToString();

            // In the exported session folder, first finds the folder that contains 
            // analysis results for a given sample, then within that folder
            // finds the file that contains data belonging to a given attributes.
            var filename = Array.Find(
                Directory.GetFiles(Array.Find(Directory.GetDirectories(path), (string f) => { return f.Contains(_sidfm[0]); })),
                (string f) => { return Path.GetFileNameWithoutExtension(f).Equals(name); });

            // Assert
            int lineCounter = 0;
            using (var reader = new StreamReader(filename))
            {
                for (int i = 0; i < peaks.Count; i++)
                {
                    var line = reader.ReadLine().Split('\t');
                    Assert.True(
                        chrs[lineCounter] == line[0] &&
                        peaks[lineCounter].Left.ToString() == line[1] &&
                        peaks[lineCounter].Right.ToString() == line[2]);
                    lineCounter++;
                }
            }

            // Clean up
            Directory.Delete(path, true);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ConsensusPeaksAreSorted(bool mspcFormat)
        {
            // Arrange & Act
            var path = RunMSPCAndExportResultsWithMultiChr();
            string filename = mspcFormat ? "ConsensusPeaks_mspc_peaks.txt" : "ConsensusPeaks.bed";

            // Assert
            int lineCounter = 0;
            using (var reader = new StreamReader(path + Path.DirectorySeparatorChar + filename))
            {
                for (int i = 0; i < peaks.Count; i++)
                {
                    var line = reader.ReadLine().Split('\t');
                    Assert.True(
                        chrs[lineCounter] == line[0] &&
                        peaks[lineCounter].Left.ToString() == line[1] &&
                        peaks[lineCounter].Right.ToString() == line[2]);
                    lineCounter++;
                }
            }

            // Clean up
            Directory.Delete(path, true);
        }

        //[Fact]
        public void ExportPeaksWithPValueZero()
        {
            // Arrange
            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

            using (var w = new StreamWriter(rep1Path))
                w.WriteLine("chr1\t10\t20\tA\t326");
            using (var w = new StreamWriter(rep2Path))
                w.WriteLine("chr1\t15\t25\tB\t326");

            // Act
            string outputPath;
            using (var o = new Orchestrator())
            {
                o.Invoke(string.Format("-i {0} -i {1} -r bio -w 1e-2 -s 1e-4", rep1Path, rep2Path).Split(' '));
                outputPath = o.OutputPath;
            }

            string rep1Confirmed;
            using (var reader = new StreamReader(outputPath + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(rep1Path) + Path.DirectorySeparatorChar + Attributes.Confirmed + ".bed"))
            {
                reader.ReadLine();
                rep1Confirmed = reader.ReadLine();
            }

            string rep2Confirmed;
            using (var reader = new StreamReader(outputPath + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(rep2Path) + Path.DirectorySeparatorChar + Attributes.Confirmed + ".bed"))
            {
                reader.ReadLine();
                rep2Confirmed = reader.ReadLine();
            }

            // Assert
            Assert.Equal("chr1\t10\t20\tA\t0", rep1Confirmed);
            Assert.Equal("chr1\t15\t25\tB\t0", rep2Confirmed);

            // Clean up
            File.Delete(rep1Path);
            File.Delete(rep2Path);
            Directory.Delete(outputPath, true);
        }
    }
}
