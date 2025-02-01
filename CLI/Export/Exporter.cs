using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.CLI.Interfaces;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Genometric.MSPC.CLI.Exporter
{
    public class Exporter<I> : IExporter<I>
        where I : IPeak
    {
        private const char _delimiter = '\t';

        private Options _options;

        public void Export(
            Dictionary<uint, string> fileNames,
            Dictionary<uint, Result<I>> results,
            Dictionary<string, Dictionary<char, List<ProcessedPeak<I>>>> consensusPeaks,
            Options options)
        {
            _options = options;

            if (!Directory.Exists(_options.Path))
                Directory.CreateDirectory(_options.Path);

            ExportConsensusPeaks(true, consensusPeaks);
            ExportConsensusPeaks(false, consensusPeaks);

            foreach (var result in results)
            {
                string samplePath = Path.Join(_options.Path, fileNames[result.Key]);
                Directory.CreateDirectory(samplePath);
                foreach (var attribute in options.AttributesToExport)
                {
                    WriteToFile(true, samplePath, result.Value, attribute);
                    WriteToFile(false, samplePath, result.Value, attribute);
                }
            }
        }

        private static string Header(bool mspcFormat)
        {
            if (mspcFormat)
                return string.Join(
                    _delimiter, new string[]
                    {
                        "chr",
                        "start",
                        "stop",
                        "name",
                        "-1xlog10(p-value)",
                        "strand",
                        "xSqrd",
                        "-1xlog10(Right-Tail Probability)",
                        "-1xlog10(AdjustedP-value)"
                    });
            else
                return string.Join(
                    _delimiter, new string[]
                    {
                        "chr",
                        "start",
                        "stop",
                        "name",
                        "-1xlog10(p-value)",
                        "strand"
                    });
        }

        private void WriteToFile(
            bool mspcFormat,
            string samplePath,
            Result<I> data,
            Attributes attribute)
        {
            string filename = Path.Join(
                samplePath,
                attribute.ToString() + (mspcFormat ? "_mspc_peaks.txt" : ".bed"));

            File.Create(filename).Dispose();
            using var writter = new StreamWriter(filename);
            if (_options.IncludeHeader)
                writter.WriteLine(Header(mspcFormat));

            var sortedChrs = data.Chromosomes.Keys.ToArray();
            Array.Sort(sortedChrs, new AlphanumComparer());

            foreach (var chr in sortedChrs)
            {
                foreach (var strand in data.Chromosomes[chr])
                {
                    var sortedDictionary =
                        from entry
                        in strand.Value.Get(attribute)
                        orderby entry
                        ascending
                        select entry;

                    if (mspcFormat)
                    {
                        foreach (var item in sortedDictionary)
                        {
                            writter.WriteLine(
                                string.Join(_delimiter, new string[]
                                {
                                        chr,
                                        item.Source.Left.ToString(),
                                        item.Source.Right.ToString(),
                                        item.Source.Name,
                                        ConvertPValue(item.Source.Value),
                                        strand.Key.ToString(),
                                        Math.Round(item.XSquared, 3).ToString(),
                                        ConvertPValue(item.RTP),
                                        ConvertPValue(item.AdjPValue)
                                }));
                        }
                    }
                    else
                    {
                        foreach (var item in sortedDictionary)
                        {
                            writter.WriteLine(
                                string.Join(_delimiter, new string[]
                                {
                                        chr,
                                        item.Source.Left.ToString(),
                                        item.Source.Right.ToString(),
                                        item.Source.Name,
                                        ConvertPValue(item.Source.Value),
                                        strand.Key.ToString()
                                }));
                        }
                    }
                }
            }
        }

        private void ExportConsensusPeaks(
            bool mspcFormat,
            Dictionary<string, Dictionary<char, List<ProcessedPeak<I>>>> peaks)
        {
            string filename = Path.Join(
                _options.Path,
                mspcFormat ? "ConsensusPeaks_mspc_peaks.txt" : "ConsensusPeaks.bed");

            File.Create(filename).Dispose();
            using var writter = new StreamWriter(filename);
            if (_options.IncludeHeader)
                writter.WriteLine(Header(mspcFormat));

            var sortedChrs = peaks.Keys.ToArray();
            Array.Sort(sortedChrs, new AlphanumComparer());

            foreach (var chr in sortedChrs)
            {
                foreach (var strand in peaks[chr])
                {
                    var sortedPeaks = from entry
                                      in peaks[chr][strand.Key]
                                      orderby entry
                                      ascending
                                      select entry;

                    if (mspcFormat)
                    {
                        foreach (var peak in sortedPeaks)
                        {
                            writter.WriteLine(
                                string.Join(_delimiter, new string[]
                                {
                                        chr,
                                        peak.Source.Left.ToString(),
                                        peak.Source.Right.ToString(),
                                        peak.Source.Name,
                                        ConvertPValue(peak.Source.Value),
                                        strand.Key.ToString(),
                                        Math.Round(peak.XSquared, 3).ToString(),
                                        ConvertPValue(peak.RTP),
                                        ConvertPValue(peak.AdjPValue)
                                }));
                        }
                    }
                    else
                    {
                        foreach (var peak in sortedPeaks)
                        {
                            writter.WriteLine(
                                string.Join(_delimiter, new string[]
                                {
                                        chr,
                                        peak.Source.Left.ToString(),
                                        peak.Source.Right.ToString(),
                                        peak.Source.Name,
                                        ConvertPValue(peak.Source.Value),
                                        strand.Key.ToString()
                                }));
                        }
                    }
                }
            }
        }

        private static string ConvertPValue(double pValue)
        {
            if (pValue != 0)
                return
                    Math.Round((-1) * Math.Log10(pValue), 3)
                    .ToString(CultureInfo.CreateSpecificCulture(
                        CultureInfo.CurrentCulture.Name));
            return "inf";
        }
    }
}
