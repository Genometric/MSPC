using Genometric.MSPC.Benchmark.CLI;

namespace Genometric.MSPC.Benchmark
{
    static class Program
    {
        public static async Task<int> Main(string[] args)
        {            
            var cli = new CommandLineInterface(Handler);
            return await cli.InvokeAsync(args);
        }

        public static async Task Handler(string[] releases, DirectoryInfo dataDir, int maxRepCount)
        {
            if (!dataDir.Exists)
                throw new DirectoryNotFoundException($"{dataDir.FullName} does not exist.");

            var resultsFilename = Path.Join(
                dataDir.FullName,
                $"mspc_versions_benchmarking_results_" +
                $"{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()}.tsv");

            using (var writer = new StreamWriter(resultsFilename))
                writer.WriteLine(Result.GetHeader());
            Console.WriteLine($"Results are writen in file `{resultsFilename}`.");

            foreach (var release in releases)
            {
                try
                {
                    Console.WriteLine($"Benchmarking version {release}:");
                    PerformanceTest.Test(dataDir.FullName, resultsFilename, release, maxRepCount);
                }
                catch (Exception e)
                {
                    Console.Write($"{e.Message}\tSkipping this version.");
                }
            }
        }
    }
}
