// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;

namespace Genometric.MSPC.CLI.Interfaces
{
    public interface IExporter<I>
        where I: IPeak
    {
        void Export(
            Dictionary<uint, string> fileNames,
            List<Bed<PPeak>> samples,
            Dictionary<string, List<PPeak>> consensusPeaks,
            Options options);
    }
}
