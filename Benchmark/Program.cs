namespace Genometric.MSPC.Benchmark
{
    static class Program
    {
        public static async Task Main(string[] args)
        {
            var vsString = args[0].Split(";");
            var dataDir = args[1];

            var versions = new List<Version>();
            foreach (var v in vsString)
                versions.Add(Enum.Parse<Version>(v, true));

            foreach (var version in versions)
            {
                var test = new PerformanceTest();
                var results = await test.Test(dataDir, version);
            }
        }
    }
}
