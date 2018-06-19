// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Model;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class Main
    {
        private void CreateTempSamples(out string rep1Path, out string rep2Path)
        {
            rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

            FileStream stream = File.Create(rep1Path);
            using (StreamWriter writter = new StreamWriter(stream))
            {
                writter.WriteLine("chr1\t10\t20\tmspc_peak_1\t0.001");
                writter.WriteLine("chr1\t25\t35\tmspc_peak_1\t0.00001");
            }

            stream = File.Create(rep2Path);
            using (StreamWriter writter = new StreamWriter(stream))
            {
                writter.WriteLine("chr1\t11\t18\tmspc_peak_2\t0.01");
                writter.WriteLine("chr1\t22\t28\tmspc_peak_2\t0.0001");
                writter.WriteLine("chr1\t30\t40\tmspc_peak_2\t0.0000001");
            }
        }

        private string RunMSPC(string rep1, string rep2)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(String.Format("-i {0} -i {1} -r bio -w 1E-2 -s 1E-8", rep1, rep2).Split(' '));
                return sw.ToString();
            }
        }

        [Fact]
        public void ErrorIfLessThanTwoSamplesAreGiven()
        {
            // Arrange & Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main("-i rep1.bed -r bio -w 1E-2 -s 1E-8".Split(' '));

                // Assert
                Assert.Equal("At least two samples are required; 1 is given.\r\nMSPC cannot continue.\r\n", sw.ToString());
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
                Assert.Equal("Missing file: rep1.bed\r\nMSPC cannot continue.\r\n", sw.ToString());
            }
        }

        [Fact]
        public void AssertInformingPeaksCount()
        {
            // Arrange
            CreateTempSamples(out string rep1, out string rep2);

            // Act
            string msg = RunMSPC(rep1, rep2);

            // Assert
            Assert.Contains("Read peaks#:\t2\r\n", msg);
            Assert.Contains("Read peaks#:\t3\r\n", msg);
        }

        [Fact]
        public void AssertInformingMinPValue()
        {
            // Arrange
            CreateTempSamples(out string rep1, out string rep2);

            // Act
            string msg = RunMSPC(rep1, rep2);

            // Assert
            Assert.Contains("Min p-value:\t1.000E-005\r\n", msg);
            Assert.Contains("Min p-value:\t1.000E-007\r\n", msg);
        }

        [Fact]
        public void AssertInformingMaxPValue()
        {
            // Arrange
            CreateTempSamples(out string rep1, out string rep2);

            // Act
            string msg = RunMSPC(rep1, rep2);

            // Assert
            Assert.Contains("Max p-value:\t1.000E-003\r\n", msg);
            Assert.Contains("Max p-value:\t1.000E-002\r\n", msg);
        }

        [Fact]
        public void SuccessfulAnalysis()
        {
            // Arrange
            CreateTempSamples(out string rep1, out string rep2);

            // Act
            string msg = RunMSPC(rep1, rep2);

            // Assert
            Assert.Contains("All processes successfully finished [Analysis ET: ", msg);
        }
    }
}
