using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.Model
{
    public class Result<I>
        where I : IChIPSeqPeak, new()
    {
        public Dictionary<string, AnalysisResult<I>> Chromosomes { set; get; }

        public Result()
        {
            Chromosomes = new Dictionary<string, AnalysisResult<I>>();
        }

        public void AddChromosome(string chr)
        {
            Chromosomes.Add(chr, new AnalysisResult<I>());
        }
    }
}
