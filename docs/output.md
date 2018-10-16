---
title: Input
---

MSPC `CLI` outputs different classifications of peaks (e.g., stringent, weak, stringent-confirmed,
weak-discarded) into merged and separated BED files. An output BED file contains all parsed 
information of a peak (i.e., chr, chromStart, chromEnd, name, and p-value) plus the combined 
significance and corresponding right-tail probability. An example is as follows: 

```shell
# from input:
chr1  32600  32680  MACS_peak_4  4.08

# in the output:
chr1  32600  32680  MACS_peak_4  4.08  222.936  46.373
```
