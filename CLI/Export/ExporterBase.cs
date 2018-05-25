// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;

namespace Genometric.MSPC.CLI.Exporter
{
    public class ExporterBase<P>
        where P : IChIPSeqPeak, new()
    {
        public string samplePath { set; get; }
        protected string sessionPath { set; get; }
        protected bool includeBEDHeader { set; get; }
        protected Result<P> data { set; get; }
        protected ReadOnlyDictionary<string, SortedList<P, P>> mergedReplicates { set; get; }

        protected void Export__R_j__o_BED()
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "AOutputSet.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "AOutputSet.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\t-1xlog10(p-value)\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.Chromosomes)
                {
                    foreach (var item in chr.Value.Get(Attributes.Confirmed))
                    {
                        if (item.Classification.Contains(Attributes.TruePositive))
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Source.Left.ToString() + "\t" +
                                item.Source.Right.ToString() + "\t" +
                                item.Source.Name + "\t" +
                                ConvertPValue(item.Source.Value) + "\t" +
                                Math.Round(item.XSquared, 3) + "\t" +
                                ConvertPValue(item.RTP));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "AOutputSetstringentConfirmedpeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "AOutputSetstringentConfirmedpeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.Chromosomes)
                {
                    foreach (var item in chr.Value.Get(Attributes.Confirmed))
                    {
                        if (item.Classification.Contains(Attributes.TruePositive) && item.Classification.Contains(Attributes.Stringent) && item.Classification.Contains(Attributes.Confirmed))
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Source.Left.ToString() + "\t" +
                                item.Source.Right.ToString() + "\t" +
                                item.Source.Name + "\t" +
                                ConvertPValue(item.Source.Value) + "\t" +
                                Math.Round(item.XSquared, 3) + "\t" +
                                ConvertPValue(item.RTP));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "AOutputSetweakConfirmedpeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "AOutputSetweakConfirmedpeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.Chromosomes)
                {
                    foreach (var item in chr.Value.Get(Attributes.Confirmed))
                    {
                        if (item.Classification.Contains(Attributes.TruePositive) && item.Classification.Contains(Attributes.Weak) && item.Classification.Contains(Attributes.Confirmed))
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Source.Left.ToString() + "\t" +
                                item.Source.Right.ToString() + "\t" +
                                item.Source.Name + "\t" +
                                ConvertPValue(item.Source.Value) + "\t" +
                                Math.Round(item.XSquared, 3) + "\t" +
                                ConvertPValue(item.RTP));
                        }
                    }
                }
            }


            using (File.Create(samplePath + Path.DirectorySeparatorChar + "AOutputSetFalsePositives.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "AOutputSetFalsePositives.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.Chromosomes)
                {
                    foreach (var item in chr.Value.Get(Attributes.Confirmed))
                    {
                        if (item.Classification.Contains(Attributes.FalsePositive))
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Source.Left.ToString() + "\t" +
                                item.Source.Right.ToString() + "\t" +
                                item.Source.Name + "\t" +
                                ConvertPValue(item.Source.Value) + "\t" +
                                Math.Round(item.XSquared, 3) + "\t" +
                                ConvertPValue(item.RTP));
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

                foreach (var chr in data.Chromosomes)
                {
                    foreach (var item in chr.Value.GetInitialClassifications(Attributes.Stringent))
                    {
                        writter.WriteLine(
                            chr.Key + "\t" +
                            item.Left.ToString() + "\t" +
                            item.Right.ToString() + "\t" +
                            item.Name + "\t" +
                            ConvertPValue(item.Value));
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

                foreach (var chr in data.Chromosomes)
                {
                    foreach (var item in chr.Value.GetInitialClassifications(Attributes.Weak))
                    {
                        writter.WriteLine(
                            chr.Key + "\t" +
                            item.Left.ToString() + "\t" +
                            item.Right.ToString() + "\t" +
                            item.Name + "\t" +
                            ConvertPValue(item.Value));
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

                foreach (var chr in data.Chromosomes)
                {
                    var sortedDictionary = from entry in chr.Value.Get(Attributes.Confirmed) orderby entry ascending select entry;

                    foreach (var item in sortedDictionary)
                    {
                        writter.WriteLine(
                            chr.Key + "\t" +
                            item.Source.Left.ToString() + "\t" +
                            item.Source.Right.ToString() + "\t" +
                            item.Source.Name + "\t" +
                            ConvertPValue(item.Source.Value) + "\t" +
                            Math.Round(item.XSquared, 3) + "\t" +
                            ConvertPValue(item.RTP));
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "DStringentConfirmedPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "DStringentConfirmedPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.Chromosomes)
                {
                    var sortedDictionary = from entry in chr.Value.Get(Attributes.Confirmed) orderby entry ascending select entry;

                    foreach (var item in sortedDictionary)
                    {
                        if (item.Classification.Contains(Attributes.Stringent) && item.Classification.Contains(Attributes.Confirmed))
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Source.Left.ToString() + "\t" +
                                item.Source.Right.ToString() + "\t" +
                                item.Source.Name + "\t" +
                                ConvertPValue(item.Source.Value) + "\t" +
                                Math.Round(item.XSquared, 3) + "\t" +
                                ConvertPValue(item.RTP));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "DWeakConfirmedPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "DWeakConfirmedPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd\t-1xlog10(Right-Tail_Probability)");

                foreach (var chr in data.Chromosomes)
                {
                    var sortedDictionary = from entry in chr.Value.Get(Attributes.Confirmed) orderby entry ascending select entry;

                    foreach (var item in sortedDictionary)
                    {
                        if (item.Classification.Contains(Attributes.Weak) && item.Classification.Contains(Attributes.Confirmed))
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Source.Left.ToString() + "\t" +
                                item.Source.Right.ToString() + "\t" +
                                item.Source.Name + "\t" +
                                ConvertPValue(item.Source.Value) + "\t" +
                                Math.Round(item.XSquared, 3) + "\t" +
                                ConvertPValue(item.RTP));
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

                foreach (var chr in data.Chromosomes)
                {
                    foreach (var item in chr.Value.Get(Attributes.Discarded))
                    {
                        writter.WriteLine(
                            chr.Key + "\t" +
                            item.Source.Left.ToString() + "\t" +
                            item.Source.Right.ToString() + "\t" +
                            item.Source.Name + "\t" +
                            ConvertPValue(item.Source.Value) + "\t" +
                            Math.Round(item.XSquared, 3));
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "EStringentDiscardedPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "EStringentDiscardedPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd");

                foreach (var chr in data.Chromosomes)
                {
                    foreach (var item in chr.Value.Get(Attributes.Discarded))
                    {
                        if (item.Classification.Contains(Attributes.Stringent) && item.Classification.Contains(Attributes.Discarded))
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Source.Left.ToString() + "\t" +
                                item.Source.Right.ToString() + "\t" +
                                item.Source.Name + "\t" +
                                ConvertPValue(item.Source.Value) + "\t" +
                                Math.Round(item.XSquared, 3));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "EWeakDiscardedPeaks.bed")) { }
            using (StreamWriter writter = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "EWeakDiscardedPeaks.bed"))
            {
                if (includeBEDHeader)
                    writter.WriteLine("chr\tstart\tstop\tname\tpValue(-1xlog10(p-value))\txSqrd");

                foreach (var chr in data.Chromosomes)
                {
                    foreach (var item in chr.Value.Get(Attributes.Discarded))
                    {
                        if (item.Classification.Contains(Attributes.Weak) && item.Classification.Contains(Attributes.Discarded))
                        {
                            writter.WriteLine(
                                chr.Key + "\t" +
                                item.Source.Left.ToString() + "\t" +
                                item.Source.Right.ToString() + "\t" +
                                item.Source.Name + "\t" +
                                ConvertPValue(item.Source.Value) + "\t" +
                                Math.Round(item.XSquared, 3));
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
                            item.Value.Left.ToString() + "\t" +
                            item.Value.Right.ToString() + "\t" +
                            item.Value.Name + "\t" +
                            Math.Round(item.Value.Value, 3));
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
