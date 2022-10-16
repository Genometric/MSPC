using Genometric.GeUtilities.IGenomics;
using System.Collections.Concurrent;

namespace Genometric.MSPC.Core.Model
{
    public class Result<I>
        where I : IPeak
    {
        private readonly ReplicateType _replicateType;
        public ConcurrentDictionary<string, ConcurrentDictionary<char, Sets<I>>> Chromosomes { set; get; }

        internal Result(ReplicateType replicateType)
        {
            _replicateType = replicateType;
            Chromosomes = new ConcurrentDictionary<string, ConcurrentDictionary<char, Sets<I>>>();
        }

        public void AddChromosome(string chr)
        {
            Chromosomes.TryAdd(chr, new ConcurrentDictionary<char, Sets<I>>());
        }

        public void AddStrand(string chr, char strand, int capacity)
        {
            Chromosomes[chr].TryAdd(strand, new Sets<I>(capacity, _replicateType));
        }
    }
}
