// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Genometric.MSPC.CLI.Exporter
{
    public class Options
    {
        public Options(
            string path,
            bool includeHeader,
            List<Attributes> attributesToExport)
        {
            Path = path;
            IncludeHeader = includeHeader;
            _attributesToExport = new List<Attributes>(attributesToExport);
        }

        private readonly List<Attributes> _attributesToExport;
        public ReadOnlyCollection<Attributes> AttributesToExport { get { return _attributesToExport.AsReadOnly(); } }

        public bool IncludeHeader { private set; get; }
        public string Path { private set; get; }
    }
}
