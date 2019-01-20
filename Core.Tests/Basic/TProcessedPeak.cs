// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class TProcessedPeak
    {
        private PPeak GetP(int left = 1000, int right = 10000, double value = 100, string name = "", int summit = 0)
        {
            return new PPeak(left, right, value, name, summit) { XSquared = 10, SupportingPeaks = null };
        }

        [Fact]
        public void CompareToANullObject()
        {
            // Arrange
            var pp = GetP();

            // Act
            var r = pp.CompareTo(null);

            // Assert
            Assert.True(r == 1);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(double.NaN)]
        public void CompareTwoEqualInstances(double xSquared)
        {
            // Arrange
            var pp1 = new PPeak(10, 20, 100, "MSPC_Peak", 15) { AdjPValue = 11, XSquared = 22, SupportingPeaksCount = 33, SupportingPeaks = null };
            var pp2 = new PPeak(10, 20, 100, "MSPC_Peak", 15) { AdjPValue = 11, XSquared = 22, SupportingPeaksCount = 33, SupportingPeaks = null };

            // Act
            var r = pp1.Equals(pp2);

            // Assert
            Assert.True(r);
        }

        [Fact]
        public void ComputeHashCode()
        {
            // Arrange
            var pp = GetP();

            // Act
            var r = pp.GetHashCode();

            // Assert
            Assert.True(r != 0);
        }

        [Theory]
        [InlineData(100, 10, true)]
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
        public void NotEqualOperatorWhenXIsNull()
        {
            // Arrange
            var y = GetP();

            // Assert
            Assert.True(null != y);
        }

        [Fact]
        public void NotEqualOperatorWhenYIsNull()
        {
            // Arrange
            var x = GetP();

            // Assert
            Assert.True(x != null);
        }
    }
}
