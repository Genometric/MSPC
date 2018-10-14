---
title: Quick Start
---

1. [Download and install](https://www.microsoft.com/net/download) Microsoft .NET Core Runtime;
2. [Download](https://github.com/Genometric/MSPC/releases) latest release of MSPC and extract executables from the zip archive;
3. [Download sample data](http://www.bioinformatics.deib.polimi.it/genomic_computing/MSPC/packages/ENCODE_Samples.zip) and extract the archive to same path as MSPC;
4. Run MSPC using the following command:

```shell
dotnet .\CLI.dll -i rep1.bed -i rep2.bed -r bio -w 1E-4 -s 1E-8
```

MSPC produces the following output:

```shell
TO BE ADDED
```

## Remarks


## Input
MSPC takes as input a BED file for each replicate, each containing enriched regions (aka _peaks_) called with a permissive p-value threshold. The columns of the BED files should comply the field order defined on Ensembl:
1. chrom;
2. chromStart;
3. chromEnd;
4. name; 
5. score (p-value). 

The p-value needs to be in **-Log10(p-value)** format. More columns can be present, but they are not considered.

## Output
MSPC outputs different classifications of peaks (e.g., stringent, weak, stringent-confirmed, weak-discarded) into merged and separated BED files. An output BED file contains all parsed information of a peak (i.e., chr, chromStart, chromEnd, name, and p-value) plus the combined significance and corresponding right-tail probability. An example is as follows: 

    from input:
    chr1  32600  32680  MACS_peak_4  4.08
    
    in the output:
    chr1  32600  32680  MACS_peak_4  4.08  222.936  46.373


## Arguments
You may consider following table for quick start, a detailed description of arguments are available at [this page](Arguments-in-details).



| Argument | Type | short argument | long argument | valid values | default value |
| -------- | ---- | -------------- | ------------- | ------------ | ------------- |
| Input          | Required | -i | -input | BED file | none |
| Replicate Type | Required | -r | -replicate | Bio, Biological, Tec, Technical | none |
| Stringency threshold | Required | -s | -tauS | Double | none |
| Weak threshold | Required | -w | -tauW | Double | none |
| Gamma (combined stringency threshold) | Optional | -g | -gamma | Double | Stringency threshold |
| C (minimum required overlapping peaks) | Optional | -c | none | Integer | 1 |
| Alpha (BH multiple testing correction threshold) | Optional | -a | -alpha | Double | 0.05 |