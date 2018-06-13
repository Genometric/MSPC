// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Genometric.MSPC.CLI
{
    internal class Orchestrator
    {
        private readonly Config _options;
        private BackgroundWorker _analysisBGW { set; get; }
        internal MSPC<ChIPSeqPeak> _mspc { set; get; }
        internal Exporter<ChIPSeqPeak> exporter { set; get; }

        private List<BED<ChIPSeqPeak>> _samples { set; get; }
        internal ReadOnlyCollection<BED<ChIPSeqPeak>> samples { get { return _samples.AsReadOnly(); } }


        internal Orchestrator(Config options, IReadOnlyList<string> input)
        {
            _options = options;
            _mspc = new MSPC<ChIPSeqPeak>();
            _mspc.StatusChanged += _mspc_statusChanged;
            _samples = new List<BED<ChIPSeqPeak>>();
        }

        public void LoadSample(string fileName)
        {
            var bedParser = new BEDParser();
            _samples.Add(bedParser.Parse(fileName));
        }

        internal void Run()
        {
            foreach (var sample in _samples)
                _mspc.AddSample(sample.FileHashKey, sample);
            _mspc.RunAsync(_options);
            _mspc.done.WaitOne();
        }
        private void _mspc_statusChanged(object sender, ValueEventArgs e)
        {
            Console.WriteLine("[" + e.Value.Step + "/" + e.Value.StepCount + "] " + e.Value.Message);
        }

        internal void Export()
        {
            exporter = new Exporter<ChIPSeqPeak>();
            var options = new ExportOptions(
                sessionPath: Environment.CurrentDirectory + Path.DirectorySeparatorChar + "session_" +
                             DateTime.Now.Year +
                             DateTime.Now.Month +
                             DateTime.Now.Day +
                             DateTime.Now.Hour +
                             DateTime.Now.Minute +
                             DateTime.Now.Second,
                includeBEDHeader: true,
                Export_R_j__o_BED: true,
                Export_R_j__s_BED: true,
                Export_R_j__w_BED: true,
                Export_R_j__c_BED: true,
                Export_R_j__d_BED: true,
                Export_Chromosomewide_stats: false);

            exporter.Export(_samples.ToDictionary(x => x.FileHashKey, x => x.FileName), _mspc.GetResults(), _mspc.GetMergedReplicates(), options);
        }
    }
}
