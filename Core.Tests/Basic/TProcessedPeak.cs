using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using Xunit;

namespace Genometric.MSPC.Core.Tests.Basic
{
    public class TProcessedPeak
    {
        private ProcessedPeak<Peak> GetP(int left = 1000, int right = 10000, double value = 100, string name = "", int summit = 0)
        {
            return new ProcessedPeak<Peak>(new Peak(left, right, value, name, summit), 10, new List<SupportingPeak<Peak>>());
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
            var p = new Peak
            (
                left: 10,
                summit: 15,
                right: 20,
                name: "MSPC_Peak",
                value: 100
            );

            var sup = new List<SupportingPeak<Peak>>
            {
                new SupportingPeak<Peak>(new Peak
                (
                    left : 5,
                    right : 25,
                    summit : 15,
                    name : "MSPC_SupPeak",
                    value : 123
                ), 1)
            };

            var pp1 = new ProcessedPeak<Peak>(p, xSquared, sup);
            var pp2 = new ProcessedPeak<Peak>(p, xSquared, sup);

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
