---
title: Quick Start
---

## Preparation

1. [Download and install](https://www.microsoft.com/net/download) Microsoft .NET Core Runtime;
2. [Download](https://github.com/Genometric/MSPC/releases) the latest release of MSPC and extract executables from the zip archive;
3. [Download sample data](https://github.com/Genometric/MSPC/raw/dev/SampleFiles/demo.zip) and extract the archive to the same path as MSPC;

At this point, your MSPC folder should contain the following files: 

```shell
.
└── mspc_v3.0
    ├── mspc.deps.json
    ├── mspc.dll
    ├── mspc.pdb
    ├── mspc.runtimeconfig.json
    ├── Core.dll
    ├── Core.pdb
    ├── GeUtilities.dll
    ├── MathNet.Numerics.dll
    ├── Microsoft.Extensions.CommandLineUtils.dll
    ├── Microsoft.VisualStudio.CodeCoverage.Shim.dll
    ├── Newtonsoft.Json.dll
    ├── System.Collections.Immutable.dll
    ├── rep1.bed
    └── rep2.bed

```

## Run

Open your favorite command-line shell (e.g., 
[PowerShell Core]( https://docs.microsoft.com/en-us/powershell/scripting/powershell-scripting?view=powershell-6))
and browse to the MSPC folder, then run the following command: 

```shell
dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1E-4 -s 1E-8
```

## Output

MSPC creates a folder in the current execution path named `session_X_Y`, where `X` and `Y` are execution date and time respectively, which contains the following files and folders:

```shell
.
└── session_20181015_200924342
    ├── ConsensusPeaks.bed
    ├── rep1
    │   ├── Background.bed
    │   ├── Confirmed.bed
    │   ├── Discarded.bed
    │   ├── FalsePositive.bed
    │   ├── Stringent.bed
    │   ├── TruePositive.bed
    │   └── Weak.bed
    └── rep2
        ├── Background.bed
        ├── Confirmed.bed
        ├── Discarded.bed
        ├── FalsePositive.bed
        ├── Stringent.bed
        ├── TruePositive.bed
        └── Weak.bed
```

## See Also

- [Welcome page](welcome.md)
- [Input file format](cli/input.md)
- [Output files](cli/output.md)
