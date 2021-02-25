---
title: Sets
---

import useBaseUrl from '@docusaurus/useBaseUrl';
import ThemedImage from '@theme/ThemedImage';

MSPC classifies peaks under `Background`, `Weak`, `Stringent`,
`Confirmed`, `Discarded`, `True-Positive`, and `False-Positive`
categories. (Refer to [method description](method/about.md)
for details.) The following figure is a schematic view of 
these categories and their relation.

<ThemedImage
  alt="Sets"
  sources={{
    light: useBaseUrl('/img/sets.svg'),
    dark: useBaseUrl('/img/sets_dark.svg'),
  }}
/>

(The number of ERs in different sets reported in this figure, are the 
result of `wgEncodeSydhTfbsK562CmycStdAlnRep1` and 
`wgEncodeSydhTfbsK562CmycStdAlnRep2` (available for download from 
[Sample Data page](../sample_data)) comparative analysis using 
`-r bio -w 1e-4 -s 1e-8` parameters.)


## Background
Peaks with `p-value >= `[`weak threshold`](cli/args.md#weak-threshold).



## Weak
Peaks with [`stringency threshold`](cli/args.md#stringency-threshold)` <= p-value < `[`weak threshold`](cli/args.md#weak-threshold).


## Stringent
Peaks with `p-value < `[`stringency threshold`](cli/args.md#stringency-threshold).


## Confirmed
Peaks that:

1. are supported by at least [`c`](cli/args.md#c) peaks from replicates, and
2. their combined stringency (xSquared) satisfies the [given threshold](cli/args.md#gamma):
`xSquared >= the inverse of the right-tailed probability of Gamma` and
3. if [technical replicate](cli/args.md#replicate-type), passed all the 
tests, and if [biological replicate](cli/args.md#replicate-type), 
passed at least one test.

(see [method description](method/about.md))


## Discarded
Peaks that:

1. does not have minimum required (i.e., [`c`](cli/args.md#c)) supporting evidence, or
2. their combined stringency (xSquared) does not satisfy the [given threshold](cli/args.md#gamma), or
3. if [technical replicate](cli/args.md#replicate-type), failed a test.


## TruePositive
The confirmed peaks that pass the Benjamini-Hochberg multiple 
testing correction at level [`alpha`](cli/args.md#alpha).


## FalsePositive
The confirmed peaks that fail Benjamini-Hochberg multiple 
testing correction at level [`alpha`](cli/args.md#alpha).

## See Also

- [Method description](about.md)
- [CLI output](cli/output.md)
