namespace Genometric.MSPC.Benchmark
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var versions = args[0].Split(";");
            var dataDir = args[1];
            var maxRepCount = int.Parse(args[2]);

            var resultsFilename = Path.Join(
                dataDir, 
                $"mspc_versions_benchmarking_results_" +
                $"{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()}.tsv");

            using (var writer = new StreamWriter(resultsFilename))
                writer.WriteLine(Result.GetHeader());
            Console.WriteLine($"Results are writen in file `{resultsFilename}`.");

            foreach (var version in versions)
            {
                try
                {
                    PerformanceTest.Test(dataDir, resultsFilename, version, maxRepCount);
                }
                catch (Exception e)
                {
                    Console.Write($"{e.Message}\tSkipping this version.");
                }
            }
        }
    }
}
