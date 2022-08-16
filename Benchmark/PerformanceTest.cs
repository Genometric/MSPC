using System.Diagnostics;

namespace Genometric.MSPC.Benchmark
{
    public class PerformanceTest
    {
        public Dictionary<Version, Func<List<string>, string>> Invocations = new()
        {
            {
                Version.V5, (inputs) =>
                {
                    return "mspc -r bio -w 1e-4 -s 1e-8" + string.Join(" -i", inputs);
                }
            }
        };

        public List<List<string>> Cases = new()
        {
            new() { "file1", "file2" },
        };

        public List<Result> Test(string mspcExePath, Version version)
        {
            var results = new List<Result>();

            foreach (var c in Cases)
                results.Add(MeasurePerformance(
                    Path.Join(
                        mspcExePath,
                        Invocations[version](c))));

            return results;
        }

        private static Result MeasurePerformance(string cmd)
        {
            var result = new Result();

            using (var process = Process.Start(cmd))
            {
                do
                {
                    if (!process.HasExited)
                    {
                        process.Refresh();

                        result.PeakPhysicalMemoryUsage = process.PeakWorkingSet64;
                        result.PeakPagedMemoryUsage = process.PeakPagedMemorySize64;
                        result.PeakVirtualMemoryUsage = process.PeakVirtualMemorySize64;
                    }
                }
                while (!process.WaitForExit(1000));
            }

            return result;
        }
    }
}
