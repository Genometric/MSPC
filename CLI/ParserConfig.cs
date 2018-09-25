// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers.Model;
using Newtonsoft.Json;
using System.IO;

namespace Genometric.MSPC.CLI
{
    internal class ParserConfig
    {
        public BedColumns ParseBed(string path)
        {
            string json = null;
            using (StreamReader r = new StreamReader(path))
                json = r.ReadToEnd();

            return JsonConvert.DeserializeObject<BedColumns>(json);
        }
    }
}
