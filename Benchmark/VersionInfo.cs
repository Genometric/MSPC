﻿using System.Diagnostics;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Genometric.MSPC.Benchmark
{
    internal class VersionInfo
    {
        private string _invocation = "mspc.exe";

        public string Args { set; get; } = "-r bio -w 1e-4 -s 1e-8";

        public string Version { get; }

        public ProcessStartInfo StartInfo
        {
            get
            {
                return new ProcessStartInfo(
                    _invocation,
                    Args + " -i " + string.Join(" -i ", InputFiles));
            }
        }

        public List<string> InputFiles { set; get; } = new();

        public Uri ReleaseUri { private set; get; }

        public string? OutputDir { private set; get; } = null;


        public VersionInfo(
            string version,
            string baseReleaseUri = "https://github.com/Genometric/MSPC/releases/")
        {
            Version = version;
            ReleaseUri = new Uri(baseReleaseUri);
            if (!TryRunVerSpecificConfig(version))
                throw new ArgumentException($"Unsupported version {version}.");

            (var success, var root) = TryLocalize().Result;
            if (!success)
                throw new ArgumentException($"No release for version {version} is found.");
            _invocation = Path.Join(root, _invocation);
        }

        private bool TryRunVerSpecificConfig(string version)
        {
            var pattern = new Regex(@"^v[4,5]\.\d+(\.\d+)?$");
            if (pattern.IsMatch(version))
            {
                _invocation = "mspc.exe";
                ReleaseUri = new Uri(ReleaseUri, $"download/{version}/win-x64.zip");
                SetOutputDir();
                return true;
            }

            pattern = new Regex(@"^v2\.\d+(\.\d+)?$");
            if (pattern.IsMatch(version))
            {
                _invocation = "mspc.exe";
                ReleaseUri = new Uri(ReleaseUri, $"download/{version}/v2.1.zip");
                return true;
            }

            if (version == "v1.1")
            {
                _invocation = "mspc.exe";
#pragma warning disable S1075 // URIs should not be hardcoded
                ReleaseUri = new Uri("https://github.com/Genometric/MSPC/raw/cfb7ec899cf3982805277384b0a6a27d8f3aceac/Downloads/v1.1.zip");
#pragma warning restore S1075 // URIs should not be hardcoded
                return true;
            }

            return false;
        }

        private async Task<(bool, string)> TryLocalize()
        {
            var dir = Path.Join(Path.GetTempPath(), "mspc", Version.ToLower().Replace(".", "_"));
            if (Directory.Exists(dir))
                return (true, dir);
                
            Directory.CreateDirectory(dir);
            var filename = Path.Join(dir, "mspc.zip");

            var client = new HttpClient();
            var response = await client.GetAsync(ReleaseUri);
            if (!response.IsSuccessStatusCode)
                return (false, string.Empty);

            using (var stream = new FileStream(filename, FileMode.CreateNew))
                await response.Content.CopyToAsync(stream);

            ZipFile.ExtractToDirectory(filename, dir);

            return (true, dir);
        }

        private void SetOutputDir()
        {
            OutputDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(OutputDir);
            Args += $" -o {Path.Combine(OutputDir, "tmp")}";
        }
    }
}
