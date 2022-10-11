using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
//using System.CommandLine.Help;
//using Spectre.Console;

namespace Genometric.MSPC.CLI.CommandLineInterface
{
    internal class Cli
    {
        private readonly IConsole _console;
        private readonly Parser _parser;

        public Cli(
            IConsole console,
            Action<CliConfig> handler,
            Action<Exception, InvocationContext> exceptionHandler)
        {
            _console = console;

            var inputsOption = new Option<List<string>>(
                name: "--input",
                description: "Input samples to be processed in " +
                "Browser Extensible Data (BED) Format.",
                parseArgument: x =>
                {
                    if (!x.Tokens.Any())
                        x.ErrorMessage = "Required";

                    var filenames = new List<string>();
                    var missingFiles = new List<string>();
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
                            else
                                missingFiles.Add(filename);
                        }
                    }

                    if (missingFiles.Count > 0)
                    {
                        var errMsgbuilder = new StringBuilder();
                        errMsgbuilder.AppendLine("The following files are missing.");
                        foreach (var file in missingFiles)
                            errMsgbuilder.AppendLine($"- {file}");
                        x.ErrorMessage = errMsgbuilder.ToString();
                    }
                    else if (filenames.Count < 2)
                    {
                        x.ErrorMessage = $"At least two samples are required, {filenames.Count} given.";
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
                description: "Sets a path where analysis results should be persisted.",
                getDefaultValue: () =>
                {
                    return Path.Join(
                        Environment.CurrentDirectory,
                        "session_" + DateTime.Now.ToString(
                            "yyyyMMdd_HHmmssfff",
                            CultureInfo.InvariantCulture));
                });
            outputOption.AddAlias("-o");
            outputOption.LegalFilePathsOnly();
            outputOption.AddValidator(x =>
            {
                var value = x.GetValueForOption(outputOption);

                var p = Path.GetFullPath(value);
                p = p.EndsWith(Path.DirectorySeparatorChar) ? p[..^1] : p;

                var outputPath = p;
                try
                {
                    if (Directory.Exists(outputPath) && Directory.GetFiles(outputPath).Any())
                    {
                        int counter = 0;
                        do outputPath = $"{p}_{counter++}";
                        while (Directory.Exists(outputPath));
                    }

                    // To make sure it can write to the path.
                    Directory.CreateDirectory(outputPath);
                    Directory.Delete(outputPath, true);
                }
                catch (Exception e)
                {
                    x.ErrorMessage = $"Cannot ensure the given output " +
                    $"path `{outputPath}`: {e.Message}";
                }
            });

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
            repTypeOption.FromAmong(new string[] { "bio", "biological", "tec", "technical" });

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
                "of Benjamini–Hochberg step-up procedure.", getDefaultValue: () => 0.05F);
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
                "peaks before combining p-values.",
                getDefaultValue: () => "1")
            {
                IsRequired = false
            };
            cOption.AddValidator(x =>
            {
                if (x.Token is null)
                    return;
                var token = x.Token;
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
            mOption.FromAmong(new string[] { "lowest", "highest" });

            var dpOption = new Option<int?>(
                name: "--degreeOfParallelism",
                description: "Set the degree of parallelism.");
            dpOption.AddAlias("-d");

            var excludeHeaderOption = new Option<bool>(
                name: "--excludeHeader",
                description: "If provided, MSPC will not add a header line to its output.");

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
                var tauWResult = x.FindResultFor(wTOption);
                if (tauWResult is null || tauWResult.Token is null)
                    return;

                var tauSResult = x.FindResultFor(sTOption);
                if (tauSResult is null || tauSResult.Token is null)
                    return;

                var tauW = tauWResult.GetValueOrDefault<double>();
                var tauS = tauSResult.GetValueOrDefault<double>();

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
                    exceptionHandler(e, context);
                }, errorExitCode: 1)
                .UseVersionOption()
                .UseEnvironmentVariableDirective()
                .UseParseDirective()
                .UseSuggestDirective()
                .RegisterWithDotnetSuggest()
                .UseTypoCorrections()
                .UseParseErrorReporting()
                .CancelOnProcessTermination()
                .UseTypoCorrections()
                .UseHelp()
                /*.UseHelp(context =>
                {
                    context.HelpBuilder.CustomizeLayout(
                        _ => HelpBuilder.Default.GetLayout().Prepend(
                            _ => AnsiConsole.Write(
                                new FigletText("MSPC").Color(Color.Chartreuse1))));
                })*/
                .Build();
        }

        public int Invoke(string[] args)
        {
            var filteredArgs = args.Where(
                x => !string.IsNullOrWhiteSpace(x)).ToArray();

            // System.CommandLine has some troubling design decisions 
            // about handling exceptions. Read the following 
            // Github issues for details. They capture exceptions in 
            // a middleware and had many issues getting that work
            // as expected. Therefore, I'm leaving some old-but-solid
            // solutions I had implemented for the previous CLI for 
            // now. This can be revisited in when/if system.commandline
            // improves on exception handling.
            // https://github.com/dotnet/command-line-api/issues/796
            var exitCode = _parser.Invoke(filteredArgs, _console);
            return exitCode != 0 ? exitCode : Environment.ExitCode;
        }
    }
}
