---
title: Arguments
---

## Call Example:
```shell
// minimum
dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -s 1E-8 -w 1E-4
```

## Display help text
```shell
dotnet mspc.dll -?
```


## Arguments Quick Reference

| Argument | Required | Short arg | Valid Values | Default Value |
| -------- | -------- | -------------- | ------------ | ------------- |
| [Input](#input)          | ✓ | `-i` | BED file | none |
| [Replicate Type](#replicate-type) | ✓ | `-r` | `bio`, `tec` | none |
| [Stringency threshold](#stringency-threshold) | ✓ | `-s` | `double` | none |
| [Weak threshold](#weak-threshold) | ✓ | `-w` | `double` | none |
| [Gamma](#gamma) |  | `-g` | `double` | [Stringency threshold](#stringency-threshold) |
| [C](#c) |  | `-c` | `int` | `1` |
| [Alpha](#alpha) |  | `-a` | `double` | `0.05` |
| [Multiple Intersections](#multiple-intersections) |  | `-m` | `lowest`, `highest` |  `lowest` |
| [Degree of Parallelism](#degree-of-parallelism) |  | `-d`  | `int` | Maximum allowed by the underlying scheduler |
| [Input Parser Configuration](#input-parser-configuration) |   | `-p` | File path | none |
| [Output path](#output-path) | | `-o` | Directory path | `session_` + `<Timestamp>`|
| [Exclude Header](#exclude-header) | | | | `False` (not provided) |



## Arguments
In the following we explain arguments in details. 

### Input
Sample files are listed after the `-i` or `--input` argument.

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-i` | `--input` | ✓ | BED file | none |

Example:

```shell
dotnet mspc.dll -i rep1.bed -i rep2.bed -i rep3.bed -r bio -w 1e-4 -s 1e-8
```

You may also pass the input files space-delimited:

```shell
dotnet mspc.dll -i rep1.bed rep2.bed rep3.bed -r bio -w 1e-4 -s 1e-8
```


[Wildcard characters](https://en.wikipedia.org/wiki/Wildcard_character) can be 
used to specify multiple files; for instance:

```shell
# read all the files with .bed extension as input:
$ dotnet mspc.dll -i *.bed -r bio -w 1e-4 -s 1e-8

# read multiple set of files in different directories:
$ dotnet mspc.dll -i C:\setA\*.bed -i C:\setB\sci-ATAC*.bed -r bio -w 1e-4 -s 1e-8
```

### Replicate Type
Samples could be biological or technical replicates. MSPC differentiates between 
the two replicate types based on the fact that less variations between technical 
replicates is expected compared to biological replicates. Replicate type can be 
specified using the following argument:

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-r` | `--replicate` | ✓ | `Bio`, `Biological`, `Tec`, `Technical` | none |

Example:

```shell
dotnet mspc.dll -i rep1.bed -i rep2.bed -r tec -w 1e-4 -s 1e-8
dotnet mspc.dll -i rep1.bed -i rep2.bed -r biological -w 1e-4 -s 1e-8
```

### Weak Threshold
It sets a threshold on p-values, such that peaks with p-value between this
and stringency threshold, are considered [weak peaks](method/sets.md#weak).

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-w` | `--tauW` | ✓ | Double | none |

Example:

```shell
dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8
```


### Stringency Threshold
It sets a threshold on p-values, where peaks with p-value lower than
this threshold, are considered [stringent](method/sets.md#stringent).

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-s` | `--tauS` | ✓ | Double | none |

Example:

```shell
dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8
```


### Gamma
It sets the combined stringency threshold. Peaks with 
combined p-value below this threshold are [confirmed](method/sets.md#confirmed).

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-g` | `--gamma` | Optional | Double | Equal to Stringency Threshold |

Example:

```shell
dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -g 1e-8
```


### C
It sets the minimum number of overlapping peaks required before MSPC
combines their p-value. For example, given three replicates (rep1, rep2 
and rep3), if `C = 3`, a peak on rep1 must overlap with at least two
peaks, one from rep2 and one from rep3, before MSPC combines their 
p-value; otherwise, MSPC discards the peak. If `C = 2`, a peak on rep1 
must overlap with at least one peak from either rep2 or rep3, before
MSPC combines their p-values; otherwise MSPC discards the peak.

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-c`  |      | Optional | String  | `1`           |

The value of `C` can be given in *absolute* (e.g., `C = 2` will 
require at least `2` samples) or *percentage* of input samples 
(e.g., `C = 50%` will require at least `50%` of input samples) formats.

The minimum value of `C` is `1`. If a value less than `1` is given
(e.g., `C = 0`, `C = 0%`, or `C = -1`), MSPC automatically sets it
to `1` (i.e., `C = 1`).

Example:

```shell
dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -c 2

dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -c 50%
```

Note, you do not need to enclose a value for `C` in `"` to represent 
it as a string; the values are automatically considered as string type 
objects. In other words, you do not need to enter the value as `C "3"`.


### Alpha
It sets the threshold for [Benjamini-Hochberg multiple testing correction](https://en.wikipedia.org/wiki/False_discovery_rate#Benjamini–Hochberg_procedure).

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-a`  | `--alpha` | Optional | Double |  `0.05` |

Example:

```shell
dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -a 0.05
```

### Multiple Intersections
When multiple peaks from a sample overlap with a given peak,
this argument defines which of the peaks to be considered:
the one with lowest p-value, or the one with highest p-value? 

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-m`  | `--multipleIntersections` | Optional | `Lowest`, `Highest` |  `Lowest` |

Example:

```shell
dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -m lowest
```

### Degree of Parallelism
It sets the number of parallel threads MSPC can utilize simultaneously when processing data.

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-d`  | `--degreeOfParallelism` | Optional | `int` |  Number of logical processors on the current machine |

Example:

```shell
dotnet mspc.dll -i rep1.bed -i rep2.bed -r bio -w 1e-4 -s 1e-8 -d 12
```

### Input Parser Configuration 

Sets the path to a JSON file containing the configuration 
for the input BED file parser.

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-p`  | `--parser` | Optional | File path | none |

Refer to [this page](cli/parser.md) on how to configure the input parser
using a JSON object.

### Output Path

Sets the path in which analysis results should be persisted.
If it is not given, the default folder is name `session_` + `<Timestamp>`.
If a given folder name already exists, and is not empty, MSPC 
will append `_n` where `n` is an integer until no duplicate is 
found. See the [Output](output) page on the contents of 
this folder.

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
| `-o`  | `--output` | Optional | Directory path | `session_` + `<Timestamp>`|

### Exclude Header

This is a flag (i.e., it does not require any value), 
if provided, MSPC does not add a header to the files 
it generates. If not provided (default), MSPC will add
a header to all the files it generates.

| Short | Long | Required | Valid values | Default value |
| ----- | ---- | ---- | ------------ | ------------- |
|       | `--excludeHeader` | Optional |  | `False` (not provided) |

Example:

```shell
dotnet mspc.dll --excludeHeader
dotnet mspc.dll --excludeHeader true
dotnet mspc.dll --excludeHeader false
```