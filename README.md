
| [Documentation](https://github.com/Genometric/MSPC/wiki) | [Publication](https://academic.oup.com/bioinformatics/article/31/17/2761/183989/Using-combined-evidence-from-replicates-to)
| -- |--: |

[![measure](https://sonarcloud.io/api/project_badges/measure?project=mspc&metric=alert_status)](https://sonarcloud.io/dashboard/index/mspc)  [![codecov](https://codecov.io/gh/Genometric/MSPC/branch/master/graph/badge.svg)](https://codecov.io/gh/Genometric/MSPC)   [![measure](https://sonarcloud.io/api/project_badges/measure?project=mspc&metric=ncloc)](https://sonarcloud.io/dashboard/index/mspc) [![measure](https://sonarcloud.io/api/project_badges/measure?project=mspc&metric=sqale_rating)](https://sonarcloud.io/dashboard/index/mspc)


## About

The analysis of ChIP-seq samples outputs a number of enriched regions, each indicating a protein-DNA interaction or a specific chromatin modification. Enriched regions (commonly known as "peaks") are called when the read distribution is significantly different from the background and its corresponding significance measure (p-value) is below a user-defined threshold.

When replicate samples are analysed, overlapping enriched regions are expected. This repeated evidence can therefore be used to locally lower the minimum significance required to accept a peak. Here, we propose a method for joint analysis of weak peaks.

Given a set of peaks from (biological or technical) replicates, the method combines the p-values of overlapping enriched regions: users can choose a threshold on the combined significance of overlapping peaks and set a minimum number of replicates where the overlapping peaks should be present. The method allows the "rescue" of weak peaks occuring in more than one replicate and outputs a new set of enriched regions for each replicate. 

For details you may refer to the [MSPC publication](http://bioinformatics.oxfordjournals.org/content/31/17/2761) and  [slides on slideshare](http://www.slideshare.net/jalilivahid/mspc-50694133).

<br/>

## Download and Run

|                     | Cross-platform x64 release build status |
| :-----------------: | :-------------------------------------: |
| Microsoft Windows            | [![Build status](https://ci.appveyor.com/api/projects/status/p63wau60mm2fldcr/branch/master?svg=true)](https://ci.appveyor.com/project/VJalili/mspc/branch/master) |
| Linux Ubuntu 14.04 | [![Build status](https://travis-ci.org/Genometric/MSPC.svg?branch=master)](https://travis-ci.org/Genometric/MSPC) |

### Use MSPC (Core) library in your .NET Core project:
[![Latest version](https://img.shields.io/nuget/v/Genometric.MSPC.Core.svg?style=for-the-badge)](https://www.nuget.org/packages/Genometric.MSPC.Core/)

```shell
// Install from Package Manager:
PM> Install-Package Genometric.MSPC.Core -Version 3.0.0

// Install from .NET CLI:
> dotnet add package Genometric.MSPC.Core --version 3.0.0
```

### Run MSPC (CLI) executable from command line: 

- [Download the latest version from the Releases page.](https://github.com/Genometric/MSPC/releases) Extract the archive and change your directory to the extracted content folder.
- Requirements: download and install **.NET Core Runtime** from [this page](https://www.microsoft.com/net/download).
- Run MSPC as the following:
```shell
dotnet .\CLI.dll -i rep1.bed -i rep2.bed -r bio -w 1E-4 -s 1E-8
```

See [this page](https://github.com/Genometric/MSPC/wiki/Arguments-in-details) for a complete list of arguments.

<br/>

## Citing MSPC
If you use or extend MSPC in your published work, please cite the following publication:

    Vahid Jalili, Matteo Matteucci, Marco Masseroli, Marco J. Morelli;
    Using combined evidence from replicates to evaluate ChIP-seq peaks.
    Bioinformatics 2015; 31 (17): 2761-2769. doi: 10.1093/bioinformatics/btv293


## Graphical version
[MuSERA](Musera.codeplex.com) is a graphical tool that efficiently implements MSPC for comparative evaluation of ChIP-seq and DNase-seq samples. Additionally, it facilitates the assessment of replicates by integrating common pipelines such as _functional analysis_, _nearest feature distance distribution_, _chromosome-wide statistics_, _plotting features_, and an _integrated genome browser_.
