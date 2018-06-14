// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Genometric.MSPC.CLI
{
    static class Program
    {
        static void Main(string[] args)
        {
            var cliOptions = new CommandLineOptions();
            try
            {
                cliOptions.Parse(args);
            }
            catch (Exception e)
            {
                ShowMessageAndExit(e.Message);
            }

            if (cliOptions.Input.Count < 2)
                ShowMessageAndExit(String.Format("At least two samples are required; {0} is given.", cliOptions.Input.Count));

            foreach (var file in cliOptions.Input)
                if (!File.Exists(file))
                    ShowMessageAndExit(String.Format("Missing file: {0}", file));

            var orchestrator = new Orchestrator(cliOptions.Options, cliOptions.Input);

            var et = new Stopwatch();
            foreach (var file in cliOptions.Input)
            {
                Console.WriteLine(String.Format("Parsing sample : {0}", file));
                et.Restart();

                try
                {
                    orchestrator.ParseSample(file);
                    var parsedSample = orchestrator.samples[orchestrator.samples.Count - 1];
                    et.Stop();
                    Console.WriteLine("Done...  ET:\t{0}", et.Elapsed.ToString());
                    Console.WriteLine("Read peaks#:\t{0}", parsedSample.IntervalsCount.ToString("N0", CultureInfo.InvariantCulture));
                    Console.WriteLine("Min p-value:\t{0}", string.Format("{0:E3}", parsedSample.PValueMin.Value));
                    Console.WriteLine("Max p-value:\t{0}", string.Format("{0:E3}", parsedSample.PValueMax.Value));
                    Console.WriteLine("");
                }
                catch (Exception e)
                {
                    ShowMessageAndExit("The following exception has occurred while parsing input files: " + e.Message);
                }
            }

            Console.WriteLine("Analysis started ...");
            et.Restart();
            try
            {
                orchestrator.Run();
            }
            catch (Exception e)
            {
                ShowMessageAndExit("The following exception has occurred while processing the samples: " + e.Message);
            }

            try
            {
                Console.WriteLine("\n\rSaving results ...");
                orchestrator.Export();
            }
            catch (Exception e)
            {
                ShowMessageAndExit("The following exception has occurred while saving analysis results: " + e.Message);
            }

            et.Stop();
            Console.WriteLine(" ");
            Console.WriteLine(String.Format("All processes successfully finished [Analysis ET: {0}]", et.Elapsed.ToString()));
            Console.WriteLine(" ");
        }

        private static void ShowMessageAndExit(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("MSPC cannot continue.");
            Environment.Exit(0);
        }
    }
}
