---
title: Quick Start
---

## Preparation

1. Install a self-contained release of MSPC, using either of following commands
depending on your runtime (see [installation](installation.md) page for detailed
installation options):


	# Windows x64 (using PowerShell):
	$ wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/win-x64.zip

	# Linux x64:
	$ wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/linux-x64.zip

	# macOS x64:
	$ wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/osx-x64.zip


2. Extract the downloaded archive and browse to the containing directory:

	```bash
	$ unzip mspc.zip -d mspc
	$ cd mspc
	```

3. Download sample data:

	```bash
	$ wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip
	$ unzip demo.zip -d .
	```

## Run

To run MSPC use the following command depending on your runtime:

```bash
# Windows x64 (using PowerShell):
$ .\mspc.exe -i .\rep1.bed -i .\rep2.bed -r bio -w 1e-4 -s 1e-8

# Linux x64 or macOS x64:
$ ./mspc.dll -i .\rep1.bed -i .\rep2.bed -r bio -w 1e-4 -s 1e-8
```

## Output

MSPC creates a folder in the current execution path named `session_X_Y`, where `X` and `Y` are execution date and time respectively, which contains the following files and folders:

```bash
.
└── session_20191126_222131330
    ├── ConsensusPeaks.bed
	├── ConsensusPeaks_mspc_peaks.txt
	├── EventsLog_20191126_2221313409928.txt
    ├── rep1
    │   ├── Background.bed
    │   ├── Background_mspc_peaks.txt
    │   ├── Confirmed.bed
    │   ├── Confirmed_mspc_peaks.txt
    │   ├── Discarded.bed
    │   ├── Discarded_mspc_peaks.txt
    │   ├── FalsePositive.bed
    │   ├── FalsePositive_mspc_peaks.txt
    │   ├── Stringent.bed
    │   ├── Stringent_mspc_peaks.txt
    │   ├── TruePositive.bed
    │   ├── TruePositive_mspc_peaks.txt
    │   └── Weak.bed
    │   ├── Weak_mspc_peaks.txt
    └── rep2
        ├── Background.bed
        ├── Background_mspc_peaks.txt
        ├── Confirmed.bed
        ├── Confirmed_mspc_peaks.txt
        ├── Discarded.bed
        ├── Discarded_mspc_peaks.txt
        ├── FalsePositive.bed
        ├── FalsePositive_mspc_peaks.txt
        ├── Stringent.bed
        ├── Stringent_mspc_peaks.txt
        ├── TruePositive.bed
        ├── TruePositive_mspc_peaks.txt
        └── Weak.bed
        └── Weak.bed
```

## See Also

- [Welcome page](welcome.md)
- [Input file format](cli/input.md)
- [Output files](cli/output.md)
