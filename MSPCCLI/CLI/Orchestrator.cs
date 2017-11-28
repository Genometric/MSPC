using Genometric.IGenomics;
using System;
using System.ComponentModel;
using System.IO;
using Genometric.GIFP;
using System.Collections.Generic;
using Genometric.MSPC.Core;
using Genometric.MSPC.CLI.Exporter;
using Genometric.MSPC.Model;
using System.Collections.ObjectModel;
using Genometric.MSPC.Core.Model;
using System.Linq;

namespace Genometric.MSPC.CLI
{
    internal class Orchestrator<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        private BackgroundWorker _analysisBGW { set; get; }
        internal MSPC<Peak, Metadata> _mspc { set; get; }
        internal Exporter<Peak, Metadata> exporter { set; get; }
        internal string replicateType { set; get; }
        internal double tauS { set; get; }
        internal double tauW { set; get; }
        internal double gamma { set; get; }
        internal byte C { set; get; }
        internal float alpha { set; get; }

        private List<ParsedChIPseqPeaks<int, Peak, Metadata>> _samples { set; get; }
        internal ReadOnlyCollection<ParsedChIPseqPeaks<int, Peak, Metadata>> samples { get { return _samples.AsReadOnly(); } }

        internal Orchestrator()
        {
            _mspc = new MSPC<Peak, Metadata>();
            _mspc.StatusChanged += _mspc_statusChanged;
            _samples = new List<ParsedChIPseqPeaks<int, Peak, Metadata>>();
        }

        public void LoadSample(string fileName)
        {
            BEDParser<Peak, Metadata> bedParser =
                new BEDParser<Peak, Metadata>(
                    source: fileName,
                    species: Genomes.HomoSapiens,
                    assembly: Assemblies.hg19,
                    readOnlyValidChrs: false,
                    startOffset: 0,
                    chrColumn: 0,
                    leftEndColumn: 1,
                    rightEndColumn: 2,
                    nameColumn: 3,
                    summitColumn: -1,
                    valueColumn: 4,
                    strandColumn: -1,
                    defaultValue: 0.1,
                    pValueFormat: pValueFormat.minus1_Log10_pValue,
                    dropPeakIfInvalidValue: true,
                    hashFunction: HashFunction.One_at_a_Time);

            _samples.Add(bedParser.Parse());
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
                _mspc.AddSample(sample.fileHashKey, sample.intervals);
            _mspc.RunAsync(config);
            _mspc.done.WaitOne();
        }
        private void _mspc_statusChanged(object sender, ValueEventArgs e)
        {
            Console.WriteLine("[" + e.Value.Step + "/" + e.Value.StepCount + "] " + e.Value.Message);
        }

        internal void Export()
        {
            exporter = new Exporter<Peak, Metadata>();
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

            exporter.Export(_samples.ToDictionary(x => x.fileHashKey, x => x.fileName), _mspc.GetResults(), _mspc.GetMergedReplicates(), options);
        }
    }
}
