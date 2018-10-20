---
title: Output
---

MSPC `CLI` outputs each classifications of peaks (e.g., `stringent`, `weak`, `confirmed`, or
`discarded`) in separate BED files. See the following figure for different sets and their 
relation. 

![alt text](assets/sets.svg)


Each peak in an output BED file is represented by its parsed information (i.e., chr, chromStart, 
chromEnd, name, and p-value) and its comparative analysis values (e.g., combined significance,
and its right-tail probability). For instance:

```shell
# from input:
chr   chromStart   chromEnd    name     p-value
chr1  32600        32680       peak_1   4.08

# in the output:
chr   chromStart   chromEnd    name     p-value    xSquared     right-tail_probability
chr1  32600        32680       peak_1   4.08       222.936      46.373
```



| File name      | Description |
| -------------- | ----------- |
| Background     | Peaks with p-value above the weak threshold (i.e., `-w`). | 
| Confirmed      | Stringent confirmed and weak confirmed peaks. |
| Discarded      | Stringent and weak discarded peaks. |
| FalsePositive  | Stringent confirmed and weak confirmed peaks that fail the Benjamini-Hochberg multiple testing correction. |
| Stringent      | Peaks with p-value below the stringency threshold (i.e., `-s`). |
| TruePositive   | Stringent confirmed and weak confirmed peaks, passing the Benjamini-Hochberg multiple testing correction. |
| Weak           | Peaks with p-value above or equal to the stringency threshold (i.e., `-s`) and below the weak threshold (i.e., `-w`). |


