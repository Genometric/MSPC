// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Genometric.MSPC.CLI
{
    internal class Orchestrator
    {
        private readonly Config _options;
        private readonly MSPC<ChIPSeqPeak> _mspc;
        private readonly List<BED<ChIPSeqPeak>> _samples;

        internal Orchestrator(Config options, IReadOnlyList<string> input)
        {
            _options = options;
            _mspc = new MSPC<ChIPSeqPeak>();
            _mspc.StatusChanged += _mspc_statusChanged;
            _samples = new List<BED<ChIPSeqPeak>>();
        }

        public BED<ChIPSeqPeak> LoadSample(string fileName)
        {
            var bedParser = new BEDParser();
            var parsedSample = bedParser.Parse(fileName);
            _samples.Add(parsedSample);
            return parsedSample;
        }

        internal void Run()
        {
            foreach (var sample in _samples)
                _mspc.AddSample(sample.FileHashKey, sample);
            _mspc.RunAsync(_options);
            _mspc.Done.WaitOne();
        }
        private void _mspc_statusChanged(object sender, ValueEventArgs e)
        {
            Console.WriteLine("[" + e.Value.Step + "/" + e.Value.StepCount + "] " + e.Value.Message);
        }

        internal void Export()
        {
            var a2E = new List<Attributes>();
            foreach (var att in Enum.GetValues(typeof(Attributes)).Cast<Attributes>())
                a2E.Add(att);

            var exporter = new Exporter<ChIPSeqPeak>();
            var options = new Options(
                path: Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Session_" +
                      DateTime.Now.ToString("yyyyMMdd_HHmmssfff", CultureInfo.InvariantCulture),
                includeHeader: true,
                attributesToExport: a2E);

            exporter.Export(_samples.ToDictionary(x => x.FileHashKey, x => x.FileName), _mspc.GetResults(), _mspc.GetMergedReplicates(), options);
        }
    }
}
