// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.Comparers;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Base
{
    public class CompareProcessedPeaksByValue
    {
        private readonly ProcessedPeak<ChIPSeqPeak> _x;
        private readonly ProcessedPeak<ChIPSeqPeak> _y;

        public CompareProcessedPeaksByValue()
        {
            _x = new ProcessedPeak<ChIPSeqPeak>(new ChIPSeqPeak(), 10, new List<SupportingPeak<ChIPSeqPeak>>());
            _y = new ProcessedPeak<ChIPSeqPeak>(new ChIPSeqPeak(), 10, new List<SupportingPeak<ChIPSeqPeak>>());

            _x.Source.Value = 100;
            _x.Source.Left = 1000;
            _x.Source.Right = 10000;
            _x.Source.HashKey = 12345;

            _y.Source.Value = 100;
            _y.Source.Left = 1000;
            _y.Source.Right = 10000;
            _y.Source.HashKey = 12345;
        }

        [Fact]
        public void BothAreNull()
        {
            // Arrange
            var comparer = new CompareProcessedPeaksByValue<ChIPSeqPeak>();

            // Act
            var result = comparer.Compare(null, null);

            // Assert
            Assert.True(result == 0);
        }

        [Fact]
        public void XIsNull()
        {
            // Arrange
            var comparer = new CompareProcessedPeaksByValue<ChIPSeqPeak>();

            // Act
            var result = comparer.Compare(null, _y);

            // Assert
            Assert.True(result == -1);
        }

        [Fact]
        public void YIsNull()
        {
            // Arrange
            var comparer = new CompareProcessedPeaksByValue<ChIPSeqPeak>();

            // Act
            var result = comparer.Compare(_x, null);

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
            var comparer = new CompareProcessedPeaksByValue<ChIPSeqPeak>();
            _x.Source.Value = xValue;
            _y.Source.Value = yValue;

            // Act
            var result = comparer.Compare(_x, _y);

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
            var comparer = new CompareProcessedPeaksByValue<ChIPSeqPeak>();
            _x.Source.Left = xLeft;
            _y.Source.Left = yLeft;

            // Act
            var result = comparer.Compare(_x, _y);

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
            var comparer = new CompareProcessedPeaksByValue<ChIPSeqPeak>();
            _x.Source.Right = xRight;
            _y.Source.Right = yRight;

            // Act
            var result = comparer.Compare(_x, _y);

            // Assert
            Assert.True(result == expectedResult);
        }
    }
}
