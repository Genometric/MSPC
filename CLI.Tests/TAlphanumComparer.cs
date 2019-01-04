// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.CLI.Exporter;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class TAlphanumComparer
    {
        [Theory]
        [InlineData(null, null, 0)]
        [InlineData("a", null, 0)]
        [InlineData(null, "b", 0)]
        [InlineData("1", "2", -1)]
        [InlineData("2", "1", 1)]
        [InlineData("a", "1", 1)]
        [InlineData("1", "a", -1)]
        [InlineData("chr1", "chr1", 0)]
        [InlineData("chrx", "chrx", 0)]
        [InlineData("chr1", "chr2", -1)]
        [InlineData("chr3", "chr4", -1)]
        [InlineData("chr1", "chrx", -1)]
        [InlineData("chrx", "chry", -1)]
        [InlineData("chr1_e", "chr2_e", -1)]
        [InlineData("chr1_b", "chr2_a", -1)]
        [InlineData("chr1_b", "chrx_a", -1)]
        public void Compare(string x, string y, int expected)
        {
            Assert.Equal(expected, new AlphanumComparer().Compare(x, y));
        }
    }
}
