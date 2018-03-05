/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;

namespace Genometric.MSPC.Model
{
    public class AnalysisResult<I>
        where I : IChIPSeqPeak, new()
    {
        private Dictionary<string, Dictionary<PeakClassificationType, uint>> _stats;

        // TODO: this function should be replaced by a public property exposing stats as a read-only collection.
        public uint Stats(string chr, PeakClassificationType attribute)
        {
            return _stats[chr][attribute];
        }

        internal void SetFalsePositiveCount(string chr, uint value)
        {
            _stats[chr][PeakClassificationType.FalsePositive] = value;
        }

        public AnalysisResult()
        {
            total_scom = 0;
            total_wcom = 0;

            R_j_TP = new Dictionary<string, uint>();

            R_j__s = new Dictionary<string, List<I>>();
            R_j__w = new Dictionary<string, List<I>>();
            R_j__b = new Dictionary<string, List<I>>();

            R_j__c = new Dictionary<string, Dictionary<UInt64, ProcessedPeak<I>>>();
            R_j__d = new Dictionary<string, Dictionary<UInt64, ProcessedPeak<I>>>();
            R_j__o = new Dictionary<string, List<ProcessedPeak<I>>>();

            R_j___so = new Dictionary<string, uint>();

            R_j___wc = new Dictionary<string, uint>();
            R_j__wdc = new Dictionary<string, uint>();
            R_j__wdt = new Dictionary<string, uint>();
            R_j___wo = new Dictionary<string, uint>();

            _stats = new Dictionary<string, Dictionary<PeakClassificationType, uint>>();

            messages = new List<string>();

            /// The analyzer points to these messages by their index,
            /// hence if the order would change, the indexes will change
            /// as well resulting in improper messages
            messages.Add("X-squared is below chi-squared of Gamma");
            messages.Add("Intersecting peaks count doesn't comply minimum requirement");
        }

        public void Add(string chr, I peak, PeakClassificationType type)
        {
            switch(type)
            {
                case PeakClassificationType.Stringent:
                    R_j__s[chr].Add(peak);
                    break;

                case PeakClassificationType.Weak:
                    R_j__w[chr].Add(peak);
                    break;

                case PeakClassificationType.Background:
                    R_j__b[chr].Add(peak);
                    break;
            }
        }

        public void Add(string chr, ProcessedPeak<I> peak, PeakClassificationType type)
        {
            switch (type)
            {
                case PeakClassificationType.Confirmed:
                    if (!R_j__c[chr].ContainsKey(peak.peak.HashKey))
                    {
                        R_j__c[chr].Add(peak.peak.HashKey, peak);
                        _stats[chr][peak.classification]++;
                    }
                    break;

                case PeakClassificationType.Discarded:
                    if (!R_j__d[chr].ContainsKey(peak.peak.HashKey))
                    {
                        R_j__d[chr].Add(peak.peak.HashKey, peak);
                        _stats[chr][peak.classification]++;
                    }
                    break;

                case PeakClassificationType.Output:
                    R_j__o[chr].Add(peak);
                    break;
            }
        }

        /// <summary>
        /// Chromosome-wide stringent peaks of sample j
        /// </summary>
        public Dictionary<string, List<I>> R_j__s { set; get; }

        /// <summary>
        /// Chromosome-wide weak peaks of sample j
        /// </summary>
        public Dictionary<string, List<I>> R_j__w { set; get; }

        /// <summary>
        /// Chromosome-wide background peaks of sample j (i.e., the peaks with p-value > T_w ).
        /// </summary>
        public Dictionary<string, List<I>> R_j__b { set; get; }



        /// <summary>
        /// Chromosome-wide Confirmed peaks of sample j (i.e., the peaks that passed both intersecting
        /// peaks count threshold and X-squared test).
        /// </summary>
        public Dictionary<string, Dictionary<UInt64, ProcessedPeak<I>>> R_j__c { set; get; }

        /// <summary>
        /// Chromosome-wide Discarded peaks of sample j (i.e., the peaks that either failed intersecting
        /// peaks count threshold or X-squared test).
        /// </summary>
        public Dictionary<string, Dictionary<UInt64, ProcessedPeak<I>>> R_j__d { set; get; }

        /// <summary>
        /// Chromosome-wide set of peaks as the result of the algorithm. The peaks of this set passed
        /// three tests (i.e., intersecting peaks count, X-squared test, and benjamini-hochberg
        /// multiple testing correction).
        /// </summary>
        public Dictionary<string, List<ProcessedPeak<I>>> R_j__o { set; get; }

        /// <summary>
        /// Chromosome-wide True-Positive counter.
        /// </summary>
        public Dictionary<string, UInt32> R_j_TP { set; get; }


        /// <summary>
        /// Chromosome-wide Stringent-Discarded peaks count failing x-squared test.
        /// </summary>
        public Dictionary<string, UInt32> R_j__sdt { set; get; }
        /// <summary>
        /// Chromosome-wide Stringent peaks in output set count.
        /// </summary>
        public Dictionary<string, UInt32> R_j___so { set; get; }



        /// <summary>
        /// Chromosome-wide Weak-Confirmed peaks count.
        /// </summary>
        public Dictionary<string, UInt32> R_j___wc { set; get; }
        /// <summary>
        /// Chromosome-wide Weak-Discarded peaks count
        /// failing intersecting regions count test
        /// </summary>
        public Dictionary<string, UInt32> R_j__wdc { set; get; }
        /// <summary>
        /// Chromosome-wide Weak-Discarded peaks count failing x-squared test.
        /// </summary>
        public Dictionary<string, UInt32> R_j__wdt { set; get; }
        /// <summary>
        /// Chromosome-wide Weak peaks in output set count.
        /// </summary>
        public Dictionary<string, UInt32> R_j___wo { set; get; }


        /// <summary>
        /// gets total number of stringent peaks.
        /// </summary>
        public UInt32 total____s { private set; get; }
        /// <summary>
        /// gets total number of weak peaks.
        /// </summary>
        public UInt32 total____w { private set; get; }
        /// <summary>
        /// gets total number of garbage peaks.
        /// </summary>
        public UInt32 total____g { private set; get; }
        /// <summary>
        /// gets total number of peaks in output set.
        /// </summary>
        public UInt32 total____o { private set; get; }

        /// <summary>
        /// Total number of False-Positive peaks in output set.
        /// </summary>
        public UInt32 total___FP { private set; get; }
        /// <summary>
        /// Total number of True-Positive peaks in output set.
        /// </summary>
        public UInt32 total___TP { private set; get; }

        /// <summary>
        /// Total number of stringent peaks that are both validated and discarded
        /// through multiple tests.
        /// </summary>
        public UInt32 total_scom { set; get; }
        /// <summary>
        /// Total number of weak peaks that are both validated and discarded
        /// through multiple tests.
        /// </summary>
        public UInt32 total_wcom { set; get; }



        /// <summary>
        /// Total number of Stringent-Confirmed peaks.
        /// </summary>
        public UInt32 total___sc { private set; get; }
        /// <summary>
        /// Total number of Stringent-Discarded peaks
        /// failing intersecting regions count test
        /// </summary>
        public UInt32 total__sdc { private set; get; }
        /// <summary>
        /// Total number of Stringent-Discarded peaks failing x-squared test.
        /// </summary>
        public UInt32 total__sdt { private set; get; }
        /// <summary>
        /// Total number of Stringent peaks in output set.
        /// </summary>
        public UInt32 total___so { private set; get; }


        /// <summary>
        /// Total number of Weak-Confirmed peaks.
        /// </summary>
        public UInt32 total___wc { private set; get; }
        /// <summary>
        /// Total number of Weak-Discarded peaks
        /// failing intersecting regions count test.
        /// </summary>
        public UInt32 total__wdc { private set; get; }
        /// <summary>
        /// Total number of Weak-Discarded peaks failing x-squared test.
        /// </summary>
        public UInt32 total__wdt { private set; get; }
        /// <summary>
        /// Total number of Weak peaks in Output set.
        /// </summary>
        public UInt32 total___wo { private set; get; }


        /// <summary>
        /// Gets Chromosome-wide analysis summary statistics.
        /// </summary>
        public Dictionary<string, ChrWideStat> chrwideStats { private set; get; }

        public List<string> messages { private set; get; }

        // TODO
        // THIS MUST NOT BE REQUIRED!
        public void ReadOverallStats()
        {
            total____s = 0;
            total____w = 0;
            total____g = 0;
            total____o = 0;

            total___TP = 0;
            total___FP = 0;

            total___sc = 0;
            total__sdc = 0;
            total__sdt = 0;
            total___so = 0;

            total___wc = 0;
            total__wdc = 0;
            total__wdt = 0;
            total___wo = 0;

            double totalERsCount = 0;
            chrwideStats = new Dictionary<string, ChrWideStat>();
            foreach (KeyValuePair<string, List<I>> chr in R_j__s)
            {
                total____s += (UInt32)R_j__s[chr.Key].Count;
                total____w += (UInt32)R_j__w[chr.Key].Count;
                total____g += (UInt32)R_j__b[chr.Key].Count;
                total____o += (UInt32)R_j__o[chr.Key].Count;

                total___TP += (UInt32)R_j_TP[chr.Key];
                total___FP += Stats(chr.Key, PeakClassificationType.FalsePositive);

                total___sc += Stats(chr.Key, PeakClassificationType.StringentConfirmed);
                total__sdc += Stats(chr.Key, PeakClassificationType.StringentDiscardedC);
                total__sdt += Stats(chr.Key, PeakClassificationType.StringentDiscardedT);
                total___so += (UInt32)R_j___so[chr.Key];

                total___wc += (UInt32)R_j___wc[chr.Key];
                total__wdc += (UInt32)R_j__wdc[chr.Key];
                total__wdt += (UInt32)R_j__wdt[chr.Key];
                total___wo += (UInt32)R_j___wo[chr.Key];

                totalERsCount = R_j__s[chr.Key].Count + R_j__w[chr.Key].Count;
                chrwideStats.Add(chr.Key, new ChrWideStat()
                {
                    R_j__t_c = (UInt32)totalERsCount,
                    R_j__s_c = (UInt32)R_j__s[chr.Key].Count,
                    R_j__s_p = (Math.Round((R_j__s[chr.Key].Count * 100) / totalERsCount, 2)).ToString() + " %",
                    R_j__w_c = (UInt32)R_j__w[chr.Key].Count,
                    R_j__w_p = (Math.Round((R_j__w[chr.Key].Count * 100) / totalERsCount, 2)).ToString() + " %",
                    R_j__c_c = (UInt32)R_j__c[chr.Key].Count,
                    R_j__c_p = (Math.Round((R_j__c[chr.Key].Count * 100) / totalERsCount, 2)).ToString() + " %",
                    R_j__d_c = (UInt32)R_j__d[chr.Key].Count,
                    R_j__d_p = (Math.Round((R_j__d[chr.Key].Count * 100) / totalERsCount, 2)).ToString() + " %",
                    R_j__o_c = (UInt32)R_j__o[chr.Key].Count,
                    R_j__o_p = (Math.Round((R_j__o[chr.Key].Count * 100) / totalERsCount, 2)).ToString() + " %",
                    R_j_TP_c = (UInt32)R_j_TP[chr.Key],
                    R_j_TP_p = (Math.Round(((double)R_j_TP[chr.Key] * 100) / (double)R_j__o[chr.Key].Count, 2)).ToString() + " %",
                    R_j_FP_c = Stats(chr.Key, PeakClassificationType.FalsePositive),
                    R_j_FP_p = (Math.Round(((double)Stats(chr.Key, PeakClassificationType.FalsePositive) * 100) / (double)R_j__o[chr.Key].Count, 2)).ToString() + " %"
                });
            }
        }
        public void AddChromosome(string chromosome)
        {
            if (R_j__s.ContainsKey(chromosome))
                return;

            R_j__s.Add(chromosome, new List<I>());
            R_j__w.Add(chromosome, new List<I>());
            R_j__b.Add(chromosome, new List<I>());

            R_j__c.Add(chromosome, new Dictionary<UInt64, ProcessedPeak<I>>());
            R_j__d.Add(chromosome, new Dictionary<UInt64, ProcessedPeak<I>>());
            R_j__o.Add(chromosome, new List<ProcessedPeak<I>>());

            R_j_TP.Add(chromosome, 0);

            R_j___so.Add(chromosome, 0);

            R_j___wc.Add(chromosome, 0);
            R_j__wdc.Add(chromosome, 0);
            R_j__wdt.Add(chromosome, 0);
            R_j___wo.Add(chromosome, 0);

            _stats.Add(chromosome, new Dictionary<PeakClassificationType, uint>());
            foreach (var att in Enum.GetValues(typeof(PeakClassificationType)).Cast<PeakClassificationType>())
                _stats[chromosome].Add(att, 0);
        }
    }
}