// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static Genometric.MSPC.CLI.Logging.Table;

namespace Genometric.MSPC.CLI.Logging
{
    public class Logger
    {
        private readonly int _sectionHeaderLenght = 20;
        private readonly int _fileNameMaxLength = 20;
        private static readonly string _cannotContinue = "MSPC cannot continue.";
        private bool _lastStatusUpdatedItsPrevious;
        private Table _parserLogTable;

        private ILog log;
        private string _repository;
        private string _name;

        public Logger(string logFilePath, string repository, string name)
        {
            Setup(logFilePath, repository, name);
        }

        private void Setup(string logFilePath, string repository, string name)
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
        }

        public void LogStartOfASection(string header)
        {
            string msg = ".::." + header.PadLeft(((_sectionHeaderLenght - header.Length) / 2) + header.Length, '.').PadRight(_sectionHeaderLenght, '.') + ".::.";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Environment.NewLine + msg);
            Console.ResetColor();
            log.Info(msg);
        }

        public void LogException(Exception e)
        {
            LogException(e.Message);
        }

        public void LogException(string message)
        {
            LogExceptionStatic(message);
            log.Error(message);
            log.Info(_cannotContinue);
        }

        public static void LogExceptionStatic(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("Error: {0}", message));
            Console.ResetColor();
            Console.WriteLine(_cannotContinue);
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
                Console.Write(msg.ToString());
                _lastStatusUpdatedItsPrevious = true;
            }
            else
            {
                if (_lastStatusUpdatedItsPrevious)
                    msg.Insert(0, Environment.NewLine);
                Console.WriteLine(msg.ToString());
                _lastStatusUpdatedItsPrevious = false;
            }
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
            log.Info(message);
        }

        public void LogFinish(string message = "All processes successfully finished.")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Environment.NewLine + message + Environment.NewLine);
            Console.ResetColor();
            log.Info(message);
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
            _parserLogTable.AddHeader(new string[]
            {
                "#", "Filename", "Read peaks#", "Min p-value", "Mean p-value", "Max p-value"
            });
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
            var row = _parserLogTable.GetRow(new string[]
            {
                IdxColFormat(fileNumber, filesToParse),
                filename,
                peaksCount.ToString("N0"),
                string.Format("{0:E3}", minPValue),
                string.Format("{0:E3}", meanPValue),
                string.Format("{0:E3}", maxPValue)
            });

            Console.WriteLine(row);
            log.Info(row);
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
            table.AddHeader(headerColumns);

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

                var row = table.GetRow(sampleSummary);
                Console.WriteLine(row);
                log.Info(row);
            }
        }
    }
}
