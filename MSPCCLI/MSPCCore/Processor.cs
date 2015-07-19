/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using IntervalTreeLib;
using Polimi.DEIB.VahidJalili.IGenomics;
using Polimi.DEIB.VahidJalili.MSPC.Analyzer.Data;
using Polimi.DEIB.VahidJalili.MSPC.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using Polimi.DEIB.VahidJalili.XSquaredData;

namespace Polimi.DEIB.VahidJalili.MSPC.Analyzer
{
    internal class Processor<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        private double tXsqrd { set; get; }

        private AnalysisResult<Peak, Metadata> analysisResults { set; get; }

        private List<Peak> sourcePeaks { set; get; }

        private string chrTitle { set; get; }

        private UInt32 sampleHashKey { set; get; }


        private Dictionary<UInt32, Dictionary<string, IntervalTree<Peak, int>>> trees { set; get; }

        internal Processor()
        { }

        internal void Run(UInt32 SampleHashKey, string ChrTitle, char strand)
        {
            chrTitle = ChrTitle;
            sampleHashKey = SampleHashKey;
            sourcePeaks = Samples<Peak, Metadata>.Data[sampleHashKey].intervals[chrTitle][strand];
            analysisResults = Data<Peak, Metadata>.analysisResults[SampleHashKey];
            trees = Data<Peak, Metadata>.trees;

            foreach (Peak p in sourcePeaks)
            {
                tXsqrd = 0;

                InitialClassification(p);

                if (p.metadata.value <= Options.tauS || p.metadata.value <= Options.tauW)
                {
                    SecondaryClassification(p, FindSupportingPeaks(p));
                }
            }
        }

        private void InitialClassification(Peak p)
        {
            if (p.metadata.value <= Options.tauS)            
                analysisResults.R_j__s[chrTitle].Add(p);            
            else if (p.metadata.value <= Options.tauW)            
                analysisResults.R_j__w[chrTitle].Add(p);
        }

        private List<AnalysisResult<Peak, Metadata>.SupportingPeak> FindSupportingPeaks(Peak p)
        {
            var supPeak = new List<AnalysisResult<Peak, Metadata>.SupportingPeak>();

            foreach (var sampleKey in Data<Peak, Metadata>.sampleKeys)
            {
                if (sampleKey.Key != sampleHashKey)
                {
                    var interPeaks = new List<IntervalTreeLib.Interval<Peak, int>>();

                    if (trees[sampleKey.Key].ContainsKey(chrTitle))
                        interPeaks = trees[sampleKey.Key][chrTitle].GetIntervals(p.left, p.right);

                    switch (interPeaks.Count)
                    {
                        case 0: break;

                        case 1:
                            supPeak.Add(new AnalysisResult<Peak, Metadata>.SupportingPeak()
                            {                                
                                peak = (Peak)interPeaks[0].Data,
                                sampleIndex = sampleKey.Key
                            });
                            break;

                        default:
                            var chosenPeak = interPeaks[0];

                            foreach (var tIp in interPeaks.Skip(1))
                                if ((Options.multipleIntersections == MultipleIntersections.UseLowestPValue && tIp.Data.metadata.value < chosenPeak.Data.metadata.value) ||
                                    (Options.multipleIntersections == MultipleIntersections.UseHighestPValue && tIp.Data.metadata.value > chosenPeak.Data.metadata.value))
                                    chosenPeak = tIp;


                            supPeak.Add(new AnalysisResult<Peak, Metadata>.SupportingPeak()
                            {
                                peak = (Peak)chosenPeak.Data,
                                sampleIndex = sampleKey.Key
                            });
                            break;
                    }
                }
            }

            return supPeak;
        }

        private void SecondaryClassification(Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks)
        {
            if (supportingPeaks.Count + 1 >= Options.C)
            {
                CalculateXsqrd(p, supportingPeaks);

                if (tXsqrd >= Data<Peak, Metadata>.cachedChiSqrd[supportingPeaks.Count])
                    ConfirmPeak(p, supportingPeaks);
                else
                    DiscardPeak(p, supportingPeaks, 0);
            }
            else
            {
                DiscardPeak(p, supportingPeaks, 1);
            }
        }

