// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Genometric.MSPC.CLI.Exporter
{
    public class ExportOptions
    {
        public ExportOptions(
            string sessionPath,
            bool includeBEDHeader,
            List<Attributes> attributesToExport,
            bool Export_Chromosomewide_stats)
        {
            SessionPath = sessionPath;
            IncludeBEDHeader = includeBEDHeader;
            ExportChromosomewideStats = Export_Chromosomewide_stats;
            _attributesToExport = new List<Attributes>(attributesToExport);
        }

        private readonly List<Attributes> _attributesToExport;
        public ReadOnlyCollection<Attributes> AttributesToExport { get { return _attributesToExport.AsReadOnly(); } }

        public bool ExportChromosomewideStats { private set; get; }
        public bool IncludeBEDHeader { private set; get; }
        public string SessionPath { private set; get; }
    }
}
