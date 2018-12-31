// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Core.Model;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Genometric.MSPC.CLI
{
    internal class CommandLineOptions
    {
        private readonly CommandLineApplication _cla;

        private readonly CommandOption _cInput = new CommandOption("-i | --input <value>", CommandOptionType.MultipleValue)
        {
            Description = "Input samples to be processed in Browser Extensible Data (BED) Format."
        };

        private readonly CommandOption _cParser = new CommandOption("-p | --parser <value>", CommandOptionType.MultipleValue)
        {
            Description = "Sets the path to the parser configuration file in JSON."
        };

        private readonly CommandOption _cReplicate = new CommandOption("-r | --replicate <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets the replicate type of samples. Possible values are: { Bio, Biological, Tec, Technical }"
        };

        private readonly CommandOption _cTauS = new CommandOption("-s | --tauS <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets stringency threshold. All peaks with p-values lower than this value are considered as stringent peaks."
        };

        private readonly CommandOption _cTauW = new CommandOption("-w | --tauW <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets weak threshold. All peaks with p-values higher than this value are considered as weak peaks."
        };

        private readonly CommandOption _cGamma = new CommandOption("-g | --gamma <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets combined stringency threshold. The peaks with their combined p-values satisfying this threshold will be confirmed."
        };

        private readonly CommandOption _cAlpha = new CommandOption("-a | --alpha <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets false discovery rate of Benjamini–Hochberg step-up procedure."
        };

        private readonly CommandOption _cC = new CommandOption("-c <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets minimum number of overlapping peaks before combining p-values."
        };

        private readonly CommandOption _cM = new CommandOption("-m | --multipleIntersections <value>", CommandOptionType.SingleValue)
        {
            Description = "When multiple peaks from a sample overlap with a given peak, " +
                "this argument defines which of the peaks to be considered: the one with lowest p-value, or " +
                "the one with highest p-value? Possible values are: { Lowest, Highest }"
        };

        private ReplicateType _vreplicate;
        private double _vtauS = 1E-8;
        private double _vtauW = 1E-4;
        private double _vgamma = -1;
        private float _valpha = 0.05F;
        private int _vc = 1;
        private MultipleIntersections _vm = MultipleIntersections.UseLowestPValue;


        public Config Options { private set; get; }

        private List<string> _inputFiles;
        public IReadOnlyList<string> Input { get { return _inputFiles.AsReadOnly(); } }

        /// <summary>
        /// Gets the path of a parser configuration file in JSON.
        /// </summary>
        public string ParserConfig { get { return _cParser.Value(); } }

        public CommandLineOptions()
        {
            _cla = new CommandLineApplication();
            _cla.HelpOption("-? | -h | --help");
            _cla.Options.Add(_cInput);
            _cla.Options.Add(_cParser);
            _cla.Options.Add(_cReplicate);
            _cla.Options.Add(_cTauS);
            _cla.Options.Add(_cTauW);
            _cla.Options.Add(_cGamma);
            _cla.Options.Add(_cAlpha);
            _cla.Options.Add(_cC);
            _cla.Options.Add(_cM);
            Func<int> assertArguments = AssertArguments;
            _cla.OnExecute(assertArguments);
        }

        private int AssertArguments()
        {
            AssertRequiredArgsAreGiven();
            ReadInputFiles();
            AssertGivenValuesAreValid();
            return 0;
        }

        private void AssertRequiredArgsAreGiven()
        {
            var missingArgs = new List<string>();
            if (!_cInput.HasValue()) missingArgs.Add(_cInput.ShortName + "|" + _cInput.LongName);
            if (!_cReplicate.HasValue()) missingArgs.Add(_cReplicate.ShortName + "|" + _cReplicate.LongName);
            if (!_cTauS.HasValue()) missingArgs.Add(_cTauS.ShortName + "|" + _cTauS.LongName);
            if (!_cTauW.HasValue()) missingArgs.Add(_cTauW.ShortName + "|" + _cTauW.LongName);

            if (missingArgs.Count > 0)
            {
                var msgBuilder = new StringBuilder("The following required arguments are missing: ");
                foreach (var item in missingArgs)
                    msgBuilder.Append(item + "; ");
                throw new ArgumentException(msgBuilder.ToString());
            }
        }

        private void ReadInputFiles()
        {
            _inputFiles = new List<string>();
            foreach (var input in _cInput.Values)
                if (input.Contains("*") || input.Contains("?"))
                    foreach (var file in Directory.GetFiles(Path.GetDirectoryName(input), Path.GetFileName(input)))
                        _inputFiles.Add(file);
                else
                    _inputFiles.Add(input);
        }

        private void AssertGivenValuesAreValid()
        {
            switch (_cReplicate.Value().ToLower())
            {
                case "bio":
                case "biological":
                    _vreplicate = ReplicateType.Biological;
                    break;

                case "tec":
                case "technical":
                    _vreplicate = ReplicateType.Technical;
                    break;

                default:
                    throw new ArgumentException("Invalid value given for the `" + _cReplicate.LongName + "` argument.");
            }

            if (!double.TryParse(_cTauS.Value(), out _vtauS))
                throw new ArgumentException("Invalid value given for the `" + _cTauS.LongName + "` argument.");

            if (!double.TryParse(_cTauW.Value(), out _vtauW))
                throw new ArgumentException("Invalid value given for the `" + _cTauW.LongName + "` argument.");

            if (_cGamma.HasValue() && !double.TryParse(_cGamma.Value(), out _vgamma))
                throw new ArgumentException("Invalid value given for the `" + _cGamma.LongName + "` argument.");
            _vgamma = _vgamma == -1 ? _vtauS : _vgamma;

            if (_cAlpha.HasValue() && !float.TryParse(_cAlpha.Value(), out _valpha))
                throw new ArgumentException("Invalid value given for the `" + _cAlpha.LongName + "` argument.");

            if (_cC.HasValue())
            {
                if (_cC.Value().Contains("%"))
                {
                    if (int.TryParse(_cC.Value().Replace("%", ""), out int percentage))
                        _vc = (_inputFiles.Count * percentage) / 100;
                    else
                        throw new ArgumentException("Invalid value given for the `" + _cC.ShortName + "` argument.");
                }
                else if (!int.TryParse(_cC.Value(), out _vc))
                    throw new ArgumentException("Invalid value given for the `" + _cC.ShortName + "` argument.");

                if (_vc < 1)
                    _vc = 1;
            }

            if (_cM.HasValue())
                switch (_cM.Value().ToLower())
                {
                    case "lowest":
                        _vm = MultipleIntersections.UseLowestPValue;
                        break;

                    case "highest":
                        _vm = MultipleIntersections.UseHighestPValue;
                        break;

                    default:
                        throw new ArgumentException("Invalid value given for the `" + _cM.LongName + "` argument.");
                }
        }

        private string[] ParseExpandedInput(string[] args)
        {
            var rtv = new List<string>();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-" + _cInput.ShortName || args[i] == "--" + _cInput.LongName)
                {
                    var inputs = new List<string>();
                    for (i++; i < args.Length; i++)
                    {
                        if (_cla.Options.Any(x => args[i] == "-" + x.ShortName || args[i] == "--" + x.LongName))
                        {
                            i--;
                            break;
                        }
                        else
                            inputs.Add(args[i]);
                    }

                    foreach(var input in inputs)
                    {
                        rtv.Add("-" + _cInput.ShortName);
                        rtv.Add(input);
                    }
                }
                else
                {
                    rtv.Add(args[i]);
                }
            }

            return rtv.ToArray();
        }

        public Config Parse(string[] args)
        {
            var parsedInput = ParseExpandedInput(args);
            _cla.Execute(parsedInput);
            Options = new Config(_vreplicate, _vtauW, _vtauS, _vgamma, _vc, _valpha, _vm);
            return Options;
        }
    }
}
