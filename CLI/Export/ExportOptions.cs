using Genometric.MSPC.Core.Model;
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
        public ReadOnlyCollection<Attributes> AttributesToExport
        {
            get { return _attributesToExport.AsReadOnly(); }
        }

        public bool IncludeHeader { private set; get; }
        public string Path { private set; get; }
    }
}
