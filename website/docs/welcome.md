---
title: Welcome
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

- [**As a library in your project**](library/about.md): call it from your program, and it returns analysis 
results to your program.
