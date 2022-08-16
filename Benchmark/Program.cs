namespace Genometric.MSPC.Benchmark
{
    static class Program
    {
        public static void Main(string[] _)
        {
            var test = new PerformanceTest();
            test.Test("...", Version.V5);
        }
    }
}
