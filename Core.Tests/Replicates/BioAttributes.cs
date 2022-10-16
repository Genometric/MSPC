using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Replicates
{
    public class BioAttributes
    {
        [Fact]
        public void IfConfirmedAndDiscardedThenKeepOnlyConfirmed()
        {
            // Arrange
            var sets = new Sets<Peak>(2, ReplicateType.Biological);

            var confirmedPeak = new ProcessedPeak<Peak>(
                new Peak(1, 10, 100), 10, new List<SupportingPeak<Peak>>());
            confirmedPeak.AddClassification(Attributes.Confirmed);

            var discardedPeak = new ProcessedPeak<Peak>(
                new Peak(1, 10, 100), 10, new List<SupportingPeak<Peak>>());
            discardedPeak.AddClassification(Attributes.Discarded);

            // Act
            sets.AddOrUpdate(discardedPeak);
            sets.AddOrUpdate(confirmedPeak);

            // Assert
            Assert.True(sets.Get(Attributes.Confirmed).Any());
            Assert.False(sets.Get(Attributes.Discarded).Any());
        }
    }
}
