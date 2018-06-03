// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Genometric.MSPC.Model
{
    public class Sets<I>
        where I : IChIPSeqPeak, new()
    {
        private uint _fpCount;
        private uint _tpCount;
        private Dictionary<Attributes, Dictionary<UInt64, ProcessedPeak<I>>> _sets { set; get; }

        public Sets()
        {
            _sets = new Dictionary<Attributes, Dictionary<ulong, ProcessedPeak<I>>>
            {
                { Attributes.Stringent, new Dictionary<ulong, ProcessedPeak<I>>() },
                { Attributes.Weak, new Dictionary<ulong, ProcessedPeak<I>>() },
                { Attributes.Background, new Dictionary<ulong, ProcessedPeak<I>>() },
                { Attributes.Confirmed, new Dictionary<ulong, ProcessedPeak<I>>() },
                { Attributes.Discarded, new Dictionary<ulong, ProcessedPeak<I>>() },
                { Attributes.TruePositive, new Dictionary<ulong, ProcessedPeak<I>>() },
                { Attributes.FalsePositive, new Dictionary<ulong, ProcessedPeak<I>>() }
            };
        }

        public void Add(ProcessedPeak<I> peak)
        {
            foreach (var attribute in peak.Classification)
                if (!_sets[attribute].ContainsKey(peak.Source.HashKey))
                    _sets[attribute].Add(peak.Source.HashKey, peak);
        }

        public List<ProcessedPeak<I>> Get(Attributes attributes)
        {
            return _sets[attributes].Values.ToList();
        }

        public bool Contains(Attributes attribute, UInt64 hashkey)
        {
            return _sets[attribute].ContainsKey(hashkey);
        }

        internal bool Remove(Attributes attribute, UInt64 hashkey)
        {
            return _sets[attribute].Remove(hashkey);
        }

        internal void SetFalsePositiveCount(uint value)
        {
            _fpCount = value;
        }

        internal void SetTruePositiveCount(uint value)
        {
            _tpCount = value;
        }
    }
}
