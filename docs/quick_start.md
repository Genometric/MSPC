---
title: Quick Start
---

1. [Download and install](https://www.microsoft.com/net/download) Microsoft .NET Core Runtime;
2. [Download](https://github.com/Genometric/MSPC/releases) the latest release of MSPC and extract executables from the zip archive;
3. [Download sample data](https://github.com/Genometric/MSPC/raw/dev/Downloads/demo_data.zip) and extract the archive to the same path as MSPC;
4. Run MSPC using the following command:

```shell
dotnet CLI.dll -i rep1.bed -i rep2.bed -r bio -w 1E-4 -s 1E-8
```

MSPC creates a folder in the current execution path named `session_X_Y`, where `X` and `Y` are execution date and time respectively. This folder contains the following files and folders:

```shell
.
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
