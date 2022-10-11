// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.CLI.ConsoleAbstraction;
using Genometric.MSPC.Core.Model;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.CommandLine;
using System.IO;
using System.Reflection;
using System.Text;
using static Genometric.MSPC.CLI.Logging.Table;

namespace Genometric.MSPC.CLI.Logging
{
    public class Logger
    {
        private readonly IConsoleExtended _console;
        private readonly int _sectionHeaderLenght = 20;
        private readonly int _fileNameMaxLength = 20;
        private static readonly string _cannotContinue = "MSPC cannot continue.";
        private static readonly string _linkToDocumentation = "Documentation: https://genometric.github.io/MSPC/";
        private static readonly string _linkToIssuesPage = "Questions or bug report: https://github.com/Genometric/MSPC/issues";
        private bool _lastStatusUpdatedItsPrevious;
        private Table _parserLogTable;

        private ILog log;
        private string _repository;
        private string _name;

        /// <summary>
        /// Gets a message that hints users how to use MSPC CLI help
        /// </summary>
        private static string HintHelpMessage
        {
            get
            {
                return
                    $"You may run {Assembly.GetCallingAssembly().GetName().Name} " +
                    $"with either of [{CommandLineOptions.HelpOption}] tags for help.";
            }
        }

        public Logger(IConsoleExtended console, string logFilePath, string repository, string name, string exportPath)
        {
            _console = console;
            Setup(logFilePath, repository, name, exportPath);
        }

