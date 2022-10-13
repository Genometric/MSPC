using Genometric.MSPC.Core.Model;
using System.Collections.Generic;

namespace Genometric.MSPC.CLI.CommandLineInterface
{
    internal class CliConfig : Config
    {
        public IReadOnlyList<string> InputFiles { get; }

        public string OutputPath { get; }

        public int? DegreeOfParallelism { get; }

        public bool ExcludeHeader { get; }

        public string ParserConfigFilename { get; }

        public CliConfig(
            IReadOnlyList<string> inputFiles,
            string outputPath,
            ReplicateType replicateType,
            double tauW, double tauS, double gamma,
            int c, float alpha,
            int? degreeOfParallelism,
            bool excludeHeader,
            string parserConfigFilename,
            MultipleIntersections multipleIntersections) : base(
                replicateType: replicateType, 
                tauW: tauW, tauS: tauS, gamma: gamma, 
                c: c, alpha: alpha, 
                multipleIntersections: multipleIntersections)
        {
            InputFiles = inputFiles;
            OutputPath = outputPath;
            DegreeOfParallelism = degreeOfParallelism;
            ExcludeHeader = excludeHeader;
            ParserConfigFilename = parserConfigFilename;
        }
    }
}
