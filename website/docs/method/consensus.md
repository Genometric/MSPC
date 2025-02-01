---
title: Consensus peaks
---

A consensus peak is created on a position of genome where is covered by at least one peak from the `output` sets of the processed replicates. Accordingly, a consensus peak has the following characteristics:

- **Coordinate** (i.e., _chromosome_, _start_, and _stop_) <br/>
The **coordinate** of a consensus peak is the union of the coordinates of the peaks from `output` sets which overlap that position on the genome. For instance: 

      // From the Output set of Replicate 1 
      chr1    10    20    macs_peak_1    20.9

      // From the Output set of Replicate 2 
      chr1    15    25    macs_peak_1    14.6

      // The resulting consensus peak 
      chr1    10    25    mspc_peak_1    163.484
    Note that the p-values are in `-log10` format. 
- **Value** <br/>
The value of each consensus peak is `X^2` which is calculated by combining the `p-value`s of the overlapping peaks using [Fisher's combined probability test](https://en.wikipedia.org/wiki/Fisher%27s_method):

      ```
      X_i^2 = -2 \times \sum_{i=1}^k \ln(p_i)
      ```

where `X_i^2` is the value of consensus peak `i`, and `p_i` is the p-value of overlapping peaks. For instance, the `X^2` of aforementioned consensus peaks is `163.484`, which is calculated by combining the p-values of overlapping peaks (i.e., `20.9` and `14.6`) using the [Fisher's method](https://en.wikipedia.org/wiki/Fisher%27s_method).