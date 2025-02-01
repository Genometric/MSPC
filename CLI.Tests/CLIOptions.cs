using Genometric.MSPC.CLI.CommandLineInterface;
using Genometric.MSPC.CLI.Logging;
using Genometric.MSPC.CLI.Tests.MockTypes;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class CliOptions : IDisposable
    {
        private const string _rep1 = "replicate_1.bed";
        private const string _rep2 = "replicate_2.bed";
        private const string _rep3 = "replicate_3.bed";
        private const string _p = null;
        private const double _tauW = 1E-2;
        private const double _tauS = 1E-9;
        private const double _gamma = 1E-12;
        private const float _alpha = 0.0005F;
        private const string _c = "2";
        private const string _m = "lowest";
        private const string _r = "bio";
        private const string _d = "4";

        private string _rep1Filename;
        private string _rep2Filename;
        private string _rep3Filename;
        private bool disposedValue;

        private static Cli GetCli(Action<CliConfig> handler)
        {
            var console = new MockConsole();
            var cli = new Cli(
                console,
                handler,
                (e, c) => { Logger.LogExceptionStatic(console, e.Message); });

            return cli;
        }

        private string GenerateShortNameArguments(
            string rep1 = _rep1,
            string rep2 = _rep2,
            string rep3 = _rep3,
            double tauW = _tauW,
            double tauS = _tauS,
            double? gamma = _gamma,
            float? alpha = _alpha,
            string c = _c,
            string m = _m,
            string r = _r,
            string p = _p,
            string d = _d,
            bool? excludeHeader = false)
        {
            var builder = new StringBuilder();
            var tmpDir = Environment.CurrentDirectory;
            if (rep1 is not null)
            {
                if (!rep1.Contains('*') && !rep1.Contains('?'))
                {
                    _rep1Filename = Path.Join(tmpDir, rep1);
                    using (File.Create(_rep1Filename)) ;
                    builder.Append("-i " + _rep1Filename + " ");
                }
                else
                    builder.Append("-i " + rep1 + " ");
            }

            if (rep2 is not null)
            {
                if (!rep2.Contains('*') && !rep2.Contains('?'))
                {
                    _rep2Filename = Path.Join(tmpDir, rep2);
                    using (File.Create(_rep2Filename)) ;
                    builder.Append("-i " + _rep2Filename + " ");
                }
                else
                    builder.Append("-i " + rep2 + " ");
            }

            if (rep3 is not null)
            {
                if (!rep3.Contains('*') && !rep3.Contains('?'))
                {
                    _rep3Filename = Path.Join(tmpDir, rep3);
                    using (File.Create(_rep3Filename)) ;
                    builder.Append("-i " + _rep3Filename + " ");
                }
                else
                    builder.Append("-i " + rep3 + " ");
            }

            if (!double.IsNaN(tauW)) builder.Append("-w " + tauW + " ");
            if (!double.IsNaN(tauS)) builder.Append("-s " + tauS + " ");
            if (gamma is not null) builder.Append("-g " + gamma + " ");
            if (alpha is not null) builder.Append("-a " + alpha + " ");
            if (!string.IsNullOrEmpty(c)) builder.Append("-c " + c + " ");
            if (!string.IsNullOrEmpty(m)) builder.Append("-m " + m + " ");
            if (!string.IsNullOrEmpty(r)) builder.Append("-r " + r + " ");
            if (p is not null) builder.Append("-p " + p + " ");
            if (!string.IsNullOrEmpty(d)) builder.Append("-d " + d);
            if (excludeHeader is not null && excludeHeader == true)
                builder.Append(" --excludeHeader");
            return builder.ToString();
        }

        [Theory]
        [InlineData(_rep1, _rep2, _rep3, 3)]
        [InlineData(_rep1, _rep2, null, 2)]
        public void ReadInput(
            string rep1, string rep2, string rep3, int validInputCount)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(rep1: rep1, rep2: rep2, rep3: rep3));

            // Assert
            Assert.True(options.InputFiles.Count == validInputCount);
            Assert.True(Path.GetFileName(options.InputFiles[0]) == rep1);
            Assert.True(Path.GetFileName(options.InputFiles[1]) == rep2);
            if (rep3 != null)
                Assert.True(Path.GetFileName(options.InputFiles[2]) == rep3);
        }

        [Theory]
        [InlineData("rep1.bed rep2.bed rep3.bed", null, 3)]
        [InlineData("rep1.bed rep2.bed rep3.bed", "rep4.bed rep5.bed", 5)]
        public void ReadInputWhenWildCardCharAreExpanded(
            string inputArg1, string inputArg2, int inputCount)
        {
            /// While some shell application such as PowerShell
            /// do not expand wildcard characters (i.e., if *.bed
            /// is passed as an argument, the application receives
            /// *.bed), Some other shell applications such as Mac 
            /// Terminal expand wildcard characters (i.e., if *.bed
            /// is passed as an argument, the application receives
            /// a list of all files with .bed extension). This unit
            /// test, asserts for the later.

            // Arrange
            static List<string> CreateFiles(string files)
            {
                var inputsA = new List<string>();
                if (files is null)
                    return inputsA;
                var insA = files.Split(' ');
                var tmpDir = Environment.CurrentDirectory;
                foreach (var i in insA)
                {
                    var x = Path.Join(tmpDir, i);
                    File.Create(x).Dispose();
                    inputsA.Add(x);
                }

                return inputsA;
            }
            var filesA = CreateFiles(inputArg1);
            var filesB = CreateFiles(inputArg2);
            var argsArray = GenerateShortNameArguments(
                rep1: null, rep2: null, rep3: null).Split(' ');

            var args = $" -i {string.Join(' ', filesA)} ";
            if (inputArg2 is not null)
                args += $"-i {string.Join(' ', filesB)} ";
            args += $"{string.Join(' ', argsArray)}";

            // Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(args);

            // Assert
            Assert.True(options.InputFiles.Count == inputCount);
            Assert.True(Path.GetFileName(options.InputFiles[0]) == "rep1.bed");
            Assert.True(Path.GetFileName(options.InputFiles[1]) == "rep2.bed");
            Assert.True(Path.GetFileName(options.InputFiles[2]) == "rep3.bed");
            if (inputArg2 != null)
            {
                Assert.True(Path.GetFileName(options.InputFiles[3]) == "rep4.bed");
                Assert.True(Path.GetFileName(options.InputFiles[4]) == "rep5.bed");
            }
        }

        [Fact]
        public void InputSpecifiedUsingWildCardCharacters()
        {
            // Arrange
            var tmpPath = Environment.CurrentDirectory;
            var tmpFiles = new List<string>();
            string timeStamp = DateTime.Now.ToOADate().ToString();
            for (int i = 0; i < 9; i++)
                tmpFiles.Add(tmpPath + "mspc_" + timeStamp + "_" + i + ".bed");
            foreach (var file in tmpFiles)
                File.Create(file).Close();

            // Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(
                rep1: "thisFile.bed",
                rep2: tmpPath + "mspc_" + timeStamp + "_*.bed",
                rep3: null));

            // Assert
            Assert.True(options.InputFiles.Count == 10);
            for (int i = 0; i < 9; i++)
                Assert.True(
                    options.InputFiles.Count(
                        s => s.Contains(
                            tmpPath + "mspc_" + timeStamp + "_" + i + ".bed"))
                    == 1);

            // Clean up
            foreach (var file in tmpFiles)
                File.Delete(file);
        }

        [Theory]
        [InlineData("aParserConfig.json")]
        [InlineData(@"C:\apath\parser.json")]
        [InlineData("~/unix/parserConfig.json")]
        public void ReadParser(string parserPath)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(p: parserPath));

            // Assert
            Assert.Equal(parserPath, options.ParserConfigFilename);
        }

        [Theory]
        [InlineData(_tauS)]
        [InlineData(1E-3)]
        [InlineData(0)]
        [InlineData(1.1E-53)]
        public void ReadTauS(double tauS)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(tauS: tauS));

            // Assert
            Assert.True(options.TauS == tauS);
        }

        [Theory]
        [InlineData(_tauW)]
        [InlineData(1)]
        [InlineData(1E-8)]
        [InlineData(1.1E-3)]
        public void ReadTauW(double tauW)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(tauW: tauW));

            // Assert
            Assert.True(options.TauW == tauW);
        }

        [Theory]
        [InlineData(_gamma)]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(1.1E-53)]
        public void ReadGamma(double gamma)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(gamma: gamma));

            // Assert
            Assert.True(options.Gamma == gamma);
        }

        [Theory]
        [InlineData(_alpha)]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(1.1E-53)]
        public void ReadAlpha(float alpha)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(alpha: alpha));

            // Assert
            Assert.True(options.Alpha == alpha);
        }

        [Theory]
        [InlineData(_c)]
        [InlineData("1")]
        [InlineData("3")]
        public void ReadC(string c)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(c: c));

            // Assert
            Assert.True(options.C == int.Parse(c));
        }

        [Theory]
        [InlineData("10%")]
        [InlineData("50%")]
        [InlineData("100%")]
        public void CorrectlyComputeC(string c)
        {
            // Arrange
            int inputCount = 10;
            int expectedC = (int.Parse(c.Replace("%", "")) * 10) / 100;

            var tmpDir = Path.GetTempPath();
            var files = new List<string>();
            var args = new StringBuilder(GenerateShortNameArguments(null, null, null, c: c));
            for (int i = 0; i < inputCount; i++)
            {
                var file = Path.Join(tmpDir, $"sample_{i}.bed");
                File.Create(file).Close();
                files.Add(file);
                args.Append($" -i {file}");
            }

            // Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(args.ToString());

            // Assert
            Assert.Equal(expectedC, options.C);

            // clean up
            foreach (var file in files)
                File.Delete(file);
        }

        [Theory]
        [InlineData("120%", 2)]
        [InlineData("300%", 2)]
        public void MaxCAndShowWarrning(string inputC, int expectedC)
        {
            // TODO: displaying a warning and checking it is not tested.
            // Arrange
            var args = new StringBuilder(GenerateShortNameArguments(rep3: null, c: inputC));

            // Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(args.ToString());

            // Assert
            Assert.Equal(expectedC, options.C);
        }

        [Theory]
        [InlineData("lowest", MultipleIntersections.UseLowestPValue)]
        [InlineData("LowEST", MultipleIntersections.UseLowestPValue)]
        [InlineData("highest", MultipleIntersections.UseHighestPValue)]
        public void ReadM(string m, MultipleIntersections expectedValue)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(m: m));

            // Assert
            Assert.True(options.MultipleIntersections == expectedValue);
        }

        [Theory]
        [InlineData("bio", ReplicateType.Biological)]
        [InlineData("tec", ReplicateType.Technical)]
        [InlineData("biological", ReplicateType.Biological)]
        [InlineData("technical", ReplicateType.Technical)]
        public void ReadR(string r, ReplicateType expectedValue)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(r: r));

            // Assert
            Assert.True(options.ReplicateType == expectedValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void ReadDP(int dp)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(d: dp.ToString()));

            // Assert
            Assert.True(options.DegreeOfParallelism == dp);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ReadExcludeHeader(bool exclude)
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(excludeHeader: exclude));

            // Assert
            Assert.True(options.ExcludeHeader == exclude);
        }

        [Fact]
        public void NoExceptionIfOnlyRequiredArgumentsAreGiven()
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(
                rep3: null,
                r: "bio",
                tauW: 1e-2,
                tauS: 1e-8,
                gamma: null,
                alpha: null,
                c: null,
                m: null,
                p: null,
                d: null,
                excludeHeader: null));

            // Assert
            Assert.True(options.InputFiles.Count == 2);
            Assert.True(options.ReplicateType == ReplicateType.Biological);
            Assert.True(options.TauW == 1E-2);
            Assert.True(options.TauS == 1E-8);
        }

        [Fact]
        public void UseDefaultValuesForOptionalArguments()
        {
            // Arrange & Act
            CliConfig options = default;
            var cli = GetCli((x) => { options = x; });
            cli.Invoke(GenerateShortNameArguments(
                c: null, tauW: 1e-2, tauS: 1e-8, gamma: null, alpha: null));

            // Assert
            Assert.True(options.Gamma == 1E-8);
            Assert.True(options.Alpha == 0.05F);
            Assert.True(options.C == 1);
            Assert.True(
                options.MultipleIntersections ==
                MultipleIntersections.UseLowestPValue);
        }


        // All the following tests are disabled because
        // they cannot be easily implemented using the 
        // current cli API. The API may change, hence 
        // implementing these is postponed until the 
        // api is solid.
