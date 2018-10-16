---
title: Input
---

The MSPC `CLI` takes as input a tab-delimited file in 
[BED format]( https://uswest.ensembl.org/info/website/upload/bed.html) for each replicate, 
each containing enriched regions (aka peaks) called with a permissive p-value threshold. 

By default, the columns of the BED files should comply the following field order:

| Index | Name       |
|-------|------------|
| 0     | chrom      |
| 1     | chromStart |
| 2     | chromEnd   |
| 3     | name       |
| 4     | value      |

The p-value needs to be in `-Log10(p-value)` format. More columns can be present, but they will not be parsed.
