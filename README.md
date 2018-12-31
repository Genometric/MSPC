
# MSPC

[![NuGet Badge](https://buildstats.info/nuget/Genometric.MSPC.Core?vWidth=50&dWidth=50)](https://www.nuget.org/packages/Genometric.MSPC.Core)

[![measure](https://sonarcloud.io/api/project_badges/measure?project=mspc&metric=alert_status)](https://sonarcloud.io/dashboard/index/mspc)  [![codecov](https://codecov.io/gh/Genometric/MSPC/branch/master/graph/badge.svg)](https://codecov.io/gh/Genometric/MSPC)   [![measure](https://sonarcloud.io/api/project_badges/measure?project=mspc&metric=ncloc)](https://sonarcloud.io/dashboard/index/mspc) [![measure](https://sonarcloud.io/api/project_badges/measure?project=mspc&metric=sqale_rating)](https://sonarcloud.io/dashboard/index/mspc)

| [Download](https://github.com/Genometric/MSPC/releases) | [Documentation](https://genometric.github.io/MSPC/) | [Publication](https://genometric.github.io/MSPC/publications)
| -- | -- |--: |

<br/>

## About

The analysis of ChIP-seq samples outputs a number of enriched regions, 
each indicating a protein-DNA interaction or a specific chromatin 
modification. Enriched regions (commonly known as "peaks") are called 
when the read distribution is significantly different from the background 
and its corresponding significance measure (p-value) is below a 
user-defined threshold.

When replicate samples are analysed, overlapping enriched regions are 
expected. This repeated evidence can therefore be used to locally lower 
the minimum significance required to accept a peak. Here, we propose a 
method for joint analysis of weak peaks.

Given a set of peaks from (biological or technical) replicates, the method 
combines the p-values of overlapping enriched regions: users can choose a 
threshold on the combined significance of overlapping peaks and set a 
minimum number of replicates where the overlapping peaks should be present. 
The method allows the "rescue" of weak peaks occuring in more than one 
replicate and outputs a new set of enriched regions for each replicate. 

For details you may refer to the 
[MSPC publications](https://genometric.github.io/MSPC/publications) 
and [slides on slideshare](http://www.slideshare.net/jalilivahid/mspc-50694133).

<br/>

## Download and Run

**Cross-platform x64 release**

| Operating System |  Build Status | Build History |
| :--------------: | :-----------: | :-----------: |
| Microsoft Windows  | [![Build status](https://ci.appveyor.com/api/projects/status/p63wau60mm2fldcr/branch/master?svg=true)](https://ci.appveyor.com/project/VJalili/mspc/branch/master) | [![Build history](https://buildstats.info/appveyor/chart/VJalili/mspc)](https://ci.appveyor.com/project/VJalili/mspc/history) |
| Linux Ubuntu 14.04 | [![Build status](https://travis-ci.org/Genometric/MSPC.svg?branch=master)](https://travis-ci.org/Genometric/MSPC) | [![Build history](https://buildstats.info/travisci/chart/Genometric/MSPC)](https://travis-ci.org/Genometric/MSPC/builds) |



<br/>

#### Run MSPC (CLI) executable from command line: 

- [Download the latest version from the Releases page.](https://github.com/Genometric/MSPC/releases) 
- Extract the archive and change your directory to the extracted content folder.
- Requirements: download and install **.NET Core Runtime** from [this page](https://www.microsoft.com/net/download).
- Run MSPC as the following:
```shell
dotnet .\CLI.dll -i rep1.bed -i rep2.bed -r bio -w 1E-4 -s 1E-8
```

Read [quick start page](https://genometric.github.io/MSPC/docs/quick_start),
or for a complete list of arguments refer to
[this page](https://github.com/Genometric/MSPC/wiki/Arguments-in-details) .


<br/>

#### Use MSPC (Core) library in your .NET Core project:

```shell
// Install from Package Manager:
PM> Install-Package Genometric.MSPC.Core -Version 3.0.0

// Install from .NET CLI:
> dotnet add package Genometric.MSPC.Core --version 3.0.0
```

[Read documentation.](https://genometric.github.io/MSPC/docs/library/install)

<br/>



## Graphical version
[MuSERA](https://github.com/Genometric/MuSERA) is a graphical tool that efficiently 
implements MSPC (v2) for comparative evaluation of ChIP-seq and DNase-seq 
samples. Additionally, it facilitates the assessment of replicates by 
integrating common pipelines such as _functional analysis_,
 _nearest feature distance distribution_, _chromosome-wide statistics_, 
_plotting features_, and an _integrated genome browser_.
