/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

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
        private Dictionary<Attributes, SortedList<int, I>> _setsInit { set; get; }
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
                { Attributes.Output, new Dictionary<ulong, ProcessedPeak<I>>() }
            };

            _setsInit = new Dictionary<Attributes, SortedList<int, I>>
            {
                { Attributes.Stringent, new SortedList<int, I>() },
                { Attributes.Weak, new SortedList<int, I>() },
                { Attributes.Background, new SortedList<int, I>() }
            };
        }

        public void Add(I peak, Attributes type)
        {
            if (type != Attributes.Stringent && type != Attributes.Weak && type != Attributes.Background)
                throw new ArgumentException(
                    String.Format("Invalid attribute; accepted values are: {0}, {1}, and {2}.",
                    Attributes.Stringent.ToString(), Attributes.Weak.ToString(), Attributes.Background.ToString()));

            _setsInit[type].Add(peak.Left, peak);
        }

        public void Add(ProcessedPeak<I> peak, Attributes type)
        {
            switch (type)
            {
                case Attributes.Confirmed:
                case Attributes.Discarded:
                    if (!_sets[type].ContainsKey(peak.Source.HashKey))
                    {
                        _sets[type].Add(peak.Source.HashKey, peak);
                        foreach (var att in peak.Classification)
                            _stats[att]++;
                    }
                    break;

                case Attributes.Output:
                    if (!_sets[type].ContainsKey(peak.Source.HashKey))
                        _sets[type].Add(peak.Source.HashKey, peak);
                    break;

                default:
                    throw new ArgumentException(
                    String.Format("Invalid attribute; accepted values are: {0}, {1}, and {2}.",
                    Attributes.Confirmed.ToString(), Attributes.Discarded.ToString(), Attributes.Output.ToString()));
            }
        }

        public List<ProcessedPeak<I>> Get(Attributes attributes)
        {
            return _sets[attributes].Values.ToList();
        }

        public Dictionary<UInt64, ProcessedPeak<I>> GetDict(Attributes attributes)
        {
            return _sets[attributes];
        }

        public IList<I> GetInitialClassifications(Attributes type)
        {
            switch (type)
            {
                case Attributes.Stringent:
                case Attributes.Weak:
                case Attributes.Background:
                    return _setsInit[type].Values;

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
