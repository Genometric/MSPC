---
title: Parser Configuration
---

MSPC `CLI` uses [GeUtilities](https://github.com/Genometric/GeUtilities) 
to parse BED files. Out of box, MSPC `CLI` uses the default configuration
of [GeUtilities](https://github.com/Genometric/GeUtilities); accordingly, 
it expects an input BED file to be in format similar to the following 
example:

```
chr1	9999	10039	MACS_peak_1	2.42
chr1	10101	10190	MACS_peak_2	3.23
chr1	29303	29382	MACS_peak_3	2.44
chr1	32600	32680	MACS_peak_4	4.08
chr1	32726	32936	MACS_peak_5	17.5
chr1	34689	34797	MACS_peak_6	5.82
chr1	35083	35124	MACS_peak_7	4.59
```

The fifth column represents the p-value, and MSPC expects it be in 
`-Log10` format. Also, by default, MSPC expects each peak to have 
a p-value; otherwise, that peak will not be parsed into MSPC.

A BED file may have additional columns; however, the content of
those columns will not be parsed into MSPC.

The [GeUtilities](https://github.com/Genometric/GeUtilities)
parser is highly customizable that allows parsing BED files
represented differently. In the following we explain on how
to setup the parser. 


### Column Order

A BED file is a plain-text and tab-delimited file, it has multiple 
columns, and the type of data in each column follows widely adopted
standards with a number of variations. In order to correctly parse
different formats of BED files (standard or non-standard), without
requiring the users to convert them to a common format, MSPC allows
users to configure its parser by specifying the number of columns
that contain required information.

To specify column order, create a plain text file with the 
following content: 

```json
{  
   "Chr":0,
   "Left":1,
   "Right":2,
   "Strand":4,
   "Name":3,
   "Value":4,
   "Summit":5
}
```

Change the values of each configuration key according 
to your BED file. Then save this file with any name 
(e.g., `myConfig.json`), and give its path to MSPC 
using [`--parser` argument](cli/args.md).







