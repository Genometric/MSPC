// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Genometric.MSPC.CLI
{
    internal class Logger
    {
        private readonly int _fileNameMaxLenght = 20;

        public void Log(ProgressReport report)
        {
            var msg = new StringBuilder();
            if (report.UpdatesPrevious)
                msg.Append("\r");

            if (report.SubStep)
                msg.Append(string.Format(
                    "  └── {0}/{1}\t({2})\t{3}",
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

            if (report.UpdatesPrevious)
                Console.Write(msg.ToString());
            else
                Console.WriteLine(msg.ToString());
        }

        public void WriteToConsole(List<string> lines)
        {
            foreach (var line in lines)
                Console.WriteLine(line);
        }

        public List<string> CreateSummaryStats(
            List<Bed<Peak>> samples,
            Dictionary<uint, string> samplesDict,
            ReadOnlyDictionary<uint, Result<Peak>> results,
            ReadOnlyDictionary<string, List<ProcessedPeak<Peak>>> consensusPeaks,
            List<Attributes> exportedAttributes = null)
        {
            var rtv = new List<string>();
            int columnsCount = exportedAttributes.Count + 2;
            int columnWidth = 0;
            foreach (var attribute in exportedAttributes)
                columnWidth = Math.Max(attribute.ToString().Length, columnWidth);

            rtv.Add(Environment.NewLine + ".::. Summary statistics .::." + Environment.NewLine);

            // Create table header
            int i = 2;
            var headerColumns = new string[columnsCount];
            headerColumns[0] = "Filename";
            headerColumns[1] = "#Peaks";
            foreach (var att in exportedAttributes)
                headerColumns[i++] = att.ToString();
            rtv.Add(RenderRow(columnWidth, headerColumns));

            // Create table header line
            var headerLines = new string[columnsCount];
            headerLines[0] = new string('-', _fileNameMaxLenght);
            for (i = 1; i < columnsCount; i++)
                headerLines[i] = new string('-', columnWidth);
            rtv.Add(RenderRow(columnWidth, headerLines));

            // Per sample stats
            foreach (var res in results)
            {
                double totalPeaks = samples.Find(x => x.FileHashKey == res.Key).IntervalsCount;
                var sampleSummary = new string[columnsCount];
                sampleSummary[0] = samplesDict[res.Key];
                sampleSummary[1] = totalPeaks.ToString("N0");
                i = 2;
                foreach(var att in exportedAttributes)
                {
                    int value = 0;
                    foreach (var chr in res.Value.Chromosomes)
                        value += chr.Value.Count(att);
                    sampleSummary[i++] = (value / totalPeaks).ToString("P");
                }
                rtv.Add(RenderRow(columnWidth, sampleSummary));
            }
            rtv.Add(RenderRow(columnWidth, headerLines));

            // Consensus peaks stats
            rtv.Add(" ");
            int cPeaksCount = 0;
            foreach (var chr in consensusPeaks)
                cPeaksCount += chr.Value.Count;
            rtv.Add(".::. Consensus Peaks Count .::.");
            rtv.Add(cPeaksCount.ToString("N0") + Environment.NewLine);

            return rtv;
        }

        private string RenderRow(int columnwidth, params string[] columns)
        {
            var row = new StringBuilder();
            row.Append(TruncateString(columns[0], _fileNameMaxLenght) + "\t");
            for (int i = 1; i < columns.Length; i++)
                row.Append(columns[i].PadLeft(columnwidth) + "\t");
            return row.ToString();
        }

        private string TruncateString(string value, int maxLength)
        {
            return value.Length <= maxLength ? new string(' ', maxLength - value.Length) + value : "..." + value.Substring(value.Length - maxLength - 3, maxLength);
        }
    }
}
