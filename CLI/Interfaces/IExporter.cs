using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;

namespace Genometric.MSPC.CLI.Interfaces
{
    public interface IExporter<I>
        where I : IPeak
    {
        void Export(
            Dictionary<uint, string> fileNames,
            Dictionary<uint, Result<I>> results,
            Dictionary<string, Dictionary<char, List<ProcessedPeak<I>>>> consensusPeaks,
            Options options);
    }
}
