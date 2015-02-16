/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Polimi.DEIB.VahidJalili.IGenomics;
using Polimi.DEIB.VahidJalili.MSPC.Warehouse;
using System;
using System.IO;
using System.Linq;

namespace Polimi.DEIB.VahidJalili.MSPC.Exporter
{
    public class ExporterBase<Peak, Metadata> : Enums
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, new()
    {
        public string samplePath { set; get; }

        public string sessionPath { set; get; }

        public bool includeBEDHeader { set; get; }

        internal void Export__R_j__o(AnalysisResult<Peak, Metadata> data)
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "A_Output_Set.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "A_Output_Set.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__o)
                {
                    chr.Value.Sort();

                    foreach (var item in chr.Value)
                    {
                        if (item.falsePositive == false)
                        {
                            SP_Writer.WriteLine(
                                chr.Key + "\t" +
                                item.peak.left.ToString() + "\t" +
                                item.peak.right.ToString() + "\t" +
                                item.peak.metadata.name + "\t" +
                                ConvertPValue(item.peak.metadata.value));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "A_Output_Set_stringent_Confirmed_peaks.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "A_Output_Set_stringent_Confirmed_peaks.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__o)
                {
                    chr.Value.Sort();

                    foreach (var item in chr.Value)
                    {
                        if (item.falsePositive == false && item.classification == Classification.StringentConfirmed)
                        {
                            SP_Writer.WriteLine(
                                chr.Key + "\t" +
                                item.peak.left.ToString() + "\t" +
                                item.peak.right.ToString() + "\t" +
                                item.peak.metadata.name + "\t" +
                                ConvertPValue(item.peak.metadata.value));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "A_Output_Set_weak_Confirmed_peaks.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "A_Output_Set_weak_Confirmed_peaks.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__o)
                {
                    chr.Value.Sort();

                    foreach (var item in chr.Value)
                    {
                        if (item.falsePositive == false && item.classification == Classification.WeakConfirmed)
                        {
                            SP_Writer.WriteLine(
                                chr.Key + "\t" +
                                item.peak.left.ToString() + "\t" +
                                item.peak.right.ToString() + "\t" +
                                item.peak.metadata.name + "\t" +
                                ConvertPValue(item.peak.metadata.value));
                        }
                    }
                }
            }


            using (File.Create(samplePath + Path.DirectorySeparatorChar + "A_Output_Set_False_Positives.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "A_Output_Set_False_Positives.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__o)
                {
                    chr.Value.Sort();

                    foreach (var item in chr.Value)
                    {
                        if (item.falsePositive == true)
                        {
                            SP_Writer.WriteLine(
                                chr.Key + "\t" +
                                item.peak.left.ToString() + "\t" +
                                item.peak.right.ToString() + "\t" +
                                item.peak.metadata.name + "\t" +
                                ConvertPValue(item.peak.metadata.value));
                        }
                    }
                }
            }
        }

        internal void Export__R_j__s(AnalysisResult<Peak, Metadata> data)
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "B_Stringent_Peaks.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "B_Stringent_Peaks.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__s)
                {
                    chr.Value.Sort();

                    foreach (var item in chr.Value)
                    {
                        SP_Writer.WriteLine(
                            chr.Key + "\t" +
                            item.left.ToString() + "\t" +
                            item.right.ToString() + "\t" +
                            item.metadata.name + "\t" +
                            ConvertPValue(item.metadata.value));
                    }
                }
            }
        }

        internal void Export__R_j__w(AnalysisResult<Peak, Metadata> data)
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "C_Weak_Peaks.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "C_Weak_Peaks.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__w)
                {
                    chr.Value.Sort();

                    foreach (var item in chr.Value)
                    {
                        SP_Writer.WriteLine(
                            chr.Key + "\t" +
                            item.left.ToString() + "\t" +
                            item.right.ToString() + "\t" +
                            item.metadata.name + "\t" +
                            ConvertPValue(item.metadata.value));
                    }
                }
            }
        }

        internal void Export__R_j__v(AnalysisResult<Peak, Metadata> data)
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "D_Confirmed_Peaks.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "D_Confirmed_Peaks.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__c)
                {
                    var sortedDictionary = from entry in chr.Value orderby entry.Value ascending select entry;

                    foreach (var item in sortedDictionary)
                    {
                        SP_Writer.WriteLine(
                            chr.Key + "\t" +
                            item.Value.peak.left.ToString() + "\t" +
                            item.Value.peak.right.ToString() + "\t" +
                            item.Value.peak.metadata.name + "\t" +
                            ConvertPValue(item.Value.peak.metadata.value));
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "D_Stringent_Confirmed_Peaks.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "D_Stringent_Confirmed_Peaks.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__c)
                {
                    var sortedDictionary = from entry in chr.Value orderby entry.Value ascending select entry;

                    foreach (var item in sortedDictionary)
                    {
                        if (item.Value.classification == Classification.StringentConfirmed)
                        {
                            SP_Writer.WriteLine(
                                chr.Key + "\t" +
                                item.Value.peak.left.ToString() + "\t" +
                                item.Value.peak.right.ToString() + "\t" +
                                item.Value.peak.metadata.name + "\t" +
                                ConvertPValue(item.Value.peak.metadata.value));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "D_Weak_Confirmed_Peaks.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "D_Weak_Confirmed_Peaks.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__c)
                {
                    var sortedDictionary = from entry in chr.Value orderby entry.Value ascending select entry;

                    foreach (var item in sortedDictionary)
                    {
                        if (item.Value.classification == Classification.WeakConfirmed)
                        {
                            SP_Writer.WriteLine(
                                chr.Key + "\t" +
                                item.Value.peak.left.ToString() + "\t" +
                                item.Value.peak.right.ToString() + "\t" +
                                item.Value.peak.metadata.name + "\t" +
                                ConvertPValue(item.Value.peak.metadata.value));
                        }
                    }
                }
            }
        }

        internal void Export__R_j__d(AnalysisResult<Peak, Metadata> data)
        {
            using (File.Create(samplePath + Path.DirectorySeparatorChar + "E_Discarded_Peaks.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "E_Discarded_Peaks.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__d)
                {
                    foreach (var item in chr.Value)
                    {
                        SP_Writer.WriteLine(
                            chr.Key + "\t" +
                            item.Value.peak.left.ToString() + "\t" +
                            item.Value.peak.right.ToString() + "\t" +
                            item.Value.peak.metadata.name + "\t" +
                            ConvertPValue(item.Value.peak.metadata.value));
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "E_Stringent_Discarded_Peaks.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "E_Stringent_Discarded_Peaks.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__d)
                {
                    foreach (var item in chr.Value)
                    {
                        if (item.Value.classification == Classification.StringentDiscarded)
                        {
                            SP_Writer.WriteLine(
                                chr.Key + "\t" +
                                item.Value.peak.left.ToString() + "\t" +
                                item.Value.peak.right.ToString() + "\t" +
                                item.Value.peak.metadata.name + "\t" +
                                ConvertPValue(item.Value.peak.metadata.value));
                        }
                    }
                }
            }

            using (File.Create(samplePath + Path.DirectorySeparatorChar + "E_Weak_Discarded_Peaks.bed")) { }
            using (StreamWriter SP_Writer = new StreamWriter(samplePath + Path.DirectorySeparatorChar + "E_Weak_Discarded_Peaks.bed"))
            {
                if (includeBEDHeader)
                    SP_Writer.WriteLine("chr\tstart\tstop\tpeak_identifier\tp_value(-10xlog10(p-value))");

                foreach (var chr in data.R_j__d)
                {
                    foreach (var item in chr.Value)
                    {
                        if (item.Value.classification == Classification.WeakDiscarded)
                        {
                            SP_Writer.WriteLine(
                                chr.Key + "\t" +
                                item.Value.peak.left.ToString() + "\t" +
                                item.Value.peak.right.ToString() + "\t" +
                                item.Value.peak.metadata.name + "\t" +
                                ConvertPValue(item.Value.peak.metadata.value));
                        }
                    }
                }
            }
        }

        private string ConvertPValue(double pValue)
        {
            if (pValue != 0)
            {
                return (Math.Round((-10.0) * Math.Log10(pValue), 3)).ToString();
            }

            return "0";
        }
    }
}
