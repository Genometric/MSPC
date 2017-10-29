/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Polimi.DEIB.VahidJalili.MSPC.Warehouse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Polimi.DEIB.VahidJalili.MSPC.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime processStartTime = DateTime.Now;
            Stopwatch analysisTime = new Stopwatch();
            Stopwatch parserStopwatch = new Stopwatch();

            var p = new Process<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>();

            if (Samples<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>.Data == null)
                Samples<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>.Data = new Dictionary<uint, GIFP.ParsedChIPseqPeaks<int, Interval<int, MChIPSeqPeak>, MChIPSeqPeak>>();
            if (Sessions<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>.Data == null)
                Sessions<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>.Data = new Dictionary<string, Session<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>>();

            var options = new CommandLineOptions();
            var files = new List<string>();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                for (int i = 0; i < args.Length; i = i + 2)
                {
                    if (args[i] == "-i")
                        files.Add(args[i + 1]);
                }

                if (files.Count <= 1)
                    files = options.inputFiles.ToList();


                if (files.Count < 2)
                {
                    Console.WriteLine("At least two samples are required");
                    Console.WriteLine("Application exit with no process.");
                    Environment.Exit(0);
                }


                /// Checks whether all the samples are valid or not before parsing.
                foreach (var file in files)
                {
                    if (!File.Exists(file))
                    {
                        Console.WriteLine("Missing file: {0}", file);
                        Console.WriteLine("Application exit with no process.");
                        Environment.Exit(0);
                    }
                }


                foreach (var file in files)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Parsing sample : {0}", file);
                    parserStopwatch.Restart();

                    try
                    {
                        var newSample = LoadSamples<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>.LoadSample(file);
                        Samples<Interval<int, MChIPSeqPeak>, MChIPSeqPeak>.Data.Add(newSample.fileHashKey, newSample);
                        p.analyzer.AddSample(newSample.fileHashKey, newSample.fileName);

                        parserStopwatch.Stop();
                        Console.WriteLine("Done...  ET:\t{0}", parserStopwatch.Elapsed.ToString());
                        Console.WriteLine("Read peaks#:\t{0}", newSample.intervalsCount.ToString("N0", CultureInfo.InvariantCulture));
                        Console.WriteLine("Min p-value:\t{0}", string.Format("{0:E3}", newSample.pValueMin.metadata.value));
                        Console.WriteLine("Max p-value:\t{0}", string.Format("{0:E3}", newSample.pValueMax.metadata.value));

                        Console.WriteLine("");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(" ---! an unknown Exception occured while parsing BED files, program will terminate.");
                        Environment.Exit(0);
                    }
                }

                if (options.gamma == -1) options.gamma = options.tauS;

                p.replicateType = options.replicateType;
                p.tauS = options.tauS;
                p.tauW = options.tauW;
                p.alpha = options.alpha;
                p.gamma = options.gamma;
                p.C = options.C;

                Console.WriteLine("Analysis Started...");
                analysisTime.Start();
                try { p.Run(); }
                catch (Exception exception)
                {
                    Console.WriteLine(" ---! an unknown Exception occured while processing samples, program will terminate.");
                    Environment.Exit(0);
                }


                string sessionTitle = p.GetSessionTitle();

                try { p.SaveSession(sessionTitle, processStartTime, DateTime.Now); }
                catch (Exception exception)
                {
                    Console.WriteLine(" ---! an unknown Exception occured while saving analysis results, program will terminate.");
                    Environment.Exit(0);
                }

                try
                {
                    Console.WriteLine("[" + (files.Count + 5).ToString() + "\\" + (files.Count + 5).ToString() + "] Exporting Data.");
                    p.Export(sessionTitle);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(" ---! an unknown Exception occured while exporting analysis results, program will terminate.");
                    Environment.Exit(0);
                }

                Console.WriteLine(" ");
                Console.WriteLine("All processes successfully finished [Analysis ET: {0}", analysisTime.Elapsed.ToString());
                Console.WriteLine(" ");
            }
        }
    }
}
