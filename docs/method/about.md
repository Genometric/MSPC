---
title: About
---

The analysis of ChIP-seq samples outputs a number of enriched regions (commonly known as "peaks"), each indicating a protein-DNA interaction or a specific chromatin modification.

When replicate samples are analysed, overlapping peaks are expected. This repeated evidence can therefore be used to locally lower the minimum significance required to accept a peak. MSPC is a method for joint analysis of peaks.

Given a set of peaks from (biological or technical) replicates, MSPC combines the p-values of overlapping enriched regions, and allows the "rescue" of weak peaks occuring in more than one replicate.


![alt text](assets/simplified_flow_chart.svg)
