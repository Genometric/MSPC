---
title: Arguments
---

## Call Example (v3 and newer):
```shell
// minimum
dotnet CLI.dll -i rep1.bed -i rep2.bed -r bio -s 1E-8 -w 1E-4
    
// complete
dotnet CLI.dll -i rep1.bed -i rep2.bed -r bio -s 1E-8 -w 1E-4 -g 1E-9 -c 2 -a 0.005
```


## Arguments Quick Reference

| Argument | Required | Short arg | Long arg | Valid Values | Default Value |
| -------- | -------- | -------------- | ------------- | ------------ | ------------- |
| [Input](#input)          | ✓ | `-i` | `--input` | BED file | none |
| [Replicate Type](#replicate-type) | ✓ | `-r` | `--replicate` | bio, tec | none |
| [Stringency threshold](#stringency-threshold) | ✓ | `-s` | `--tauS` | Double | none |
| [Weak threshold](#weak-threshold) | ✓ | `-w` | `--tauW` | Double | none |
| [Gamma](#gamma) |  | `-g` | `--gamma` | Double | tauS |
| [C](#c) |  | `-c` | none | Integer | 1 |
| [Alpha](#alpha) |  | `-a` | `--alpha` | Double | 0.05 |



## Arguments
In the following we explain arguments in details. 

### Input
Method A: Sample files are listed with the character '&' as separator between them. <br/>
Method B: Sample files are listed after the '-i' argument.

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| -i | -input | Required | any BED file in the format above specified | none |

Example:

    dotnet .\CLI.dll -i rep1.bed&rep2.bed&rep3.bed
    dotnet .\CLI.dll -i rep1.bed -i rep2.bed -i rep3.bed


### Replicate Type
Samples could be biological or technical replicates. The algorithm differentiates between the two replicate types based on the fact that less variations between technical replicates is expected compared to biological replicates. Replicate type can be specified using the following argument:

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| -r | -replicate | Required | Bio, Biological, Tec, Technical | none |

Example:

    dotnet .\CLI.dll -i rep1.bed -i rep2.bed  -r tec
    dotnet .\CLI.dll -i rep1.bed -i rep2.bed -i rep3.bed -r biological


### Stringency Threshold
It specifies the threshold for stringent peaks. Any peak with p-value lower than this threshold is set as stringent peak.

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| -s | -tauS | Required | Double | none |

Example:

    dotnet .\CLI.dll -i rep1.bed -i rep2.bed -s 1E-8


### Weak Threshold
It specifies the threshold for weak peaks. Any peak with p-value lower than this threshold and higher or equal to the Stringency Threshold is set as weak peak; any peak with p-value higher than this threshold is discarded.

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| -w | -tauW | Required | Double | none |

Example:

    dotnet .\CLI.dll -i rep1.bed -i rep2.bed -w 1E-4


### Gamma
It sets the combined stringency threshold. Peaks with combined p-value below this threshold are confirmed.

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| -g | -gamma | Optional | Double | Equal to Stringency Threshold |

Example:

    dotnet .\CLI.dll -i rep1.bed -i rep2.bed -g 1E-8


### C
It specifies the minimum number of samples where overlapping peaks must be called to combine their p-value. For example, given three replicates (rep1, rep2 and rep3), if `C = 3`, a peak on rep1 must intersect with at least one peak from both rep2 and rep3 to combine their p-values, otherwise the peak is discarded; if `C = 2`, a peak on rep1 must intersect with at least one peak from either rep2 or rep3 to combine their p-values, otherwise the peak is discarded.

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| -c | | Optional | Integer | 1 |

Example:

    dotnet .\CLI.dll -i rep1.bed -i rep2.bed -c 2


### Alpha
Threshold for Benjamini-Hochberg multiple testing correction.

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| -a | -alpha | Optional | Double |  0.05 |

Example:

    dotnet .\CLI.dll -i rep1.bed -i rep2.bed -a 0.05


## Output
For each sample, the following bed files are created in a folder named as the sample:

| File name      | Description |
| -------------- | ----------- |
| Background     | Peaks with p-value above the weak threshold (i.e., `-w`). | 
| Confirmed      | Stringent confirmed and weak confirmed peaks. |
| Discarded      | Stringent and weak discarded peaks. |
| FalsePositive  | Stringent confirmed and weak confirmed peaks that fail the Benjamini-Hochberg multiple testing correction. |
| Stringent      | Peaks with p-value below the stringency threshold (i.e., `-s`). |
| TruePositive   | Stringent confirmed and weak confirmed peaks, passing the Benjamini-Hochberg multiple testing correction. |
| Weak           | Peaks with p-value above or equal to the stringency threshold (i.e., `-s`) and below the weak threshold (i.e., `-w`). |

