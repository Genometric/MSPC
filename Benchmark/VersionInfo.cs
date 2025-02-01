using System.Diagnostics;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Genometric.MSPC.Benchmark
{
    internal class VersionInfo
    {
        private string _invocation = "mspc.exe";
        private string _prgName = string.Empty;

        public string Args { set; get; } = "-r bio -w 1e-4 -s 1e-8";

        public string Version { get; }

        public ProcessStartInfo StartInfo
        {
            get
            {
                return new ProcessStartInfo(
                    _invocation,
                    _prgName.TrimEnd() + " " + Args.Trim() + " -i " + string.Join(" -i ", InputFiles));
            }
        }

        public List<string> InputFiles { set; get; } = new();

        public Uri ReleaseUri { private set; get; }

        public string? OutputDir { private set; get; } = null;

        private string? _archivePath;


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

            if (string.IsNullOrEmpty(_prgName))
                _invocation = Path.Join(root, _invocation);
            else
                _prgName = Path.Join(root, _prgName);
        }

        private bool TryRunVerSpecificConfig(string version)
        {
            var pattern = new Regex(@"^v[4,5,6]\.\d+(\.\d+)?$");
            if (pattern.IsMatch(version))
            {
                _invocation = "dotnet";
                _prgName = "mspc.dll";
                ReleaseUri = new Uri(ReleaseUri, $"download/{version}/mspc.zip");
                SetOutputDir();
                return true;
            }

            pattern = new Regex(@"^v2\.\d+(\.\d+)?$");
            if (pattern.IsMatch(version))
            {
                if (!OperatingSystem.IsWindows())
                    throw new ArgumentException($"Running version {version} is only supported on Windows.");

                _invocation = "mspc.exe";
                ReleaseUri = new Uri(ReleaseUri, $"download/{version}/v2.1.zip");
                return true;
            }

            if (version == "v1.1")
            {
                if (!OperatingSystem.IsWindows())
                    throw new ArgumentException($"Running version {version} is only supported on Windows.");

                _invocation = "mspc.exe";
                _archivePath = "v.1.1";
#pragma warning disable S1075 // URIs should not be hardcoded
                ReleaseUri = new Uri("https://github.com/Genometric/MSPC/raw/cfb7ec899cf3982805277384b0a6a27d8f3aceac/Downloads/v1.1.zip");
#pragma warning restore S1075 // URIs should not be hardcoded
                return true;
            }

            return false;
        }

        private async Task<(bool, string)> TryLocalize()
        {
            var dir = Path.Combine(Environment.CurrentDirectory, "mspc", Version.ToLower().Replace(".", "_"));
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

            // If this condition is satisfied, it implies that release
            // files are extracted to a directory, which need to be
            // moved to `dir`. 
            if (_archivePath != null)
            {
                var releaseFilesDir = Path.Join(dir, _archivePath);
                foreach (var file in Directory.GetFiles(releaseFilesDir))
                    File.Move(file, Path.Join(dir, Path.GetFileName(file)));
                Directory.Delete(releaseFilesDir, true);
            }

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
