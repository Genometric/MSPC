﻿using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.MSPC.CLI.Tests.MockTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    [Collection("Sequential")]
    public class TParserConfig
    {
        [Theory]
        [InlineData(0, 1, 2, 3, 4, 5, 6, true, 1E-4, PValueFormats.minus1_Log10_pValue, "fa-IR")]
        [InlineData(5, 0, -1, 12, -1, -1, 1, false, 123.456, PValueFormats.SameAsInput, "fa-IR")]
        public void ReadParserConfig(
            byte chr,
            byte left,
            sbyte right,
            byte name,
            sbyte strand,
            sbyte summit,
            byte value,
            bool dropPeakIfInvalidValue,
            double defaultValue,
            PValueFormats pValueFormat,
            string culture)
        {
            // Arrange
            var cols = new ParserConfig()
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
                Culture = culture
            };
            var path = Path.Join(
                Environment.CurrentDirectory,
                "MSPCTests_" + new Random().NextDouble().ToString());

            using (var w = new StreamWriter(path))
                w.WriteLine(JsonConvert.SerializeObject(cols));

            // Act
            var parsedCols = ParserConfig.LoadFromJSON(path);

            // Assert
            Assert.True(parsedCols.Equals(cols));

            // Clean up
            File.Delete(path);
        }

        [Fact]
        public void ReadMalformedJSON()
        {
            // Arrange
            var expected = new ParserConfig() { Chr = 123 };
            var path = Path.Join(
                Environment.CurrentDirectory,
                "MSPCTests_" + new Random().NextDouble().ToString());

            using (var w = new StreamWriter(path))
                w.WriteLine("{\"m\":7,\"l\":789,\"u\":-1,\"Chr\":123,\"L\":9,\"R\":2,\"d\":-1}");

            // Act
            var parsedCols = ParserConfig.LoadFromJSON(path);

            // Assert
            Assert.True(parsedCols.Equals(expected));

            // Clean up
            File.Delete(path);
        }

        [Fact]
        public void HandleExceptionReadingInvalidJSON()
        {
            // Arrange
            var parserFilename = Path.Join(
                Environment.CurrentDirectory,
                Guid.NewGuid().ToString() + ".json");

            using (var w = new StreamWriter(parserFilename))
                w.WriteLine("abc");

            string rep1Path = Path.Join(
                Environment.CurrentDirectory,
                Guid.NewGuid().ToString() + ".bed");

            string rep2Path = Path.Join(
                Environment.CurrentDirectory,
                Guid.NewGuid().ToString() + ".bed");

            new StreamWriter(rep1Path).Close();
            new StreamWriter(rep2Path).Close();

            // Act
            string logFile;
            var console = new MockConsole();
            using (var o = new Orchestrator(console))
            {
                o.Invoke(string.Format("-i {0} -i {1} -r bio -w 1e-2 -s 1e-4 -p {2}", rep1Path, rep2Path, parserFilename).Split(' '));
                logFile = o.LogFile;
            }

            var msg = console.GetStdo();
            var log = new List<string>();
            string line;
            using (var reader = new StreamReader(logFile))
                while ((line = reader.ReadLine()) != null)
                    log.Add(line);

            // Assert
            Assert.Contains(
                "Unexpected character encountered while parsing value",
                msg);

            // Clean up
            File.Delete(parserFilename);
            File.Delete(rep1Path);
            File.Delete(rep2Path);
        }

        [Fact]
        public void RaiseExceptionForInvalidParserFiles()
        {
            // Arrange
            string rep1Path = Path.Join(
                Environment.CurrentDirectory,
                Guid.NewGuid().ToString() + ".bed");

            string rep2Path = Path.Join(
                Environment.CurrentDirectory,
                Guid.NewGuid().ToString() + ".bed");

            new StreamWriter(rep1Path).Close();
            new StreamWriter(rep2Path).Close();

            // Act
            string logFile;
            var console = new MockConsole();
            using (var o = new Orchestrator(console))
            {
                o.Invoke(string.Format(
                    "-i {0} -i {1} -r bio -w 1e-2 -s 1e-4 -p {2}",
                    rep1Path,
                    rep2Path,
                    Path.GetTempFileName()).Split(' '));
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
                    "error reading parser configuration " +
                    "JSON object, check if the given file"));
        }

        [Fact]
        public void TwoEqualConfigs()
        {
            // Arrange
            var c1 = new ParserConfig();
            var c2 = new ParserConfig();

            // Act & Assert
            Assert.Equal(c1, c2);
        }

        [Fact]
        public void TwoNotEqualConfigs()
        {
            // Arrange
            var c1 = new ParserConfig() { Chr = 1 };
            var c2 = new ParserConfig() { Chr = 2 };

            // Act & Assert
            Assert.NotEqual(c1, c2);
        }

        [Fact]
        public void DoesNotEqualToANullObject()
        {
            // Arrange
            var config = new ParserConfig();

            // Act & Assert
            Assert.True(!config.Equals(null));
        }

        [Fact]
        public void ThrowExceptionForInvalidCultureValue()
        {
            // Arrange
            var parserFilename = Path.Join(
                Environment.CurrentDirectory,
                Guid.NewGuid().ToString() + ".json");

            // Create an json file with a `culture` field containing 
            // invalid culture name. 
            using (var w = new StreamWriter(parserFilename))
                w.WriteLine("{\"Culture\":\"xyz\"}");

            // Act
            Result x;
            using (var tmpMSPC = new TmpMspc())
                x = tmpMSPC.Run(parserFilename: parserFilename);

            // Assert
            Assert.Contains("Error setting value to 'Culture'", x.ConsoleOutput);

            // Clean up
            File.Delete(parserFilename);
        }
    }
}
