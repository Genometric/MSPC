// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

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
