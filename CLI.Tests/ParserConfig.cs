// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class ParserConfig
    {
        private bool Equal(BedColumns obj1, BedColumns obj2)
        {
            return
                obj1.Chr == obj2.Chr &&
                obj1.Left == obj2.Left &&
                obj1.Right == obj2.Right &&
                obj1.Name == obj2.Name &&
                obj1.Strand == obj2.Strand &&
                obj1.Summit == obj2.Summit;
        }

        [Fact]
        public void ReadParserConfig()
        {
            // Arrange
            var cols = new BedColumns();
            var path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "MSPCTests_" + new Random().NextDouble().ToString();
            var obj = JsonConvert.SerializeObject(cols);
            using (StreamWriter w = new StreamWriter(path))
                w.WriteLine(obj);

            // Act
            var parsedCols = new CLI.ParserConfig().ParseBed(path);
            File.Delete(path);

            Assert.True(Equal(parsedCols, cols));
        }
    }
}
