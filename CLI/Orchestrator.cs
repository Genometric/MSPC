// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.CLI.Interfaces;
using Genometric.MSPC.CLI.Logging;
using Genometric.MSPC.Core;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Genometric.MSPC.CLI
{
    internal class Orchestrator : IDisposable
    {
        private Logger _logger;
        private readonly IExporter<Peak> _exporter;
        private readonly string _defaultLoggerRepoName = "EventsLog";
        public string OutputPath { private set; get; }
        public string LogFile { private set; get; }

        internal string loggerTimeStampFormat = "yyyyMMdd_HHmmssfffffff";

        public Orchestrator() : this(new Exporter<Peak>()) { }

        public Orchestrator(IExporter<Peak> exporter)
        {
            _exporter = exporter;
        }

        public void Orchestrate(string[] args)
        {
            if (!ParseArgs(args, out CommandLineOptions options))
                return;

            if (!AssertOutputPath(options.OutputPath))
                return;

            if (!SetupLogger())
                return;

            if (!AssertInput(options.Input))
                return;

            if (!LoadParserConfig(options, out ParserConfig config))
                return;

            if (!ParseFiles(options.Input, config, out List<Bed<Peak>> samples))
                return;

            if (!Run(samples, options.Options, out Mspc mspc))
                return;

            var attributes = Enum.GetValues(typeof(Attributes)).Cast<Attributes>().ToList();
            var dict = samples.ToDictionary(x => x.FileHashKey, x => Path.GetFileNameWithoutExtension(x.FileName));
            if (!Export(mspc, dict, attributes))
                return;

            _logger.LogStartOfASection("Summary Statistics");
            _logger.LogSummary(samples, dict, mspc.GetResults(), mspc.GetConsensusPeaks(), attributes);

            _logger.LogStartOfASection("Consensus Peaks Count");
            int cPeaksCount = 0;
            foreach (var chr in mspc.GetConsensusPeaks())
                cPeaksCount += chr.Value.Count;
            _logger.Log(cPeaksCount.ToString("N0"));

            _logger.LogFinish();
        }

        private bool ParseArgs(string[] args, out CommandLineOptions options)
        {
            options = new CommandLineOptions();

            try
            {
                options.Parse(args, out bool helpIsDisplayed);
                if (helpIsDisplayed)
                    return false;
                return true;
            }
            catch (Exception e)
            {
                if (_logger == null)
                    Console.WriteLine(e.Message);
                else
                    _logger.LogException(e);
                return false;
            }
        }

        private bool AssertOutputPath(string path)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
                path =
                    Environment.CurrentDirectory + Path.DirectorySeparatorChar +
                    "session_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff", CultureInfo.InvariantCulture);

            OutputPath = path;
            try
            {
                if (Directory.Exists(OutputPath))
                {
                    if (Directory.GetFiles(OutputPath).Any())
                    {
                        int c = 0;
                        do OutputPath = path + c++;
                        while (Directory.Exists(OutputPath));
                        Directory.CreateDirectory(OutputPath);
                    }
                }
                else
                {
                    Directory.CreateDirectory(OutputPath);
                }

                return true;
            }
            catch (Exception e)
            {
                if (_logger == null)
                    Console.WriteLine(e.Message);
                else
                    _logger.LogException(e);
                return false;
            }
        }

        private bool SetupLogger()
        {
            try
            {
                if (_logger != null)
                    return true;

                var repository = _defaultLoggerRepoName + "_" + DateTime.Now.ToString(loggerTimeStampFormat, CultureInfo.InvariantCulture);
                LogFile = OutputPath + Path.DirectorySeparatorChar + repository;
                _logger = new Logger(LogFile, repository, Guid.NewGuid().ToString());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private bool AssertInput(IReadOnlyList<string> input)
        {
            if (input.Count < 2)
            {
                _logger.LogException(
                    string.Format("At least two samples are required; {0} is given.", input.Count));
                return false;
            }

            var missingFiles = new List<string>();
            foreach (var file in input)
                if (!File.Exists(file))
                    missingFiles.Add(file);
            if (missingFiles.Count > 0)
            {
                _logger.LogException(
                    string.Format("The following files are missing: {0}", string.Join("; ", missingFiles.ToArray())));
                return false;
            }

            return true;
        }

        private bool LoadParserConfig(CommandLineOptions options, out ParserConfig config)
        {
            config = new ParserConfig();
            if (options.ParserConfig != null)
            {
                try
                {
                    config = ParserConfig.LoadFromJSON(options.ParserConfig);
                }
                catch (Exception e)
                {
                    _logger.LogException("Error reading parser configuration JSON object: " + e);
                    return false;
                }
            }
            return true;
        }

        private bool ParseFiles(IReadOnlyList<string> files, ParserConfig parserConfig, out List<Bed<Peak>> samples)
        {
            try
            {
                samples = new List<Bed<Peak>>();
                _logger.LogStartOfASection("Parsing Samples");
                _logger.InitializeLoggingParser(files.Count);

                int counter = 0;
                foreach (var file in files)
                {
                    var bedParser = new BedParser(parserConfig)
                    {
                        PValueFormat = parserConfig.PValueFormat,
                        DefaultValue = parserConfig.DefaultValue,
                        DropPeakIfInvalidValue = parserConfig.DropPeakIfInvalidValue
                    };
                    var parsedData = bedParser.Parse(file);
                    samples.Add(parsedData);
                    counter++;
                    _logger.LogParser(
                        counter,
                        files.Count,
                        Path.GetFileNameWithoutExtension(file),
                        parsedData.IntervalsCount,
                        parsedData.PValueMin.Value,
                        parsedData.PValueMean,
                        parsedData.PValueMax.Value);
                }

                return true;
            }
            catch (Exception e)
            {
                samples = null;
                _logger.LogException("Error parsing data: " + e.Message);
                return false;
            }
        }

        private bool Run(List<Bed<Peak>> samples, Config config, out Mspc mspc)
        {
            try
            {
                _logger.LogStartOfASection("Analyzing Samples");
                mspc = new Mspc();
                mspc.StatusChanged += _logger.LogMSPCStatus;
                foreach (var sample in samples)
                    mspc.AddSample(sample.FileHashKey, sample);
                mspc.RunAsync(config);
                mspc.Done.WaitOne();
                return true;
            }
            catch (Exception e)
            {
                mspc = null;
                _logger.LogException(e);
                return false;
            }
        }

        private bool Export(Mspc mspc, Dictionary<uint, string> samplesDictionary, List<Attributes> attributesToExport)
        {
            try
            {
                _logger.LogStartOfASection("Saving Results");
                var options = new Options(
                    path: OutputPath,
                    includeHeader: true,
                    attributesToExport: attributesToExport);

                _exporter.Export(
                    samplesDictionary,
                    mspc.GetResults(), mspc.GetConsensusPeaks(), options);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_logger != null)
                _logger.ShutdownLogger();
        }
    }
}