        private void Setup(string logFilePath, string repository, string name, string exportPath)
        {
            _repository = repository;
            _name = name;
            LogManager.CreateRepository(_repository);
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository(_repository);

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date\t[%thread]\t%-5level\t%message%newline";
            patternLayout.ActivateOptions();

            var roller = new RollingFileAppender
            {
                AppendToFile = false,
                File = logFilePath,
                Layout = patternLayout,
                MaxSizeRollBackups = 5,
                MaximumFileSize = "1GB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
            log = LogManager.GetLogger(_repository, _name);

            log.Info("NOTE THAT THE LOG PATTERN IS: <Date> <#Thread> <Level> <Message>");
            Log($"Export Directory: {exportPath}", ConsoleColor.DarkGray);
        }

        public void LogStartOfASection(string header)
        {
            string msg = ".::." + header.PadLeft(((_sectionHeaderLenght - header.Length) / 2) + header.Length, '.').PadRight(_sectionHeaderLenght, '.') + ".::.";
            Log(Environment.NewLine + msg, ConsoleColor.Yellow);
        }

        public void LogException(Exception e)
        {
            LogException(e.Message);
        }

        public void LogException(string message)
        {
            LogExceptionStatic(_console, message);
            log.Error(message);
            log.Info(_linkToDocumentation);
            log.Info(_linkToIssuesPage);
            log.Info(HintHelpMessage);
            log.Info(_cannotContinue);
        }

        public static void LogExceptionStatic(IConsoleExtended console, string message)
        {
            console.SetForegroundColor(ConsoleColor.Red);
            console.WriteLine(string.Format("Error: {0}", message));
            console.SetForegroundColor(ConsoleColor.Yellow);
            console.WriteLine(HintHelpMessage);
            console.ResetColor();
            console.WriteLine(_cannotContinue);
        }

        public void LogWarning(string message)
        {
            log.Warn(message);
            LogWarningStatic(_console, message);
        }

        public static void LogWarningStatic(IConsoleExtended console, string message)
        {
            console.SetForegroundColor(ConsoleColor.DarkMagenta);
            console.WriteLine($"Warning: {message}");
            console.ResetColor();
        }

        public void LogMSPCStatus(object sender, ValueEventArgs e)
        {
            var msg = new StringBuilder();
            var report = e.Value;

            if (report.SubStep)
                msg.Append(string.Format(
                    "  └── {0}/{1}\t({2}) {3}",
                    report.Step.ToString("N0"),
                    report.StepCount.ToString("N0"),
                    (report.Step / (double)report.StepCount).ToString("P"),
                    report.Message ?? ""));
            else
                msg.Append(string.Format(
                    "[{0}/{1}] {2}",
                    report.Step,
                    report.StepCount,
                    report.Message));

            log.Info(msg.ToString());
            if (report.UpdatesPrevious)
            {
                msg.Insert(0, "\r");
                _console.Write(msg.ToString());
                _lastStatusUpdatedItsPrevious = true;
            }
            else
            {
                if (_lastStatusUpdatedItsPrevious)
                    msg.Insert(0, Environment.NewLine);
                _console.WriteLine(msg.ToString());
                _lastStatusUpdatedItsPrevious = false;
            }
        }

        public void Log(string message, ConsoleColor color = ConsoleColor.Black)
        {
            if (color != ConsoleColor.Black)
                _console.SetForegroundColor(color);
            _console.WriteLine(message);
            _console.ResetColor();
            log.Info(message);
        }

        public void LogFinish(string elapsedTime, string message = "All processes successfully finished.")
        {
            _console.SetForegroundColor(ConsoleColor.Green);
            _console.WriteLine(Environment.NewLine);
            Log($"Elapsed time: {elapsedTime}");
            Log(message);
            _console.WriteLine(Environment.NewLine);
            _console.ResetColor();
        }

        public void ShutdownLogger()
        {
            LogManager.Flush(5000);
            log4net.Repository.Hierarchy.Logger l = (log4net.Repository.Hierarchy.Logger)LogManager.GetLogger(_repository, _name).Logger;
            l.RemoveAllAppenders();
            LogManager.GetLogger(_repository, _name).Logger.Repository.Shutdown();
        }

        public void InitializeLoggingParser(int samplesCount)
        {
            var columnsWidth = new int[] { IdxColChars(samplesCount), _fileNameMaxLength, 11, 11, 12, 11 };
            _parserLogTable = new Table(columnsWidth);
            _parserLogTable.AddHeader(out string renderedHeaders, out string renderedHeaderLines, new string[]
            {
                "#", "Filename", "Read peaks#", "Min p-value", "Mean p-value", "Max p-value"
            });

            Log(renderedHeaders);
            Log(renderedHeaderLines);
        }

        public void LogParser(
            int fileNumber,
            int filesToParse,
            string filename,
            int peaksCount,
            double minPValue,
            double meanPValue,
            double maxPValue)
        {
            var columns = new string[]
            {
                IdxColFormat(fileNumber, filesToParse),
                filename,
                peaksCount.ToString("N0"),
                string.Format("{0:E3}", minPValue),
                string.Format("{0:E3}", meanPValue),
                string.Format("{0:E3}", maxPValue)
            };

            columns[1] = Path.GetFileNameWithoutExtension(filename);
            _console.WriteLine(_parserLogTable.GetRow(true, columns));

            columns[1] = filename;
            log.Info(_parserLogTable.GetRow(false, columns));
        }

        public void LogSummary(
            List<Bed<Peak>> samples,
            Dictionary<uint, string> samplesDict,
            ReadOnlyDictionary<uint, Result<Peak>> results,
            ReadOnlyDictionary<string, List<ProcessedPeak<Peak>>> consensusPeaks,
            List<Attributes> exportedAttributes = null)
        {
            // Create table header
            int i;
            int columnsCount = exportedAttributes.Count + 3;
            int[] columnsWidth = new int[columnsCount];
            var headerColumns = new string[columnsCount];
            headerColumns[0] = "#";
            headerColumns[1] = "Filename";
            headerColumns[2] = "Read peaks#";
            columnsWidth[0] = IdxColChars(samples.Count);
            columnsWidth[1] = _fileNameMaxLength;
            columnsWidth[2] = headerColumns[2].Length;
            for (i = 3; i < columnsCount; i++)
            {
                headerColumns[i] = exportedAttributes[i - 3].ToString();
                columnsWidth[i] = headerColumns[i].Length > 8 ? headerColumns[i].Length : 8;
            }
            var table = new Table(columnsWidth);
            table.AddHeader(out string renderedHeaders, out string renderedHeaderLines, headerColumns);
            Log(renderedHeaders);
            Log(renderedHeaderLines);

            // Per sample stats
            int j = 1;
            foreach (var res in results)
            {
                double totalPeaks = samples.Find(x => x.FileHashKey == res.Key).IntervalsCount;
                var sampleSummary = new string[columnsCount];
                sampleSummary[0] = IdxColFormat(j++, results.Count);
                sampleSummary[1] = samplesDict[res.Key];
                sampleSummary[2] = totalPeaks.ToString("N0");
                i = 3;
                foreach (var att in exportedAttributes)
                {
                    int value = 0;
                    foreach (var chr in res.Value.Chromosomes)
                        value += chr.Value.Count(att);
                    sampleSummary[i++] = (value / totalPeaks).ToString("P");
                }

                var row = table.GetRow(true, sampleSummary);
                _console.WriteLine(row);
                log.Info(row);
            }
        }
    }
}
