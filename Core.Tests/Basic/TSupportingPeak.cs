// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

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
            var sp = new SupportingPeak<Peak>(new Peak(1, 10, 100, 5, ""), 1);

            // Act & Assert
            Assert.True(sp.CompareTo(null) == 1);
        }
    }
}
