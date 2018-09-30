// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.Core.Comparers;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class CompareProcessedPeaksByValue
    {
        private ProcessedPeak<Peak> GetP(int left = 1000, int right = 10000, double value = 100)
        {
            return new ProcessedPeak<Peak>(new Peak(left, right, value), 10, new List<SupportingPeak<Peak>>());
        }

        [Fact]
        public void BothAreNull()
        {
            // Arrange
            var comparer = new CompareProcessedPeaksByValue<Peak>();

            // Act
            var result = comparer.Compare(null, null);

            // Assert
            Assert.True(result == 0);
        }

        [Fact]
        public void XIsNull()
        {
            // Arrange
            var comparer = new CompareProcessedPeaksByValue<Peak>();
            var y = GetP();

            // Act
            var result = comparer.Compare(null, y);

            // Assert
            Assert.True(result == -1);
        }

        [Fact]
        public void YIsNull()
        {
            // Arrange
            var comparer = new CompareProcessedPeaksByValue<Peak>();
            var x = GetP();

            // Act
            var result = comparer.Compare(x, null);

            // Assert
            Assert.True(result == 1);
        }

        [Theory]
        [InlineData(100, 10, 1)]
        [InlineData(10, 100, -1)]
        [InlineData(100, 100, 0)]
        public void CompareByValue(int xValue, int yValue, int expectedResult)
        {
            // Arrange
            var comparer = new CompareProcessedPeaksByValue<Peak>();
            var x = GetP(value: xValue);
            var y = GetP(value: yValue);

            // Act
            var result = comparer.Compare(x, y);

            // Assert
            Assert.True(result == expectedResult);
        }

        [Theory]
        [InlineData(100, 10, 1)]
        [InlineData(10, 100, -1)]
        [InlineData(100, 100, 0)]
        public void EqualValueCompareByIntervalLeft(int xLeft, int yLeft, int expectedResult)
        {
            // Arrange
            var comparer = new CompareProcessedPeaksByValue<Peak>();
            var x = GetP(left: xLeft);
            var y = GetP(left: yLeft);

            // Act
            var result = comparer.Compare(x, y);

            // Assert
            Assert.True(result == expectedResult);
        }

        [Theory]
        [InlineData(100, 10, 1)]
        [InlineData(10, 100, -1)]
        [InlineData(100, 100, 0)]
        public void EqualValueCompareByIntervalRight(int xRight, int yRight, int expectedResult)
        {
            // Arrange
            var comparer = new CompareProcessedPeaksByValue<Peak>();
            var x = GetP(right: xRight);
            var y = GetP(right: yRight);

            // Act
            var result = comparer.Compare(x, y);

            // Assert
            Assert.True(result == expectedResult);
        }
    }
}
