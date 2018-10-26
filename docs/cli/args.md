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
Sample files are listed after the `-i` or `--input` argument.

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-i` | `--input` | Required | BED file | none |

Example:

```shell
dotnet CLI.dll -i rep1.bed -i rep2.bed -i rep3.bed -r bio -w 1e-4 -s 1e-8
```

### Replicate Type
Samples could be biological or technical replicates. MSPC differentiates between 
the two replicate types based on the fact that less variations between technical 
replicates is expected compared to biological replicates. Replicate type can be 
specified using the following argument:

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-r` | `--replicate` | Required | `Bio`, `Biological`, `Tec`, `Technical` | none |

Example:

```shell
dotnet CLI.dll -i rep1.bed -i rep2.bed -r tec -w 1e-4 -s 1e-8
dotnet CLI.dll -i rep1.bed -i rep2.bed -r biological -w 1e-4 -s 1e-8
```

### Weak Threshold
It sets a threshold on p-values, such that peaks with p-value between this
and stringency threshold, are considered [weak peaks](method/sets.md#weak).

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-w` | `--tauW` | Required | Double | none |

Example:

```shell
dotnet CLI.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8
```


### Stringency Threshold
It sets a threshold on p-values, where peaks with p-value lower than
this threshold, are considered [stringent](method/sets.md#stringent).

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-s` | `--tauS` | Required | Double | none |

Example:

```shell
dotnet CLI.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8
```


### Gamma
It sets the combined stringency threshold. Peaks with 
combined p-value below this threshold are [confirmed](method/sets.md#confirmed).

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-g` | `--gamma` | Optional | Double | Equal to Stringency Threshold |

Example:

```shell
dotnet CLI.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -g 1e-8
```


### C
It sets the minimum number of overlapping peaks required before MSPC
combines their p-value. For example, given three replicates (rep1, rep2 
and rep3), if `C = 3`, a peak on rep1 must overlap with at least two
peaks, one from rep2 and one from rep3, before MSPC combines their 
p-value. Otherwise, MSPC discard the peaks. If `C = 2`, a peak on rep1 
must overlap with at least one peak from either rep2 or rep3, before
MSPC combines their p-values; otherwise the peak is discarded.

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-c`  |      | Optional | Integer  | `1`           |

Example:

```shell
dotnet CLI.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -g 1e-8 -c 2
```


### Alpha
It sets the threshold for [Benjamini-Hochberg multiple testing correction](https://en.wikipedia.org/wiki/False_discovery_rate#Benjamini–Hochberg_procedure).

| Short | Long | Type | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-a`  | `--alpha` | Optional | Double |  `0.05` |

Example:

```shell
dotnet CLI.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -g 1e-8 -c 2 -a 0.05
```
