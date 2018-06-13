// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Genometric.MSPC.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var cliOptions = new CommandLineOptions();
            try
            {
                cliOptions.Parse(args);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }

            if (cliOptions.Input.Count < 2)
            {
                Console.WriteLine(String.Format("At least two samples are required; {0} is given.", cliOptions.Input.Count));
                Environment.Exit(0);
            }

            foreach (var file in cliOptions.Input)
                if (!File.Exists(file))
                {
                    Console.WriteLine("Missing file: {0}", file);
                    Console.WriteLine("MSPC can not continue.");
                    Environment.Exit(0);
                }

            var orchestrator = new Orchestrator(cliOptions.Options, cliOptions.Input);

            var et = new Stopwatch();
            foreach (var file in cliOptions.Input)
            {
                Console.WriteLine("Parsing sample : {0}", file);
                et.Restart();

                try
                {
                    orchestrator.LoadSample(file);
                    var parsedSample = orchestrator.samples[orchestrator.samples.Count - 1];
                    et.Stop();
                    Console.WriteLine("Done...  ET:\t{0}", et.Elapsed.ToString());
                    Console.WriteLine("Read peaks#:\t{0}", parsedSample.IntervalsCount.ToString("N0", CultureInfo.InvariantCulture));
                    Console.WriteLine("Min p-value:\t{0}", string.Format("{0:E3}", parsedSample.PValueMin.Value));
                    Console.WriteLine("Max p-value:\t{0}", string.Format("{0:E3}", parsedSample.PValueMax.Value));
                    Console.WriteLine("");
                }
                catch (Exception)
                {
                    Console.WriteLine(" ---! an unknown Exception occurred while parsing BED files; MSPC can not continue.");
                    Environment.Exit(0);
                }
            }

            //if (options.gamma == -1)
            //options.gamma = options.tauS;

            Console.WriteLine("Analysis started ...");
            et.Restart();
            try { orchestrator.Run(); }
            catch (Exception e)
            {
                Console.WriteLine(" ---! an unknown Exception occurred while processing samples, program will terminate." + e.Message);
                Environment.Exit(0);
            }

            try
            {
                Console.WriteLine("\n\rSaving results ...");
                orchestrator.Export();
            }
            catch (Exception e)
            {
                Console.WriteLine(" ---! an unknown Exception occurred while exporting analysis results, program will terminate." + e.Message);
                Environment.Exit(0);
            }

            et.Stop();
            Console.WriteLine(" ");
            Console.WriteLine("All processes successfully finished [Analysis ET: {0}", et.Elapsed.ToString() + "]");
            Console.WriteLine(" ");
        }
    }
}
