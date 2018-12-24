// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.Core;
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
        private readonly Mspc _mspc;
        private readonly List<Bed<Peak>> _samples;

        internal Orchestrator(Config options)
        {
            _options = options;
            _mspc = new Mspc();
            _mspc.StatusChanged += _mspc_statusChanged;
            _samples = new List<Bed<Peak>>();
        }

        public Bed<Peak> LoadSample(string fileName, BedColumns columns)
        {
            var bedParser = new BedParser(columns)
            {
                PValueFormat = PValueFormats.minus1_Log10_pValue,
            };
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

            var exporter = new Exporter<Peak>();
            var options = new Options(
                path: Environment.CurrentDirectory + Path.DirectorySeparatorChar + "session_" +
                      DateTime.Now.ToString("yyyyMMdd_HHmmssfff", CultureInfo.InvariantCulture),
                includeHeader: true,
                attributesToExport: a2E);

            exporter.Export(
                _samples.ToDictionary(x => x.FileHashKey, x => x.FileName),
                _mspc.GetResults(), _mspc.GetConsensusPeaks(), options);
        }
    }
}
