using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Genometric.MSPC.CLI.CommandLineInterface
{
    internal class Cli
    {
        private readonly Parser _parser;

        public Cli(Action<CliConfig> handler)
        {
            var inputsOption = new Option<List<string>>(
                name: "--input",
                description: "Input samples to be processed in " +
                "Browser Extensible Data (BED) Format.",
                parseArgument: x =>
                {
                    if (!x.Tokens.Any())
                        x.ErrorMessage = "Required";

                    var filenames = new List<string>();
                    foreach (var token in x.Tokens)
                    {
                        var filename = token.Value;
                        if (filename.Contains('*') || filename.Contains('?'))
                        {
                            foreach (var file in Directory.GetFiles(
                                Path.GetDirectoryName(filename),
                                Path.GetFileName(filename)))
                            {
                                filenames.Add(file);
                            }
                        }
                        else
                        {
                            if (File.Exists(filename))
                                filenames.Add(filename);
                        }
                    }

                    return filenames;
                });
            inputsOption.AddAlias("-i");
            inputsOption.IsRequired = true;
            inputsOption.AllowMultipleArgumentsPerToken = true;

            var inputsDirOption = new Option<List<string>>(
                name: "--folder-input",
                description: "Sets a path to a folder that all " +
                "its containing files in BED format are considered " +
                "as inputs.",
                parseArgument: x =>
                {
                    if (!x.Tokens.Any())
                        x.ErrorMessage = "Required";

                    var filenames = new List<string>();
                    foreach (var token in x.Tokens)
                    {
                        var dirName = token.Value;
                        var files = Directory.GetFiles(
                            Path.GetDirectoryName(dirName),
                            Path.GetFileName(dirName));
                        foreach (var file in files)
                            filenames.Add(file);
                    }

                    return filenames;
                });
            inputsDirOption.AddAlias("-f");

            var outputOption = new Option<string>(
                name: "--output",
                description: "Sets a path where analysis results should be persisted.");
            outputOption.AddAlias("-o");

            var parserFilenameOption = new Option<string>(
                name: "--parser",
                description: "Sets the path to the parser configuration file in JSON.");
            parserFilenameOption.AddAlias("-p");

            var repTypeOption = new Option<ReplicateType>(
                name: "--replicate",
                description: "Sets the replicate type of samples. " +
                "Possible values are: {Bio, Biological, Tec, Technical}.",
                parseArgument: x =>
                {
                    if (!x.Tokens.Any())
                        x.ErrorMessage = "Required";

                    switch (x.Tokens.Single().Value.ToLower())
                    {
                        case "bio":
                        case "biological":
                            return ReplicateType.Biological;

                        case "tec":
                        case "technical":
                            return ReplicateType.Technical;

                        default:
                            x.ErrorMessage = "Invalid value";
                            return 0;
                    }
                });
            repTypeOption.AddAlias("-r");
            repTypeOption.IsRequired = true;

            var sTOption = new Option<double>(
                name: "--tauS",
                description: "Sets stringency threshold. All peaks " +
                "with p-values lower than this value are considered as " +
                "stringent peaks.");
            sTOption.AddAlias("-s");
            sTOption.IsRequired = true;
            sTOption.AddValidator(x =>
            {
                var value = x.GetValueForOption(sTOption);
                if (value < 0 || value > 1)
                    x.ErrorMessage = "Invalid probability";
            });

            var wTOption = new Option<double>(
                name: "--tauW",
                description: "Sets weak threshold. All peaks with p-values " +
                "higher than this value are considered as weak peaks.");
            wTOption.AddAlias("-w");
            wTOption.IsRequired = true;
            wTOption.AddValidator(x =>
            {
                var value = x.GetValueForOption(wTOption);
                if (value < 0 || value > 1)
                    x.ErrorMessage = "Invalid probability";
            });

            var gTOption = new Option<double>(
                name: "--gamma",
                description: "Sets combined stringency threshold. " +
                "The peaks with their combined p-values satisfying " +
                "this threshold will be confirmed.");
            gTOption.AddAlias("-g");
            gTOption.AddValidator(x =>
            {
                var value = x.GetValueForOption(gTOption);
                if (value < 0 || value > 1)
                    x.ErrorMessage = "Invalid probability";
            });

            var alphaOption = new Option<float>(
                name: "--alpha",
                description: "Sets false discovery rate " +
                "of Benjamini–Hochberg step-up procedure.");
            alphaOption.AddAlias("-a");
            alphaOption.AddValidator(x =>
            {
                var value = x.GetValueForOption(alphaOption);
                if (value < 0 || value > 1)
                    x.ErrorMessage = "Invalid value";
            });

            var cOption = new Option<string>(
                name: "-c",
                description: "Sets minimum number of overlapping " +
                "peaks before combining p-values.")
            {
                IsRequired = false
            };
            cOption.AddValidator(x =>
            {
                var token = x.Token;
                if (token == null)
                    return;
                var value = token.Value;

                // TODO: this can be improved using regex.
                // Currently values such as `1%2` will be
                // incorrectly parsed as valid values.
                if (!int.TryParse(value.Replace("%", ""), out int c) || c < 1)
                    x.ErrorMessage = "Invalid value";
            });

            var mOption = new Option<MultipleIntersections>(
                name: "--multipleIntersections",
                description: "When multiple peaks from a sample overlap " +
                "with a given peak, this argument defines which of the " +
                "peaks to be considered: the one with lowest p-value, or " +
                "the one with highest p-value? Possible values are: " +
                "{ Lowest, Highest }",
                parseArgument: x =>
                {
                    if (!x.Tokens.Any())
                        x.ErrorMessage = "Required";

                    switch (x.Tokens.Single().Value.ToLower())
                    {
                        case "lowest":
                            return MultipleIntersections.UseLowestPValue;

                        case "highest":
                            return MultipleIntersections.UseHighestPValue;

                        default:
                            x.ErrorMessage = "Invalid value";
                            return 0;
                    }
                });
            mOption.AddAlias("-m");

            var dpOption = new Option<int?>(
                name: "--degreeOfParallelism",
                description: "Set the degree of parallelism.");
            dpOption.AddAlias("-d");

            var excludeHeaderOption = new Option<bool>(
                name: "--excludeHeader",
                description: "If provided, MSPC will not add a header line to its output.");


            // TODO: compare tauS and tauW


            var rootCmd = new RootCommand(
                "Using combined evidence from replicates to evaluate " +
                "ChIP-seq and single-cell peaks." +
                $"{Environment.NewLine}Documentation:\thttps://genometric.github.io/MSPC/" +
                $"{Environment.NewLine}Source Code:\thttps://github.com/Genometric/MSPC" +
                $"{Environment.NewLine}Publications:\thttps://genometric.github.io/MSPC/publications" +
                $"{Environment.NewLine}");
            rootCmd.AddOption(inputsOption);
            rootCmd.AddOption(inputsDirOption);
            rootCmd.AddOption(outputOption);
            rootCmd.AddOption(parserFilenameOption);
            rootCmd.AddOption(repTypeOption);
            rootCmd.AddOption(sTOption);
            rootCmd.AddOption(wTOption);
            rootCmd.AddOption(gTOption);
            rootCmd.AddOption(alphaOption);
            rootCmd.AddOption(cOption);
            rootCmd.AddOption(mOption);
            rootCmd.AddOption(dpOption);
            rootCmd.AddOption(excludeHeaderOption);

            rootCmd.SetHandler((options) =>
            {
                handler(options);
            },
            new OptionsBinder(
                inputsOption, inputsDirOption,
                outputPathOption: outputOption,
                parserConfigFilenameOption: parserFilenameOption,
                replicateTypeOption: repTypeOption,
                sTOption: sTOption,
                wTOption: wTOption,
                gTOption: gTOption,
                alphaOption: alphaOption,
                cOption: cOption,
                mOption: mOption,
                dbOption: dpOption,
                excludeHeaderOption: excludeHeaderOption));

            rootCmd.AddValidator(x =>
            {
                var tauW = x.GetValueForOption(wTOption);
                var tauS = x.GetValueForOption(sTOption);

                if (tauW <= tauS)
                    x.ErrorMessage = "Stringency threshold (TauS) " +
                    "should be lower than weak threshold (TauW).";
            });

            _parser = new CommandLineBuilder(rootCmd)
                //.UseDefaults() // Do NOT add this since it will cause issues with handling exceptions.
                // See this issue: 
                // https://github.com/dotnet/command-line-api/issues/796
                .UseExceptionHandler((e, context) =>
                {
                    Console.Error.WriteLine(e.Message);
                }, 1)
                .UseHelp()
                .UseEnvironmentVariableDirective()
                .UseParseDirective()
                .UseSuggestDirective()
                .RegisterWithDotnetSuggest()
                .UseTypoCorrections()
                .UseParseErrorReporting()
                .CancelOnProcessTermination()
                .Build();
        }

        public async Task<int> InvokeAsync(string[] args)
        {
            return await _parser.InvokeAsync(args);
        }
    }
}
