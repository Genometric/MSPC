---
title: Sets
---

MSPC rigorously combines the statistical significance of the overlapping
enriched regions (ER), in order to "rescue" weak peaks, which would probably
be discarded in a single-sample analysis, when their combined evidence 
across multiple samples is sufficiently strong. [[Ref](https://doi.org/10.1093/bioinformatics/btv293)]

MSPC takes as input, for each replicate, a list of ERs and a measure of 
their individual significance in terms of a P-value. 
Starting from a permissive call, it divides the initial ERs in "stringent" 
(highly significant) and "weak" (moderately significant), and it assesses 
the presence of overlapping enriched regions across multiple replicates. 
Non-overlapping regions can be penalised or discarded according to specific
needs. 

The significance of the overlapping regions is rigorously combined with the 
[Fisher's method](https://en.wikipedia.org/wiki/Fisher%27s_method) to obtain
a global score. This score is assessed against an adjustable threshold on 
the combined evidence, and peaks in each replicate are either confirmed or 
discarded.

Finally, in order to account for multiple testing correction, MSPC applies the
[Benjamini-Hochberg procedure](https://en.wikipedia.org/wiki/False_discovery_rate#Benjamini–Hochberg_procedure), 
and outputs ERs with false-discovery rate smaller than an adjustable threshold. 

The following figure is a schematic view of this method.

![alt text](assets/sets.svg)

(The number of ERs in different sets reported in this figure, are based on applying
MSPC on `wgEncodeSydhTfbsK562CmycStdAlnRep1` and `wgEncodeSydhTfbsK562CmycStdAlnRep2`
(available for download from [Sample Data page](sample_data)) using 
`-r bio -w 1e-4 -s 1e-8` parameters.)
