// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Model;
using System;
using System.Text;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class CLIOptions
    {
        private const string _rep1 = "replicate_1.bed";
        private const string _rep2 = "replicate_2.bed";
        private const string _rep3 = "replicate_3.bed";
        private const double _tauW = 1E-2;
        private const double _tauS = 1E-9;
        private const double _gamma = 1E-12;
        private const float _alpha = 0.0005F;
        private const byte _c = 2;
        private const string _m = "lowest";
        private const string _r = "bio";

        private string GenerateShortNameArguments(
            string rep1 = _rep1, string rep2 = _rep2, string rep3 = _rep3, double tauW = _tauW, double tauS = _tauS,
            double gamma = _gamma, float alpha = _alpha, byte c = _c, string m = _m, string r = _r)
        {
            var builder = new StringBuilder();
            if (rep1 != null) builder.Append("-i " + rep1 + " ");
            if (rep2 != null) builder.Append("-i " + rep2 + " ");
            if (rep3 != null) builder.Append("-i " + rep3 + " ");
            if (!Double.IsNaN(tauW)) builder.Append("-w " + tauW + " ");
            if (!Double.IsNaN(tauS)) builder.Append("-s " + tauS + " ");
            if (!Double.IsNaN(gamma)) builder.Append("-g " + gamma + " ");
            if (!float.IsNaN(alpha)) builder.Append("-a " + alpha + " ");
            builder.Append("-c " + c + " ");
            builder.Append("-m " + m + " ");
            builder.Append("-r " + r);
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
            options.Parse(GenerateShortNameArguments(rep1: rep1, rep2: rep2, rep3: rep3).Split(' '));

            // Assert
            Assert.True(options.Input.Count == validInputCount);
            Assert.True(options.Input[0] == rep1);
            Assert.True(options.Input[1] == rep2);
            if (rep3 != null)
                Assert.True(options.Input[2] == rep3);
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
            var po = options.Parse(GenerateShortNameArguments(tauS: tauS).Split(' '));

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
            var po = options.Parse(GenerateShortNameArguments(tauW: tauW).Split(' '));

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
            var po = options.Parse(GenerateShortNameArguments(gamma: gamma).Split(' '));

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
            var po = options.Parse(GenerateShortNameArguments(alpha: alpha).Split(' '));

            // Assert
            Assert.True(po.Alpha == alpha);
        }

        [Theory]
        [InlineData(_c)]
        [InlineData(1)]
        [InlineData(5)]
        public void ReadC(byte c)
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse(GenerateShortNameArguments(c: c).Split(' '));

            // Assert
            Assert.True(po.C == c);
        }

        [Theory]
        [InlineData("lowest", MultipleIntersections.UseLowestPValue)]
        [InlineData("LowEST", MultipleIntersections.UseLowestPValue)]
        [InlineData("highest", MultipleIntersections.UseHighestPValue)]
        public void ReadM(string m, MultipleIntersections expectedValue)
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse(GenerateShortNameArguments(m: m).Split(' '));

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
            var po = options.Parse(GenerateShortNameArguments(r: r).Split(' '));

            // Assert
            Assert.True(po.ReplicateType == expectedValue);
        }

        [Fact]
        public void NoExceptionIfOnlyRequiredArgumentsAreGiven()
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse("-i rep1.bed -i rep2.bed -r bio -w 1E-2 -s 1E-8".Split(' '));

            // Assert
            Assert.True(options.Input.Count == 2);
            Assert.True(po.ReplicateType == ReplicateType.Biological);
            Assert.True(po.TauW == 1E-2);
            Assert.True(po.TauS == 1E-8);
        }

        [Fact]
        public void ThrowExceptionForMissingInput()
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            string[] arguments = "-w 1E-2 -s 1E-8 -g 1E-16 -r bio".Split(' ');

            // Assert
            Assert.Throws<ArgumentException>(() => options.Parse(arguments));
        }
    }
}
