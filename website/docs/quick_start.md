---
title: Quick Start
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

You may run MSPC as a command-line application, a 
[C# library](library/install), or an R package available from Bioconductor. 
In the following, we provide quick start for using MSPC as a
[command-line application](#quick-start-command-line) and an 
[R package](#quick-start-r-package), and provide details in their 
respective pages. 

## Quick Start: Command-line
### Preparation

1. Install a self-contained release of MSPC, using either of following commands
depending on your runtime (see [installation](installation.md) page for detailed
installation options):

<Tabs
 groupId="operating-systems"
 defaultValue="win"
 values={[
  { label: 'Windows', value: 'win', },
  { label: 'Linux', value: 'linux', },
  { label: 'macOS', value: 'mac', },
 ]
}>
 <TabItem value="win">

 ```shell
 wget -O win-x64.zip https://github.com/Genometric/MSPC/releases/latest/download/win-x64.zip
 ```

 </TabItem>
 <TabItem value="linux">

 ```shell
 wget -O linux-x64.zip https://github.com/Genometric/MSPC/releases/latest/download/linux-x64.zip
 ```

 </TabItem>
 <TabItem value="mac">

 ```shell
 wget -O osx-x64.zip https://github.com/Genometric/MSPC/releases/latest/download/osx-x64.zip
 ```

 </TabItem>
</Tabs>

2. Extract the downloaded archive and browse to the containing directory:

<Tabs
 groupId="operating-systems"
 defaultValue="win"
 values={[
  { label: 'Windows', value: 'win', },
  { label: 'Linux', value: 'linux', },
  { label: 'macOS', value: 'mac', },
 ]
}>
 <TabItem value="win">

 ```shell
 unzip win-x64.zip -d mspc; cd mspc
 ```

 </TabItem>
 <TabItem value="linux">

 ```shell
 unzip -q linux-x64.zip -d mspc && cd mspc && chmod +x mspc
 ```

 </TabItem>
 <TabItem value="mac">

 ```shell
 unzip -q osx-x64.zip -d mspc && cd mspc && chmod +x mspc
 ```

 </TabItem>
</Tabs>

   Notice that if you are working on Windows x64, you will need to download the program unzip.

3. Download sample data:

<Tabs
 groupId="operating-systems"
 defaultValue="win"
 values={[
  { label: 'Windows', value: 'win', },
  { label: 'Linux', value: 'linux', },
  { label: 'macOS', value: 'mac', },
 ]
}>
 <TabItem value="win">

 ```shell
 wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip; unzip demo.zip -d .
 ```

 </TabItem>
 <TabItem value="linux">

 ```shell
 wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .
 ```

 </TabItem>
 <TabItem value="mac">

 ```shell
 wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .
 ```

 </TabItem>
</Tabs>

### Run

To run MSPC use the following command depending on your runtime:

<Tabs
 groupId="operating-systems"
 defaultValue="win"
 values={[
  { label: 'Windows', value: 'win', },
  { label: 'Linux', value: 'linux', },
  { label: 'macOS', value: 'mac', },
 ]
}>
 <TabItem value="win">

 ```shell
 .\mspc.exe -i .\rep*.bed -r bio -w 1e-4 -s 1e-8
 ```

 </TabItem>
 <TabItem value="linux">

 ```shell
 ./mspc -i rep*.bed -r bio -w 1e-4 -s 1e-8
 ```

 </TabItem>
 <TabItem value="mac">

 ```shell
 ./mspc -i rep*.bed -r bio -w 1e-4 -s 1e-8
 ```

 </TabItem>
</Tabs>

### Output

MSPC creates a folder in the current execution path named `session_X_Y`, 
where `X` and `Y` are execution date and time respectively, which contains 
the following files and folders:

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
        └── Weak_mspc_peaks.txt
```

:::info
Note that in these instructions you download a 
[self-contained deployment](https://docs.microsoft.com/en-us/dotnet/core/deploying/#publish-self-contained)
of MSPC, hence you do __not__ need to install .NET (or 
any other dependencies) for the program to execute. 

For other installation options see the [installation page](installation).   
:::

## Quick Start: R package

MSPC is also distributed as a 
[Bioconductor package](https://bioconductor.org/packages/release/bioc/html/rmspc.html).
To use in R programming language, you may take the following steps.

### Install and Load

```r
if (!require("BiocManager"))
    install.packages("BiocManager")
BiocManager::install("rmspc")

library(rmspc)
```

### Run

```r
# Load example data available in the package.
path <- system.file("extdata", package = "rmspc")

# Run MSPC
results <- mspc(
        input = input, 
        replicateType = "Technical",
        stringencyThreshold = 1e-8,
        weakThreshold = 1e-4, 
        gamma = 1e-8,
        keep = FALSE,
        GRanges = TRUE,
        multipleIntersections = "Lowest",
        c = 2,
        alpha = 0.05)
```

:::info
The MSPC R package can load data from files and Granges objects. 
For a complete documentation on the package, please refer to the
[bioconductor documentation](https://bioconductor.org/packages/release/bioc/vignettes/rmspc/inst/doc/rmpsc.html). 
:::

## See Also

- [Welcome page](welcome.md)
- [Input file format](cli/input.md)
- [Output files](cli/output.md)
- [Sets](method/sets.md)
