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
Parsing sample: rep1.bed
Done...  ET:    00:00:00.7992607
Read peaks#:    53,697
Min p-value:    2.239E-074
Max p-value:    1.000E-002

Parsing sample: rep2.bed
Done...  ET:    00:00:00.1976851
Read peaks#:    37,717
Min p-value:    5.370E-301
Max p-value:    9.550E-003

Analysis started ...
[1/4] Initializing
[2/4] Processing samples
[3/4] Performing Multiple testing correction
[4/4] Creating consensus peaks set

Saving results ...

All processes successfully finished [Analysis ET: 00:00:04.3618518]
```

In this example, the MSPC `CLI.dll` is invocated using two samples 
(i.e., `-i rep1.bed -i rep2.bed`), which are to be considered 
biological replicates (`-r bio`) with `1E-4` and `1E-8` thresholds
on p-values defining stringent and weak peaks respectively. 


## See Also

- [Method description](method/about.md)
- [CLI arguments](cli/args.md)