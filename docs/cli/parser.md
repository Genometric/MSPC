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

The fifth column represents the p-value, and by default, MSPC expects
it be in `-Log10` format (read [how to adjust this configuration](#p-value-format). 
Also, by default, MSPC expects each peak 
to have a p-value; otherwise, that peak will not be parsed into MSPC.

A BED file may have additional columns; however, the content of
those columns will not be parsed into MSPC.

The [GeUtilities](https://github.com/Genometric/GeUtilities)
parser is highly customizable that allows parsing BED files
represented differently. The following JSON object contains
all the configuration attributes, each discussed in details
in the following sections.

```json
{
   "Chr":0,
   "Left":1,
   "Right":2,
   "Name":3,
   "Strand":4,
   "Summit":5,
   "Value":6,
   "PValueFormat":1,
   "DefaultValue":0.0001,
   "DropPeakIfInvalidValue":true
}
```


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
using [`--parser` argument](cli/args.md#input-parser-configuration).

### p-Value format

By default, MSPC expects p-values in a BED file to 
represented in `-log10(p-value)` format. However, 
some tools produce p-values in `-10log10(p-value)`, 
`-100log10(p-value)`, or actual p-value (without 
`log10` scale). To set MSPC `CLI` to correctly
parse peaks according to their p-values representation,
use the `"PValueFormat"` attribute in 
the parser configuration JSON object according to 
the following table:

| Format                | JSON attribute       | Example p-value from BED file | Parsed p-value |
| :-------------------- | :------------------- | :---------------------------- | :------------- |
| Same as input         | `"PValueFormat":0` | `0.001`   | `0.001` |
| `-log10(p-value)`    | `"PValueFormat":1` | `3`       | `0.001` |
| `-10log10(p-value)`  | `"PValueFormat":2` | `30`      | `0.001` |
| `-100log10(p-value)` | `"PValueFormat":3` | `300`     | `0.001` |


(See [p-value formats](https://github.com/Genometric/GeUtilities/blob/30bb4691fc2ad37eda6131c6e3f3714c5464dbb4/GeUtilities/Intervals/Parsers/Bed/Enums.cs#L7])
of GeUtilities.)
