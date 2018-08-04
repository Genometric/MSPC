// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class TestProcessedPeak
    {
        [Fact]
        public void CompareToANullObject()
        {
            // Arrange
            var pp = new ProcessedPeak<ChIPSeqPeak>(new ChIPSeqPeak(), 10, new List<SupportingPeak<ChIPSeqPeak>>());

            // Act
            var r = pp.CompareTo(null);

            // Assert
            Assert.True(r == 1);
        }

        [Fact]
        public void ComputeHashCode()
        {
            // Arrange
            var pp = new ProcessedPeak<ChIPSeqPeak>(new ChIPSeqPeak(), 10, new List<SupportingPeak<ChIPSeqPeak>>());

            // Act
            var r = pp.GetHashCode();

            // Assert
            Assert.True(r != 0);
        }
    }
}
