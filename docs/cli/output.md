---
title: Output
---

MSPC `CLI` outputs each classifications of peaks (e.g., `stringent`, `weak`, `confirmed`, or
`discarded`) in separate BED files. See the following figure for different sets and their 
relation. 

![alt text](assets/sets.svg)

See [Sets](method/sets.md) page for description of each category.


Each peak in an output BED file is represented by its parsed information (i.e., `chr`, `chromStart`, 
`chromEnd`, `name`, and `p-value`) and its comparative analysis values (e.g., combined significance,
and its right-tail probability). For instance:

```shell
# Input:
chr   chromStart   chromEnd    name     p-value
chr1  32600        32680       peak_1   4.08

# Output (e.g., Confirmed.bed):
chr   chromStart   chromEnd    name     p-value    xSquared     right-tail_probability
chr1  32600        32680       peak_1   4.08       222.936      46.373
```

## See Also

- [Method description](method/about.md)
- [Sets description](method/sets.md)
