// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Genometric.MSPC.CLI
{
    internal class ParserConfig : BedColumns, IEquatable<ParserConfig>
    {
        public bool DropPeakIfInvalidValue { set; get; }
        public double DefaultValue { set; get; }
        public PValueFormats PValueFormat { set; get; }

        public string Culture
        {
            set
            {
                if (!CultureInfo.GetCultures(CultureTypes.AllCultures).Any(x => x.Name == value))
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid culture info.");
                _culture = value;
            }
            get
            {
                return _culture;
            }
        }

        private string _culture;


        public ParserConfig()
        {
            DropPeakIfInvalidValue = true;
            DefaultValue = 1E-8;
            PValueFormat = PValueFormats.minus1_Log10_pValue;
            Culture = CultureInfo.CurrentCulture.Name;
        }

        public static ParserConfig LoadFromJSON(string path)
        {
            string json = null;
            using (StreamReader r = new StreamReader(path))
                json = r.ReadToEnd();

            return JsonConvert.DeserializeObject<ParserConfig>(json);
        }

        public bool Equals(ParserConfig other)
        {
            if (other == null) return false;
            return
                Chr == other.Chr &&
                Left == other.Left &&
                Right == other.Right &&
                Name == other.Name &&
                Summit == other.Summit &&
                Strand == other.Strand &&
                Value == other.Value &&
                PValueFormat == other.PValueFormat &&
                DefaultValue == other.DefaultValue &&
                DropPeakIfInvalidValue == other.DropPeakIfInvalidValue;
        }
    }
}
