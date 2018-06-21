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
            this.sessionPath = sessionPath;
            this.includeBEDHeader = includeBEDHeader;
            _attributesToExport = new List<Attributes>(attributesToExport);
            this.Export_Chromosomewide_stats = Export_Chromosomewide_stats;
        }

        private readonly List<Attributes> _attributesToExport;
        public ReadOnlyCollection<Attributes> AttributesToExport { get { return _attributesToExport.AsReadOnly(); } }

        public bool Export_Chromosomewide_stats { private set; get; }
        public bool includeBEDHeader { private set; get; }
        public string sessionPath { private set; get; }
    }
}
