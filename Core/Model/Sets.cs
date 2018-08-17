// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Genometric.MSPC.Core.Model
{
    public class Sets<I>
        where I : IChIPSeqPeak, new()
    {
        private int _fpCount;
        private int _tpCount;
        private readonly ReplicateType _replicateType;
        private readonly Dictionary<UInt64, ProcessedPeak<I>> _peaks;

        public Sets(int capacity, ReplicateType replicateType)
        {
            _replicateType = replicateType;
            _peaks = new Dictionary<ulong, ProcessedPeak<I>>(capacity: capacity);
        }

        public void AddOrUpdate(ProcessedPeak<I> processedPeak)
        {
            if (_peaks.TryGetValue(processedPeak.Source.HashKey, out ProcessedPeak<I> oldValue))
            {
                if (_replicateType == ReplicateType.Biological)
                {
                    if ((oldValue.Classification.Contains(Attributes.Discarded) && processedPeak.Classification.Contains(Attributes.Confirmed)) ||
                        (!oldValue.Classification.Contains(Attributes.Confirmed) && !oldValue.Classification.Contains(Attributes.Discarded)))
                        _peaks[processedPeak.Source.HashKey] = processedPeak;
                }
                else
                {
                    if (oldValue.Classification.Contains(Attributes.Confirmed) && processedPeak.Classification.Contains(Attributes.Discarded) ||
                        (!oldValue.Classification.Contains(Attributes.Confirmed) && !oldValue.Classification.Contains(Attributes.Discarded)))
                        _peaks[processedPeak.Source.HashKey] = processedPeak;
                }
            }
            else
            {
                _peaks.Add(processedPeak.Source.HashKey, processedPeak);
            }
        }

        public IEnumerable<ProcessedPeak<I>> Get(Attributes attributes)
        {
            return _peaks.Where(kvp => kvp.Value.Classification.Contains(attributes)).Select(kvp => kvp.Value);
        }

        internal void SetFalsePositiveCount(int value)
        {
            _fpCount = value;
        }

        internal void SetTruePositiveCount(int value)
        {
            _tpCount = value;
        }
    }
}
