---
title: About
---

MSPC `CLI.dll` is a command line interface to the MSPC method; 
it parses input datasets (e.g., BED files), invokes the method
to analyze them, and persists the results. 

The MSPC `CLI.dll` is cross-platform and can be invoked from 
any shell, such as Windows PowerShell, Linux shell, or 
Mac OS Terminal. A basic `CLI.dll` invocation takes four 
arguments (i.e., [input](cli/args.md#input), 
[replicate type](cli/args.md#replicate-type), and
[stringency](cli/args.md#stringency-threshold) and
[weak](cli/args.md#weak-threshold) thresholds), runs 
MSPC `Core`, and persists results. 
For instance, see the following sample execution: 

```shell
PS C:\code\mspc> dotnet CLI.dll -i rep1.bed -i rep2.bed -r bio -w 1E-4 -s 1E-8
Parsing sample: .\rep1.bed
Done...  ET:    00:00:00.2260588
Read peaks#:    53,697
Min p-value:    2.239E-074
Max p-value:    1.000E-002

Parsing sample: .\rep2.bed
Done...  ET:    00:00:00.1457216
Read peaks#:    37,717
Min p-value:    5.370E-301
Max p-value:    9.550E-003

Analysis started ...
[1/4] Initializing
[2/4] Processing samples
[3/4] Performing Multiple testing correction
[4/4] Creating consensus peaks set

Saving results ...

All processes successfully finished [Analysis ET: 00:00:01.5766437]


.::. Summary statistics .::.

            Filename           #Peaks       Stringent            Weak      Background       Confirmed       Discarded    TruePositive   FalsePositive
--------------------    -------------   -------------   -------------   -------------   -------------   -------------   -------------   -------------
            rep1.bed           53,697          10.01%          42.95%          47.05%          26.84%          26.12%          26.84%           0.00%
            rep2.bed           37,717          33.35%          50.35%          16.30%          43.48%          40.22%          43.48%           0.00%
--------------------    -------------   -------------   -------------   -------------   -------------   -------------   -------------   -------------

.::. Consensus Peaks Count .::.
17,290
```

In this example, the MSPC `CLI` is called using two samples 
(i.e., `-i rep1.bed -i rep2.bed`), which are considered as
biological replicates (`-r bio`) with `1E-4` and `1E-8` thresholds
on p-values defining stringent and weak peaks respectively. 


Once executed, the MSPC `CLI` reports the following: 
- For each parsed sample, it reports the number of parsed peaks,
minimum and maximum p-values parsed from the file;
- Reports analysis steps;
- Summary statistics containing the following information:
  - per-sample summary statistics that informs the number
    of analyzed peaks, and what percentage of these peaks
    are identified as *Stringent*, *Weak*, *Confirmed*,
    and etc. (see [sets description](method/sets.md)).

    > Note that the percentages reported for *Stringent*, *Weak*, 
    and *Background* sets, should add-up to `100%`;
    however, the percentage reported for *Confirmed* and
    *Discarded* is not expected to add-up to `100%` if
    the percentage of *Background* set is not `0%`. 
    Similarly, the percentages reported for `TruePositive`
    and `FalsePositive` sets will not add-up to `100%`
    if the percentage of *Background* and *Discarded* is 
    not `0%` (see [sets description](method/sets.md)).

 - Number of consensus peaks.



## See Also

- [Method description](method/about.md)
- [CLI arguments](cli/args.md)