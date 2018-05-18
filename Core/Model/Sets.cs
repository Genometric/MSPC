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
        private Dictionary<Attributes[], Dictionary<UInt64, ProcessedPeak<I>>> _sets { set; get; }

        private Dictionary<Attributes, uint> _stats;

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

        public Sets()
        {
            _stats = new Dictionary<Attributes, uint>();
            foreach (var att in Enum.GetValues(typeof(Attributes)).Cast<Attributes>())
                _stats.Add(att, 0);

            _sets = new Dictionary<Attributes[], Dictionary<ulong, ProcessedPeak<I>>>(comparer: new AttributesComparer());
            _sets.Add(new Attributes[] { Attributes.Confirmed }, new Dictionary<ulong, ProcessedPeak<I>>());
            _sets.Add(new Attributes[] { Attributes.Discarded }, new Dictionary<ulong, ProcessedPeak<I>>());

            total_scom = 0;
            total_wcom = 0;

            R_j__s = new SortedList<int, I>();
            R_j__w = new SortedList<int, I>();
            R_j__b = new SortedList<int, I>();

            R_j__o = new List<ProcessedPeak<I>>();
        }

        public void Add(I peak, Attributes type)
        {
            switch (type)
            {
                case Attributes.Stringent:
                    R_j__s.Add(peak.Left, peak);
                    break;

                case Attributes.Weak:
                    R_j__w.Add(peak.Left, peak);
                    break;

                case Attributes.Background:
                    R_j__b.Add(peak.Left, peak);
                    break;
            }
        }

        public void Add(ProcessedPeak<I> peak, Attributes type)
        {
            switch (type)
            {
                case Attributes.Confirmed:
                    if (!_sets[new Attributes[] { Attributes.Confirmed }].ContainsKey(peak.peak.HashKey))
                    {
                        _sets[new Attributes[] { Attributes.Confirmed }].Add(peak.peak.HashKey, peak);
                        foreach (var att in peak.classification)
                            _stats[att]++;
                    }
                    break;

                case Attributes.Discarded:
                    if (!_sets[new Attributes[] { Attributes.Discarded }].ContainsKey(peak.peak.HashKey))
                    {
                        _sets[new Attributes[] { Attributes.Discarded }].Add(peak.peak.HashKey, peak);
                        foreach (var att in peak.classification)
                            _stats[att]++;
                    }
                    break;

                case Attributes.Output:
                    R_j__o.Add(peak);
                    break;
            }
        }

        /// <summary>
        /// Chromosome-wide stringent peaks of sample j
        /// </summary>
        private SortedList<int, I> R_j__s { set; get; }

        /// <summary>
        /// Chromosome-wide weak peaks of sample j
        /// </summary>
        private SortedList<int, I> R_j__w { set; get; }

        /// <summary>
        /// Chromosome-wide background peaks of sample j (i.e., the peaks with p-value > T_w ).
        /// </summary>
        private SortedList<int, I> R_j__b { set; get; }

        public Dictionary<UInt64, ProcessedPeak<I>> Get(Attributes[] attributes)
        {
            return _sets[attributes];
        }

        public IList<I> Get(Attributes attribute)
        {
            switch (attribute)
            {
                case Attributes.Stringent:
                    return R_j__s.Values;

                case Attributes.Weak:
                    return R_j__w.Values;

                case Attributes.Background:
                    return R_j__b.Values;

                default:
                    return new List<I>();
            }
        }

        /// <summary>
        /// Chromosome-wide set of peaks as the result of the algorithm. The peaks of this set passed
        /// three tests (i.e., intersecting peaks count, X-squared test, and benjamini-hochberg
        /// multiple testing correction).
        /// </summary>
        public List<ProcessedPeak<I>> R_j__o { set; get; }

        /// <summary>
        /// Total number of stringent peaks that are both validated and discarded
        /// through multiple tests.
        /// </summary>
        public UInt32 total_scom { set; get; }
        /// <summary>
        /// Total number of weak peaks that are both validated and discarded
        /// through multiple tests.
        /// </summary>
        public UInt32 total_wcom { set; get; }
    }
}