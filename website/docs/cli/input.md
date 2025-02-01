---
title: Input
---

MSPC takes as input a tab-delimited file in 
[BED format]( https://uswest.ensembl.org/info/website/upload/bed.html) for each replicate, 
each containing enriched regions (aka peaks) called with a permissive p-value threshold. 
By default, the columns of the BED files should comply the following field order:


| Chrom  | Start  | End | Name | Value |
|--------|--------|-----|------|-------|

The p-value needs to be in `-Log10(p-value)` format. More columns 
can be present, but they will not be parsed.


For instance:

```
chrom	start	end		name	value
chr1	1000	2000	peak_1	11
chr1	3000	4000	peak_2	22
chr1	5000	6000	peak_3	33
```

:::info
* If your files are not in this format, you may configure MSPC's parser 
[according to your files](parser.md).

* If you files are stranded, you would need to [adjust the parser](parser.md)
 accordingly, otherwise, MSPC will read 
them un-stranded.
:::


