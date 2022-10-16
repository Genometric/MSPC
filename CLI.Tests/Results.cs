using Genometric.MSPC.CLI.CommandLineInterface;
using System.Collections.Generic;

namespace Genometric.MSPC.CLI.Tests
{
    public class Result
    {
        public int ExitCode { get; }
        public string ConsoleOutput { get; }
        public CliConfig Config { get; }
        public IReadOnlyCollection<string> Logs { get; }

        public Result(int exitCode, string consoleOutput) :
            this(exitCode, consoleOutput, new List<string>())
        { }

        public Result(
            int exitCode,
            string consoleOutput,
            IReadOnlyCollection<string> logs,
            CliConfig config = null)
        {
            ExitCode = exitCode;
            ConsoleOutput = consoleOutput;
            Logs = logs;
            Config = config;
        }
    }
}
