
using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using Genometric.MSPC.Core;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.Model;
using System.Collections.ObjectModel;
using Genometric.MSPC.Core.Model;
using System.Linq;
using System.Threading;
using Genometric.GeUtilities.IGenomics;
using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;

namespace Genometric.MSPC.CLI
{
    internal class Orchestrator
    {
        private BackgroundWorker _analysisBGW { set; get; }
        internal MSPC<ChIPSeqPeak> _mspc { set; get; }
        internal Exporter<ChIPSeqPeak> exporter { set; get; }
        internal string replicateType { set; get; }
        internal double tauS { set; get; }
        internal double tauW { set; get; }
        internal double gamma { set; get; }
        internal byte C { set; get; }
        internal float alpha { set; get; }

        private List<BED<ChIPSeqPeak>> _samples { set; get; }
        internal ReadOnlyCollection<BED<ChIPSeqPeak>> samples { get { return _samples.AsReadOnly(); } }

        internal Orchestrator()
        {
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
            var config = new Config(
                replicateType: (replicateType.ToLower() == "tec" || replicateType.ToLower() == "technical") ? ReplicateType.Technical : ReplicateType.Biological,
                tauW: tauW,
                tauS: tauS,
                gamma: gamma,
                C: C,
                alpha: alpha,
                multipleIntersections: MultipleIntersections.UseLowestPValue);


            foreach (var sample in _samples)
                _mspc.AddSample(sample.FileHashKey, sample);
            _mspc.RunAsync(config);
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
