// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Replicates
{
    public class BioAttributes
    {
        /*
        [Fact]
        public void IfConfirmedAndDiscardedThenKeepOnlyConfirmed()
        {
            // Arrange
            var sets = new Sets<PPeak>(2, ReplicateType.Biological);

            var confirmedPeak = new PPeak<PPeak>(
                new Peak(1, 10, 100), 10, new List<SupportingPeak<PPeak>>());
            confirmedPeak.AddClassification(Attributes.Confirmed);

            var discardedPeak = new PPeak<PPeak>(
                new Peak(1, 10, 100), 10, new List<SupportingPeak<PPeak>>());
            discardedPeak.AddClassification(Attributes.Discarded);

            // Act
            sets.AddOrUpdate(discardedPeak);
            sets.AddOrUpdate(confirmedPeak);

            // Assert
            Assert.True(sets.Get(Attributes.Confirmed).Any());
            Assert.False(sets.Get(Attributes.Discarded).Any());
        }*/
    }
}
