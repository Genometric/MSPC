// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Genometric.MSPC.CLI.Exporter
{
    public class Exporter<I>
        where I : IPeak
    {
        private Options _options;

        public void Export(
            Dictionary<uint, string> fileNames,
            ReadOnlyDictionary<uint, Result<I>> results,
            ReadOnlyDictionary<string, List<ProcessedPeak<I>>> consensusPeaks,
            Options options)
        {
            _options = options;

            if (!Directory.Exists(_options.Path))
                Directory.CreateDirectory(_options.Path);

            ExportConsensusPeaks(true, consensusPeaks);
            ExportConsensusPeaks(false, consensusPeaks);

            foreach (var result in results)
            {
                string samplePath = _options.Path + Path.DirectorySeparatorChar + fileNames[result.Key];
                Directory.CreateDirectory(samplePath);
                foreach (var attribute in options.AttributesToExport)
                {
                    WriteToFile(true, samplePath, result.Value, attribute);
                    WriteToFile(false, samplePath, result.Value, attribute);
                }
            }
        }

        private string Header(bool mspcFormat)
        {
            return mspcFormat ?
                "chr\tstart\tstop\tname\t-1xlog10(p-value)\txSqrd\t-1xlog10(Right-Tail Probability)\t-1xlog10(AdjustedP-value)" :
                "chr\tstart\tstop\tname\t-1xlog10(p-value)";
        }

        private void WriteToFile(bool mspcFormat, string samplePath, Result<I> data, Attributes attribute)
        {
            string filename = samplePath + Path.DirectorySeparatorChar + attribute.ToString() + (mspcFormat ? "_mspc_peaks.txt" : ".bed");
            File.Create(filename).Dispose();
            using (StreamWriter writter = new StreamWriter(filename))
            {
                if (_options.IncludeHeader)
                    writter.WriteLine(Header(mspcFormat));

                var sortedChrs = data.Chromosomes.Keys.ToArray();
                Array.Sort(sortedChrs, new AlphanumComparer());

                foreach (var chr in sortedChrs)
                {
                    var sortedDictionary = from entry
                                           in data.Chromosomes[chr].Get(attribute)
                                           orderby entry
                                           ascending
                                           select entry;

                    if (mspcFormat)
                        foreach (var item in sortedDictionary)
                            writter.WriteLine(
                                chr + "\t" +
                                item.Source.Left.ToString() + "\t" +
                                item.Source.Right.ToString() + "\t" +
                                item.Source.Name + "\t" +
                                ConvertPValue(item.Source.Value) + "\t" +
                                Math.Round(item.XSquared, 3) + "\t" +
                                ConvertPValue(item.RTP) + "\t" +
                                ConvertPValue(item.AdjPValue));
                    else
                        foreach (var item in sortedDictionary)
                            writter.WriteLine(
                                chr + "\t" +
                                item.Source.Left.ToString() + "\t" +
                                item.Source.Right.ToString() + "\t" +
                                item.Source.Name + "\t" +
                                ConvertPValue(item.Source.Value));
                }
            }
        }

        private void ExportConsensusPeaks(bool mspcFormat, ReadOnlyDictionary<string, List<ProcessedPeak<I>>> peaks)
        {
            string filename = _options.Path + Path.DirectorySeparatorChar + (mspcFormat ? "ConsensusPeaks_mspc_peaks.txt" : "ConsensusPeaks.bed");
            File.Create(filename).Dispose();
            using (StreamWriter writter = new StreamWriter(filename))
            {
                if (_options.IncludeHeader)
                    writter.WriteLine(Header(mspcFormat));

                var sortedChrs = peaks.Keys.ToArray();
                Array.Sort(sortedChrs, new AlphanumComparer());

                foreach (var chr in sortedChrs)
                {
                    var sortedPeaks = from entry
                                      in peaks[chr]
                                      orderby entry
                                      ascending
                                      select entry;

                    if (mspcFormat)
                        foreach (var peak in sortedPeaks)
                            writter.WriteLine(
                                chr + "\t" +
                                peak.Source.Left.ToString() + "\t" +
                                peak.Source.Right.ToString() + "\t" +
                                peak.Source.Name + "\t" +
                                ConvertPValue(peak.Source.Value) + "\t" +
                                Math.Round(peak.XSquared, 3) + "\t" +
                                ConvertPValue(peak.RTP) + "\t" +
                                ConvertPValue(peak.AdjPValue));
                    else
                        foreach (var peak in sortedPeaks)
                            writter.WriteLine(
                                chr + "\t" +
                                peak.Source.Left.ToString() + "\t" +
                                peak.Source.Right.ToString() + "\t" +
                                peak.Source.Name + "\t" +
                                ConvertPValue(peak.Source.Value));
                }
            }
        }

        private string ConvertPValue(double pValue)
        {
            if (pValue != 0)
                return (Math.Round((-1) * Math.Log10(pValue), 3)).ToString();
            return "0";
        }
    }
}
