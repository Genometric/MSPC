---
title: Output
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

MSPC persists the results of each execution to a separate folder. Users can specify 
the output directory via the optional argument [`-o | --output`](cli/args#output-path); 
if not specified, MSPC creates an output directory with the following naming scheme. 

```
session_[DATE]_[TIME]

// For example:
session_20191126_222131330
```

Each output folder contains the following information: 

A log file that contains the execution log;
Consensus peaks in standard BED and MSPC format;
One folder per each replicates contains BED files containing 
[`stringent`](../method/sets#stringent), [`weak`](../method/sets#weak), 
[`background`](../method/sets#background), [`confirmed`](../method/sets#confirmed), 
[`discarded`](../method/sets#discarded), [`true-positive`](../method/sets#truepositive), 
and [`false-positive`](../method/sets#falsepositive) peaks. 
You may refer to the [Sets](../method/sets.md) page for a detailed 
description of each category.

An MSPC generated output for two replicates `rep1` and `rep2` is as the following: 

```
.
└── session_20210309_131747501
    ├── ConsensusPeaks.bed
	├── ConsensusPeaks_mspc_peaks.txt
	├── EventsLog_20210309_1317475050929.txt
    ├── rep1
    │   ├── Background.bed
    │   ├── Background_mspc_peaks.txt
    │   ├── Confirmed.bed
    │   ├── Confirmed_mspc_peaks.txt
    │   ├── Discarded.bed
    │   ├── Discarded_mspc_peaks.txt
    │   ├── FalsePositive.bed
    │   ├── FalsePositive_mspc_peaks.txt
    │   ├── Stringent.bed
    │   ├── Stringent_mspc_peaks.txt
    │   ├── TruePositive.bed
    │   ├── TruePositive_mspc_peaks.txt
    │   └── Weak.bed
    │   ├── Weak_mspc_peaks.txt
    └── rep2
        ├── Background.bed
        ├── Background_mspc_peaks.txt
        ├── Confirmed.bed
        ├── Confirmed_mspc_peaks.txt
        ├── Discarded.bed
        ├── Discarded_mspc_peaks.txt
        ├── FalsePositive.bed
        ├── FalsePositive_mspc_peaks.txt
        ├── Stringent.bed
        ├── Stringent_mspc_peaks.txt
        ├── TruePositive.bed
        ├── TruePositive_mspc_peaks.txt
        └── Weak.bed
        └── Weak_mspc_peaks.txt
```

## BED files

The `*.bed` files contain peaks in the standard 
[BED format](https://genome.ucsc.edu/FAQ/FAQformat.html#format1). 
These files contain peaks parsed from input files organized under 
the `stringent`, `weak`, `background`, `confirmed`, `discarded`, 
`true-positive` and `false-positive` groups. For example, peaks 
in a `Confirmed.bed` and `Discarded.bed` files may read is the following: 

```
$ head .\rep1\Confirmed.bed
chr     start   stop    name    -1xlog10(p-value)
chr1    32600   32680   MACS_peak_4     4.08
chr1    32726   32936   MACS_peak_5     17.5
chr1    34689   34797   MACS_peak_6     5.82
chr1    35083   35124   MACS_peak_7     4.59
chr1    38593   38836   MACS_peak_8     10.7

$ head .\rep1\Discarded.bed
chr     start   stop    name    -1xlog10(p-value)
chr1    137343  137383  MACS_peak_10    4.8
chr1    228585  228625  MACS_peak_12    4.37
chr1    265059  265115  MACS_peak_14    5.22
chr1    557793  557833  MACS_peak_29    4.16
chr1    725914  725963  MACS_peak_34    5.95
``` 

Accordingly, the peak named `MACS_peak_4 ` is _confirmed_ while 
a peak with `MACS_peak_10 ` name is _discarded_. 

## MSPC Peaks

The `*_mspc_peaks.txt` files group input peaks in different 
groups similar to the `*.bed` files. In addition, they 
contain information about the analysis performed on each peak. 
The additional information are: 

1. Combined probability of 
each peak that is X-squared of 
[Fisher’s method](https://en.wikipedia.org/wiki/Fisher%27s_method); 

2. Right-tailed probability of the X-squared 
(represented in `-Log10 (right-tailed probability)`; 

3. Benjamini–Hochberg corrected p-value (represented in 
`-Log10 (Adjusted p-value)`. 

For example, peaks corresponding to the above-mentioned 
peaks in the confirmed and discarded sets are as the following.

```
$ head .\rep1\Confirmed_mspc_peaks.txt
chr     start   stop    name    -1xlog10(p-value)       xSqrd   -1xlog10(Right-Tail Probability)        -1xlog10(AdjustedP-value)
chr1    32600   32680   MACS_peak_4     4.08    222.936 46.359  4.07
chr1    32726   32936   MACS_peak_5     17.5    284.738 59.674  16.146
chr1    34689   34797   MACS_peak_6     5.82    74.005  14.49   5.634
chr1    35083   35124   MACS_peak_7     4.59    52.867  10.042  4.537
chr1    38593   38836   MACS_peak_8     10.7    121.576 24.609  9.892

$ head .\rep1\Discarded_mspc_peaks.txt
chr     start   stop    name    -1xlog10(p-value)       xSqrd   -1xlog10(Right-Tail Probability)        -1xlog10(AdjustedP-value)
chr1    137343  137383  MACS_peak_10    4.8     22.105  4.8     NaN
chr1    228585  228625  MACS_peak_12    4.37    20.125  4.37    NaN
chr1    265059  265115  MACS_peak_14    5.22    24.039  5.22    NaN
chr1    557793  557833  MACS_peak_29    4.16    19.158  4.16    NaN
chr1    725914  725963  MACS_peak_34    5.95    27.401  5.95    NaN
```

Note that the first five columns are identical between `*.bed` 
and `*_mspc_peaks.txt` files, the columns 6, 7, and 8 are 
added in the `*_mspc_peaks.txt` files. 

In order to reproduce these results, you may run the following commands:

<Tabs
 groupId="operating-systems"
 defaultValue="win"
 values={[
  { label: 'Windows', value: 'win', },
  { label: 'Linux', value: 'linux', },
  { label: 'macOS', value: 'mac', },
 ]
}>
 <TabItem value="win">

 ```shell
 wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip; unzip demo.zip -d .
 .\mspc.exe -i .\rep*.bed -r bio -w 1e-4 -s 1e-8
 ```

 </TabItem>
 <TabItem value="linux">

 ```shell
 wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .
 ./mspc -i rep*.bed -r bio -w 1e-4 -s 1e-8
 ```

 </TabItem>
 <TabItem value="mac">

 ```shell
 wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .
 ./mspc -i rep*.bed -r bio -w 1e-4 -s 1e-8
 ```

 </TabItem>
</Tabs>




## Log File

This file contains the information, debugging messages, 
and exceptions that occurred during the execution. 
The files reads as the following: 

```
2021-03-09 15:47:22,524	[1]	INFO 	NOTE THAT THE LOG PATTERN IS: <Date> <#Thread> <Level> <Message>
2021-03-09 15:47:22,570	[1]	INFO 	Export Directory: C:\mspc\session_20210309_154722332
2021-03-09 15:47:22,572	[1]	INFO 	Degree of parallelism is set to 8.
2021-03-09 15:47:22,595	[1]	INFO 	.::...Parsing Samples....::.
2021-03-09 15:47:22,597	[1]	INFO 	   #	            Filename	Read peaks#	Min p-value	Mean p-value	Max p-value	
2021-03-09 15:47:22,597	[1]	INFO 	----	--------------------	-----------	-----------	------------	-----------	
2021-03-09 15:47:22,906	[1]	INFO 	1/2	.\rep1.bed	53,697	2.239E-074	1.085E-003	1.000E-002	
2021-03-09 15:47:23,082	[1]	INFO 	2/2	.\rep2.bed	37,717	5.370E-301	1.520E-004	9.550E-003	
2021-03-09 15:47:23,084	[1]	INFO 	.::..Analyzing Samples...::.
2021-03-09 15:47:23,093	[5]	INFO 	[1/4] Initializing
2021-03-09 15:47:23,412	[5]	INFO 	[2/4] Processing samples
...
2021-03-09 15:47:23,749	[1]	INFO 	.::....Saving Results....::.
2021-03-09 15:47:26,162	[1]	INFO 	.::..Summary Statistics..::.
2021-03-09 15:47:26,163	[1]	INFO 	   #	            Filename	Read peaks#	Background	    Weak	Stringent	Confirmed	Discarded	TruePositive	FalsePositive	
2021-03-09 15:47:26,164	[1]	INFO 	----	--------------------	-----------	----------	--------	---------	---------	---------	------------	-------------	
2021-03-09 15:47:26,178	[1]	INFO 	 1/2	                rep1	     53,697	    47.05%	  42.95%	   10.01%	   26.84%	   26.12%	      26.84%	        0.00%	
2021-03-09 15:47:26,191	[1]	INFO 	 2/2	                rep2	     37,717	    16.30%	  50.35%	   33.35%	   43.48%	   40.22%	      43.48%	        0.00%	
2021-03-09 15:47:26,192	[1]	INFO 	.::.Consensus Peaks Count.::.
2021-03-09 15:47:26,192	[1]	INFO 	17,290
2021-03-09 15:47:26,193	[1]	INFO 	Elapsed time: 00:00:03.9396445
2021-03-09 15:47:26,193	[1]	INFO 	All processes successfully finished.

```

Note that the logs are reported in the following format: 

```
<Date> <#Thread> <Level> <Message>
```

- `Message` is the description of each log entry;

- Possible values for `Level` are `INFO`, `DEBUG` and `ERR`;

- `Thread` is the number of the process thread MSPC used for 
executing each process. MSPC runs operations in parallel 
using `n` threads, where `n` is the degree of parallelism 
and reported at the beginning of the logs. It can be set 
via the [`-d | -degree-of-parallelism argument`](../cli/args#degree-of-parallelism). 
This information is useful for debugging purposes only. 



## See Also

- [Method description](method/about.md)
- [Sets description](method/sets.md)