#pragma warning disable S125 // Sections of code should not be "commented out"
        /*
                [Fact]
                public void ThrowExceptionForMissingInput()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    CliConfig options = default;
                    var cli = GetCli((x) => { options = x; });

                    // Act
                    cli.Invoke("-w 1E-2 -s 1E-8 -r bio");

                    // Assert

                    Assert.Throws<ArgumentException>(() => cli.Invoke(arguments));
                }

                [Fact]
                public void ThrowExceptionForInvalidInputFolder()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-f /none/existing/path -w 1E-4 -s 1E-8 -r bio".Split(' ');

                    // Assert
                    Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                }

                [Fact]
                public void ThrowExceptionForMissingTauS()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -r bio".Split(' ');

                    // Assert
                    Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                }

                [Fact]
                public void ThrowExceptionForMissingTauW()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -s 1E-8 -r bio".Split(' ');

                    // Assert
                    Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                }

                [Fact]
                public void ThrowExceptionForMissingReplicateType()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s 1E-8".Split(' ');

                    // Assert
                    var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                    Assert.Equal("the following required arguments are missing: -r|--replicate.", exception.Message);
                }

                [Fact]
                public void ThrowExceptionForInvalidTauW()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w ABC -s 1E-8 -r bio".Split(' ');

                    // Assert
                    var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                    Assert.Equal("Invalid value given for the `tauW` argument.", exception.Message);
                }

                [Fact]
                public void ThrowExceptionForInvalidTauS()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s ABC -r bio".Split(' ');

                    // Assert
                    var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                    Assert.Equal("Invalid value given for the `tauS` argument.", exception.Message);
                }

                [Fact]
                public void ThrowExceptionIfTauSIsNotLowerThanTauW()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-8 -s 1E-4 -r bio".Split(' ');

                    // Assert
                    var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                    Assert.Equal("Stringency threshold (TauS) should be lower than weak threshold (TauW).", exception.Message);
                }

                [Fact]
                public void ThrowExceptionForInvalidReplicateType()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s 1e-8 -r biooo".Split(' ');

                    // Assert
                    var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                    Assert.Equal("Invalid value given for the `replicate` argument.", exception.Message);
                }

                [Fact]
                public void ThrowExceptionForInvalidGamma()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s 1e-8 -r bio -g ABC".Split(' ');

                    // Assert
                    var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                    Assert.Equal("Invalid value given for the `gamma` argument.", exception.Message);
                }

                [Fact]
                public void ThrowExceptionForInvalidAlpha()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s 1e-8 -r bio -a ABC".Split(' ');

                    // Assert
                    var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                    Assert.Equal("Invalid value given for the `alpha` argument.", exception.Message);
                }

                [Fact]
                public void ThrowExceptionForInvalidC()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s 1e-8 -r bio -c ABC".Split(' ');

                    // Assert
                    var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                    Assert.Equal("Invalid value given for the `c` argument.", exception.Message);
                }

                [Fact]
                public void ThrowExceptionForInvalidPercentageOfC()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s 1e-8 -r bio -c 1A%".Split(' ');

                    // Assert
                    var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                    Assert.Equal("Invalid value given for the `c` argument.", exception.Message);
                }

                [Fact]
                public void ThrowExceptionForInvalidMultipleIntersection()
                {
                    // The new cli does not throw exception.
                    // Arrange & Act
                    var options = new CommandLineOptions();
                    string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s 1e-8 -r bio -m ABC".Split(' ');

                    // Assert
                    var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
                    Assert.Equal("Invalid value given for the `multipleIntersections` argument.", exception.Message);
                }
        */
#pragma warning restore S125 // Sections of code should not be "commented out"

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_rep1Filename is not null)
                        File.Delete(_rep1Filename);
                    if (_rep2Filename is not null)
                        File.Delete(_rep2Filename);
                    if (_rep3Filename is not null)
                        File.Delete(_rep3Filename);
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
