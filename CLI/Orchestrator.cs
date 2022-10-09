// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.CLI.CommandLineInterface;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.CLI.Interfaces;
using Genometric.MSPC.CLI.Logging;
using Genometric.MSPC.Core;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Genometric.MSPC.CLI
{
    internal class Orchestrator : IDisposable
    {
        private int _degreeOfParallelism = Environment.ProcessorCount;
        private Logger _logger;
        private readonly IExporter<Peak> _exporter;
        private readonly string _defaultLoggerRepoName = "EventsLog";
        public string OutputPath { private set; get; }
        public string LogFile { private set; get; }

        internal string loggerTimeStampFormat = "yyyyMMdd_HHmmssfffffff";

        private readonly Cli _cli;

        public Orchestrator() : this(new Exporter<Peak>()) { }

        public Orchestrator(IExporter<Peak> exporter)
        {
            _exporter = exporter;

            _cli = new Cli(
                Invoke,
                (e, c) =>
                {
                    Logger.LogExceptionStatic(e.Message);
                    Environment.ExitCode = 1;
                });
        }

        // TODO: make this method async and avoid using Wait()
        public void Invoke(string[] args)
        {
            _cli.Invoke(args);
        }

        private void Invoke(CliConfig options)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (!AssertOutputPath(options.OutputPath))
                return;

            if (!SetupLogger())
                return;

            if (!AssertInput(options.InputFiles))
                return;

            if (!LoadParserConfig(options.ParserConfigFilename, out ParserConfig config))
                return;

            if (!ParseFiles(options.InputFiles, config, out List<Bed<Peak>> samples))
                return;

            if (!Run(samples, options, out Mspc mspc))
                return;

            var attributes = Enum.GetValues(typeof(Attributes)).Cast<Attributes>().ToList();
            var dict = samples.ToDictionary(x => x.FileHashKey, x => Path.GetFileNameWithoutExtension(x.FileName));
            if (!Export(mspc, dict, attributes, options.ExcludeHeader))
                return;

            _logger.LogStartOfASection("Summary Statistics");
            _logger.LogSummary(samples, dict, mspc.GetResults(), mspc.GetConsensusPeaks(), attributes);

            _logger.LogStartOfASection("Consensus Peaks Count");
            int cPeaksCount = 0;
            foreach (var chr in mspc.GetConsensusPeaks())
                cPeaksCount += chr.Value.Count;
            _logger.Log(cPeaksCount.ToString("N0"));

            stopwatch.Stop();
            _logger.LogFinish(stopwatch.Elapsed.ToString());
        }

        private bool AssertOutputPath(string path)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
                path =
                    Environment.CurrentDirectory + Path.DirectorySeparatorChar +
                    "session_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff", CultureInfo.InvariantCulture);
            else
                path = path.TrimEnd(Path.DirectorySeparatorChar);

            OutputPath = Path.GetFullPath(path);
            try
            {
                if (Directory.Exists(OutputPath))
                {
                    if (Directory.GetFiles(OutputPath).Any())
                    {
                        int c = 0;
                        do OutputPath = $"{path}_{c++}";
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
                string msg =
                    $"Cannot ensure the given output path " +
                    $"`{OutputPath}`: {e.Message}";
                if (_logger == null)
                    Logger.LogExceptionStatic(msg);
                else
                    _logger.LogException(msg);
                Environment.ExitCode = 1;
                return false;
            }
        }

        private bool SetupLogger()
        {
            if (_logger != null)
                return true;

            var repository = 
                _defaultLoggerRepoName + "_" + 
                DateTime.Now.ToString(
                    loggerTimeStampFormat, 
                    CultureInfo.InvariantCulture);

            LogFile = OutputPath + Path.DirectorySeparatorChar + repository + ".txt";
            _logger = new Logger(LogFile, repository, Guid.NewGuid().ToString(), OutputPath);
            return true;
        }

        private bool AssertInput(IReadOnlyCollection<string> input)
        {
            if (input.Count < 2)
            {
                _logger.LogException(
                    string.Format("at least two samples are required; {0} is given.", input.Count));
                Environment.ExitCode = 1;
                return false;
            }

            var missingFiles = new List<string>();
            foreach (var file in input)
                if (!File.Exists(file))
                    missingFiles.Add(file);
            if (missingFiles.Count > 0)
            {
                _logger.LogException(
                    string.Format("the following files are missing: {0}", string.Join("; ", missingFiles.ToArray())));
                Environment.ExitCode = 1;
                return false;
            }

            return true;
        }

        private bool LoadParserConfig(string filename, out ParserConfig config)
        {
            config = new ParserConfig();
            if (filename != null)
            {
                try
                {
                    config = ParserConfig.LoadFromJSON(filename);
                    if (config == null)
                    {
                        _logger.LogException(string.Format(
                            "error reading parser configuration JSON object, " +
                            "check if the given file '{0}' exists and is accessible.",
                            filename));
                        return false;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogException("error reading parser configuration JSON object: " + e.Message);
                    Environment.ExitCode = 1;
                    return false;
                }
            }
            return true;
        }

        private bool ParseFiles(IReadOnlyCollection<string> files, ParserConfig parserConfig, out List<Bed<Peak>> samples)
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
                        DropPeakIfInvalidValue = parserConfig.DropPeakIfInvalidValue,
                        Culture = parserConfig.Culture
                    };
                    var parsedData = bedParser.Parse(file);
                    samples.Add(parsedData);
                    counter++;
                    _logger.LogParser(
                        counter,
                        files.Count,
                        file,
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
                _logger.LogException("error parsing data: " + e.Message);
                Environment.ExitCode = 1;
                return false;
            }
        }

        private bool Run(List<Bed<Peak>> samples, CliConfig options, out Mspc mspc)
        {
            try
            {
                _logger.LogStartOfASection("Analyzing Samples");
                mspc = new Mspc()
                {
                    DegreeOfParallelism = _degreeOfParallelism
                };
                mspc.StatusChanged += _logger.LogMSPCStatus;
                foreach (var sample in samples)
                    mspc.AddSample(sample.FileHashKey, sample);
                mspc.RunAsync(options);
                mspc.Done.WaitOne();
                return true;
            }
            catch (Exception e)
            {
                mspc = null;
                _logger.LogException(e);
                Environment.ExitCode = 1;
                return false;
            }
        }

        private bool Export(Mspc mspc, Dictionary<uint, string> samplesDictionary, List<Attributes> attributesToExport, bool excludeHeader)
        {
            try
            {
                _logger.LogStartOfASection("Saving Results");
                var options = new Options(
                    path: OutputPath,
                    includeHeader: !excludeHeader,
                    attributesToExport: attributesToExport);

                _exporter.Export(
                    samplesDictionary,
                    mspc.GetResults(), mspc.GetConsensusPeaks(), options);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                Environment.ExitCode = 1;
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
