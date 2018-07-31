// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.Core.Model;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Base
{
    public class TestPeakClass
    {
        private readonly Peak<ChIPSeqPeak> _x;
        private readonly Peak<ChIPSeqPeak> _y;

        public TestPeakClass()
        {
            _x = new Peak<ChIPSeqPeak>(new ChIPSeqPeak());
            _y = new Peak<ChIPSeqPeak>(new ChIPSeqPeak());

            _x.Source.Value = 100;
            _x.Source.Left = 1000;
            _x.Source.Right = 10000;

            _y.Source.Value = 100;
            _y.Source.Left = 1000;
            _y.Source.Right = 10000;
        }

        [Fact]
        public void PeakIsBiggerThanNull()
        {
            // Arrange & Act
            var r = _x.CompareTo(null);

            // Assert
            Assert.True(r == 1);
        }

        [Theory]
        [InlineData(100, 10, 1)]
        [InlineData(10, 100, -1)]
        [InlineData(100,100, 0)]
        public void CompareByLeftEnd(int xLeft, int yLeft, int expectedResult)
        {
            // Arrange
            _x.Source.Left = xLeft;
            _y.Source.Left = yLeft;

            // Act
            var r = _x.CompareTo(_y);

            // Assert
            Assert.True(r == expectedResult);
        }

        [Theory]
        [InlineData(100, 10, 1)]
        [InlineData(10, 100, -1)]
        [InlineData(100, 100, 0)]
        public void CompareByRightEnd(int xRight, int yRight, int expectedResult)
        {
            // Arrange
            _x.Source.Right = xRight;
            _y.Source.Right = yRight;

            // Act
            var r = _x.CompareTo(_y);

            // Assert
            Assert.True(r == expectedResult);
        }

        [Theory]
        [InlineData(100, 10, 1)]
        [InlineData(10, 100, -1)]
        [InlineData(100, 100, 0)]
        public void CompareByValue(int xValue, int yValue, int expectedResult)
        {
            // Arrange
            _x.Source.Value = xValue;
            _y.Source.Value = yValue;

            // Act
            var r = _x.CompareTo(_y);

            // Assert
            Assert.True(r == expectedResult);
        }

        [Theory]
        [InlineData(100, 10, 1)]
        [InlineData(10, 100, -1)]
        [InlineData(100, 100, 0)]
        public void CompareByHashkey(uint xHashkey, uint yHashkey, int expectedResult)
        {
            // Arrange
            _x.Source.HashKey = xHashkey;
            _y.Source.HashKey = yHashkey;

            // Act
            var r = _x.CompareTo(_y);

            // Assert
            Assert.True(r == expectedResult);
        }
    }
}
