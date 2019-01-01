// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using System.Linq;

namespace Genometric.MSPC.CLI.Tests
{
    public class CliOptions
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

        private string GenerateShortNameArguments(
            string rep1 = _rep1, string rep2 = _rep2, string rep3 = _rep3, double tauW = _tauW, double tauS = _tauS,
            double gamma = _gamma, float alpha = _alpha, string c = _c, string m = _m, string r = _r, string p = _p)
        {
            var builder = new StringBuilder();
            if (rep1 != null) builder.Append("-i " + rep1 + " ");
            if (rep2 != null) builder.Append("-i " + rep2 + " ");
            if (rep3 != null) builder.Append("-i " + rep3 + " ");
            if (!double.IsNaN(tauW)) builder.Append("-w " + tauW + " ");
            if (!double.IsNaN(tauS)) builder.Append("-s " + tauS + " ");
            if (!double.IsNaN(gamma)) builder.Append("-g " + gamma + " ");
            if (!float.IsNaN(alpha)) builder.Append("-a " + alpha + " ");
            builder.Append("-c " + c + " ");
            builder.Append("-m " + m + " ");
            builder.Append("-r " + r);
            if (p != null) builder.Append(" -p " + p);
            return builder.ToString();
        }

        [Theory]
        [InlineData("rep_1.bed", "rep_2.bed", "rep_3.bed", 3)]
        [InlineData("rep_1.bed", "rep_2.bed", null, 2)]
        [InlineData("C:\\TestPath\\replicate_1", "C:\\AnotherTestPath\\replicate_2", "C:\\YetAnotherTestPath\\replicate_3", 3)]
        public void ReadInput(string rep1, string rep2, string rep3, int validInputCount)
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            options.Parse(GenerateShortNameArguments(rep1: rep1, rep2: rep2, rep3: rep3).Split(' '), out bool _);

