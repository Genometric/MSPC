// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

//using CommandLine;
//using CommandLine.Text;
using System.Collections.Generic;

namespace Genometric.MSPC.CLI
{
    /*
    internal class CommandLineOptions
    {
        [OptionList('i', "input", Separator = '&', Required = true, HelpText = "Input samples to be processed in Browser Extensible Data (BED) Format")]
        public IList<string> inputFiles { set; get; }

        [Option('r', "replicate", Required = true, /*DefaultValue = "Biological", HelpText = "Sets the replicate type of samples. Possible values are: { Bio, Biological, Tec, Technical }")]
        public string replicateType { set; get; }

        [Option('s', "tauS", Required = true, /*DefaultValue = 1E-8, HelpText = "Sets stringency threshold. All peaks with p-values lower than this value are considered as stringent peaks")]
        public double tauS { set; get; }

        [Option('w', "tauW", Required = true, /*DefaultValue = 1E-4, HelpText = "Sets weak threshold. All peaks with p-values higher than this value are considered as weak peaks.")]
        public double tauW { set; get; }

        [Option('g', "gamma", Required = false, DefaultValue = -1, HelpText = "Sets combined stringency threshold. The peaks with their combined p-values satisfying this threshold will be confirmed.")]
        public double gamma { set; get; }

        [Option('a', "alpha", Required = false, DefaultValue = 0.05F, HelpText = "Sets false discovery rate of Benjamini–Hochberg step-up procedure")]
        public float alpha { set; get; }

        [Option('c', "C", Required = false, DefaultValue = (byte)1, HelpText = "Sets minimum number of required regions for a valid intersecting group.")]
        public byte C { set; get; }

        [Option('m', "multipleIntersections", Required = false, DefaultValue = "Lowest", HelpText = "Sets the default peak to be used when multiple peaks from one sample intesect with a given peak. Possible values are: { Lowest, Highest }")]
        public string M { set; get; }


        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }

*/
}
