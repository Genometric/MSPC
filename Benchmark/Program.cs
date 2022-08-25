namespace Genometric.MSPC.Benchmark
{
    static class Program
    {
        public static async Task Main(string[] args)
        {
            var vsString = args[0].Split(";");
            var dataDir = args[1];
            var resultsFilename = Path.Join(dataDir, "mspc_versions_benchmarking_results.tsv");
            using (var writer = new StreamWriter(resultsFilename))
                writer.WriteLine(Result.GetHeader());

            var versions = new List<Version>();
            foreach (var v in vsString)
                versions.Add(Enum.Parse<Version>(v, true));

            foreach (var version in versions)
            {
                var test = new PerformanceTest();
                var results = await test.Test(dataDir, version);
                using (var writer = new StreamWriter(resultsFilename))
                    foreach (var result in results)
                        writer.WriteLine(result.ToString());

            }
        }
    }
}
