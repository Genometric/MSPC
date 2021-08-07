// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.MSPC.CLI.Tests.MockTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    [Collection("Sequential")]
    public class Main
    {
        /// <summary>
        /// This path should be illegal cross-platform
        /// for the tests to be reproducible on different
        /// platforms. Since the trailing path separator
        /// is trimmed in Orchestrator from every given
        /// path, hence the underscore (_) char is added
        /// to keep forward slash (/) in the path.
        /// </summary>
        private readonly string _illegalPath = "/_";

        private static void WriteSampleFiles(out string rep1Filename, out string rep2Filename, out string culture)
        {
            culture = "fa-IR";
            double pValue;
            rep1Filename = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            using (StreamWriter writter = new StreamWriter(rep1Filename))
            {
                pValue = 7.12;
                writter.WriteLine("chr1\t10\t20\tmspc_peak_1\t" + pValue.ToString(CultureInfo.CreateSpecificCulture(culture)));
                pValue = 5.5067;
                writter.WriteLine("chr1\t25\t35\tmspc_peak_2\t" + pValue.ToString(CultureInfo.CreateSpecificCulture(culture)));
            }

            rep2Filename = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            using (StreamWriter writter = new StreamWriter(rep2Filename))
            {
                pValue = 19.9;
                writter.WriteLine("chr1\t4\t12\tmspc_peak_3\t" + pValue.ToString(CultureInfo.CreateSpecificCulture(culture)));
                pValue = 8.999999;
                writter.WriteLine("chr1\t30\t45\tmspc_peak_4\t" + pValue.ToString(CultureInfo.CreateSpecificCulture(culture)));
            }
        }

        [Fact]
        public void ErrorIfLessThanTwoSamplesAreGiven()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(false, "-i rep1.bed -r bio -w 1E-2 -s 1E-8");

            // Assert
            Assert.Contains("at least two samples are required; 1 is given.", msg);
            Assert.False(Environment.ExitCode == 0);
        }

        [Fact]
        public void ErrorIfARequiredArgumentIsMissing()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(false, "-i rep1.bed -i rep2.bed -w 1E-2 -s 1E-8");

            // Assert
            Assert.Contains("the following required arguments are missing: -r|--replicate.", msg);
            Assert.False(Environment.ExitCode == 0);
        }

        [Fact]
        public void ErrorIfASpecifiedFileIsMissing()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(false, "-i rep1.bed -i rep2.bed -r bio -w 1E-2 -s 1E-8");

            // Assert
            Assert.Contains("the following files are missing: rep1.bed; rep2.bed", msg);
            Assert.False(Environment.ExitCode == 0);
        }

        [Fact]
        public void AssertInformingPeaksCount()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run();

            // Assert
            Assert.Contains("  2\t", msg);
            Assert.Contains("  3\t", msg);
        }

        [Fact]
        public void AssertInformingMinPValue()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run();

            // Assert
            Assert.Contains("1.000E-005", msg);
            Assert.Contains("1.000E-007", msg);
        }

        [Fact]
        public void AssertInformingMaxPValue()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run();

            // Assert
            Assert.Contains("1.000E-003", msg);
            Assert.Contains("1.000E-002", msg);
        }

        [Fact]
        public void ReportRuntime()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run();

            // Assert
            Assert.Contains("Elapsed time: ", msg);
        }

        [Fact]
        public void SuccessfulAnalysis()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run();

            // Assert
            Assert.Contains("All processes successfully finished", msg);
            Assert.True(Environment.ExitCode == 0);
        }

        [Fact]
        public void CorrectAndSuccessfulAnalysis()
        {
            /// TODO: This is a big test as it does an end-to-end assertion 
            /// of many aspects. However, using some mock types this test 
            /// implementation can be significantly simplified. 
            /// 

            // Arrange
            string outputPath = "session_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff_", CultureInfo.InvariantCulture) + new Random().Next(100000, 999999).ToString();
            WriteSampleFiles(out string rep1Filename, out string rep2Filename, out string culture);

            ParserConfig cols = new ParserConfig() { Culture = culture };
            var configFilename = Path.GetTempPath() + Guid.NewGuid().ToString() + ".json";
            using (StreamWriter w = new StreamWriter(configFilename))
                w.WriteLine(JsonConvert.SerializeObject(cols));

            string args = $"-i {rep1Filename} -i {rep2Filename} -r bio -w 1e-2 -s 1e-4 -p {configFilename} -o {outputPath}";


            // Act
            string output;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(args.Split(' '));
                output = sw.ToString();
            }

            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);

            // Assert
            Assert.Contains("All processes successfully finished", output);
            using (var reader = new StreamReader(Directory.GetFiles(outputPath, "*ConsensusPeaks.bed")[0]))
            {
                Assert.Equal("chr\tstart\tstop\tname\t-1xlog10(p-value)", reader.ReadLine());
                Assert.Equal("chr1\t4\t20\tMSPC_Peak_2\t25.219", reader.ReadLine());
                Assert.Equal("chr1\t25\t45\tMSPC_Peak_1\t12.97", reader.ReadLine());
                Assert.Null(reader.ReadLine());
            }

            var dirs = Directory.GetDirectories(outputPath);
            Assert.True(dirs.Length == 2);

            Assert.True(Directory.GetFiles(dirs[0]).Length == 14);
            string line;
            using (var reader = new StreamReader(Directory.GetFiles(dirs[0], "*TruePositive.bed")[0]))
            {
                Assert.Equal("chr\tstart\tstop\tname\t-1xlog10(p-value)", reader.ReadLine());
                line = reader.ReadLine();
                Assert.True("chr1\t10\t20\tmspc_peak_1\t7.12" == line || "chr1\t4\t12\tmspc_peak_3\t19.9" == line);
                line = reader.ReadLine();
                Assert.True("chr1\t25\t35\tmspc_peak_2\t5.507" == line || "chr1\t30\t45\tmspc_peak_4\t9" == line);
                Assert.Null(reader.ReadLine());
            }

            Assert.True(Environment.ExitCode == 0);
            Assert.True(Directory.GetFiles(dirs[1]).Length == 14);
            using (var reader = new StreamReader(Directory.GetFiles(dirs[1], "*TruePositive.bed")[0]))
            {
                Assert.Equal("chr\tstart\tstop\tname\t-1xlog10(p-value)", reader.ReadLine());
                line = reader.ReadLine();
                Assert.True("chr1\t10\t20\tmspc_peak_1\t7.12" == line || "chr1\t4\t12\tmspc_peak_3\t19.9" == line);
                line = reader.ReadLine();
                Assert.True("chr1\t25\t35\tmspc_peak_2\t5.507" == line || "chr1\t30\t45\tmspc_peak_4\t9" == line);
                Assert.Null(reader.ReadLine());
            }

            // Clean up
            Directory.Delete(outputPath, true);
            File.Delete(rep1Filename);
            File.Delete(rep2Filename);
        }

        [Theory]
        [InlineData("-?")]
        [InlineData("-h")]
        [InlineData("--help")]
        public void ShowsHelpText(string template)
        {
            // Arrange
            string msg;
            string n = Environment.NewLine;
            string expected =
                $"{n}{n}Usage: MSPC CLI [options]{n}{n}Options:" +
                $"{n}  -? | -h | --help                      Show help information" +
                $"{n}  -v | --version                        Show version information" +
                $"{n}  -i | --input <value>                  Input samples to be processed in Browser Extensible Data (BED) Format." +
                $"{n}  -f | --folder-input <value>           Sets a path to a folder that all its containing files in BED format are considered as inputs." +
                $"{n}  -r | --replicate <value>              Sets the replicate type of samples. Possible values are: {{ Bio, Biological, Tec, Technical }}" +
                $"{n}  -w | --tauW <value>                   Sets weak threshold. All peaks with p-values higher than this value are considered as weak peaks." +
                $"{n}  -s | --tauS <value>                   Sets stringency threshold. All peaks with p-values lower than this value are considered as stringent peaks." +
                $"{n}  -g | --gamma <value>                  Sets combined stringency threshold. The peaks with their combined p-values satisfying this threshold will be confirmed." +
                $"{n}  -a | --alpha <value>                  Sets false discovery rate of Benjamini–Hochberg step-up procedure." +
                $"{n}  -c <value>                            Sets minimum number of overlapping peaks before combining p-values." +
                $"{n}  -m | --multipleIntersections <value>  When multiple peaks from a sample overlap with a given peak, this argument defines which of the peaks to be considered: the one with lowest p-value, or the one with highest p-value? Possible values are: {{ Lowest, Highest }}" +
                $"{n}  -d | --degreeOfParallelism <value>    Set the degree of parallelism." +
                $"{n}  -p | --parser <value>                 Sets the path to the parser configuration file in JSON." +
                $"{n}  -o | --output <value>                 Sets a path where analysis results should be persisted." +
                $"{n}  --excludeHeader                       If provided, MSPC will not add a header line to its output." +
                $"{n}" +
                $"{n}Documentation:\thttps://genometric.github.io/MSPC/" +
                $"{n}Source Code:\thttps://github.com/Genometric/MSPC" +
                $"{n}Publications:\thttps://genometric.github.io/MSPC/publications" +
                $"{n}";

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(template: template);

            // Assert
            Assert.Contains(expected, msg);
            Assert.True(Environment.ExitCode == 0);
        }

        [Theory]
        [InlineData("-v")]
        [InlineData("--version")]
        public void ShowVersion(string template)
        {
            // Arrange
            string msg;
            string expected = $"{Environment.NewLine}Version ";

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(template: template);

            // Assert
            Assert.Contains(expected, msg);
            Assert.True(Environment.ExitCode == 0);
        }

        [Fact]
        public void HintHowToUseHelpWhenAnExceptionOccurs()
        {
            // Arrange
            string msg;
            string expected = "You may run mspc with either of [-? | -h | --help] tags for help.";

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(template: "");

            // Assert
            Assert.Contains(expected, msg);
            Assert.False(Environment.ExitCode == 0);
        }

        [Fact]
        public void ExportToGivenOutputPath()
        {
            // Arrange
            var output_path = "mspc_test_output_" + new Random().Next(10000, 99999);
            WriteSampleFiles(out string rep1Filename, out string rep2Filename, out _);

            // Act
            Program.Main($"-i {rep1Filename} -i {rep2Filename} -r bio -w 1E-2 -s 1E-8 -o {output_path}".Split(' '));

            // Assert
            Assert.True(Directory.Exists(output_path));
            // There should be three files in the output directory:
            // Log file; Consensus peaks in BED and MSPC format.
            Assert.Equal(3, Directory.GetFiles(output_path).Length);
            // There should be two folders in the output directory 
            // one per each input replicate.
            Assert.Equal(2, Directory.GetDirectories(output_path).Length);

            rep1Filename = Path.Combine(output_path, Path.GetFileNameWithoutExtension(rep1Filename));
            rep2Filename = Path.Combine(output_path, Path.GetFileNameWithoutExtension(rep2Filename));
            Assert.Contains(rep1Filename, Directory.GetDirectories(output_path));
            Assert.Contains(rep2Filename, Directory.GetDirectories(output_path));
        }

        [Fact]
        public void TrailingSlashIsRemovedFromOutputFolder()
        {
            // Arrange
            var output_path = "mspc_test_output_" + new Random().Next(10000, 99999);
            Directory.CreateDirectory(output_path);
            File.Create(output_path + Path.DirectorySeparatorChar + "test").Dispose();
            WriteSampleFiles(out string rep1Filename, out string rep2Filename, out _);

            // Act
            Program.Main($"-i {rep1Filename} -i {rep2Filename} -r bio -w 1E-2 -s 1E-8 -o {output_path + Path.DirectorySeparatorChar}".Split(' '));
            output_path += "_0";

            // Assert
            Assert.True(Directory.Exists(output_path));
            // There should be three files in the output directory:
            // Log file; Consensus peaks in BED and MSPC format.
            Assert.Equal(3, Directory.GetFiles(output_path).Length);
            // There should be two folders in the output directory 
            // one per each input replicate.
            Assert.Equal(2, Directory.GetDirectories(output_path).Length);
        }

        [Fact]
        public void GenerateOutputPathIfNotGiven()
        {
            // Arrange
            var o = new Orchestrator();

            // Act
            o.Orchestrate("-i rep1.bed -i rep2.bed -r bio -w 1E-2 -s 1E-8".Split(' '));

            // Assert
            Assert.True(!string.IsNullOrEmpty(o.OutputPath) && !string.IsNullOrWhiteSpace(o.OutputPath));
        }

        [Fact]
        public void AppendNumberToGivenPathIfAlreadyExists()
        {
            // Arrange
            var o = new Orchestrator();
            string baseName = @"TT" + new Random().Next().ToString();
            var dirs = new List<string>
            {
                baseName, baseName + "_0", baseName + "_1"
            };

            foreach (var dir in dirs)
            {
                Directory.CreateDirectory(dir);
                File.Create(dir + Path.DirectorySeparatorChar + "test").Dispose();
            }

            // Act
            o.Orchestrate($"-i rep1.bed -i rep2.bed -r bio -w 1E-2 -s 1E-8 -o {dirs[0]}".Split(' '));

            // Assert
            Assert.Equal(o.OutputPath, dirs[0] + "_2");

            // Clean up
            o.Dispose();
            foreach (var dir in dirs)
                Directory.Delete(dir, true);
            Directory.Delete(dirs[0] + "_2", true);
        }

        [Fact]
        public void ExeExceptionWritingToIllegalPath()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(sessionPath: _illegalPath);

            // Assert
            Assert.True(
                msg.Contains("Illegal characters in path.") ||
                msg.Contains("The filename, directory name, or volume label syntax is incorrect") ||
                msg.Contains($"Cannot ensure the given output path `{_illegalPath}`: Read-only file system"));
            Assert.False(Environment.ExitCode == 0);
        }

        [Fact]
        public void ReuseExistingLogger()
        {
            // Arrange
            List<string> messages;

            // Act
            using (var tmpMspc = new TmpMspc())
                messages = tmpMspc.FailRun();

            // Assert
            Assert.Contains(messages, x => x.Contains("the following files are missing: rep1; rep2"));
            Assert.Contains(messages, x => x.Contains("the following required arguments are missing: (-i|--input or -f|--folder-input)."));
        }

        [Fact]
        public void DontReportSuccessfullyFinishedIfExitedAfterAnError()
        {
            // Arrange
            List<string> messages;

            // Act
            using (var tmpMspc = new TmpMspc())
                messages = tmpMspc.FailRun();

            // Assert
            Assert.DoesNotContain(messages, x => x.Contains("All processes successfully finished"));
        }

        [Fact]
        public void WriteOutputPathExceptionToLoggerIfAvailable()
        {
            // Arrange
            List<string> messages;

            // Act
            using (var tmpMspc = new TmpMspc())
                messages = tmpMspc.FailRun(template2: $"-i rep1 -i rep2 -o {_illegalPath} -r bio -s 1e-8 -w 1e-4");

            // Assert
            Assert.Contains(messages, x => x.Contains("the following files are missing: rep1; rep2"));
            Assert.Contains(
                messages,
                x => x.Contains("Illegal characters in path.") ||
                x.Contains("The filename, directory name, or volume label syntax is incorrect") ||
                x.Contains($"Cannot ensure the given output path `{_illegalPath}`: Read-only file system"));
            Assert.False(Environment.ExitCode == 0);
        }

        [Fact]
        public void CaptureExporterExceptions()
        {
            // Arrange
            string message;

            // Act
            using (var tmpMspc = new TmpMspc())
                message = tmpMspc.Run(new MExporter());

            // Assert
            Assert.Contains("The method or operation is not implemented.", message);
            Assert.DoesNotContain("All processes successfully finished", message);
            Assert.False(Environment.ExitCode == 0);
        }

        [Fact]
        public void CaptureExceptionsRaisedCreatingLogger()
        {
            // Arrange
            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

            var o = new Orchestrator
            {
                loggerTimeStampFormat = "yyyyMMdd_HHmmssffffffffffff"
            };

            // Act
            string output;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                o.Orchestrate(string.Format("-i {0} -i {1} -r bio -w 1e-2 -s 1e-4", rep1Path, rep2Path).Split(' '));
                output = sw.ToString();
            }
            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);

            // Assert
            Assert.Contains("Input string was not in a correct format.", output);
            Assert.False(Environment.ExitCode == 0);
        }

        [Fact]
        public void ReadDataAccordingToParserConfig()
        {
            // Arrange
            ParserConfig cols = new ParserConfig()
            {
                Chr = 0,
                Left = 3,
                Right = 4,
                Name = 1,
                Strand = 2,
                Summit = 6,
                Value = 5,
                DefaultValue = 1.23E-45,
                PValueFormat = PValueFormats.minus1_Log10_pValue,
                DropPeakIfInvalidValue = false,
            };
            var path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "MSPCTests_" + new Random().NextDouble().ToString();
            using (StreamWriter w = new StreamWriter(path))
                w.WriteLine(JsonConvert.SerializeObject(cols));

            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

            FileStream stream = File.Create(rep1Path);
            using (StreamWriter writter = new StreamWriter(stream))
                writter.WriteLine("chr1\tMSPC_PEAK\t.\t10\t20\t16\t15");

            stream = File.Create(rep2Path);
            using (StreamWriter writter = new StreamWriter(stream))
                writter.WriteLine("chr1\tMSPC_PEAK\t.\t15\t25\tEEE\t20");

            // Act
            string msg;
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(createSample: false, template: string.Format("-i {0} -i {1} -p {2} -r bio -w 1e-2 -s 1e-4", rep1Path, rep2Path, path));

            // Assert
            Assert.Contains("1.000E-016", msg);
            Assert.Contains("1.230E-045", msg);
        }

        [Fact]
        public void CaptureExceptionReadingFile()
        {
            // Arrange
            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

            new StreamWriter(rep1Path).Close();
            new StreamWriter(rep2Path).Close();

            // Lock the file so the parser cannot access it.
            var fs = new FileStream(path: rep1Path, mode: FileMode.OpenOrCreate, access: FileAccess.Write, share: FileShare.None);

            // Act
            string logFile;
            string path;
            using (var o = new Orchestrator())
            {
                o.Orchestrate(string.Format("-i {0} -i {1} -r bio -w 1e-2 -s 1e-4", rep1Path, rep2Path).Split(' '));
                logFile = o.LogFile;
                path = o.OutputPath;
            }

            string line;
            var messages = new List<string>();
            using (var reader = new StreamReader(logFile))
                while ((line = reader.ReadLine()) != null)
                    messages.Add(line);
            fs.Close();

            // Assert
            Assert.Contains(messages, x =>
            x.Contains("The process cannot access the file") &&
            x.Contains("because it is being used by another process."));
            Assert.False(Environment.ExitCode == 0);

            // Clean up
            File.Delete(rep1Path);
            File.Delete(rep2Path);
            Directory.Delete(path, true);
        }

        [Fact]
        public void LogFullPathOfInputFile()
        {
            // Arrange
            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

            new StreamWriter(rep1Path).Close();
            new StreamWriter(rep2Path).Close();

            // Act
            string logFile;
            string path;
            using (var o = new Orchestrator())
            {
                o.Orchestrate(string.Format("-i {0} -i {1} -r bio -w 1e-2 -s 1e-4", rep1Path, rep2Path).Split(' '));
                logFile = o.LogFile;
                path = o.OutputPath;
            }

            string line;
            var messages = new List<string>();
            using (var reader = new StreamReader(logFile))
                while ((line = reader.ReadLine()) != null)
                    messages.Add(line);

            // Assert
            Assert.True(messages.FindAll(x => x.Contains(rep1Path)).Count == 1);
            Assert.True(messages.FindAll(x => x.Contains(rep2Path)).Count == 1);

            // Clean up
            File.Delete(rep1Path);
            File.Delete(rep2Path);
            Directory.Delete(path, true);
        }

        [Fact]
        public void WriteTablesHeaderInLogFile()
        {
            // Arrange
            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

            new StreamWriter(rep1Path).Close();
            new StreamWriter(rep2Path).Close();

            // Act
            string logFile;
            string path;
            using (var o = new Orchestrator())
            {
                o.Orchestrate(string.Format("-i {0} -i {1} -r bio -w 1e-2 -s 1e-4", rep1Path, rep2Path).Split(' '));
                logFile = o.LogFile;
                path = o.OutputPath;
            }

            string line;
            var messages = new List<string>();
            using (var reader = new StreamReader(logFile))
                while ((line = reader.ReadLine()) != null)
                    messages.Add(line);

            // Assert
            Assert.True(
                messages.FindAll(
                    x => x.Contains("Filename\tRead peaks#\tMin p-value\tMean p-value\tMax p-value\t"))
                .Count == 1);

            Assert.True(
                messages.FindAll(
                    x => x.Contains(
                        "Filename\tRead peaks#\tBackground\t    Weak\tStringent" +
                        "\tConfirmed\tDiscarded\tTruePositive\tFalsePositive\t"))
                .Count == 1);

            // Clean up
            File.Delete(rep1Path);
            File.Delete(rep2Path);
            Directory.Delete(path, true);
        }

        [Theory]
        [InlineData("300%", "2")]
        [InlineData("-300%", "1")]
        public void WarningDisplayedForInvalidC(string c, string expected)
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(template: $"-i sample_1 -i sample_2 -r bio -w 1E-2 -s 1E-8 -c {c}");

            // Assert
            Assert.Contains($"Invalid `C={c}`, it is set to `C={expected}`.", msg);
        }

        [Fact]
        public void ExportPathIsReportedInLogs()
        {
            // Arrange
            var outputPath = "mspc_test_output_" + new Random().Next(10000, 99999);
            WriteSampleFiles(out string rep1Filename, out string rep2Filename, out _);

            // Act
            string logFile;
            using (var o = new Orchestrator())
            {
                o.Orchestrate($"-i {rep1Filename} -i {rep2Filename} -r bio -w 1e-2 -s 1e-4 -o {outputPath}".Split(' '));
                logFile = o.LogFile;
            }

            string line;
            var messages = new List<string>();
            using (var reader = new StreamReader(logFile))
                while ((line = reader.ReadLine()) != null)
                    messages.Add(line);

            // Assert
            var outputPathLogMessage = messages.FindAll(x => x.Contains("INFO \tExport Directory: "));
            Assert.True(outputPathLogMessage.Count == 1);

            var rx = new Regex(".*Export Directory: (.*)");
            var loggedOutputPath = rx.Match(outputPathLogMessage[0]).Groups[1].Value;
            Assert.True(Path.IsPathRooted(loggedOutputPath));
        }

        [Fact]
        public void ExportPathIsReportedInConsole()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run();

            // Assert
            var rx = new Regex($".*Export Directory: (.*){Environment.NewLine}.*");
            var loggedOutputPath = rx.Match(msg).Groups[1].Value.Trim();

            var isAValidPath = TryGetFullPath(loggedOutputPath, out _);
            Assert.True(isAValidPath);
        }

        private static bool TryGetFullPath(string path, out string result)
        {
            result = string.Empty;
            if (string.IsNullOrWhiteSpace(path))
                return false;

            try
            {
                result = Path.GetFullPath(path);
                return true;
            }
            catch (Exception e) when (e is ArgumentException or
                                           SecurityException or 
                                           NotSupportedException or 
                                           PathTooLongException)
            {
                return false;
            }
        }
    }
}
