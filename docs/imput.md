---
title: Input
---

## Input

The input to MSPC `CLI` is a tab-delimited file in 
[BED format]( https://uswest.ensembl.org/info/website/upload/bed.html),
which has the following columns in the given order: 

| Index | Name       | Description                                                       |
|-------|------------|-------------------------------------------------------------------|
| 0     | chrom      | Name of the chromosome or scaffold                                |
| 1     | chromStart | Start position of the feature in standard chromosomal coordinates |
| 2     | chromEnd   | End position of the feature in standard chromosomal coordinates   |
| 3     | name       | A label of the feature                                            |
| 4     | value      | P-value of the feature in -Log10(p-value) format                  |
