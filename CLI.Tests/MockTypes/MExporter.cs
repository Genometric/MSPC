using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.CLI.Interfaces;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;

namespace Genometric.MSPC.CLI.Tests.MockTypes
{
    public class MExporter : IExporter<Peak>
    {
        public void Export(
            Dictionary<uint, string> fileNames,
            Dictionary<uint, Result<Peak>> results,
            Dictionary<string, Dictionary<char, List<ProcessedPeak<Peak>>>> consensusPeaks,
            Options options)
        {
            throw new NotImplementedException();
        }
    }
}
