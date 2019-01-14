// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class Main
    {
        [Fact]
        public void ErrorIfLessThanTwoSamplesAreGiven()
        {
            // Arrange & Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main("-i rep1.bed -r bio -w 1E-2 -s 1E-8".Split(' '));

                // Assert
                Assert.Equal("At least two samples are required; 1 is given.\r\n", sw.ToString());
            }
        }

        [Fact]
        public void ErrorIfARequiredArgumentIsMissing()
        {
            // Arrange & Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main("-i rep1.bed -i rep2.bed -w 1E-2 -s 1E-8".Split(' '));

                // Assert
                Assert.Equal("The following required arguments are missing: r|replicate; \r\nMSPC cannot continue.\r\n", sw.ToString());
            }
        }

        [Fact]
        public void ErrorIfASpecifiedFileIsMissing()
        {
            // Arrange & Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main("-i rep1.bed -i rep2.bed -r bio -w 1E-2 -s 1E-8".Split(' '));

                // Assert
                Assert.Contains("The following files are missing: rep1.bed", sw.ToString());
            }
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
        public void SuccessfulAnalysis()
        {
            // Arrange
            string msg;

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run();

            // Assert
            Assert.Contains("All processes successfully finished", msg);
        }

        [Theory]
        [InlineData("-?")]
        [InlineData("-h")]
        [InlineData("--help")]
        public void ShowsHelpText(string template)
        {
            // Arrange
            string msg;
            string expected =
                "\r\n\r\nUsage: MSPC CLI [options]\r\n\r\nOptions:" +
                "\r\n  -? | -h | --help                      Show help information" +
                "\r\n  -v | --version                        Show version information" +
                "\r\n  -i | --input <value>                  Input samples to be processed in Browser Extensible Data (BED) Format." +
                "\r\n  -r | --replicate <value>              Sets the replicate type of samples. Possible values are: { Bio, Biological, Tec, Technical }" +
                "\r\n  -w | --tauW <value>                   Sets weak threshold. All peaks with p-values higher than this value are considered as weak peaks." +
                "\r\n  -s | --tauS <value>                   Sets stringency threshold. All peaks with p-values lower than this value are considered as stringent peaks." +
                "\r\n  -g | --gamma <value>                  Sets combined stringency threshold. The peaks with their combined p-values satisfying this threshold will be confirmed." +
                "\r\n  -a | --alpha <value>                  Sets false discovery rate of Benjamini–Hochberg step-up procedure." +
                "\r\n  -c <value>                            Sets minimum number of overlapping peaks before combining p-values." +
                "\r\n  -m | --multipleIntersections <value>  When multiple peaks from a sample overlap with a given peak, this argument defines which of the peaks to be considered: the one with lowest p-value, or the one with highest p-value? Possible values are: { Lowest, Highest }" +
                "\r\n  -p | --parser <value>                 Sets the path to the parser configuration file in JSON." +
                "\r\n  -o | --output <value>                 Sets a path where analysis results should be persisted." +
                "\r\n" +
                "\n\rDocumentation:\thttps://genometric.github.io/MSPC/" +
                "\n\rSource Code:\thttps://github.com/Genometric/MSPC" +
                "\n\rPublications:\thttps://genometric.github.io/MSPC/publications" +
                "\n\r\r\n";

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(template);

            // Assert
            Assert.Contains(expected, msg);
        }

        [Theory]
        [InlineData("-v")]
        [InlineData("--version")]
        public void ShowVersion(string template)
        {
            // Arrange
            string msg;
            string expected = "\r\nVersion ";

            // Act
            using (var tmpMspc = new TmpMspc())
                msg = tmpMspc.Run(template);

            // Assert
            Assert.Contains(expected, msg);
        }
    }
}
