using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.MSPC.CLI;
using System.Diagnostics;

namespace Genometric.MSPC.Benchmark
{
    public static class PerformanceTest
    {
        public static List<Result> Test(string dataDir, string version, int maxRepCount)
        {
            var cases = new List<List<string>>();
            foreach (var dir in Directory.GetDirectories(dataDir))
            {
                var c = new List<string>();
                foreach (var file in Directory.GetFiles(dir))
                    c.Add(file);

                if (c.Count > 0)
                    cases.Add(c);
            }

            var results = new List<Result>();
            var verInfo = new VersionInfo(version);
            foreach (var c in cases)
            {
                var reps = SyntheticReps.Generate(c, maxRepCount);

                for (int i = 2; i <= maxRepCount; i++)
                {
                    var testReps = reps.Take(i).ToList();

                    verInfo.InputFiles = testReps;
                    var result = MeasurePerformance(verInfo.StartInfo);
                    result.ReplicateCount = testReps.Count;
                    foreach (var filename in testReps)
                        result.IntervalCount += GetPeaksCount(filename);
                    results.Add(result);
                }
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
    }
}
