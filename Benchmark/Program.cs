namespace Genometric.MSPC.Benchmark
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var versions = args[0].Split(";");
            var dataDir = args[1];
            var maxRepCount = int.Parse(args[2]);

            var resultsFilename = Path.Join(dataDir, "mspc_versions_benchmarking_results.tsv");
            using (var writer = new StreamWriter(resultsFilename))
                writer.WriteLine(Result.GetHeader());

            foreach (var version in versions)
            {
                List<Result> results;
                try
                {
                    results = PerformanceTest.Test(dataDir, version, maxRepCount);
                }
                catch (Exception e)
                {
                    Console.Write($"{e.Message}\tSkipping this version.");
                    continue;
                }

                using (var writer = new StreamWriter(resultsFilename))
                    foreach (var result in results)
                        writer.WriteLine(result.ToString());

            }
        }
    }
}
