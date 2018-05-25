// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Genometric.MSPC.Model
{
    public class Sets<I>
        where I : IChIPSeqPeak, new()
    {
        private readonly Dictionary<Attributes, uint> _stats;
        private Dictionary<Attributes, Dictionary<UInt64, I>> _setsInit { set; get; }
        private Dictionary<Attributes, Dictionary<UInt64, ProcessedPeak<I>>> _sets { set; get; }

        public Sets()
        {
            _stats = new Dictionary<Attributes, uint>();
            foreach (var att in Enum.GetValues(typeof(Attributes)).Cast<Attributes>())
                _stats.Add(att, 0);

            _sets = new Dictionary<Attributes, Dictionary<ulong, ProcessedPeak<I>>>
            {
                { Attributes.Confirmed, new Dictionary<ulong, ProcessedPeak<I>>() },
                { Attributes.Discarded, new Dictionary<ulong, ProcessedPeak<I>>() },
                { Attributes.TruePositive, new Dictionary<ulong, ProcessedPeak<I>>() },
                { Attributes.FalsePositive, new Dictionary<ulong, ProcessedPeak<I>>() }
            };

            _setsInit = new Dictionary<Attributes, Dictionary<UInt64, I>>
            {
                { Attributes.Stringent, new Dictionary<UInt64, I>() },
                { Attributes.Weak, new Dictionary<UInt64, I>() },
                { Attributes.Background, new Dictionary<UInt64, I>() }
            };
        }

        public void Add(I peak, Attributes attribute)
        {
            switch (attribute)
            {
                case Attributes.Stringent:
                case Attributes.Weak:
                case Attributes.Background:
                    _setsInit[attribute].Add(peak.HashKey, peak);
                    break;

                default:
                    throw new ArgumentException(
                    String.Format("Invalid attribute; accepted values are: {0}, {1}, and {2}.",
                    Attributes.Stringent.ToString(), Attributes.Weak.ToString(), Attributes.Background.ToString()));
            }
        }

        public void Add(ProcessedPeak<I> peak, Attributes attribute)
        {
            switch (attribute)
            {
                case Attributes.Confirmed:
                case Attributes.Discarded:
                    if (!_sets[attribute].ContainsKey(peak.Source.HashKey))
                    {
                        _sets[attribute].Add(peak.Source.HashKey, peak);
                        foreach (var att in peak.Classification)
                            _stats[att]++;
                    }
                    break;

                default:
                    throw new ArgumentException(
                    String.Format("Invalid attribute; accepted values are: {0} and {1}.",
                    Attributes.Confirmed.ToString(), Attributes.Discarded.ToString()));
            }
        }

        public void Add(ProcessedPeak<I> peak)
        {
            foreach(var attribute in peak.Classification)
            {
                switch(attribute)
                {
                    case Attributes.Stringent:
                    case Attributes.Weak:
                    case Attributes.Background:
                        if (!_setsInit[attribute].ContainsKey(peak.Source.HashKey))
                        {
                            _setsInit[attribute].Add(peak.Source.HashKey, peak.Source);
                            _stats[attribute]++;
                        }
                        break;

                    case Attributes.Confirmed:
                    case Attributes.Discarded:
                        if (!_sets[attribute].ContainsKey(peak.Source.HashKey))
                        {
                            _sets[attribute].Add(peak.Source.HashKey, peak);
                            _stats[attribute]++;
                        }
                        break;
                }
            }
        }

        public List<ProcessedPeak<I>> Get(Attributes attributes)
        {
            return _sets[attributes].Values.ToList();
        }

        public bool Contains(Attributes attribute, UInt64 hashkey)
        {
            switch (attribute)
            {
                case Attributes.Stringent:
                case Attributes.Weak:
                case Attributes.Background:
                    return _setsInit[attribute].ContainsKey(0);

                default:
                    return _sets[attribute].ContainsKey(hashkey);
            }
        }

        internal bool Remove(Attributes attribute, UInt64 hashkey)
        {
            switch (attribute)
            {
                case Attributes.Stringent:
                case Attributes.Weak:
                case Attributes.Background:
                    return _setsInit[attribute].Remove(0);

                default:
                    return _sets[attribute].Remove(hashkey);
            }
        }

        public IList<I> GetInitialClassifications(Attributes attribute)
        {
            switch (attribute)
            {
                case Attributes.Stringent:
                case Attributes.Weak:
                case Attributes.Background:
                    return _setsInit[attribute].Values.ToList();

                default:
                    throw new ArgumentException(
                    String.Format("Invalid attribute; accepted values are: {0}, {1}, and {2}.",
                    Attributes.Stringent.ToString(), Attributes.Weak.ToString(), Attributes.Background.ToString()));
            }
        }

        public ImmutableDictionary<Attributes, uint> Stats
        {
            get { return _stats.ToImmutableDictionary(); }
        }

        internal void SetFalsePositiveCount(uint value)
        {
            _stats[Attributes.FalsePositive] = value;
        }

        internal void SetTruePositiveCount(uint value)
        {
            _stats[Attributes.TruePositive] = value;
        }
    }
}
