using Genometric.MSPC.Core.Model;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Binding;
using System.IO;
using System.Linq;

namespace Genometric.MSPC.CLI.CommandLineInterface
{
    internal class OptionsBinder : BinderBase<CliConfig>
    {
        private readonly Option<List<string>> _inputsOption;
        private readonly Option<List<string>> _inputsPathOption;
        private readonly Option<string> _outputPathOption;
        private readonly Option<string> _parserConfigFilenameOption;
        private readonly Option<ReplicateType> _replicateTypeOption;
        private readonly Option<double> _sTOption;
        private readonly Option<double> _wTOption;
        private readonly Option<double> _gTOption;
        private readonly Option<float> _alphaOption;
        private readonly Option<string> _cOption;
        private readonly Option<MultipleIntersections> _mOption;
        private readonly Option<int?> _dpOption;
        private readonly Option<bool> _excludeHeaderOption;

        private BindingContext _context;

        public OptionsBinder(
            Option<List<string>> inputsOption,
            Option<List<string>> inputsDirOption,
            Option<string> outputPathOption,
            Option<string> parserConfigFilenameOption,
            Option<ReplicateType> replicateTypeOption,
            Option<double> sTOption,
            Option<double> wTOption,
            Option<double> gTOption,
            Option<float> alphaOption,
            Option<string> cOption,
            Option<MultipleIntersections> mOption,
            Option<int?> dbOption,
            Option<bool> excludeHeaderOption)
        {
            _inputsOption = inputsOption;
            _inputsPathOption = inputsDirOption;
            _outputPathOption = outputPathOption;
            _parserConfigFilenameOption = parserConfigFilenameOption;
            _replicateTypeOption = replicateTypeOption;
            _sTOption = sTOption;
            _wTOption = wTOption;
            _gTOption = gTOption;
            _alphaOption = alphaOption;
            _cOption = cOption;
            _mOption = mOption;
            _dpOption = dbOption;
            _excludeHeaderOption = excludeHeaderOption;
        }

        protected override CliConfig GetBoundValue(
            BindingContext bindingContext)
        {
            _context = bindingContext;

            var filenames = GetValue(_inputsOption, new List<string>());
            filenames.AddRange(GetValue(_inputsPathOption, new List<string>()));

            return new CliConfig(
                inputFiles: filenames.AsReadOnly(),
                outputPath: EnsureOutputPath(GetValue(_outputPathOption)),
                replicateType: GetValue(_replicateTypeOption),
                parserConfigFilename: GetValue(_parserConfigFilenameOption),
                tauS: GetValue(_sTOption),
                tauW: GetValue(_wTOption),
                gamma: GetValue(_gTOption, GetValue(_sTOption)),
                alpha: GetValue(_alphaOption),
                c: ParseAndAdjustCInNeeded(GetValue(_cOption), filenames.Count),
                multipleIntersections: GetValue(_mOption),
                degreeOfParallelism: GetValue(_dpOption),
                excludeHeader: GetValue(_excludeHeaderOption));
        }

        private T GetValue<T>(
            Option<T> option,
            T defaultValue = default)
        {
            var valueGiven = _context.ParseResult.FindResultFor(option) != null;

            if (!valueGiven)
                return defaultValue;

            var value = _context.ParseResult.GetValueForOption(option);
            if (value == null)
                return defaultValue;

            return value;
        }

        private static int ParseAndAdjustCInNeeded(string proposedC, int inputFilesCount)
        {
            int c = 1;
            if (proposedC is not null)
            {
                if (proposedC.Contains('%'))
                {
                    var percentage = int.Parse(proposedC.Replace("%", ""));
                    c = inputFilesCount * percentage / 100;
                }
                else
                {
                    c = int.Parse(proposedC);
                    if (c > inputFilesCount)
                        c = inputFilesCount;
                }
            }
            return c;
        }

        private static string EnsureOutputPath(string tentativeOutputPath)
        {
            tentativeOutputPath = Path.GetFullPath(tentativeOutputPath);
            var outputPath = tentativeOutputPath;
            try
            {
                if (Directory.Exists(outputPath))
                {
                    if (Directory.GetFiles(outputPath).Any())
                    {
                        int counter = 0;
                        do outputPath = $"{tentativeOutputPath}_{counter++}";
                        while (Directory.Exists(outputPath));
                        Directory.CreateDirectory(outputPath);
                    }
                }
                else
                {
                    Directory.CreateDirectory(outputPath);
                }
            }
            catch (Exception e)
            {
                throw new IOException(
                    $"Cannot ensure the given output path " +
                    $"`{outputPath}`: {e.Message}");
            }
            return outputPath;
        }
    }
}
