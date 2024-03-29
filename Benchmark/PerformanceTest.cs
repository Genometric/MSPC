﻿using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.MSPC.CLI;
using System.Diagnostics;

namespace Genometric.MSPC.Benchmark
{
    public static class PerformanceTest
    {
        public static void Test(string dataDir, string resultsFilename, string version, int maxRepCount)
        {
            var cases = new Dictionary<string, List<string>>();
            foreach (var dir in Directory.GetDirectories(dataDir))
            {
                var c = new List<string>();
                foreach (var file in Directory.GetFiles(dir))
                    c.Add(file);

                if (c.Count > 0)
                    cases.Add(new DirectoryInfo(dir).Name, c);
            }

            var counter = 0;
            var timer = new Stopwatch();
            var verInfo = new VersionInfo(version);
            foreach (var c in cases)
            {
                timer.Restart();
                var results = new List<Result>();
                var msg = $"\t[{++counter}/{cases.Count}]\tBenchmarking using {c.Key}: ... ";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(msg);
                Console.ResetColor();
                var reps = SyntheticReps.Generate(c.Value, maxRepCount);
                var syntheticReps = reps.Except(c.Value).ToList();

                var minRepCount = 2;
                for (int i = minRepCount; i <= maxRepCount; i++)
                {
                    var testReps = reps.Take(i).ToList();
                    verInfo.InputFiles = testReps;
                    var result = MeasurePerformance(verInfo.StartInfo);
                    result.Version = verInfo.Version;
                    result.ExperimentId = c.Key;
                    result.ReplicateCount = testReps.Count;
                    foreach (var filename in testReps)
                        result.IntervalCount += GetPeaksCount(filename);

                    results.Add(result);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"\r{msg}{Math.Floor((i - minRepCount) / (double)(maxRepCount - minRepCount) * 100)}%");
                    Console.ResetColor();
                }

                foreach (var syntheticRep in syntheticReps)
                    File.Delete(syntheticRep);
                if (verInfo.OutputDir != null)
                    Directory.Delete(verInfo.OutputDir, true);
                timer.Stop();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\r{msg}Done!\t(ET: {timer.Elapsed}");
                Console.ResetColor();

                var writer = new StreamWriter(resultsFilename, append: true);
                foreach (var result in results)
                    writer.WriteLine(result.ToString());
                writer.Close();
            }
        }

        private static Result MeasurePerformance(ProcessStartInfo info, int waitToExitInMilliseconds = 100)
        {
            var result = new Result();

            // Do not enable redirecting stdout. 
            // Because some older versions of MSPC write messages to 
            // console in a way that cause the console to exit, hence
            // this processes assumes it as MSPC has finished processing
            // data, while in fact, all MSPC did was print a message 
            // in a particular way.
            // 
            // info.UseShellExecute = false
            // info.RedirectStandardOutput = true

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

                        // process.StandardOutput.ReadToEnd()
                    }
                }
                while (!process.WaitForExit(waitToExitInMilliseconds));
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
