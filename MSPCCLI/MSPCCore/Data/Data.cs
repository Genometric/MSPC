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
using Polimi.DEIB.VahidJalili.MSPC.Warehouse;
using System;
using System.Collections.Generic;

namespace Polimi.DEIB.VahidJalili.MSPC.Analyzer.Data
{
    internal static class Data<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, new()
    {
        internal static Dictionary<uint, string> sampleKeys { set; get; }
        internal static Dictionary<uint, Dictionary<string, IntervalTree<Peak, int>>> trees { set; get; }
        internal static Dictionary<uint, AnalysisResult<Peak, Metadata>> analysisResults { set; get; }
        internal static List<double> cachedChiSqrd { set; get; }
        internal static void BuildSharedItems()
        {
            trees = new Dictionary<uint, Dictionary<string, IntervalTree<Peak, int>>>();
            Data<Peak, Metadata>.analysisResults = new Dictionary<uint, AnalysisResult<Peak, Metadata>>();

            foreach (var sampleKey in sampleKeys)
            {
                var sample = Samples<Peak, Metadata>.Data[sampleKey.Key];

                analysisResults.Add(sample.fileHashKey, new AnalysisResult<Peak, Metadata>(sample.fileName, sample.filePath, Samples<Peak, Metadata>.Data.Count * 2));

                Dictionary<string, IntervalTree<Peak, int>> sampleTree = new Dictionary<string, IntervalTree<Peak, int>>();

                foreach (var chromosome in sample.intervals)
                    foreach (var strand in chromosome.Value)
                    {
                        sampleTree.Add(chromosome.Key, new IntervalTree<Peak, int>());
                        analysisResults[sampleKey.Key].AddChromosome(chromosome.Key);

                        foreach (Peak p in strand.Value)
                            if (p.metadata.value <= Options.tauW)
                                sampleTree[chromosome.Key].AddInterval(p.left, p.right, p);
                            else
                                analysisResults[sampleKey.Key].R_j__b[chromosome.Key].Add(p);
                    }

                trees.Add(sample.fileHashKey, sampleTree);
            }
        }
    }
}
