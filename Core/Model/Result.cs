// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System.Collections.Concurrent;

namespace Genometric.MSPC.Core.Model
{
    public class Result<I>
        where I : IChIPSeqPeak, new()
    {
        private readonly ReplicateType _replicateType;
        public ConcurrentDictionary<string, Sets<I>> Chromosomes { set; get; }

        internal Result(ReplicateType replicateType)
        {
            _replicateType = replicateType;
            Chromosomes = new ConcurrentDictionary<string, Sets<I>>();
        }

        public void AddChromosome(string chr, int capacity)
        {
            Chromosomes.TryAdd(chr, new Sets<I>(capacity, _replicateType));
        }
    }
}
