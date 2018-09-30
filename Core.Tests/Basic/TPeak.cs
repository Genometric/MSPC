// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.Core.Model;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class TPeak
    {
        private Peak<Peak> GetP(int left = 1000, int right = 10000, double value = 100, string name = "", string hashSeed = "")
        {
            return new Peak<Peak>(new Peak(left, right, value, name, hashSeed: hashSeed));
        }

        [Fact]
        public void PeakIsBiggerThanNull()
        {
            // Arrange & Act
            var r = GetP().CompareTo(null);

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
            var x = GetP(left: xLeft);
            var y = GetP(left: yLeft);

            // Act
            var r = x.CompareTo(y);

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
            var x = GetP(right: xRight);
            var y = GetP(right: yRight);

            // Act
            var r = x.CompareTo(y);

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
            var x = GetP(value: xValue);
            var y = GetP(value: yValue);

            // Act
            var r = x.CompareTo(y);

            // Assert
            Assert.True(r == expectedResult);
        }

        [Theory]
        [InlineData(100, 10, false)]
        [InlineData(10, 100, false)]
        [InlineData(100, 100, true)]
        public void CompareByHashkey(uint xHashSeed, uint yHashSeed, bool equal)
        {
            // Arrange
            var x = GetP(hashSeed: xHashSeed.ToString());
            var y = GetP(hashSeed: yHashSeed.ToString());

            // Act
            var r = x.CompareTo(y);

            // Assert
            Assert.True((r == 0) == equal);
        }

        [Theory]
        [InlineData(100, 10,true)]
        [InlineData(10, 100, false)]
        [InlineData(100, 100, false)]
        public void GreaterOperator(int xValue, int yValue, bool expectedResult)
        {
            // Arrange
            var x = GetP(value: xValue);
            var y = GetP(value: yValue);

            // Act
            var r = x > y;

            // Assert
            Assert.True(r == expectedResult);
        }

        [Theory]
        [InlineData(100, 10, false)]
        [InlineData(10, 100, true)]
        [InlineData(100, 100, false)]
        public void SmallerOperator(int xValue, int yValue, bool expectedResult)
        {
            // Arrange
            var x = GetP(value: xValue);
            var y = GetP(value: yValue);

            // Act
            var r = x < y;

            // Assert
            Assert.True(r == expectedResult);
        }

        [Theory]
        [InlineData(100, 10, true)]
        [InlineData(10, 100, false)]
        [InlineData(100, 100, true)]
        public void GreaterOrEqualOperator(int xValue, int yValue, bool expectedResult)
        {
            // Arrange
            var x = GetP(value: xValue);
            var y = GetP(value: yValue);

            // Act
            var r = x >= y;

            // Assert
            Assert.True(r == expectedResult);
        }

        [Theory]
        [InlineData(100, 10, false)]
        [InlineData(10, 100, true)]
        [InlineData(100, 100, true)]
        public void SmallerOrEqualOperator(int xValue, int yValue, bool expectedResult)
        {
            // Arrange
            var x = GetP(value: xValue);
            var y = GetP(value: yValue);

            // Act
            var r = x <= y;

            // Assert
            Assert.True(r == expectedResult);
        }

        [Fact]
        public void NotEqualOperator()
        {
            // Arrange
            var x = GetP();
            var y = GetP(right: 123456789);

            // Assert
            Assert.True(x != y);
        }

        [Fact]
        public void Equal()
        {
            // Arrange
            var x = GetP();

            // Assert
            Assert.True(x.Equals(x));
        }

        [Fact]
        public void GetHashCodeReturnsANumberOtherThanZero()
        {
            // Arrange
            var x = GetP();

            // Assert
            Assert.True(x.GetHashCode() != 0);
        }

        [Fact]
        public void ToStringDoesNotReturnNull()
        {
            // Arrange
            var x = GetP();

            // Assert
            Assert.NotNull(x.ToString());
        }
    }
}
