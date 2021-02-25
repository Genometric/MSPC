---
title: About
---

MSPC `mspc.dll` is a command line interface to the MSPC method; 
it parses input datasets (e.g., BED files), invokes the method
to analyze them, and persists the results. 

The `mspc.dll` is a cross-platform command-line application, and 
can be invoked from any shell, such as Windows PowerShell, Linux 
shell, or Mac OS Terminal. A basic `mspc.dll` invocation takes four 
arguments (i.e., [input](cli/args.md#input), 
[replicate type](cli/args.md#replicate-type), and
[stringency](cli/args.md#stringency-threshold) and
[weak](cli/args.md#weak-threshold) thresholds), runs 
MSPC `Core`, and persists results. 
For instance, see the following sample execution: 

```shell
$ dotnet .\mspc.dll -i rep1.bed -i rep2.bed \
                    -r bio -w 1e-4 -s 1e-8

```

Output:
```shell
.::........Parsing Samples.........::.
     #                Filename    Read peaks#     Min p-value     Mean p-value    Max p-value
----    --------------------    -----------     -----------     ------------    -----------
 1/2                    rep1         53,697      2.239E-074       1.085E-003     1.000E-002
 2/2                    rep2         37,717      5.370E-301       1.520E-004     9.550E-003

.::.......Analyzing Samples........::.

[1/4] Initializing
[2/4] Processing samples
  └── 60,004/60,004     (100.00%) peaks processed
[3/4] Performing Multiple testing correction
[4/4] Creating consensus peaks set

.::.........Saving Results.........::.


.::.......Summary Statistics.......::.

   #                Filename    Read peaks#     Background          Weak        Stringent       Confirmed       Discarded       TruePositive    FalsePositive
----    --------------------    -----------     ----------      --------        ---------       ---------       ---------       ------------    -------------
 1/2                    rep1         53,697         47.05%        42.95%           10.01%          26.84%          26.12%             26.84%            0.00%
 2/2                    rep2         37,717         16.30%        50.35%           33.35%          43.48%          40.22%             43.48%            0.00%

.::.....Consensus Peaks Count......::.

17,290

All processes successfully finished
```

In this example, MSPC is called using two samples 
(i.e., `-i rep1.bed -i rep2.bed`), which are considered as
biological replicates (`-r bio`) with `1E-4` and `1E-8` thresholds
on p-values defining stringent and weak peaks respectively. 


Once executed, MSPC reports the following: 
- For each parsed sample, it reports the number of parsed peaks,
minimum and maximum p-values parsed from the file;
- Reports analysis steps;
- Summary statistics containing the following information:
  - per-sample summary statistics that informs the number
    of analyzed peaks, and what percentage of these peaks
    are identified as *Stringent*, *Weak*, *Confirmed*,
    and etc. (see [sets description](method/sets.md)).

    :::info
    The percentages reported for *Stringent*, *Weak*, and *Background* 
    sets, should add-up to _100%_; however, the percentage reported for 
    *Confirmed* and *Discarded* is not expected to add-up to _100%_ if 
    the percentage of *Background* set is not _0%_. Similarly, the 
    percentages reported for **TruePositive** and **FalsePositive** 
    sets will not add-up to _100%_ if the percentage of *Background* 
    and *Discarded* is not _0%_ (see [sets description](method/sets.md)).
    :::

 - Number of consensus peaks.



## See Also

- [Method description](method/about.md)
- [CLI arguments](cli/args.md)
