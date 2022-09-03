using System.CommandLine;

namespace Genometric.MSPC.Benchmark.CLI
{
    internal class CommandLineInterface
    {
        private readonly RootCommand _rootCmd;

        public CommandLineInterface(Func<string[], DirectoryInfo, int, Task> handler)
        {
            var releaseOption = new Option<string[]>(
                name: "--release",
                description: "The version of an MSPC release to be benchmarked. " +
                "The given version should match the name of a public releases of MSPC at " +
                "https://github.com/Genometric/MSPC/releases/.")
            {
                IsRequired = true,
                AllowMultipleArgumentsPerToken = true
            };
            releaseOption.AddAlias("-r");

            var dataDirOption = new Option<DirectoryInfo>(
                name: "--data-dir",
                description: "The directory containing data to be used for benchmarking. " +
                "Sample data for benchmarking are available from https://osf.io/jqrwu/.")
            {
                IsRequired = true
            };
            dataDirOption.AddAlias("-d");

            var maxRepCount = new Option<int>(
                name: "--max-rep-count",
                description: "The maximum number of replicates to be used for testing. " +
                "If the number of replicates in the given directory is less than the " +
                "given value, the available replicates will be randomly modified to " +
                "make synthetic replicates such that the total number of available plus " +
                "synthetic replicates equals the value of this argument.")
            {
                IsRequired = true
            };
            maxRepCount.AddAlias("-c");

            _rootCmd = new RootCommand(
                "Benchmarks given versions of MSPC using the given data cohort. " +
                "It records the runtime and peak memory usage.")
            {
                releaseOption,
                dataDirOption,
                maxRepCount
            };

            _rootCmd.SetHandler(async (releases, dir, repCount) =>
            {
                await handler(releases, dir, repCount);
            }, 
            releaseOption, dataDirOption, maxRepCount);
        }

        public async Task<int> InvokeAsync(string[] args)
        {
            return await _rootCmd.InvokeAsync(args);
        }
    }
}
