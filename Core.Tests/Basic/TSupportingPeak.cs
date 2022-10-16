using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.Core.Model;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class TSupportingPeak
    {
        [Fact]
        public void CompareWithANullObject()
        {
            // Arrange
            var sp = new SupportingPeak<Peak>(new Peak(1, 10, 100), 1);

            // Act & Assert
            Assert.True(sp.CompareTo(null) == 1);
        }
    }
}
