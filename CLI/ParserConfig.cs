// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Newtonsoft.Json;
using System.IO;

namespace Genometric.MSPC.CLI
{
    internal class ParserConfig : BedColumns
    {
        public bool DropPeakIfInvalidValue { set; get; }
        public double DefaultValue { set; get; }
        public string PValueFormat { set; get; }

        public ParserConfig()
        {
            DropPeakIfInvalidValue = true;
            DefaultValue = 1E-8;
            PValueFormat = "minus1_Log10_pValue";
        }

        public ParserConfig ParseBed(string path)
        {
            string json = null;
            using (StreamReader r = new StreamReader(path))
                json = r.ReadToEnd();

            return JsonConvert.DeserializeObject<ParserConfig>(json);
        }
    }
}
