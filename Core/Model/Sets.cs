// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System.Collections.Generic;
using System.Linq;

namespace Genometric.MSPC.Core.Model
{
    public class Sets<I>
        where I : IPeak
    {
        private readonly ReplicateType _replicateType;
        private readonly Dictionary<int, ProcessedPeak<I>> _peaks;

        public Sets(int capacity, ReplicateType replicateType)
        {
            _replicateType = replicateType;
            _peaks = new Dictionary<int, ProcessedPeak<I>>(capacity: capacity);
        }

        public void AddOrUpdate(ProcessedPeak<I> processedPeak)
        {
            if (_peaks.TryGetValue(processedPeak.Source.GetHashCode(), out ProcessedPeak<I> oldValue))
            {
                if (_replicateType == ReplicateType.Biological)
                {
                    if ((oldValue.HasAttribute(Attributes.Discarded) && processedPeak.HasAttribute(Attributes.Confirmed)) ||
                        (!oldValue.HasAttribute(Attributes.Confirmed) && !oldValue.HasAttribute(Attributes.Discarded)))
                        _peaks[processedPeak.Source.GetHashCode()] = processedPeak;
                }
                else
                {
                    if (oldValue.HasAttribute(Attributes.Confirmed) && processedPeak.HasAttribute(Attributes.Discarded) ||
                        (!oldValue.HasAttribute(Attributes.Confirmed) && !oldValue.HasAttribute(Attributes.Discarded)))
                        _peaks[processedPeak.Source.GetHashCode()] = processedPeak;
                }
            }
            else
            {
                _peaks.Add(processedPeak.Source.GetHashCode(), processedPeak);
            }
        }

        public IEnumerable<ProcessedPeak<I>> Get(Attributes attributes)
        {
            return _peaks.Where(kvp => kvp.Value.HasAttribute(attributes)).Select(kvp => kvp.Value);
        }

        public int Count(Attributes attribute)
        {
            return _peaks.Count(kvp => kvp.Value.HasAttribute(attribute));
        }
    }
}