        private void ConfirmPeak(Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks)
        {
            var anRe = new AnalysisResult<Peak, Metadata>.ProcessedPeak()
            {
                peak = p,
                xSquared = tXsqrd,
                rtp = ChiSquaredCache.ChiSqrdDistRTP(tXsqrd, 2 + (supportingPeaks.Count * 2)),
                supportingPeaks = supportingPeaks
            };

            if (p.metadata.value <= Options.tauS)
            {
                analysisResults.R_j___sc[chrTitle]++;
                anRe.classification = PeakClassificationType.StringentConfirmed;
            }
            else
            {
                analysisResults.R_j___wc[chrTitle]++;
                anRe.classification = PeakClassificationType.WeakConfirmed;
            }

            if (!analysisResults.R_j__c[chrTitle].ContainsKey(p.metadata.hashKey))
                analysisResults.R_j__c[chrTitle].Add(p.metadata.hashKey, anRe);

            ConfirmeSupportingPeaks(p, supportingPeaks);
        }

        private void ConfirmeSupportingPeaks(Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks)
        {
            foreach (var supPeak in supportingPeaks)
            {
                if (!Data<Peak, Metadata>.analysisResults[supPeak.sampleIndex].R_j__c[chrTitle].ContainsKey(supPeak.peak.metadata.hashKey))
                {
                    var tSupPeak = new List<AnalysisResult<Peak, Metadata>.SupportingPeak>();
                    var targetSample = Data<Peak, Metadata>.analysisResults[supPeak.sampleIndex];
                    tSupPeak.Add(new AnalysisResult<Peak, Metadata>.SupportingPeak() { peak = p, sampleIndex = sampleHashKey });

                    foreach (var sP in supportingPeaks)
                        if (supPeak.CompareTo(sP) != 0)
                            tSupPeak.Add(sP);

                    var anRe = new AnalysisResult<Peak, Metadata>.ProcessedPeak()
                    {
                        peak = supPeak.peak,
                        xSquared = tXsqrd,
                        rtp = ChiSquaredCache.ChiSqrdDistRTP(tXsqrd, 2 + (supportingPeaks.Count * 2)),
                        supportingPeaks = tSupPeak
                    };

                    if (supPeak.peak.metadata.value <= Options.tauS)
                    {
                        targetSample.R_j___sc[chrTitle]++;
                        anRe.classification = PeakClassificationType.StringentConfirmed;
                    }
                    else
                    {
                        targetSample.R_j___wc[chrTitle]++;
                        anRe.classification = PeakClassificationType.WeakConfirmed;
                    }

                    targetSample.R_j__c[chrTitle].Add(supPeak.peak.metadata.hashKey, anRe);
                }
            }
        }

        private void DiscardPeak(Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks, byte discardReason)
        {
            var anRe = new AnalysisResult<Peak, Metadata>.ProcessedPeak
            {
                peak = p,
                xSquared = tXsqrd,
                reason = discardReason,
                supportingPeaks = supportingPeaks
            };

            if (p.metadata.value <= Options.tauS)
            {
                // The cause of discarding the region is :
                if (supportingPeaks.Count + 1 >= Options.C)
                    analysisResults.R_j__sdt[chrTitle]++;  // - Test failure
                else analysisResults.R_j__sdc[chrTitle]++; // - insufficient intersecting regions count

                anRe.classification = PeakClassificationType.StringentDiscarded;
            }
            else
            {
                // The cause of discarding the region is :
                if (supportingPeaks.Count + 1 >= Options.C)
                    analysisResults.R_j__wdt[chrTitle]++;  // - Test failure
                else analysisResults.R_j__wdc[chrTitle]++; // - insufficient intersecting regions count

                anRe.classification = PeakClassificationType.WeakDiscarded;
            }

            if (!analysisResults.R_j__d[chrTitle].ContainsKey(p.metadata.hashKey))
                analysisResults.R_j__d[chrTitle].Add(p.metadata.hashKey, anRe);

            if (supportingPeaks.Count + 1 >= Options.C)
                DiscardSupportingPeaks(p, supportingPeaks, discardReason);
        }

