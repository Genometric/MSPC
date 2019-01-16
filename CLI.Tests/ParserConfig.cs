// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class TParserConfig
    {
        [Theory]
        [InlineData(0, 1, 2, 3, 4, 5, 6, true, 1E-4, PValueFormats.minus1_Log10_pValue)]
        [InlineData(5, 0, -1, 12, -1, -1, 1, false, 123.456, PValueFormats.SameAsInput)]
        public void ReadParserConfig(byte chr, byte left, sbyte right, byte name, sbyte strand, sbyte summit, byte value, bool dropPeakIfInvalidValue, double defaultValue, PValueFormats pValueFormat)
        {
            // Arrange
            ParserConfig cols = new ParserConfig()
            {
                Chr = chr,
                Left = left,
                Right = right,
                Name = name,
                Strand = strand,
                Summit = summit,
                Value = value,
                DefaultValue = defaultValue,
                PValueFormat = pValueFormat,
                DropPeakIfInvalidValue = dropPeakIfInvalidValue,
            };
            var path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "MSPCTests_" + new Random().NextDouble().ToString();
            using (StreamWriter w = new StreamWriter(path))
                w.WriteLine(JsonConvert.SerializeObject(cols));

            // Act
            ParserConfig parsedCols = ParserConfig.LoadFromJSON(path);
            File.Delete(path);

            // Assert
            Assert.True(parsedCols.Equals(cols));
        }

        [Fact]
        public void ReadMalformedJSON()
        {
            // Arrange
            var expected = new ParserConfig() { Chr = 123 };
            var path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "MSPCTests_" + new Random().NextDouble().ToString();
            using (StreamWriter w = new StreamWriter(path))
                w.WriteLine("{\"m\":7,\"l\":789,\"u\":-1,\"Chr\":123,\"L\":9,\"R\":2,\"d\":-1}");

            // Act
            var parsedCols = ParserConfig.LoadFromJSON(path);
            File.Delete(path);

            // Assert
            Assert.True(parsedCols.Equals(expected));
        }

        [Fact]
        public void HandleExceptionReadingInvalidJSON()
        {
            // Arrange
            var path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "MSPCTests_" + new Random().NextDouble().ToString();
            using (StreamWriter w = new StreamWriter(path))
                w.WriteLine("abc");

            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            new StreamWriter(rep1Path).Close();
            new StreamWriter(rep2Path).Close();

            // Act
            string logFile;
            using (var o = new Orchestrator())
            {
                o.Orchestrate(string.Format("-i {0} -i {1} -r bio -w 1e-2 -s 1e-4 -p {2}", rep1Path, rep2Path, path).Split(' '));
                logFile = o.LogFile;
            }

            var log = new List<string>();
            string line;
            using (var reader = new StreamReader(logFile))
                while ((line = reader.ReadLine()) != null)
                    log.Add(line);

            // Assert
            Assert.Contains(
                log, 
                x => x.Contains(
                    "Error reading parser configuration JSON object: Newtonsoft.Json.JsonReaderException: " +
                    "Unexpected character encountered while parsing value"));
        }
    }
}
