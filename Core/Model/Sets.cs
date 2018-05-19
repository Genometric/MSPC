using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;
/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Genometric.MSPC.Model
{
    public class Sets<I>
        where I : IChIPSeqPeak, new()
    {
        private Dictionary<Attributes, Dictionary<UInt64, ProcessedPeak<I>>> _sets { set; get; }

        private Dictionary<Attributes, SortedList<int, I>> _setsInit { set; get; }

        private Dictionary<Attributes, uint> _stats;

        public ImmutableDictionary<Attributes, uint> Stats
        {
            get { return _stats.ToImmutableDictionary(); }
        }

        public Sets()
        {
            _stats = new Dictionary<Attributes, uint>();
            foreach (var att in Enum.GetValues(typeof(Attributes)).Cast<Attributes>())
                _stats.Add(att, 0);

            _sets = new Dictionary<Attributes, Dictionary<ulong, ProcessedPeak<I>>>();
            _sets.Add(Attributes.Confirmed, new Dictionary<ulong, ProcessedPeak<I>>());
            _sets.Add(Attributes.Discarded, new Dictionary<ulong, ProcessedPeak<I>>());
            _sets.Add(Attributes.Output, new Dictionary<ulong, ProcessedPeak<I>>());

            _setsInit = new Dictionary<Attributes, SortedList<int, I>>();
            _setsInit.Add(Attributes.Stringent, new SortedList<int, I>());
            _setsInit.Add(Attributes.Weak, new SortedList<int, I>());
            _setsInit.Add(Attributes.Background, new SortedList<int, I>());
        }

        public void Add(I peak, Attributes type)
        {
            switch (type)
            {
                case Attributes.Stringent:
                    _setsInit[Attributes.Stringent].Add(peak.Left, peak);
                    break;

                case Attributes.Weak:
                    _setsInit[Attributes.Weak].Add(peak.Left, peak);
                    break;

                case Attributes.Background:
                    _setsInit[Attributes.Background].Add(peak.Left, peak);
                    break;
            }
        }

        public void Add(ProcessedPeak<I> peak, Attributes type)
        {
            switch (type)
            {
                case Attributes.Confirmed:
                    if (!_sets[Attributes.Confirmed].ContainsKey(peak.peak.HashKey))
                    {
                        _sets[Attributes.Confirmed].Add(peak.peak.HashKey, peak);
                        foreach (var att in peak.classification)
                            _stats[att]++;
                    }
                    break;

                case Attributes.Discarded:
                    if (!_sets[Attributes.Discarded].ContainsKey(peak.peak.HashKey))
                    {
                        _sets[Attributes.Discarded].Add(peak.peak.HashKey, peak);
                        foreach (var att in peak.classification)
                            _stats[att]++;
                    }
                    break;

                case Attributes.Output:
                    if (!_sets[Attributes.Output].ContainsKey(peak.peak.HashKey))
                        _sets[Attributes.Output].Add(peak.peak.HashKey, peak);
                    break;
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

        public IList<I> GetInitialClassifications(Attributes attribute)
        {
            switch (attribute)
            {
                case Attributes.Stringent:
                    return _setsInit[Attributes.Stringent].Values;

                case Attributes.Weak:
                    return _setsInit[Attributes.Weak].Values;

                case Attributes.Background:
                    return _setsInit[Attributes.Background].Values;

                default:
                    return new List<I>();
            }
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