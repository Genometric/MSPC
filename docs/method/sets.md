---
title: Sets
---

MSPC classifies peaks under `Background`, `Weak`, `Stringent`,
`Confirmed`, `Discarded`, `True-Positive`, and `False-Positive`
categories. (Refer to [method description](method/about.md)
for details.) The following figure is a schematic view of 
these categories and their relation.

![alt text](assets/sets.svg)

(The number of ERs in different sets reported in this figure, are the 
result of `wgEncodeSydhTfbsK562CmycStdAlnRep1` and 
`wgEncodeSydhTfbsK562CmycStdAlnRep2` (available for download from 
[Sample Data page](sample_data)) comparative analysis using 
`-r bio -w 1e-4 -s 1e-8` parameters.)


### Background
Peaks with p-value above the weak threshold (i.e., `-w`): 
`p-value >= weak threashold`


### Weak
Peaks with p-value above or equal to the stringency threshold 
(i.e., `-s`) and below the weak threshold (i.e., `-w`):
`stringency threshold <= p-value < weak threshold`


### Stringent
Peaks with p-value below the stringency threshold (i.e., `-s`):
`p-value < stringency threshold`


### Confirmed
Peaks that are:

1. supported by at least `c` peaks from replicates, and
2. their combined stringency satisfies the given threshold (i.e., `-g`): 
`xSquared >= the inverse of the right-tailed probability of Gamma` and
3. if technical replicate, passed all the tests, and if biological 
replicate, passed at least one test.

(see [method description](method/about.md))


### Discarded
Peaks that are:

1. does not have minimum required (i.e., `c`) supporting evidence, or
2. their combined stringency does not satisfy the given threshold, or
3. if technical replicate, failed a test.


### TruePositive
The confirmed peaks that pass the Benjamini-Hochberg multiple 
testing correction at level `alpha` (i.e., `-a`).


### FalsePositive
The confirmed peaks that fail Benjamini-Hochberg multiple 
testing correction at level `alpha` (i.e., `-a`).
