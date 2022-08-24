using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.MSPC.CLI;
using System.Diagnostics;
using System.IO.Compression;

namespace Genometric.MSPC.Benchmark
{
    public class PerformanceTest
    {
        private readonly Dictionary<Version, Func<string, List<string>, ProcessStartInfo>> _invocations = new()
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

        public async Task<List<Result>> Test(string dataDir, Version version)
        {
            var mspcExePath = await GetMspc(version);

            var cases = new List<List<string>>();
            foreach (var dir in Directory.GetDirectories(dataDir))
            {
                var c = new List<string>();
                foreach (var file in Directory.GetFiles(dir))
                    c.Add(file);

                cases.Add(c);
            }

            var results = new List<Result>();
            foreach (var c in cases)
            {
                var result = MeasurePerformance(_invocations[version](mspcExePath, c));
                result.ReplicateCount = c.Count;
                foreach (var filename in c)
                    result.IntervalCount += GetPeaksCount(filename);
                results.Add(result);
            }

            return results;
        }

        private static Result MeasurePerformance(ProcessStartInfo info, int waitMilliseconds = 100)
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
                        catch (InvalidOperationException)
                        {
                            // There is a rare chance of the process exiting after the
                            // `HasExited` was checked. In that case, getting memory
                            // usage information will throw this error. 
                        }
                    }
                }
                while (!process.WaitForExit(waitMilliseconds));
                result.Runtime.Stop();
            }

            return result;
        }

        private static int GetPeaksCount(string filename)
        {
            var parserConfig = new ParserConfig();
            var bedParser = new BedParser(parserConfig)
            {
                PValueFormat = parserConfig.PValueFormat,
                DefaultValue = parserConfig.DefaultValue,
                DropPeakIfInvalidValue = parserConfig.DropPeakIfInvalidValue,
                Culture = parserConfig.Culture
            };
            var parsedData = bedParser.Parse(filename);
            return parsedData.IntervalsCount;
        }

        private static async Task<string> GetMspc(Version version)
        {
            Uri uri = version switch
            {
                Version.V5 => new Uri("https://github.com/Genometric/MSPC/releases/download/v5.5.0/mspc.zip"),
                _ => new Uri("https://github.com/Genometric/MSPC/releases/latest/mspc.zip"),
            };

            var dir = Path.Join(Path.GetTempPath(), "mspc", version.ToString().ToLower().Replace(".", "_"));
            Directory.CreateDirectory(dir);
            var filename = Path.Join(dir, "mspc.zip");

            var client = new HttpClient();
            var response = await client.GetAsync(uri);
            using (var stream = new FileStream(filename, FileMode.CreateNew))
            {
                await response.Content.CopyToAsync(stream);
            }

            ZipFile.ExtractToDirectory(filename, dir);

            return dir;
        }
    }
}
