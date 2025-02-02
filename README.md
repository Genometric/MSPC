<p align="center">
  <a href="https://genometric.github.io/MSPC/">
    <img src="https://raw.githubusercontent.com/Genometric/MSPC/dev/website/static/logo/logo_w_txt_banner.svg?raw=true" alt="MSPC" width="300" />
  </a>
</p>


<p align="center">
    <a href="https://www.nuget.org/packages/Genometric.MSPC.Core">
        <img src="https://img.shields.io/nuget/v/Genometric.MSPC.Core?style=flat&color=%2390c04f&link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FGenometric.MSPC.Core%2F">
    </a>
    <a href="https://sonarcloud.io/project/overview?id=genometric_mspc">
        <img src="https://sonarcloud.io/api/project_badges/measure?project=genometric_mspc&metric=alert_status">
    </a>
    <a href="https://codecov.io/gh/Genometric/MSPC">
    <img src="https://codecov.io/gh/Genometric/MSPC/branch/master/graph/badge.svg?token=TRSk39hCh3"/>
    </a>
    <a href="https://sonarcloud.io/summary/new_code?id=genometric_mspc">
        <img src="https://sonarcloud.io/api/project_badges/measure?project=genometric_mspc&metric=ncloc">
    </a>
    <a href="https://sonarcloud.io/summary/new_code?id=genometric_mspc">
        <img src="https://sonarcloud.io/api/project_badges/measure?project=genometric_mspc&metric=sqale_rating">
    </a>
    <a href="https://sonarcloud.io/summary/new_code?id=genometric_mspc">
        <img src="https://sonarcloud.io/api/project_badges/measure?project=genometric_mspc&metric=reliability_rating">
    </a>
</p>

<p align="center">
  <a href="https://genometric.github.io/MSPC/docs/quick_start">Quick Start</a> |
  <a href="https://genometric.github.io/MSPC/">Documentation</a> |
  <a href="https://github.com/Genometric/MSPC/releases">Download</a> |
  <a href="https://genometric.github.io/MSPC/publications">Publication</a>
</p>

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

<p align="center">
    <a href="https://genometric.github.io/MSPC/docs/method/sets">
        <img src="https://raw.githubusercontent.com/Genometric/MSPC/dev/website/static/img/sets.svg">
    </a>
</p>


<br/>

## Download and Run

- #### [__Quick Start__: _download, install, and run a demo use-case_](https://genometric.github.io/MSPC/docs/quick_start);
- #### [__Install__: _details on different installation options_](https://genometric.github.io/MSPC/docs/installation);
- #### [__Bioconductor R package__:](https://bioconductor.org/packages/release/bioc/html/rmspc.html) [Bioconductor user guide with examples on installing and using it in R](https://bioconductor.org/packages/release/bioc/vignettes/rmspc/inst/doc/rmpsc.html). 

MSPC is distributed as a cross-platform console application, a .NET library, 
and a Bioconductor R package. 