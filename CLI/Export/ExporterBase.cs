/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Genometric.MSPC.Core.Model;
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Genometric.GeUtilities.IGenomics;

namespace Genometric.MSPC.CLI.Exporter
{
    public class ExporterBase<P>
        where P : IChIPSeqPeak, new()
    {
        public string samplePath { set; get; }
        protected string sessionPath { set; get; }
        protected bool includeBEDHeader { set; get; }
        protected AnalysisResult<P, M> data { set; get; }
        protected ReadOnlyDictionary<string, SortedList<P, P>> mergedReplicates { set; get; }

        protected void Export__R_j__o_BED()
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "AOutputSet.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "AOutputSet.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\t-1xlog10(p-value)\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.R_j__o)
                {
                    chr.Value.Sort();
                    foreach (var item in chr.Value)
                    {
                        if (item.statisticalClassification == PeakClassificationType.TruePositive)
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.peak.left.ToString() + "\t" +
                                item.peak.right.ToString() + "\t" +
                                item.peak.metadata.name + "\t" +
                                ConvertPValue(item.peak.metadata.value) + "\t" +
                                Math.Round(item.xSquared, 3) + "\t" +
                                ConvertPValue(item.rtp));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "AOutputSetstringentConfirmedpeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "AOutputSetstringentConfirmedpeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.R_j__o)
                {
                    chr.Value.Sort();
                    foreach (var item in chr.Value)
                    {
                        if (item.statisticalClassification == PeakClassificationType.TruePositive && item.classification == PeakClassificationType.StringentConfirmed)
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.peak.left.ToString() + "\t" +
                                item.peak.right.ToString() + "\t" +
                                item.peak.metadata.name + "\t" +
                                ConvertPValue(item.peak.metadata.value) + "\t" +
                                Math.Round(item.xSquared, 3) + "\t" +
                                ConvertPValue(item.rtp));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "AOutputSetweakConfirmedpeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "AOutputSetweakConfirmedpeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.R_j__o)
                {
                    chr.Value.Sort();
                    foreach (var item in chr.Value)
                    {
                        if (item.statisticalClassification == PeakClassificationType.TruePositive && item.classification == PeakClassificationType.WeakConfirmed)
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.peak.left.ToString() + "\t" +
                                item.peak.right.ToString() + "\t" +
                                item.peak.metadata.name + "\t" +
                                ConvertPValue(item.peak.metadata.value) + "\t" +
                                Math.Round(item.xSquared, 3) + "\t" +
                                ConvertPValue(item.rtp));
                        }
                    }
                }
            }


            using (File.Create(samplePath + Path.DirectorySeparatorChar + "AOutputSetFalsePositives.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "AOutputSetFalsePositives.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.R_j__o)
                {
                    chr.Value.Sort();
                    foreach (var item in chr.Value)
                    {
                        if (item.statisticalClassification == PeakClassificationType.FalsePositive)
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.peak.left.ToString() + "\t" +
                                item.peak.right.ToString() + "\t" +
                                item.peak.metadata.name + "\t" +
                                ConvertPValue(item.peak.metadata.value) + "\t" +
                                Math.Round(item.xSquared, 3) + "\t" +
                                ConvertPValue(item.rtp));
                        }
                    }
                }
            }
        }
        protected void Export__R_j__s_BED()
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "BStringentPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "BStringentPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))");

                foreach (var chr in data.R_j__s)
                {
                    chr.Value.Sort();
                    foreach (var item in chr.Value)
                    {
                        writter.WriteLine(
                            chr.Key + "\t" +
                            item.left.ToString() + "\t" +
                            item.right.ToString() + "\t" +
                            item.metadata.name + "\t" +
                            ConvertPValue(item.metadata.value));
                    }
                }
            }
        }
        protected void Export__R_j__w_BED()
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "CWeakPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "CWeakPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))");

                foreach (var chr in data.R_j__w)
                {
                    chr.Value.Sort();
                    foreach (var item in chr.Value)
                    {
                        writter.WriteLine(
                            chr.Key + "\t" +
                            item.left.ToString() + "\t" +
                            item.right.ToString() + "\t" +
                            item.metadata.name + "\t" +
                            ConvertPValue(item.metadata.value));
                    }
                }
            }
        }
        protected void Export__R_j__c_BED()
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "DConfirmedPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "DConfirmedPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.R_j__c)
                {
                    var sortedDictionary = from entry in chr.Value orderby entry.Value ascending select entry;

                    foreach (var item in sortedDictionary)
                    {
                        writter.WriteLine(
                            chr.Key + "\t" +
                            item.Value.peak.left.ToString() + "\t" +
                            item.Value.peak.right.ToString() + "\t" +
                            item.Value.peak.metadata.name + "\t" +
                            ConvertPValue(item.Value.peak.metadata.value) + "\t" +
                            Math.Round(item.Value.xSquared, 3) + "\t" +
                            ConvertPValue(item.Value.rtp));
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "DStringentConfirmedPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "DStringentConfirmedPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.R_j__c)
                {
                    var sortedDictionary = from entry in chr.Value orderby entry.Value ascending select entry;

                    foreach (var item in sortedDictionary)
                    {
                        if (item.Value.classification == PeakClassificationType.StringentConfirmed)
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Value.peak.left.ToString() + "\t" +
                                item.Value.peak.right.ToString() + "\t" +
                                item.Value.peak.metadata.name + "\t" +
                                ConvertPValue(item.Value.peak.metadata.value) + "\t" +
                                Math.Round(item.Value.xSquared, 3) + "\t" +
                                ConvertPValue(item.Value.rtp));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "DWeakConfirmedPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "DWeakConfirmedPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.R_j__c)
                {
                    var sortedDictionary = from entry in chr.Value orderby entry.Value ascending select entry;

                    foreach (var item in sortedDictionary)
                    {
                        if (item.Value.classification == PeakClassificationType.WeakConfirmed)
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Value.peak.left.ToString() + "\t" +
                                item.Value.peak.right.ToString() + "\t" +
                                item.Value.peak.metadata.name + "\t" +
                                ConvertPValue(item.Value.peak.metadata.value) + "\t" +
                                Math.Round(item.Value.xSquared, 3) + "\t" +
                                ConvertPValue(item.Value.rtp));
                        }
                    }
                }
            }
        }
        protected void Export__R_j__d_BED()
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "EDiscardedPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "EDiscardedPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd");

                foreach (var chr in data.R_j__d)
                {
                    foreach (var item in chr.Value)
                    {
                        writter.WriteLine(
                            chr.Key + "\t" +
                            item.Value.peak.left.ToString() + "\t" +
                            item.Value.peak.right.ToString() + "\t" +
                            item.Value.peak.metadata.name + "\t" +
                            ConvertPValue(item.Value.peak.metadata.value) + "\t" +
                            Math.Round(item.Value.xSquared, 3));
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "EStringentDiscardedPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "EStringentDiscardedPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd");

                foreach (var chr in data.R_j__d)
                {
                    foreach (var item in chr.Value)
                    {
                        if (item.Value.classification == PeakClassificationType.StringentDiscarded)
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Value.peak.left.ToString() + "\t" +
                                item.Value.peak.right.ToString() + "\t" +
                                item.Value.peak.metadata.name + "\t" +
                                ConvertPValue(item.Value.peak.metadata.value) + "\t" +
                                Math.Round(item.Value.xSquared, 3));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "EWeakDiscardedPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "EWeakDiscardedPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd");

                foreach (var chr in data.R_j__d)
                {
                    foreach (var item in chr.Value)
                    {
                        if (item.Value.classification == PeakClassificationType.WeakDiscarded)
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Value.peak.left.ToString() + "\t" +
                                item.Value.peak.right.ToString() + "\t" +
                                item.Value.peak.metadata.name + "\t" +
                                ConvertPValue(item.Value.peak.metadata.value) + "\t" +
                                Math.Round(item.Value.xSquared, 3));
                        }
                    }
                }
            }
        }
        protected void Export__MergedReps()
        {
            using (File.Create(sessionPath + Path.DirectorySeparatorChar + "MergedReplicates.bed")) { }
            using (StreamWriter writter = new StreamWriter(sessionPath + Path.DirectorySeparatorChar + "MergedReplicates.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tX-squared");

                foreach (var chr in mergedReplicates)
                {
                    foreach (var item in chr.Value)
                    {
                        writter.WriteLine(
                            chr.Key + "\t" +
                            item.Value.left.ToString() + "\t" +
                            item.Value.right.ToString() + "\t" +
                            item.Value.metadata.name + "\t" +
                            Math.Round(item.Value.metadata.value, 3));
                    }
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
