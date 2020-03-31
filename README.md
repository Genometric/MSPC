
# MSPC

[![NuGet Badge](https://buildstats.info/nuget/Genometric.MSPC.Core?vWidth=50&dWidth=50)](https://www.nuget.org/packages/Genometric.MSPC.Core)

[![measure](https://sonarcloud.io/api/project_badges/measure?project=mspc&metric=alert_status)](https://sonarcloud.io/dashboard/index/mspc)  [![codecov](https://codecov.io/gh/Genometric/MSPC/branch/master/graph/badge.svg)](https://codecov.io/gh/Genometric/MSPC)   [![measure](https://sonarcloud.io/api/project_badges/measure?project=mspc&metric=ncloc)](https://sonarcloud.io/dashboard/index/mspc) [![measure](https://sonarcloud.io/api/project_badges/measure?project=mspc&metric=sqale_rating)](https://sonarcloud.io/dashboard/index/mspc)

| [Quick Start](https://genometric.github.io/MSPC/docs/quick_start) | [Documentation](https://genometric.github.io/MSPC/) | [Download](https://github.com/Genometric/MSPC/releases) | [Publication](https://genometric.github.io/MSPC/publications) |
| -- | -- | -- | --: |


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

In general, the method groups enriched regions as 
[_background_](https://genometric.github.io/MSPC/docs/method/sets#background), 
[_weak_](https://genometric.github.io/MSPC/docs/method/sets#weak),
or [_stringent_](https://genometric.github.io/MSPC/docs/method/sets#stringent)
based on user-defined 
[weak](https://genometric.github.io/MSPC/docs/cli/args#weak-threshold) 
and [stringency thresholds](https://genometric.github.io/MSPC/docs/cli/args#stringency-threshold). 
The method then [_confirms_](https://genometric.github.io/MSPC/docs/method/sets#confirmed)
or [_discards_](https://genometric.github.io/MSPC/docs/method/sets#discarded)
the _weak_ and _stringent_ enriched regions if their combined stringency is at least as significant 
as a [user-defined threshold](https://genometric.github.io/MSPC/docs/cli/args#gamma). 
The method then performs a multiple testing correction on 
_confirmed_ enriched regions at 
[a user-defined false-discovery rate](https://genometric.github.io/MSPC/docs/cli/args#alpha), 
identifying 
[true-positive](https://genometric.github.io/MSPC/docs/method/sets#truepositive) and 
[false-positive](https://genometric.github.io/MSPC/docs/method/sets#falsepositive)
regions. See the following figure as an example, and you may refer to 
[MSPC publications](https://genometric.github.io/MSPC/publications),
[slides on slideshare](http://www.slideshare.net/jalilivahid/mspc-50694133),
or [documentation](https://genometric.github.io/MSPC/docs/method/about) 
page for more details.

![sets](docs/assets/sets.svg)


<br/>

## Download and Run

MSPC is released as a cross-platform console application and a .NET Core library. 
See the following figure for its current cross-platform build stats, 
or [this quick start page on how to install and run it](https://genometric.github.io/MSPC/docs/quick_start).
Additionally, check [this page on how to run it from command line](https://genometric.github.io/MSPC/docs/cli/about)
or [how to use the library in your .NET Core application](https://genometric.github.io/MSPC/docs/library/install).

| Operating System |  Build Status | Build History |
| :--------------: | :-----------: | :-----------: |
| Microsoft Windows  | [![Build status](https://ci.appveyor.com/api/projects/status/p63wau60mm2fldcr/branch/master?svg=true)](https://ci.appveyor.com/project/VJalili/mspc/branch/master) | [![Build history](https://buildstats.info/appveyor/chart/VJalili/mspc)](https://ci.appveyor.com/project/VJalili/mspc/history) |
| Linux Ubuntu 14.04 | [![Build status](https://travis-ci.org/Genometric/MSPC.svg?branch=master)](https://travis-ci.org/Genometric/MSPC) | [![Build history](https://buildstats.info/travisci/chart/Genometric/MSPC)](https://travis-ci.org/Genometric/MSPC/builds) |


<br/>

## Graphical version
[MuSERA](https://github.com/Genometric/MuSERA) is a graphical tool that efficiently 
implements MSPC (v2) for comparative evaluation of ChIP-seq and DNase-seq 
samples. Additionally, it facilitates the assessment of replicates by 
integrating common pipelines such as _functional analysis_,
 _nearest feature distance distribution_, _chromosome-wide statistics_, 
_plotting features_, and an _integrated genome browser_.
