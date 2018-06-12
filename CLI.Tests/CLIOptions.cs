// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Model;
using System;
using System.Text;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class CLIOptions
    {
        private const string _rep1 = "replicate_1.bed";
        private const string _rep2 = "replicate_2.bed";
        private const string _rep3 = "replicate_3.bed";
        private const double _tauW = 1E-2;
        private const double _tauS = 1E-9;
        private const double _gamma = 1E-12;
        private const float _alpha = 0.0005F;
        private const byte _c = 2;
        private const MultipleIntersections _m = MultipleIntersections.UseLowestPValue;
        private const ReplicateType _r = ReplicateType.Biological;

        private string GenerateShortNameArguments(
            string rep1 = _rep1, string rep2 = _rep2, string rep3 = _rep3, double tauW = _tauW, double tauS = _tauS,
            double gamma = _gamma, float alpha = _alpha, byte c = _c, MultipleIntersections m = _m, ReplicateType r = _r)
        {
            var builder = new StringBuilder();
            if (rep1 != null) builder.Append("-i " + rep1 + " ");
            if (rep2 != null) builder.Append("-i " + rep2 + " ");
            if (rep3 != null) builder.Append("-i " + rep3 + " ");
            if (Double.IsNaN(tauW)) builder.Append("-w " + tauW + " ");
            if (Double.IsNaN(tauS)) builder.Append("-s " + tauS + " ");
            if (Double.IsNaN(gamma)) builder.Append("-g " + gamma + " ");
            if (float.IsNaN(alpha)) builder.Append("-a " + alpha + " ");
            builder.Append("-c " + c + " ");
            builder.Append("-m " + m + " ");
            builder.Append("-r " + r + " ");
            return builder.ToString();
        }

        [Fact]
        public void ReadTauS()
        {
            // Arrange & Act
            var options = new CommandLineOptions();
            var po = options.Parse(GenerateShortNameArguments().Split(' '));

            // Assert
            Assert.True(po.TauS == _tauS);
        }
    }
}
