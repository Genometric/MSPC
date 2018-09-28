// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Newtonsoft.Json;
using System;
using System.IO;
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
                writter.WriteLine("chr1\t10\t20\tmspc_peak_1\t3");
                writter.WriteLine("chr1\t25\t35\tmspc_peak_1\t5");
            }

            stream = File.Create(rep2Path);
            using (StreamWriter writter = new StreamWriter(stream))
            {
                writter.WriteLine("chr1\t11\t18\tmspc_peak_2\t2");
                writter.WriteLine("chr1\t22\t28\tmspc_peak_2\t3");
                writter.WriteLine("chr1\t30\t40\tmspc_peak_2\t7");
            }
        }

        private string RunMSPC(string rep1, string rep2)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(string.Format("-i {0} -i {1} -r bio -w 1E-2 -s 1E-8", rep1, rep2).Split(' '));
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
            File.Delete(rep1);
            File.Delete(rep2);

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
            File.Delete(rep1);
            File.Delete(rep2);

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
            File.Delete(rep1);
            File.Delete(rep2);

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
            File.Delete(rep1);
            File.Delete(rep2);

            // Assert
            Assert.Contains("All processes successfully finished [Analysis ET: ", msg);
        }

        [Fact]
        public void ParseBasedOnGivenParserConfig()
        {
            // Arrange
            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

            FileStream stream = File.Create(rep1Path);
            using (StreamWriter writter = new StreamWriter(stream))
            {
                writter.WriteLine("ABC\t____\t10\t+++++\t100.0\tchr1\t___====____\tmspc_peak_1\t20");
                writter.WriteLine("ABC\t____\t25\t+++++\t123.4\tchr1\t___====____\tmspc_peak_2\t35");
            }

            stream = File.Create(rep2Path);
            using (StreamWriter writter = new StreamWriter(stream))
            {
                writter.WriteLine("ABC\t____\t11\t+++++\t31.4\tchr1\t___====____\tmspc_peak_3\t18");
                writter.WriteLine("ABC\t____\t22\t+++++\t21.4\tchr1\t___====____\tmspc_peak_4\t28");
                writter.WriteLine("ABC\t____\t30\t+++++\t99.9\tchr1\t___====____\tmspc_peak_5\t40");
            }

            var cols = new BedColumns()
            {
                Chr = 5,
                Left = 2,
                Right = 8,
                Name = 7,
                Strand = -1,
                Summit = -1
            };
            var path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "MSPCTests_" + new Random().NextDouble().ToString();
            using (StreamWriter w = new StreamWriter(path))
                w.WriteLine(JsonConvert.SerializeObject(cols));


            // Act
            string console = null;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(string.Format("-i {0} -i {1} -r bio -w 1E-1 -s 1E-4 -c 1 -p {2}", rep1Path, rep2Path, path).Split(' '));
                File.Delete(rep1Path);
                File.Delete(rep2Path);
                console = sw.ToString();
            }


            // Assert
            Assert.Contains("Read peaks#:\t2", console);
            Assert.Contains("Read peaks#:\t3", console);

            Assert.Contains("Max p-value:\t1.000E-100", console);
            Assert.Contains("Min p-value:\t3.981E-124", console);
            Assert.Contains("Min p-value:\t1.259E-100", console);
            Assert.Contains("Max p-value:\t3.981E-022", console);
        }
    }
}
