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
        private readonly ProcessedPeak<ChIPSeqPeak> _x;
        private readonly ProcessedPeak<ChIPSeqPeak> _y;

        public TestProcessedPeak()
        {
            _x = new ProcessedPeak<ChIPSeqPeak>(new ChIPSeqPeak(), 10, new List<SupportingPeak<ChIPSeqPeak>>());
            _y = new ProcessedPeak<ChIPSeqPeak>(new ChIPSeqPeak(), 10, new List<SupportingPeak<ChIPSeqPeak>>());

            _x.Source.Value = 100;
            _x.Source.Left = 1000;
            _x.Source.Right = 10000;
            _x.Source.Name = "";

            _y.Source.Value = 100;
            _y.Source.Left = 1000;
            _y.Source.Right = 10000;
            _y.Source.Name = "";
        }

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
        public void CompareTwoEqualInstances()
        {
            // Arrange
            var p = new ChIPSeqPeak
            {
                Left = 10,
                Summit = 15,
                Right = 20,
                Name = "MSPC_Peak",
                Value = 100,
                HashKey = 1234567890
            };

            var sup = new List<SupportingPeak<ChIPSeqPeak>>
            {
                new SupportingPeak<ChIPSeqPeak>(new ChIPSeqPeak()
                {
                    Left = 5,
                    Right = 25,
                    Summit = 15,
                    Name = "MSPC_SupPeak",
                    Value = 123,
                    HashKey = 987654321
                }, 1)
            };

            var pp1 = new ProcessedPeak<ChIPSeqPeak>(p, 10, sup);
            var pp2 = new ProcessedPeak<ChIPSeqPeak>(p, 10, sup);

            // Act
            var r = pp1.Equals(pp2);

            // Assert
            Assert.True(r);
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

        [Theory]
        [InlineData(100, 10, true)]
        [InlineData(10, 100, false)]
        [InlineData(100, 100, false)]
        public void GreaterOperator(int xValue, int yValue, bool expectedResult)
        {
            // Arrange
            _x.Source.Value = xValue;
            _y.Source.Value = yValue;

            // Act
            var r = _x > _y;

            // Assert
            Assert.True(r == expectedResult);
        }
    }
}
