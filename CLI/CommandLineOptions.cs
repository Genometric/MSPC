// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Model;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Genometric.MSPC.CLI.Tests")]
namespace Genometric.MSPC.CLI
{
    internal class CommandLineOptions
    {
        private readonly CommandLineApplication _cla;

        private static CommandOption _input = new CommandOption("-i | --input <value>", CommandOptionType.MultipleValue)
        {
            Description = "Input samples to be processed in Browser Extensible Data (BED) Format."
        };

        private static CommandOption _replicate = new CommandOption("-r | --replicate <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets the replicate type of samples. Possible values are: { Bio, Biological, Tec, Technical }"
        };

        private static CommandOption _tauS = new CommandOption("-s | --tauS <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets stringency threshold. All peaks with p-values lower than this value are considered as stringent peaks."
        };

        private static CommandOption _tauW = new CommandOption("-w | --tauW <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets weak threshold. All peaks with p-values higher than this value are considered as weak peaks."
        };

        private static CommandOption _gamma = new CommandOption("-g | --gamma <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets combined stringency threshold. The peaks with their combined p-values satisfying this threshold will be confirmed."
        };

        private static CommandOption _alpha = new CommandOption("-a | --alpha <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets false discovery rate of Benjamini–Hochberg step-up procedure."
        };

        private static CommandOption _c = new CommandOption("-c <value>", CommandOptionType.SingleValue)
        {
            Description = "Sets minimum number of overlapping peaks before combining p-values."
        };

        private static CommandOption _m = new CommandOption("-m | --multipleIntersections <value>", CommandOptionType.SingleValue)
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
        private byte _vc = 1;
        private MultipleIntersections _vm = MultipleIntersections.UseLowestPValue;


        public Config Options { private set; get; }

        public CommandLineOptions()
        {
            _cla = new CommandLineApplication();
            _cla.Options.Add(_replicate);
            _cla.Options.Add(_tauS);
            _cla.Options.Add(_tauW);
            _cla.Options.Add(_gamma);
            _cla.Options.Add(_alpha);
            _cla.Options.Add(_c);
            _cla.Options.Add(_m);
            Func<int> assertArguments = AssertArguments;
            _cla.OnExecute(assertArguments);
        }

        private int AssertArguments()
        {
            var missingArgs = new List<string>();
            if (!_input.HasValue()) missingArgs.Add(_input.ShortName + "|" + _input.LongName);
            if (!_replicate.HasValue()) missingArgs.Add(_replicate.ShortName + "|" + _replicate.LongName);
            if (!_tauS.HasValue()) missingArgs.Add(_tauS.ShortName + "|" + _tauS.LongName);
            if (!_tauW.HasValue()) missingArgs.Add(_tauW.ShortName + "|" + _tauW.LongName);

            if (missingArgs.Count > 0)
            {
                var msgBuilder = new StringBuilder("The following required arguments are missing: ");
                foreach (var item in missingArgs)
                    msgBuilder.Append(item + "; ");
                msgBuilder.Append(".");
                throw new ArgumentException(msgBuilder.ToString());
            }

            switch (_replicate.Value().ToLower())
            {
                case "bio":
                case "biological":
                    _vreplicate = ReplicateType.Biological;
                    break;

                case "tec":
                case "Technical":
                    _vreplicate = ReplicateType.Technical;
                    break;

                default:
                    ThrowInvalidException(_replicate.LongName);
                    break;
            }

            if (!double.TryParse(_tauS.Value(), out _vtauS))
                ThrowInvalidException(_tauS.LongName);

            if (!double.TryParse(_tauW.Value(), out _vtauW))
                ThrowInvalidException(_tauW.LongName);

            if (!double.TryParse(_gamma.Value(), out _vgamma))
                ThrowInvalidException(_gamma.LongName);

            if (!float.TryParse(_alpha.Value(), out _valpha))
                ThrowInvalidException(_alpha.LongName);

            if (!byte.TryParse(_c.Value(), out _vc))
                ThrowInvalidException(_c.LongName);

            switch (_m.Value().ToLower())
            {
                case "lowest":
                    _vm = MultipleIntersections.UseLowestPValue;
                    break;

                case "highest":
                    _vm = MultipleIntersections.UseHighestPValue;
                    break;

                default:
                    ThrowInvalidException(_m.LongName);
                    break;
            }

            return 0;
        }

        private void ThrowInvalidException(string commandOption)
        {
            throw new ArgumentException("Invalid value given for the " + commandOption + " argument.");
        }

        public Config Parse(string[] args)
        {
            _cla.Execute(args);
            Options = new Config(_vreplicate, _vtauW, _vtauS, _vgamma, _vc, _valpha, _vm);
            return Options;
        }
    }
}
