using System.Diagnostics;

namespace Genometric.MSPC.Benchmark
{
    public class PerformanceTest
    {
        public Dictionary<Version, Func<string, List<string>, ProcessStartInfo>> Invocations = new()
        {
            {
                Version.V5, (root, inputs) =>
                {
                    return new ProcessStartInfo(Path.Join(root, "mspc.exe"), GetArgs(inputs));
                }
            }
        };

        private static string GetArgs(List<string> inputs)
        {
            return "-r bio -w 1e-4 -s 1e-8 -i " + string.Join(" -i ", inputs);
        }

        public List<List<string>> Cases = new()
        {
            new() { "file1", "file2" },
        };

        public List<Result> Test(string mspcExePath, string dataDir, Version version)
        {
            var results = new List<Result>();

            var cases = new List<List<string>>();
            foreach (var dir in Directory.GetDirectories(dataDir))
            {
                var c = new List<string>();
                foreach(var file in Directory.GetFiles(dir))
                    c.Add(file);
                
                cases.Add(c);
            }

            foreach (var c in cases)
                results.Add(MeasurePerformance(Invocations[version](mspcExePath, c)));

            return results;
        }

        private static Result MeasurePerformance(ProcessStartInfo info)
        {
            var result = new Result();

            using (var process = Process.Start(info))
            {
                if (process == null)
                    return result;

                result.Runtime.Start();
                do
                {
                    if (!process.HasExited)
                    {
                        process.Refresh();

                        try
                        {
                            result.PeakPhysicalMemoryUsage = process.PeakWorkingSet64;
                            result.PeakPagedMemoryUsage = process.PeakPagedMemorySize64;
                            result.PeakVirtualMemoryUsage = process.PeakVirtualMemorySize64;
                        }
                        catch (System.InvalidOperationException)
                        {
                            // There is a rare chance of the process exiting after the
                            // `HasExited` was checked. In that case, getting memory
                            // usage information will throw this error. 
                        }
                    }
                }
                while (!process.WaitForExit(1000));
                result.Runtime.Stop();
            }

            return result;
        }
    }
}