        private void DiscardSupportingPeaks(Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks, byte discardReason)
        {
            foreach (var supPeak in supportingPeaks)
            {
                if (!Data<Peak, Metadata>.analysisResults[supPeak.sampleIndex].R_j__d[chrTitle].ContainsKey(supPeak.peak.metadata.hashKey))
                {
                    var tSupPeak = new List<AnalysisResult<Peak, Metadata>.SupportingPeak>();
                    var targetSample = Data<Peak, Metadata>.analysisResults[supPeak.sampleIndex];
                    tSupPeak.Add(new AnalysisResult<Peak, Metadata>.SupportingPeak() { peak = p, sampleIndex = sampleHashKey });

                    foreach (var sP in supportingPeaks)
                        if (supPeak.CompareTo(sP) != 0)
                            tSupPeak.Add(sP);

                    var anRe = new AnalysisResult<Peak, Metadata>.ProcessedPeak()
                    {
                        peak = supPeak.peak,
                        xSquared = tXsqrd,
                        reason = discardReason,
                        rtp = ChiSquaredCache.ChiSqrdDistRTP(tXsqrd, 2 + (supportingPeaks.Count * 2)),
                        supportingPeaks = tSupPeak
                    };


                    if (supPeak.peak.metadata.value <= Options.tauS)
                    {
                        targetSample.R_j__sdt[chrTitle]++;
                        anRe.classification = PeakClassificationType.StringentDiscarded;
                    }
                    else
                    {
                        targetSample.R_j__wdt[chrTitle]++;
                        anRe.classification = PeakClassificationType.WeakDiscarded;
                    }

                    targetSample.R_j__d[chrTitle].Add(supPeak.peak.metadata.hashKey, anRe);
                }
            }
        }

        private void CalculateXsqrd(Peak p, List<AnalysisResult<Peak, Metadata>.SupportingPeak> supportingPeaks)
        {
            if (p.metadata.value != 0)
                tXsqrd = Math.Log(p.metadata.value, Math.E);
            else
                tXsqrd = Math.Log(Options.default0PValue, Math.E);

            foreach (var supPeak in supportingPeaks)
            {
                if (supPeak.peak.metadata.value != 0)
                    tXsqrd += Math.Log(supPeak.peak.metadata.value, Math.E);
                else
                    tXsqrd += Math.Log(Options.default0PValue, Math.E);
            }

            tXsqrd = tXsqrd * (-2);

            if (tXsqrd >= Math.Abs(Options.defaultMaxLogOfPVvalue))
                tXsqrd = Math.Abs(Options.defaultMaxLogOfPVvalue);
        }

        internal void IntermediateSetsPurification()
        {
            var analysisResults = Data<Peak, Metadata>.analysisResults;
            var samples = Data<Peak, Metadata>.sampleKeys;

            if (Options.replicateType == ReplicateType.Biological)
            {
                // Performe : R_j__d = R_j__d \ { R_j__d intersection R_j__c }

                foreach (var sample in samples)
                {
                    foreach (var chr in analysisResults[sample.Key].R_j__c)
                    {
                        foreach (var confirmedPeak in chr.Value)
                        {
                            if (analysisResults[sample.Key].R_j__d[chr.Key].ContainsKey(confirmedPeak.Key))
                            {
                                if (confirmedPeak.Value.peak.metadata.value <= Options.tauS)
                                    analysisResults[sample.Key].total_scom++;
                                else if (confirmedPeak.Value.peak.metadata.value <= Options.tauW)
                                    analysisResults[sample.Key].total_wcom++;

                                analysisResults[sample.Key].R_j__d[chr.Key].Remove(confirmedPeak.Key);
                            }
                        }
                    }
                }
            }
            else
            {
                // Performe : R_j__c = R_j__c \ { R_j__c intersection R_j__d }

                foreach (var sample in samples)
                {
                    foreach (var chr in analysisResults[sample.Key].R_j__d)
                    {
                        foreach (var discardedPeak in chr.Value)
                        {
                            if (analysisResults[sample.Key].R_j__c[chr.Key].ContainsKey(discardedPeak.Key))
                            {
                                if (discardedPeak.Value.peak.metadata.value <= Options.tauS)
                                    analysisResults[sample.Key].total_scom++;
                                else if (discardedPeak.Value.peak.metadata.value <= Options.tauW)
                                    analysisResults[sample.Key].total_wcom++;

                                analysisResults[sample.Key].R_j__c[chr.Key].Remove(discardedPeak.Key);
                            }
                        }
                    }
                }
            }
        }

