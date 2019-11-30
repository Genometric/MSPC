---
title: About
---

The analysis of ChIP-seq samples outputs a number of enriched regions 
(commonly known as "peaks"), each indicating a protein-DNA interaction 
or a specific chromatin modification.

When replicate samples are analyzed, overlapping peaks are expected. 
This repeated evidence can therefore be used to locally lower the 
minimum significance required to accept a peak. MSPC is a method for 
joint analysis of peaks.

Given a set of peaks from (biological or technical) replicates, 
MSPC combines the p-values of overlapping enriched regions, and allows 
the "rescue" of weak peaks occurring in more than one replicate.


## Remarks

MSPC rigorously combines the statistical significance of the overlapping
enriched regions (ER), in order to "rescue" weak peaks, which would probably
be discarded in a single-sample analysis, when their combined evidence 
across multiple samples is sufficiently strong. 
[[Ref](https://doi.org/10.1093/bioinformatics/btv293)]

MSPC takes as input, for each replicate, a list of ERs and a measure of 
their individual significance in terms of a p-value. 
Starting from a permissive call, it divides the initial ERs in "stringent" 
(highly significant) and "weak" (moderately significant), and it assesses 
the presence of overlapping enriched regions across multiple replicates. 
Non-overlapping regions can be penalized or discarded according to specific
needs. 

The significance of the overlapping regions is rigorously combined with the 
[Fisher's method](https://en.wikipedia.org/wiki/Fisher%27s_method) to obtain
a global score. This score is assessed against an adjustable threshold on 
the combined evidence, and peaks in each replicate are either confirmed or 
discarded.

Finally, in order to account for multiple testing correction, MSPC applies the
[Benjamini-Hochberg procedure](https://en.wikipedia.org/wiki/False_discovery_rate#Benjamini–Hochberg_procedure), 
and outputs ERs with false-discovery rate smaller than an adjustable threshold. 

This flow is captured in the following flowchart for each ER of each replicate: 

![alt text](assets/simplified_flow_chart.svg)

(This flowchart is a simplified version of the flowchart available in
[MSPC's manuscript](https://doi.org/10.1093/bioinformatics/btv293).)



In other words:

- for each replicate, MSPC classifies input ERs as _stringent_, _weak_, 
and _background_, based on their p-value;
- performs a comparative analysis, and based on the combined stringency test,
it classifies _stringent_ and _weak_ ERs as _confirmed_ or _discarded_;
- based on false-discovery rate, it classifies _confirmed_ ERs as
_true-positive_ or _false-positive_.

The following figure is a schematic view of this procedure.

![alt text](assets/sets.svg)

(The number of ERs in different sets reported in this figure, are the 
result of `wgEncodeSydhTfbsK562CmycStdAlnRep1` and 
`wgEncodeSydhTfbsK562CmycStdAlnRep2` (available for download from 
[Sample Data page](sample_data)) comparative analysis using 
`-r bio -w 1e-4 -s 1e-8` parameters.)

