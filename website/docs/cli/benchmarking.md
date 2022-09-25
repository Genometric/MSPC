---
title: Performance
---

We continuously improve MSPC’s performance. We strive to speed up 
the runtime and decrease resource requirements with every release. 
We stress-test every release of MSPC and benchmark it against earlier 
versions to identify and resolve performance regression.

We use the following resources for benchmarking MSPC: 

- **Test data.** We use `48` randomly selected experiments from ENCODE, 
where each contains two [biological] replicates. We then call peaks 
on these samples using `MACS2` with permissive p-value threshold (cutoff at `0.0001`).
The peaks we called on the samples of this cohort are available from the 
following page: https://osf.io/jqrwu/. 
Please visit [sample data](sample_data.md) page for details and other cohorts.

- **Benchmarking scripts.** We have developed a console application written 
in C# for benchmarking different releases of MSPC
(see [this section](#benchmark-a-release-version) on how to use it).
The application is distributed along with MSPC as `benchmark.exe`, 
and its source code is available from the [MSPC/Benchmark on github](https://github.com/Genometric/MSPC/tree/dev/Benchmark).
This code currently supports releases [`v5.x`, `v4.x`, `v2.x`, and `v1.1`](https://github.com/Genometric/MSPC/blob/909dc99eecbf60646fb44d59a1646b10efef4a77/Benchmark/VersionInfo.cs#L49)
and other release will be added.

- **Jupyter Notebook for Downstream Analysis of Benchmarks.**
We have developed a Jupyter Notebook for plotting and 
in-depth analysis of the runtime. The notebook can be 
executed on Colab, and is available from 
[MSPC github page](https://github.com/Genometric/MSPC/blob/dev/Benchmark/PlotBenchmarkings.ipynb).
 
- **Our Benchmarks.** We publicly distribute the results
of running `benchmark.exe` on the aforementioned cohort
at the following page. https://osf.io/jqrwu/


## Benchmark a Released Version

The `benchmark` program takes the following arguments and 
runs every specified version of MSPC on the given cohort, 
and reports the runtime and resource usage in the output. 

- `--release`: A list of _tag_ names of public MSPC releases
(as labeled and avaialble on the [Releases page](https://github.com/Genometric/MSPC/releases));

- `--data-dir`: A directory that contains the test cohort, 
which is expected to have a structure similar to the 
following. 

  ```
  ├── ENCSR000BNU
  │   ├── ENCFF308RGN-rep1.bed
  │   └── ENCFF438DHS-rep2.bed
  ├── ENCSR000EFR
  │   ├── ENCFF276XFZ-rep2.bed
  │   └── ENCFF387NUG-rep1.bed
  ...
  ```

- `--max-rep-count`: Set the maximum number of replicates to be used for benchmarking. 
The program starts benchmarking MSPC at minimum two replicates, and iteratively increases
the number of replicates until `--max-rep-count`. If the experiment does not have the 
set number of replicates, the `benchmark` program will automatically generate synthetic 
replicates by randomly alternating the given replicates. For instance, if `--max-rep-count 4`,
`benchmark` will run the following tests for the experiment `ENCSR000BNU` (as shown in the above example):

  ```	
  # Run 1:
  $ mspc.exe -i ENCFF308RGN-rep1.bed -i ENCFF438DHS-rep2.bed
  
  # Run 2:
  $ mspc.exe -i ENCFF308RGN-rep1.bed -i ENCFF438DHS-rep2.bed \
             -i ENCFF308RGN-rep1-randomly-modified.bed

  # Run 3:
  $ mspc.exe -i ENCFF308RGN-rep1.bed -i ENCFF438DHS-rep2.bed \
             -i ENCFF308RGN-rep1-randomly-modified.bed \
             -i ENCFF438DHS-rep2-randomly-modified.bed
  ```
	
  For brevity, other required arguments of MSPC are not shown. 