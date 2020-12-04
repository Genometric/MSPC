---
title: Welcome
slug: /
---

The analysis of ChIP-seq samples outputs a number 
of enriched regions (commonly known as "peaks"), 
each indicating a protein-DNA interaction or a 
specific chromatin modification. When replicate 
samples are analyzed, overlapping peaks are expected. 
This repeated evidence can therefore be used to 
locally lower the minimum significance required to 
accept a peak. Here, we propose a method for joint 
analysis of weak peaks. Given a set of peaks from 
(biological or technical) replicates, the method 
combines the p-values of overlapping enriched regions, 
and allows the "rescue" of weak peaks occurring in 
more than one replicate.

MSPC comparatively evaluates ChIP-seq peaks and 
combines the statistical significance of 
repeated evidences, with the aim of lowering the
minimum significance required to “rescue” 
weak peaks; hence reducing false-negatives. 


MSPC can be used from: 

- [**Command Line Interface (CLI)**](cli/about.md): call MSPC CLI from your favorite terminal with necessary 
arguments, and it writes the analysis results to BED files.

- [**As a library in your project**](library/install.md): call it from your program, and it returns analysis 
results to your program.


# About
With most peak callers (e.g, MACS), the false-positive 
rate is a function of a user-defined p-value threshold, 
where the more conservative thresholds result in lower 
false-positive rates---the penalty of which is the 
increase in the number of false-negatives. While several 
probabilistic methods are developed to jointly model 
binding affinities across replicated samples to identify 
combinatorial enrichment patterns (e.g., 
[jMOSAiCS](https://genomebiology.biomedcentral.com/articles/10.1186/gb-2013-14-4-r38),
or [this](https://academic.oup.com/biostatistics/article/15/2/296/226404),
or [this](https://link.springer.com/chapter/10.1007/978-3-319-05269-4_14),
or [this](https://www.frontiersin.org/articles/10.3389/fgene.2018.00731/full)),
the more commonly used peak callers (e.g., MACS) operate 
on single samples. Additionally, the choice of peak 
caller is generally an established step in a ChIP-seq 
analysis pipeline, since altering it may require changes 
to the data pre- and post-processing of the pipeline. 
Therefore, the remainder of this manuscript is focused 
on the post peak calling methods to lower false-positive 
rates as they can be applied on the output of any peak 
caller with minimal changes to the established analysis 
pipeline, and can combine evidence across replicated samples.


# Motivation
Ultimately, with most peak callers (e.g, MACS), the false-positive 
rate is a function of a user-defined p-value threshold, where the more 
conservative thresholds result in lower false-positive rates---the penalty 
of which is the increase in the number of false-negatives. While several 
probabilistic methods are developed to jointly model binding affinities 
across replicated samples to identify combinatorial enrichment patterns 
(e.g., 
[REF1](https://genomebiology.biomedcentral.com/articles/10.1186/gb-2013-14-4-r38), 
[REF2](https://academic.oup.com/biostatistics/article/15/2/296/226404), 
[REF3](https://link.springer.com/chapter/10.1007/978-3-319-05269-4_14), 
[REF4](https://academic.oup.com/bioinformatics/article/31/1/17/2366199), 
[REF5](https://www.frontiersin.org/articles/10.3389/fgene.2018.00731/full)), 
the more commonly used peak callers 
(e.g., MACS) operate on single samples. Additionally, the choice of peak 
caller is generally an established step in a ChIP-seq analysis pipeline, 
since altering it may require changes to the data pre- and post-processing 
of the pipeline.
