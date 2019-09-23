// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class Table
    {
        [Theory]
        [InlineData("aaaaaa", 5)]
        [InlineData("aaaaaa", 4)]
        [InlineData("aaaaaa", 3)]
        public void ColumnWidthLessThanContentLenght(string content, int length)
        {
            // Arrange
            var table = new Logging.Table(new int[] { length });

            // Act
            var row = table.GetRow(columns: new string[] { content }).Replace('\t', ' ').Trim();

            // Assert
            Assert.Contains("...", row);
            Assert.Equal(row.Length, length);
        }

        [Theory]
        [InlineData("aaaaaa", 8)]
        [InlineData("aaaaaa", 7)]
        [InlineData("aaaaaa", 6)]
        public void ShouldNotTruncateWhenEnoughSpaceAvailable(string content, int length)
        {
            // Arrange
            var table = new Logging.Table(new int[] { length });

            // Act
            var row = table.GetRow(columns: new string[] { content }).Replace('\t', ' ').Trim();

            // Assert
            Assert.DoesNotContain("...", row);
        }
    }
}