        internal void CreateOuputSet()
        {
            var analysisResults = Data<Peak, Metadata>.analysisResults;
            var samples = Data<Peak, Metadata>.sampleKeys;

            foreach (var sample in samples)
            {
                foreach (var chr in analysisResults[sample.Key].R_j__c)
                {
                    foreach (var confirmedPeak in chr.Value)
                    {
                        var outputPeak = new AnalysisResult<Peak, Metadata>.ProcessedPeak()
                        {
                            peak = confirmedPeak.Value.peak,
                            rtp = confirmedPeak.Value.rtp,
                            xSquared = confirmedPeak.Value.xSquared,
                            classification = PeakClassificationType.TruePositive,
                            supportingPeaks = confirmedPeak.Value.supportingPeaks,
                        };

                        if (confirmedPeak.Value.peak.metadata.value <= Options.tauS)
                        {
                            outputPeak.classification = PeakClassificationType.StringentConfirmed;
                            analysisResults[sample.Key].R_j___so[chr.Key]++;
                        }
                        else if (confirmedPeak.Value.peak.metadata.value <= Options.tauW)
                        {
                            outputPeak.classification = PeakClassificationType.WeakConfirmed;
                            analysisResults[sample.Key].R_j___wo[chr.Key]++;
                        }

                        analysisResults[sample.Key].R_j__o[chr.Key].Add(outputPeak);
                    }
                }
            }
        }

        /// <summary>
        /// Benjamini–Hochberg procedure (step-up procedure)
        /// </summary>
        internal void EstimateFalseDiscoveryRate()
        {
            var analysisResults = Data<Peak, Metadata>.analysisResults;
            var samples = Data<Peak, Metadata>.sampleKeys;

            foreach (var sample in samples)
            {
                foreach (var chr in analysisResults[sample.Key].R_j__o)
                {
                    analysisResults[sample.Key].R_j_TP[chr.Key] = (uint)chr.Value.Count;
                    analysisResults[sample.Key].R_j_FP[chr.Key] = 0;

                    var outputSet = analysisResults[sample.Key].R_j__o[chr.Key];

                    int m = outputSet.Count;

                    // Sorts output set based on the values of peaks. 
                    outputSet.Sort(new Comparers.CompareProcessedPeakByValue<Peak, Metadata>());

                    for (int k = 1; k <= m; k++)
                    {
                        if (outputSet[k - 1].peak.metadata.value > ((double)k / (double)m) * Options.alpha)
                        {
                            k--;

                            for (int l = 1; l < k; l++)
                            {
                                // This should update the [analysisResults[sample.Key].R_j__o[chr.Key]] ; is it updating ?
                                outputSet[l].adjPValue = (((double)k * outputSet[l].peak.metadata.value) / (double)m) * Options.alpha;
                                outputSet[l].statisticalClassification = PeakClassificationType.TruePositive;
                            }
                            for (int l = k; l < m; l++)
                            {
                                outputSet[l].adjPValue = (((double)k * outputSet[l].peak.metadata.value) / (double)m) * Options.alpha;
                                outputSet[l].statisticalClassification = PeakClassificationType.FalsePositive;
                            }

                            analysisResults[sample.Key].R_j_TP[chr.Key] = (uint)k;
                            analysisResults[sample.Key].R_j_FP[chr.Key] = (uint)(m - k);

                            break;
                        }
                    }

                    // Sorts output set using default comparer. 
                    // The default sorter gives higher priority to two ends than values. 
                    outputSet.Sort();
                }
            }
        }
    }
}
