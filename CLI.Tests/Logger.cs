using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class Logger
    {
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
            Assert.Contains(messages, x => x.Contains(rep1Path));
            Assert.Contains(messages, x => x.Contains(rep2Path));

            // Clean up
            File.Delete(rep1Path);
            File.Delete(rep2Path);
            Directory.Delete(path, true);
        }
    }
}
