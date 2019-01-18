// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Genometric.MSPC.CLI.Interfaces
{
    public interface IExporter<I>
        where I: IPeak
    {
        void Export(
            Dictionary<uint, string> fileNames,
            ReadOnlyDictionary<uint, Result<I>> results,
            ReadOnlyDictionary<string, List<ProcessedPeak<I>>> consensusPeaks,
            Options options);
    }
}
