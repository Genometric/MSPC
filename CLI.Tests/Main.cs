﻿using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.CLI.Tests.MockTypes;
using Genometric.MSPC.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
        /// operating systems. 
        /// 
        /// Reg. the value for non-Windows env: 
        /// Since the trailing path separator is trimmed 
        /// in Orchestrator from every given path, hence 
        /// the underscore (_) char is added to keep 
        /// forward slash (/) in the path.
        /// </summary>
        private static string IllegalPath
        {
            get
            {
                return OperatingSystem.IsWindows() ? "C:\\*<>*\\//" : "/_";
            }
        }

        public static string GetFilename(string prefix, string postfix = ".bed")
        {
            var rnd = new Random();
            return
                $"{prefix}" +
                $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}" +
                $"{rnd.Next(1000, 9999)}" +
                $"{postfix}";
        }

        private static void WriteSampleFiles(
            out string rep1Filename,
            out string rep2Filename,
            out string culture)
        {
            culture = "fa-IR";
            double pValue;
            rep1Filename = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            using (var writter = new StreamWriter(rep1Filename))
            {
                pValue = 7.12;
                writter.WriteLine(
                    "chr1\t10\t20\tmspc_peak_1\t" +
                    pValue.ToString(CultureInfo.CreateSpecificCulture(culture)));

                pValue = 5.5067;
                writter.WriteLine(
                    "chr2\t25\t35\tmspc_peak_2\t" +
                    pValue.ToString(CultureInfo.CreateSpecificCulture(culture)));
            }

            rep2Filename = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            using (var writter = new StreamWriter(rep2Filename))
            {
                pValue = 19.9;
                writter.WriteLine(
                    "chr1\t4\t12\tmspc_peak_3\t" +
                    pValue.ToString(CultureInfo.CreateSpecificCulture(culture)));

                pValue = 8.999999;
                writter.WriteLine(
                    "chr2\t30\t45\tmspc_peak_4\t" +
                    pValue.ToString(CultureInfo.CreateSpecificCulture(culture)));
            }
        }

        [Fact]
        public void ErrorIfLessThanTwoSamplesAreGiven()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run(false, $"-i {tmpMspc.TmpSamples[0]} -r bio -w 1E-2 -s 1E-8");

            // Assert
            Assert.Contains("At least two samples are required, 1 given.", x.ConsoleOutput);
            Assert.False(x.ExitCode == 0);
        }

        [Fact]
        public void ErrorIfARequiredArgumentIsMissing()
        {
            // Arrange
            Result x;
            var rep1 = GetFilename("rep1");
            var rep2 = GetFilename("rep2");

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run(false, $"-i {rep1} -i {rep2} -w 1E-2 -s 1E-8");

            // Assert
            Assert.Contains("Option '--replicate' is required.", x.ConsoleOutput);
            Assert.False(x.ExitCode == 0);
        }

        [Fact]
        public void ErrorIfASpecifiedFileIsMissing()
        {
            // Arrange
            Result x;
            var rep1 = GetFilename("rep1");
            var rep2 = GetFilename("rep2");

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run(false, $"-i {rep1} -i {rep2} -r bio -w 1E-2 -s 1E-8");

            // Assert
            Assert.Contains(
                $"The following files are missing or inaccessible." +
                $"{Environment.NewLine}- {rep1}" +
                $"{Environment.NewLine}- {rep2}",
                x.ConsoleOutput);
            Assert.False(x.ExitCode == 0);
        }

        [Fact]
        public void AssertInformingPeaksCount()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run();

            // Assert
            Assert.Contains("  2\t", x.ConsoleOutput);
            Assert.Contains("  3\t", x.ConsoleOutput);
        }

        [Fact]
        public void AssertInformingMinPValue()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run();

            // Assert
            Assert.Contains("1.000E-005", x.ConsoleOutput);
            Assert.Contains("1.000E-007", x.ConsoleOutput);
        }

        [Fact]
        public void AssertInformingMaxPValue()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run();

            // Assert
            Assert.Contains("1.000E-003", x.ConsoleOutput);
            Assert.Contains("1.000E-002", x.ConsoleOutput);
        }

        [Fact]
        public void ReportRuntime()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run();

            // Assert
            Assert.Contains("Elapsed time: ", x.ConsoleOutput);
        }

        [Fact]
        public void SuccessfulAnalysis()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run();

            // Assert
            Assert.Contains(
                "All processes successfully finished",
                x.ConsoleOutput);
            Assert.True(x.ExitCode == 0);
        }

        [Fact]
        public void CorrectlyReportTheNumberOfConsensusPeaksAsASumOfAllPeaksInAllChrsAndStrands()
        {
            // Arrange
            var outputPath = Environment.CurrentDirectory;
            string rep1Path = Path.Join(outputPath, GetFilename("rep1"));
            string rep2Path = Path.Join(outputPath, GetFilename("rep2"));

            FileStream stream = File.Create(rep1Path);
            using (StreamWriter writter = new(stream))
            {
                writter.WriteLine("chr1\t10\t20\tmspc_peak_01\t44\t+");
                writter.WriteLine("chr2\t65\t80\tmspc_peak_02\t55\t-");
            }

            stream = File.Create(rep2Path);
            using (StreamWriter writter = new(stream))
            {
                writter.WriteLine("chr1\t11\t18\tmspc_peak_03\t55\t+");
                writter.WriteLine("chr2\t60\t70\tmspc_peak_04\t66\t-");
                writter.WriteLine("chr2\t90\t99\tmspc_peak_05\t88\t+");
            }

            var template = $"-i {rep1Path} {rep2Path} -c 1 -r bio -w 1e-4 -s 1e-8 -o {outputPath}";
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run(
                    template: template, 
                    appendOutputOption: false, 
                    createSample: false);

            // Assert
            Assert.Contains(
                $".::.Consensus Peaks Count.::.{Environment.NewLine}" +
                $"3{Environment.NewLine}",
                x.ConsoleOutput);
            Assert.True(x.ExitCode == 0);
        }

        [Fact]
        public void CorrectAndSuccessfulAnalysis()
        {
            /// TODO: This is a big test as it does an end-to-end assertion 
            /// of many aspects. However, using some mock types this test 
            /// implementation can be significantly simplified. 
            /// 

            // Arrange
            string outputPath =
                "session_" +
                DateTime.Now.ToString(
                    "yyyyMMdd_HHmmssfff_",
                    CultureInfo.InvariantCulture) +
                new Random().Next(100000, 999999).ToString();

            WriteSampleFiles(
                out string rep1Filename,
                out string rep2Filename,
                out string culture);

            var cols = new ParserConfig() { Culture = culture };
            var configFilename = Path.GetTempPath() + Guid.NewGuid().ToString() + ".json";
            using (var w = new StreamWriter(configFilename))
                w.WriteLine(JsonConvert.SerializeObject(cols));

            string args =
                $"-i {rep1Filename} " +
                $"-i {rep2Filename} " +
                $"-r bio -w 1e-2 -s 1e-4 " +
                $"-p {configFilename} " +
                $"-o {outputPath}";

            // Act
            string output;
            using (var sw = new StringWriter())
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
            string[] columns;
            using (var reader = new StreamReader(Directory.GetFiles(outputPath, "*ConsensusPeaks.bed")[0]))
            {
                Assert.Equal("chr\tstart\tstop\tname\t-1xlog10(p-value)\tstrand", reader.ReadLine());

                columns = reader.ReadLine().Split('\t');
                Assert.True(
                    columns[0] == "chr1" && 
                    columns[1] == "4" && 
                    columns[2] == "20" && 
                    columns[3].StartsWith("MSPC_Peak_") && 
                    columns[4] == "25.219" && 
                    columns[5] == ".");

                columns = reader.ReadLine().Split('\t');
                Assert.True(
                    columns[0] == "chr2" &&
                    columns[1] == "25" &&
                    columns[2] == "45" &&
                    columns[3].StartsWith("MSPC_Peak_") &&
                    columns[4] == "12.97" &&
                    columns[5] == ".");
                Assert.Null(reader.ReadLine());
            }

            var dirs = Directory.GetDirectories(outputPath);
            Assert.True(dirs.Length == 2);

            Assert.True(Directory.GetFiles(dirs[0]).Length == 14);
            using (var reader = new StreamReader(Directory.GetFiles(dirs[0], "*TruePositive.bed")[0]))
            {
                Assert.Equal("chr\tstart\tstop\tname\t-1xlog10(p-value)\tstrand", reader.ReadLine());

                columns = reader.ReadLine().Split('\t');
                Assert.True(
                    columns[0] == "chr1" &&
                    (columns[1] == "10" || columns[1] == "4") &&
                    (columns[2] == "20" || columns[2] == "12") &&
                    columns[3].ToLower().StartsWith("mspc_peak_") &&
                    (columns[4] == "7.12" || columns[4] == "19.9") &&
                    columns[5] == ".");

                columns = reader.ReadLine().Split('\t');
                Assert.True(
                    columns[0] == "chr2" &&
                    (columns[1] == "25" || columns[1] == "30") &&
                    (columns[2] == "35" || columns[2] == "45") &&
                    columns[3].ToLower().StartsWith("mspc_peak_") &&
                    (columns[4] == "5.507" || columns[4] == "9") &&
                    columns[5] == ".");

                Assert.Null(reader.ReadLine());
            }

            Assert.True(Environment.ExitCode == 0);
            Assert.True(Directory.GetFiles(dirs[1]).Length == 14);
            using (var reader = new StreamReader(Directory.GetFiles(dirs[1], "*TruePositive.bed")[0]))
            {
                Assert.Equal("chr\tstart\tstop\tname\t-1xlog10(p-value)\tstrand", reader.ReadLine());

                columns = reader.ReadLine().Split('\t');
                Assert.True(
                    columns[0] == "chr1" &&
                    (columns[1] == "10" || columns[1] == "4") &&
                    (columns[2] == "20" || columns[2] == "12") &&
                    columns[3].ToLower().StartsWith("mspc_peak_") &&
                    (columns[4] == "7.12" || columns[4] == "19.9") &&
                    columns[5] == ".");

                columns = reader.ReadLine().Split('\t');
                Assert.True(
                    columns[0] == "chr2" &&
                    (columns[1] == "25" || columns[1] == "30") &&
                    (columns[2] == "35" || columns[2] == "45") &&
                    columns[3].ToLower().StartsWith("mspc_peak_") &&
                    (columns[4] == "5.507" || columns[4] == "9") &&
                    columns[5] == ".");

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
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run(template: template);

            // Assert
            Assert.Contains("Description:", x.ConsoleOutput);
            Assert.Contains("Using combined evidence from replicates to evaluate ChIP-seq and single-cell peaks.", x.ConsoleOutput);
            Assert.Contains("Documentation: https://genometric.github.io/MSPC/", x.ConsoleOutput);
            Assert.Contains("Source Code:   https://github.com/Genometric/MSPC", x.ConsoleOutput);
            Assert.Contains("Publications:  https://genometric.github.io/MSPC/publications", x.ConsoleOutput);
            Assert.Contains("Usage:", x.ConsoleOutput);
            Assert.Contains("testhost [options]", x.ConsoleOutput); // `testhost` since it is running the test env, otherwise it will correctly show `mspc`.
            Assert.Contains("Options:", x.ConsoleOutput);
            Assert.Contains("-i, --input <input> (REQUIRED)", x.ConsoleOutput);
            Assert.Contains("-r, --replicate <bio|biological|tec|technical> (REQUIRED)", x.ConsoleOutput);
            Assert.Contains("-s, --tauS <tauS> (REQUIRED)", x.ConsoleOutput);
            Assert.Contains("-w, --tauW <tauW> (REQUIRED)", x.ConsoleOutput);
            Assert.Contains("-g, --gamma <gamma>         ", x.ConsoleOutput);
            Assert.Contains("-a, --alpha <alpha>         ", x.ConsoleOutput);
            Assert.Contains("p procedure. [default: 0.05]", x.ConsoleOutput);
            Assert.Contains("-m, --multipleIntersections <highest|lowest>", x.ConsoleOutput);
            Assert.Contains("-d, --degreeOfParallelism <degreeOfParallelism> ", x.ConsoleOutput);
            Assert.Contains("--excludeHeader          ", x.ConsoleOutput);
            Assert.Contains("--version", x.ConsoleOutput);
            Assert.Contains("Show version information", x.ConsoleOutput);
            Assert.Contains("-?, -h, --help", x.ConsoleOutput);
            Assert.Contains("Show help and usage information", x.ConsoleOutput);
            Assert.True(x.ExitCode == 0);
        }

        [Theory]
        [InlineData("--version")]
        public void ShowVersion(string template)
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run(template: template, appendOutputOption: false);

            // The displayed version is the version of the test env, not the
            // app. However, the app is executed, it correctly shows the version of mspc.

            // Assert
            Assert.True(x.ConsoleOutput.Split('.').Length == 3);
            Assert.True(x.ExitCode == 0);
        }

        // This feature is not currently easily possible with system.commandline.
        //[Fact]
        /*public void HintHowToUseHelpWhenAnExceptionOccurs()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run(createSample: false, template: "", appendOutputOption: false);

            // Assert
            Assert.True(
                x.ConsoleOutput.Contains("-?, -h, --help") &&
                x.ConsoleOutput.Contains("Show help and usage information\r\n") &&
                x.ConsoleOutput.Contains("Documentation:\thttps://genometric.github.io/MSPC/\r\n"));
            Assert.False(x.ExitCode == 0);
        }*/

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
            var console = new MockConsole();
            var orchestrator = new Orchestrator(console);
            orchestrator.Invoke($"-i {rep1Filename} -i {rep2Filename} -r bio -w 1E-2 -s 1E-8 -o {output_path + Path.DirectorySeparatorChar}".Split(' '));
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
            var console = new MockConsole();
            var o = new Orchestrator(console);

            // Act
            using (var mspc = new TmpMspc())
                o.Invoke($"-i {mspc.TmpSamples[0]} -i {mspc.TmpSamples[1]} -r bio -w 1E-2 -s 1E-8".Split(' '));

            // Assert
            Assert.True(
                !string.IsNullOrEmpty(o.Config.OutputPath) &&
                !string.IsNullOrWhiteSpace(o.Config.OutputPath));
        }

        [Fact]
        public void AppendNumberToGivenPathIfAlreadyExists()
        {
            // Arrange
            var console = new MockConsole();
            var o = new Orchestrator(console);
            string baseName = @"TT" + new Random().Next().ToString();
            var dirs = new List<string>
            {
                baseName, baseName + "_0", baseName + "_1"
            };

            foreach (var dir in dirs)
            {
                Directory.CreateDirectory(dir);
                File.Create(Path.Join(dir, "test")).Dispose();
            }

            // Act
            using (var tmpMspc = new TmpMspc())
            {
                var samples = tmpMspc.TmpSamples;
                o.Invoke($"-i {samples[0]} -i {samples[1]} -r bio -w 1E-2 -s 1E-8 -o {dirs[0]}".Split(' '));
            }

            // Assert
            // Gets the directory name from full path.
            var createdDir = Path.GetFileName(o.Config.OutputPath);
            Assert.Equal(createdDir, dirs[0] + "_2");

            // Clean up
            o.Dispose();
            foreach (var dir in dirs)
                Directory.Delete(dir, true);
            Directory.Delete(dirs[0] + "_2", true);
        }

        [Fact]
        public void RaiseExceptionWritingToIllegalPath()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run(sessionPath: IllegalPath);

            // Assert
            Assert.True(
                x.ConsoleOutput.Contains("Illegal characters in path.") ||
                x.ConsoleOutput.Contains("The filename, directory name, or volume label syntax is incorrect") ||
                x.ConsoleOutput.Contains($"Cannot ensure the given output path `{IllegalPath}`: Read-only file system") ||
                x.ConsoleOutput.Contains($"Cannot ensure the given output path `{IllegalPath}`: Access to the path '/_' is denied"));
            Assert.True(x.ExitCode != 0);
        }

        [Fact]
        public void ReuseExistingLogger()
        {
            // Arrange
            Result x;
            var rep1 = GetFilename("rep1");
            var rep2 = GetFilename("rep2");

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.FailRun(
                    template2: $"-i {rep1} -i {rep2} -r bio -s 1e-8 -w 1e-4");

            // Assert
            Assert.Contains("The following files are missing or inaccessible.", x.ConsoleOutput);
            Assert.Contains($"{Environment.NewLine}- {rep1}", x.ConsoleOutput);
            Assert.Contains($"{Environment.NewLine}- {rep2}", x.ConsoleOutput);
            Assert.Contains("Option '--input' is required.", x.ConsoleOutput);
        }

        [Fact]
        public void DontReportSuccessfullyFinishedIfExitedAfterAnError()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.FailRun();

            // Assert
            Assert.DoesNotContain(
                x.ConsoleOutput,
                "All processes successfully finished");
        }

        [Fact]
        public void WriteOutputPathExceptionToLoggerIfAvailable()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.FailRun(template:
                    $"-i {tmpMspc.TmpSamples[0]} " +
                    $"-i {tmpMspc.TmpSamples[1]} " +
                    $"-o {IllegalPath} " +
                    $"-r bio -s 1e-8 -w 1e-4");

            // Assert
            Assert.DoesNotContain("All processes successfully finished.", x.ConsoleOutput);
            Assert.True(
                x.ConsoleOutput.Contains("Illegal characters in path.") ||
                x.ConsoleOutput.Contains($"Cannot ensure the given output path `{IllegalPath}`") ||
                x.ConsoleOutput.Contains("The filename, directory name, or volume label syntax is incorrect") ||
                x.ConsoleOutput.Contains($"Cannot ensure the given output path `{IllegalPath}`: Read-only file system") ||
                x.ConsoleOutput.Contains($"Cannot ensure the given output path `{IllegalPath}`: Access to the path '/_' is denied"));
            Assert.False(x.ExitCode == 0);
        }

        [Fact]
        public void CaptureExporterExceptions()
        {
            // Arrange
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run(new MExporter());

            // Assert
            Assert.Contains("The method or operation is not implemented.", x.ConsoleOutput);
            Assert.DoesNotContain("All processes successfully finished", x.ConsoleOutput);
            Assert.False(x.ExitCode == 0);
        }

        [Fact]
        public void CaptureExceptionsRaisedCreatingLogger()
        {
            // Arrange
            var _console = new MockConsole();
            var o = new Orchestrator(_console)
            {
                loggerTimeStampFormat = "yyyyMMdd_HHmmssffffffffffff"
            };

            // Act
            string output;
            using (var tmpMspc = new TmpMspc())
            {
                var samples = tmpMspc.TmpSamples;
                o.Invoke($"-i {samples[0]} -i {samples[1]} -r bio -w 1e-2 -s 1e-4".Split(' '));
            }
            output = _console.GetStdo();

            // Assert
            Assert.Contains("Input string was not in a correct format.", output);
            Assert.False(Environment.ExitCode == 0);
        }

        [Fact]
        public void ReadDataAccordingToParserConfig()
        {
            // Arrange
            var cols = new ParserConfig()
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
            var path = Path.Join(
                Environment.CurrentDirectory,
                "MSPCTests_" + new Random().NextDouble().ToString());

            using (var w = new StreamWriter(path))
                w.WriteLine(JsonConvert.SerializeObject(cols));

            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

            FileStream stream = File.Create(rep1Path);
            using (var writter = new StreamWriter(stream))
                writter.WriteLine("chr1\tMSPC_PEAK\t.\t10\t20\t16\t15");

            stream = File.Create(rep2Path);
            using (var writter = new StreamWriter(stream))
                writter.WriteLine("chr1\tMSPC_PEAK\t.\t15\t25\tEEE\t20");

            // Act
            Result x;
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run(
                    createSample: false,
                    template: $"-i {rep1Path} -i {rep2Path} -p {path} -r bio -w 1e-2 -s 1e-4");

            // Assert
            Assert.Contains("1.000E-016", x.ConsoleOutput);
            Assert.Contains("1.230E-045", x.ConsoleOutput);
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
            var fs = new FileStream(
                path: rep1Path,
                mode: FileMode.OpenOrCreate,
                access: FileAccess.Write,
                share: FileShare.None);

            // Act
            string logFile;
            string path;
            var console = new MockConsole();
            using (var o = new Orchestrator(console))
            {
                o.Invoke(($"-i {rep1Path} -i {rep2Path} -r bio -w 1e-2 -s 1e-4").Split(' '));
                logFile = o.LogFile;
                path = o.Config.OutputPath;
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
            var console = new MockConsole();
            using (var o = new Orchestrator(console))
            {
                o.Invoke(($"-i {rep1Path} -i {rep2Path} -r bio -w 1e-2 -s 1e-4").Split(' '));
                logFile = o.LogFile;
                path = o.Config.OutputPath;
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
            var console = new MockConsole();
            using (var o = new Orchestrator(console))
            {
                o.Invoke(($"-i {rep1Path} -i {rep2Path} -r bio -w 1e-2 -s 1e-4").Split(' '));
                logFile = o.LogFile;
                path = o.Config.OutputPath;
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

        [Fact]
        public void ExportPathIsReportedInLogs()
        {
            // Arrange
            var outputPath = "mspc_test_output_" + new Random().Next(10000, 99999);
            WriteSampleFiles(out string rep1Filename, out string rep2Filename, out _);

            // Act
            string logFile;
            var console = new MockConsole();
            using (var o = new Orchestrator(console))
            {
                o.Invoke($"-i {rep1Filename} -i {rep2Filename} -r bio -w 1e-2 -s 1e-4 -o {outputPath}".Split(' '));
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
            Result x;

            // Act
            using (var tmpMspc = new TmpMspc())
                x = tmpMspc.Run();

            // Assert
            var rx = new Regex($".*Export Directory: (.*){Environment.NewLine}.*");
            var loggedOutputPath = rx.Match(x.ConsoleOutput).Groups[1].Value.Trim();

            var isAValidPath = TryGetFullPath(loggedOutputPath, out _);
            Assert.True(isAValidPath);
        }

        [Fact]
        public void ReadProcessWriteStranded()
        {
            // Arrange
            Result x;

            // Act
            var tmpMspc = new TmpMspc();
            x = tmpMspc.Run(stranded: true);  // -w 1e-2 -s 1e-8 -r bio
            var outputs = new Dictionary<string, Dictionary<Attributes, Bed<Peak>>>();
            var outputPath = x.Config.OutputPath;
            foreach (var inputFilename in x.Config.InputFiles)
            {
                outputs.Add(inputFilename, new Dictionary<Attributes, Bed<Peak>>());
                var sampleFolder =
                    Directory.GetDirectories(
                        outputPath).First((x) =>
                        {
                            return
                                Path.GetFileName(x) ==
                                Path.GetFileNameWithoutExtension(inputFilename);
                        });

                foreach (var attr in Enum.GetValues<Attributes>())
                {
                    var bedParser = new BedParser(new BedColumns() { Strand = 5 })
                    {
                        PValueFormat = PValueFormats.minus1_Log10_pValue
                    };
                    outputs[inputFilename].Add(
                        attr,
                        bedParser.Parse(
                            Path.Join(sampleFolder, attr.ToString() + ".bed")));
                }
            }
            tmpMspc.Dispose();

            // Assert
            var r1 = outputs[x.Config.InputFiles[0]];
            var r2 = outputs[x.Config.InputFiles[1]];

            var r1c = r1[Attributes.Confirmed].Chromosomes["chr1"];
            var r2c = r2[Attributes.Confirmed].Chromosomes["chr1"];
            Assert.True(r1c.Strands.ContainsKey('+'));
            Assert.True(r1c.Strands.ContainsKey('-'));
            Assert.False(r1c.Strands.ContainsKey('.'));
            Assert.True(r2c.Strands.ContainsKey('+'));
            Assert.True(r2c.Strands.ContainsKey('-'));
            Assert.False(r2c.Strands.ContainsKey('.'));
            Assert.True(r1c.Strands['+'].Intervals.Count == 1);
            Assert.True(r1c.Strands['-'].Intervals.Count == 2);
            Assert.True(r2c.Strands['+'].Intervals.Count == 2);
            Assert.True(r2c.Strands['-'].Intervals.Count == 2);

            var r1d = r1[Attributes.Discarded].Chromosomes["chr1"];
            var r2d = r2[Attributes.Discarded].Chromosomes["chr1"];
            Assert.True(r1d.Strands.ContainsKey('+'));
            Assert.False(r1d.Strands.ContainsKey('-'));
            Assert.False(r1d.Strands.ContainsKey('.'));
            Assert.True(r2d.Strands.ContainsKey('+'));
            Assert.False(r2d.Strands.ContainsKey('-'));
            Assert.False(r2d.Strands.ContainsKey('.'));
            Assert.True(r1d.Strands['+'].Intervals.Count == 1);
            Assert.True(r2d.Strands['+'].Intervals.Count == 1);

            Assert.True(r1[Attributes.Background].Chromosomes.Count == 0);
            var r2b = r2[Attributes.Background].Chromosomes["chr1"];
            Assert.True(r2b.Strands.ContainsKey('+'));
            Assert.True(r2b.Strands.ContainsKey('-'));
            Assert.True(r2b.Strands['+'].Intervals.Count == 1);
            Assert.True(r2b.Strands['-'].Intervals.Count == 1);
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
