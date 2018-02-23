using Polimi.DEIB.VahidJalili.MSPC.Exporter;
using Polimi.DEIB.VahidJalili.IGenomics;
using Polimi.DEIB.VahidJalili.MSPC.Analyzer;
using Polimi.DEIB.VahidJalili.MSPC.Warehouse;
using System;
using System.ComponentModel;
using System.IO;

namespace Polimi.DEIB.VahidJalili.MSPC.CLI
{
    internal class Process<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        private BackgroundWorker _analysisBGW { set; get; }
        internal Analyzer<Peak, Metadata> analyzer { set; get; }
        internal Exporter<Peak, Metadata> exporter { set; get; }
        internal string replicateType { set; get; }
        internal double tauS { set; get; }
        internal double tauW { set; get; }
        internal double gamma { set; get; }
        internal byte C { set; get; }
        internal float alpha { set; get; }

        internal Process()
        {
            analyzer = new Analyzer<Peak, Metadata>();
        }

        internal void Run()
        {
            var options = new AnalysisOptions();
            options.tauS = tauS;
            options.tauW = tauW;
            options.gamma = gamma;
            options.alpha = alpha;
            options.C = C;

            switch (replicateType.ToLower())
            {
                case "tec":
                case "technical":
                    options.replicateType = ReplicateType.Technical;
                    break;

                case "bio":
                case "biological":
                    options.replicateType = ReplicateType.Biological;
                    break;
            }

            analyzer.Run(options);
        }

        internal void SaveSession(string sessionTitle, DateTime startTime, DateTime endTime)
        {
            Sessions<Peak, Metadata>.Data.Add(sessionTitle, new Session<Peak, Metadata>()
            {
                index = Sessions<Peak, Metadata>.Data.Count,
                samples = analyzer.GetSamples(),
                startTime = startTime,
                endTime = endTime,
                elapsedTime = endTime.Subtract(startTime).ToString(),
                options = new AnalysisOptions(),
                analysisResults = analyzer.GetResults(),
                mergedReplicates = analyzer.GetMergedReplicates(),
                status = "Completed."
            });
        }

        internal void Export(string sessionTitle)
        {
            exporter = new Exporter<Peak, Metadata>(Sessions<Peak, Metadata>.Data[sessionTitle]);
            var options = new ExportOptions(
                sessionPath: Environment.CurrentDirectory + Path.DirectorySeparatorChar + sessionTitle,
                includeBEDHeader: true,
                Export_R_j__o_BED: true,
                Export_R_j__s_BED: true,
                Export_R_j__w_BED: true,
                Export_R_j__c_BED: true,
                Export_R_j__d_BED: true,
                Export_Chromosomewide_stats: false);
            exporter.Export(options);
        }

        internal string GetSessionTitle()
        {
            return Sessions<Peak, Metadata>.GetSessionTitle();
        }
    }
}
