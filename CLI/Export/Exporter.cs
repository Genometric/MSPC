﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.CLI.Interfaces;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Genometric.MSPC.CLI.Exporter
{
    public class Exporter<I> : IExporter<I>
        where I : IPeak
    {
        private Options _options;

        public void Export(
            Dictionary<uint, string> fileNames,
            List<Bed<PPeak>> samples,
            Dictionary<string, List<PPeak>> consensusPeaks,
            Options options)
        {
            _options = options;

            if (!Directory.Exists(_options.Path))
                Directory.CreateDirectory(_options.Path);

            ExportConsensusPeaks(true, consensusPeaks);
            ExportConsensusPeaks(false, consensusPeaks);

            foreach (var s in samples)
            {
                string samplePath = _options.Path + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(s.FileName);
                Directory.CreateDirectory(samplePath);
                foreach (var attribute in options.AttributesToExport)
                {
                    WriteToFile(true, samplePath, s, attribute);
                    WriteToFile(false, samplePath, s, attribute);
                }
            }
        }

        private string Header(bool mspcFormat)
        {
            return mspcFormat ?
                "chr\tstart\tstop\tname\t-1xlog10(p-value)\txSqrd\t-1xlog10(Right-Tail Probability)\t-1xlog10(AdjustedP-value)" :
                "chr\tstart\tstop\tname\t-1xlog10(p-value)";
        }

        private void WriteToFile(bool mspcFormat, string samplePath, Bed<PPeak> sample, Attributes attribute)
        {
            string filename = samplePath + Path.DirectorySeparatorChar + attribute.ToString() + (mspcFormat ? "_mspc_peaks.txt" : ".bed");
            File.Create(filename).Dispose();
            using (StreamWriter writter = new StreamWriter(filename))
            {
                if (_options.IncludeHeader)
                    writter.WriteLine(Header(mspcFormat));

                var sortedChrs = sample.Chromosomes.Keys.ToArray();
                Array.Sort(sortedChrs, new AlphanumComparer());

                foreach (var chr in sortedChrs)
                {
                    var data = new List<PPeak>();
                    foreach (var strand in sample.Chromosomes[chr].Strands)
                        data.AddRange(strand.Value.Intervals.Where(kvp => kvp.HasAttribute(attribute)));

                    var sortedDictionary = from entry
                                           in data
                                           orderby entry
                                           ascending
                                           select entry;

                    if (mspcFormat)
                        foreach (var peak in sortedDictionary)
                            writter.WriteLine(
                                chr + "\t" +
                                peak.Left.ToString() + "\t" +
                                peak.Right.ToString() + "\t" +
                                peak.Name + "\t" +
                                ConvertPValue(peak.Value) + "\t" +
                                Math.Round(peak.XSquared, 3) + "\t" +
                                ConvertPValue(peak.RTP) + "\t" +
                                ConvertPValue(peak.AdjPValue));
                    else
                        foreach (var peak in sortedDictionary)
                            writter.WriteLine(
                                chr + "\t" +
                                peak.Left.ToString() + "\t" +
                                peak.Right.ToString() + "\t" +
                                peak.Name + "\t" +
                                ConvertPValue(peak.Value));
                }
            }
        }

        private void ExportConsensusPeaks(bool mspcFormat, Dictionary<string, List<PPeak>> peaks)
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
                                peak.Left.ToString() + "\t" +
                                peak.Right.ToString() + "\t" +
                                peak.Name + "\t" +
                                ConvertPValue(peak.Value) + "\t" +
                                Math.Round(peak.XSquared, 3) + "\t" +
                                ConvertPValue(peak.RTP) + "\t" +
                                ConvertPValue(peak.AdjPValue));
                    else
                        foreach (var peak in sortedPeaks)
                            writter.WriteLine(
                                chr + "\t" +
                                peak.Left.ToString() + "\t" +
                                peak.Right.ToString() + "\t" +
                                peak.Name + "\t" +
                                ConvertPValue(peak.Value));
                }
            }
        }

        private string ConvertPValue(double pValue)
        {
            /// When p-value=0, the result of this conversion is Infinity,
            /// hence to avoid exporting `Infinity` that may not be an 
            /// acceptable input by some tools, MSPC reports 0 instead.
            if (pValue != 0)
                return (Math.Round((-1) * Math.Log10(pValue), 3)).ToString();
            return "0";
        }
    }
}
