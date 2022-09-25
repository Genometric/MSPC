---
title: Sample Data
---

:::info
The datasets used for testing and benchmarking MSPC, and 
benchmarking results are available from _Open Science Framework (OSF)_
at the following link.

https://osf.io/jqrwu/
:::

We use data publicly available from ENCODE to test and benchmark 
MSPC. This page outlines the specific experiments, our peak
calling steps, and links to download the peaks we called and used
for testing and benchmarking MSPC.

### Dataset v2
 
We benchmark MSPC v5 using a cohort containing `48` randomly selected 
experiments from ENCODE. We call peaks on the samples in each 
experiment using `MACS2` with a permissive threshold as the 
following `--auto-bimodal -p 0.0001 -g hs`. This threshold 
will result in a decreased number of false negatives, with the 
penalty of an increased number of false positives. We will then 
reduce the number of false positives while keeping 
a low rate of false negatives, leveraging combined statistical 
evidence from replicates (see the [methods page](method/about)).
We use this cohort for testing MSPC v5.

The peaks we called on this cohort are available from the 
following page: 

https://osf.io/jqrwu/


### Dataset v1

We benchmarked the [first version of MSPC](https://academic.oup.com/bioinformatics/article/31/17/2761/183989)
using the dataset v1, which contains `7` experiments selected from ENCODE.
We called peaks on this dataset using MACS2 with `--auto-bimodal -p 0.0001 -g hs`,
and the called peaks are available from the following page: 

https://osf.io/jqrwu/

The following is the list of the BAM files of the samples in this dataset. 

- [wgEncodeOpenChromChipK562CmycAlnRep1.bam](http://hgdownload.cse.ucsc.edu/goldenpath/hg19/encodeDCC/wgEncodeOpenChromChip/wgEncodeOpenChromChipK562CmycAlnRep1.bam) (412 MB);
- [wgEncodeOpenChromChipK562CmycAlnRep2.bam](http://hgdownload.cse.ucsc.edu/goldenpath/hg19/encodeDCC/wgEncodeOpenChromChip/wgEncodeOpenChromChipK562CmycAlnRep2.bam) (286 MB);
- [wgEncodeOpenChromChipK562CmycAlnRep3.bam](http://hgdownload.cse.ucsc.edu/goldenpath/hg19/encodeDCC/wgEncodeOpenChromChip/wgEncodeOpenChromChipK562CmycAlnRep3.bam) (326 MB);
- [wgEncodeSydhTfbsK562CmycIggrabAlnRep1.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIggrabAlnRep1.bam) (390 MB);
- [wgEncodeSydhTfbsK562CmycIggrabAlnRep2.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIggrabAlnRep2.bam) (528 MB);
- [wgEncodeSydhTfbsK562CmycStdAlnRep1.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycStdAlnRep1.bam) (220 MB);
- [wgEncodeSydhTfbsK562CmycStdAlnRep2.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycStdAlnRep2.bam) (209 MB);
- [wgEncodeSydhTfbsK562CmycIfna6hStdAlnRep1.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfna6hStdAlnRep1.bam) (386 MB);
- [wgEncodeSydhTfbsK562CmycIfna6hStdAlnRep2.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfna6hStdAlnRep2.bam) (832 MB);
- [wgEncodeSydhTfbsK562CmycIfna30StdAlnRep1.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfna30StdAlnRep1.bam) (417 MB);
- [wgEncodeSydhTfbsK562CmycIfna30StdAlnRep2.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfna30StdAlnRep2.bam) (723 MB);
- [wgEncodeSydhTfbsK562CmycIfng6hStdAlnRep1.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfng6hStdAlnRep1.bam) (555 MB);
- [wgEncodeSydhTfbsK562CmycIfng6hStdAlnRep2.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycIfng6hStdAlnRep2.bam) (833 MB);
- [wgEncodeSydhTfbsK562CmycStdAlnRep1.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycStdAlnRep1.bam) (221 MB);
- [wgEncodeSydhTfbsK562CmycStdAlnRep2.bam](ftp://hgdownload.cse.ucsc.edu/goldenPath/hg19/encodeDCC/wgEncodeSydhTfbs/wgEncodeSydhTfbsK562CmycStdAlnRep2.bam) (209 MB).
