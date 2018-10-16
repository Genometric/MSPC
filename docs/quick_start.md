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

- [Input file format](input.md)
- [Output files](output.md)


## Arguments
You may consider following table for quick start, a detailed description of arguments are available at [this page](Arguments-in-details).



| Argument | Type | short argument | long argument | valid values | default value |
| -------- | ---- | -------------- | ------------- | ------------ | ------------- |
| Input          | Required | -i | -input | BED file | none |
| Replicate Type | Required | -r | -replicate | Bio, Biological, Tec, Technical | none |
| Stringency threshold | Required | -s | -tauS | Double | none |
| Weak threshold | Required | -w | -tauW | Double | none |
| Gamma (combined stringency threshold) | Optional | -g | -gamma | Double | Stringency threshold |
| C (minimum required overlapping peaks) | Optional | -c | none | Integer | 1 |
| Alpha (BH multiple testing correction threshold) | Optional | -a | -alpha | Double | 0.05 |
