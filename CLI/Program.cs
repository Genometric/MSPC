// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.CLI.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;


namespace Genometric.MSPC.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = Environment.CurrentDirectory;

            DateTime processStartTime = DateTime.Now;
            Stopwatch analysisTime = new Stopwatch();
            Stopwatch parserStopwatch = new Stopwatch();

            var orchestrator = new Orchestrator();
            /*
            if (Samples<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>.Data == null)
                Samples<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>.Data = new Dictionary<uint, GIFP.ParsedChIPseqPeaks<int, Interval<int, MChIPSeqPeak>, MChIPSeqPeak>>();
            if (Sessions<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>.Data == null)
                Sessions<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>.Data = new Dictionary<string, Session<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>>();
*/
            //var options = new CommandLineOptions();
            var files = new List<string>();
            //if (CommandLine.Parser.Default.ParseArguments(args, options))
            //{
            for (int i = 0; i < args.Length; i = i + 2)
                if (args[i] == "-i")
                    files.Add(args[i + 1]);

            /*if (files.Count <= 1)
                files = options.inputFiles.ToList();*/

            /*if (files.Count < 2)
            {
                Console.WriteLine("At least two samples are required; MSPC can not continue.");
                Environment.Exit(0);
            }*/

            foreach (var file in files)
                if (!File.Exists(file))
                {
                    Console.WriteLine("Missing file: {0}", file);
                    Console.WriteLine("MSPC can not continue.");
                    Environment.Exit(0);
                }


            foreach (var file in files)
            {
                Console.WriteLine("Parsing sample : {0}", file);
                parserStopwatch.Restart();

                try
                {
                    orchestrator.LoadSample(file);
                    var parsedSample = orchestrator.samples[orchestrator.samples.Count - 1];
                    parserStopwatch.Stop();
                    Console.WriteLine("Done...  ET:\t{0}", parserStopwatch.Elapsed.ToString());
                    Console.WriteLine("Read peaks#:\t{0}", parsedSample.IntervalsCount.ToString("N0", CultureInfo.InvariantCulture));
                    Console.WriteLine("Min p-value:\t{0}", string.Format("{0:E3}", parsedSample.PValueMin.Value));
                    Console.WriteLine("Max p-value:\t{0}", string.Format("{0:E3}", parsedSample.PValueMax.Value));
                    Console.WriteLine("");
                }
                catch (Exception)
                {
                    Console.WriteLine(" ---! an unknown Exception occured while parsing BED files; MSPC can not continue.");
                    Environment.Exit(0);
                }
            }

            //if (options.gamma == -1)
            //options.gamma = options.tauS;

            orchestrator.replicateType = "bio"; // options.replicateType;
            orchestrator.tauS = 1e-8;//options.tauS;
            orchestrator.tauW = 1e-4;// options.tauW;
            orchestrator.alpha = 0.05f;// options.alpha;
            orchestrator.gamma = 1e-8;//options.gamma;
            orchestrator.C = 1;// options.C;

            Console.WriteLine("Analysis started ...");
            analysisTime.Start();
            try { orchestrator.Run(); }
            catch (Exception exception)
            {
                Console.WriteLine(" ---! an unknown Exception occured while processing samples, program will terminate.");
                Environment.Exit(0);
            }



            try
            {
                Console.WriteLine("\n\rSaving results ...");
                orchestrator.Export();
            }
            catch (Exception exception)
            {
                Console.WriteLine(" ---! an unknown Exception occured while exporting analysis results, program will terminate.");
                Environment.Exit(0);
            }

            Console.WriteLine(" ");
            Console.WriteLine("All processes successfully finished [Analysis ET: {0}", analysisTime.Elapsed.ToString() + "]");
            Console.WriteLine(" ");
        }
    }
}