            // Assert
            Assert.True(options.Input.Count == validInputCount);
            Assert.True(options.Input[0] == rep1);
            Assert.True(options.Input[1] == rep2);
            if (rep3 != null)
                Assert.True(options.Input[2] == rep3);
        }

        [Theory]
        [InlineData("rep1.bed rep2.bed rep3.bed", null, 3)]
        [InlineData("rep1.bed rep2.bed rep3.bed", "rep4.bed rep5.bed", 5)]
        public void ReadInputWhenWildCardCharAreExpanded(string inputArg1, string inputArg2, int inputCount)
        {
            /// While some shell application such as PowerShell
            /// do not expand wildcard characters (i.e., if *.bed
            /// is passed as an argument, the application receives
            /// *.bed), Some other shell applications such as Mac 
            /// Terminal expand wildcard characters (i.e., if *.bed
            /// is passed as an argument, the application receives
            /// a list of all files with .bed extension). This unit
            /// test, asserts for the later.

            // Arrange & Act
            var options = new CommandLineOptions();
            options.Parse(GenerateShortNameArguments(rep1: inputArg1, rep2: inputArg2, rep3: null).Split(' '), out bool _);

            // Assert
            Assert.True(options.Input.Count == inputCount);
            Assert.True(options.Input[0] == "rep1.bed");
            Assert.True(options.Input[1] == "rep2.bed");
            Assert.True(options.Input[2] == "rep3.bed");
            if (inputArg2 != null)
            {
                Assert.True(options.Input[3] == "rep4.bed");
                Assert.True(options.Input[4] == "rep5.bed");
            }
        }

        [Fact]
        public void InputSpecifiedUsingWildCardCharacters()
        {
            // Arrange
            var tmpPath = Path.GetTempPath();
            var tmpFiles = new List<string>();
            string timeStamp = DateTime.Now.ToOADate().ToString();
            for (int i = 0; i < 9; i++)
                tmpFiles.Add(tmpPath + "mspc_" + timeStamp + "_" + i + ".bed");
            foreach (var file in tmpFiles)
                File.Create(file).Close();

            // Act
            var options = new CommandLineOptions();
            options.Parse(GenerateShortNameArguments(rep1: "thisFile.bed", rep2: tmpPath + "mspc_" + timeStamp + "_*.bed", rep3: null).Split(' '), out bool _);

            // Assert
            Assert.True(options.Input.Count == 10);
            for (int i = 0; i < 9; i++)
                Assert.True(options.Input.Count(s => s.Contains(tmpPath + "mspc_" + timeStamp + "_" + i + ".bed")) == 1);

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
            var options = new CommandLineOptions();
            options.Parse(GenerateShortNameArguments(p: parserPath).Split(' '), out bool _);

            // Assert
            Assert.Equal(parserPath, options.ParserConfig);
        }

        [Theory]
        [InlineData(_tauS)]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(1.1E-53)]
        public void ReadTauS(double tauS)
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse(GenerateShortNameArguments(tauS: tauS).Split(' '), out bool _);

            // Assert
            Assert.True(po.TauS == tauS);
        }

        [Theory]
        [InlineData(_tauW)]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(1.1E-53)]
        public void ReadTauW(double tauW)
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse(GenerateShortNameArguments(tauW: tauW).Split(' '), out bool _);

            // Assert
            Assert.True(po.TauW == tauW);
        }

        [Theory]
        [InlineData(_gamma)]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(1.1E-53)]
        public void ReadGamma(double gamma)
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse(GenerateShortNameArguments(gamma: gamma).Split(' '), out bool _);

            // Assert
            Assert.True(po.Gamma == gamma);
        }

        [Theory]
        [InlineData(_alpha)]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(1.1E-53)]
        public void ReadAlpha(float alpha)
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse(GenerateShortNameArguments(alpha: alpha).Split(' '), out bool _);

            // Assert
            Assert.True(po.Alpha == alpha);
        }

        [Theory]
        [InlineData(_c)]
        [InlineData("1")]
        [InlineData("5")]
        public void ReadC(string c)
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse(GenerateShortNameArguments(c: c).Split(' '), out bool _);

            // Assert
            Assert.True(po.C == byte.Parse(c));
        }

        [Theory]
        [InlineData("0%")]
        [InlineData("10%")]
        [InlineData("50%")]
        [InlineData("100%")]
        public void CorrectlyComputeC(string c)
        {
            // Arrange
            int inputCount = 10;
            int expectedC = (int.Parse(c.Replace("%", "")) * 10) / 100;
            if (expectedC == 0)
                expectedC = 1;

            var args = new StringBuilder(GenerateShortNameArguments(null, null, null, c: c));
            for (int i = 0; i < inputCount; i++)
                args.Append(" -i sample_" + i);

            // Act
            var options = new CommandLineOptions();
            var po = options.Parse(args.ToString().Split(' '), out bool _);

            // Assert
            Assert.Equal(expectedC, po.C);
        }

        [Theory]
        [InlineData("lowest", MultipleIntersections.UseLowestPValue)]
        [InlineData("LowEST", MultipleIntersections.UseLowestPValue)]
        [InlineData("highest", MultipleIntersections.UseHighestPValue)]
        public void ReadM(string m, MultipleIntersections expectedValue)
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse(GenerateShortNameArguments(m: m).Split(' '), out bool _);

            // Assert
            Assert.True(po.MultipleIntersections == expectedValue);
        }

        [Theory]
        [InlineData("bio", ReplicateType.Biological)]
        [InlineData("tec", ReplicateType.Technical)]
        [InlineData("BioloGicAl", ReplicateType.Biological)]
        [InlineData("Technical", ReplicateType.Technical)]
        public void ReadR(string r, ReplicateType expectedValue)
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse(GenerateShortNameArguments(r: r).Split(' '), out bool _);

            // Assert
            Assert.True(po.ReplicateType == expectedValue);
        }

        [Fact]
        public void NoExceptionIfOnlyRequiredArgumentsAreGiven()
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse("-i rep1.bed -i rep2.bed -r bio -w 1E-2 -s 1E-8".Split(' '), out bool _);

            // Assert
            Assert.True(options.Input.Count == 2);
            Assert.True(po.ReplicateType == ReplicateType.Biological);
            Assert.True(po.TauW == 1E-2);
            Assert.True(po.TauS == 1E-8);
        }

        [Fact]
        public void UseDefaultValuesForOptionalArguments()
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse("-i rep1.bed -i rep2.bed -r bio -w 1E-2 -s 1E-8".Split(' '), out bool _);

            // Assert
            Assert.True(po.Gamma == 1E-8);
            Assert.True(po.Alpha == 0.05F);
            Assert.True(po.C == 1);
            Assert.True(po.MultipleIntersections == MultipleIntersections.UseLowestPValue);
        }

        [Fact]
        public void ThrowExceptionForMissingInput()
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            string[] arguments = "-w 1E-2 -s 1E-8 -r bio".Split(' ');

            // Assert
            Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
        }

        [Fact]
        public void ThrowExceptionForMissingTauS()
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -r bio".Split(' ');

            // Assert
            Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
        }

        [Fact]
        public void ThrowExceptionForMissingTauW()
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            string[] arguments = "-i rep1.bed -i rep2.bed -s 1E-8 -r bio".Split(' ');

            // Assert
            Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
        }

        [Fact]
        public void ThrowExceptionForMissingReplicateType()
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s 1E-8".Split(' ');

            // Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
            Assert.Equal("The following required arguments are missing: r|replicate; ", exception.Message);
        }

        [Fact]
        public void ThrowExceptionForInvalidTauW()
        {
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
            // Arrange & Act
            var options = new CommandLineOptions();
            string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s ABC -r bio".Split(' ');

            // Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
            Assert.Equal("Invalid value given for the `tauS` argument.", exception.Message);
        }

        [Fact]
        public void ThrowExceptionForInvalidReplicateType()
        {
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
            // Arrange & Act
            var options = new CommandLineOptions();
            string[] arguments = "-i rep1.bed -i rep2.bed -w 1E-2 -s 1e-8 -r bio -m ABC".Split(' ');

            // Assert
            var exception = Assert.Throws<ArgumentException>(() => options.Parse(arguments, out bool _));
            Assert.Equal("Invalid value given for the `multipleIntersections` argument.", exception.Message);
        }
    }
}
