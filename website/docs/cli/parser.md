---
title: Parser Configuration
---

MSPC uses [GeUtilities](https://github.com/Genometric/GeUtilities) 
to parse BED files. Out of box, MSPC uses the default configuration
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
it be in `-Log10` format (read [how to adjust this configuration](#p-value-format)). 
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
   "Value":4,
   "Strand":-1,
   "Summit":-1,
   "Culture":"en-US",
   "PValueFormat":1,
   "DefaultValue":0.0001,
   "DropPeakIfInvalidValue":true
}
```


## Column Order

A BED file is a plain-text and tab-delimited file, it has multiple 
columns, and the type of data in each column follows widely adopted
standards with a number of variations. In order to correctly parse
different formats of BED files (standard or non-standard), without
requiring the users to convert them to a common format, MSPC allows
users to configure its parser by specifying the number of columns
that contain required information.


For instance, if your samples are stranded, you may 
configure the parser as the following. 

```
chr1	633859	634162	peak_1	137	.
chr1	1079427	1079669	peak_2	67	.
chr1	1109848	1110187	peak_3	91	.
```

Create a JSON file as the following:

```json
{"Strand": 5}
```

and execute MSPC as the following passing the 
[parser configuration](args#input-parser-configuration) :

```shell
mspc.exe -i inputs*.bed -r bio -w 1e-4 -s 1e-8 -p parser_config.json
```

where `parser_config.json` is the filename of the file
containing the JSON object you created above.


## p-Value Format

By default, MSPC expects p-values in a BED file to 
represented in `-log10(p-value)` format. However, 
some tools produce p-values in `-10log10(p-value)`, 
`-100log10(p-value)`, or actual p-value (without 
`log10` scale). To set MSPC to correctly
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


## Peaks with Missing or Invalid p-Values

Some BED files may not a valid p-value for some
or none of the peaks in that file; for instance,
BED files generated per cell in single-cell 
assays do not commonly provide a p-value
per peak. Hence, following cases are possible:

- missing p-value for some peaks:
    ```
    chr1	9999	10039	MACS_peak_1	2.42
    chr1	10101	10190	MACS_peak_2	3.23
    chr1	29303	29382	MACS_peak_3
    ```

- none of the peaks have a p-value (commonly in 
single-cell assays):
    ```
    chr1	9999	10039
    chr1	10101	10190
    chr1	29303	29382
    ```

By default, MSPC drops all the peaks that 
do not have a valid p-value. To set MSPC
to read such peaks, use the following 
attribute in parser configuration JSON 
object: 

```json
"DropPeakIfInvalidValue":false
```

With this configuration, MSPC sets the p-value
of peaks with invalid/missing p-value to `1E-8`
(see [this initialization](https://github.com/Genometric/GeUtilities/blob/30bb4691fc2ad37eda6131c6e3f3714c5464dbb4/GeUtilities/Intervals/Parsers/Bed/BedParserGeneric.cs#L89)).
To change the default p-value, use the following
attribute in parser configuration JSON
object: 

```json
"DefaultValue":0.0001
```

Hence, to read the previous example, 
set the parser configuration JSON object as 
the following:

```json
{
   "Chr":0,
   "Left":1,
   "Right":2,
   "DefaultValue":0.0001,
   "DropPeakIfInvalidValue":true
}
```

## Culture info
Numbers can be formatted differently depending on the operating 
system's culture (or locale) settings. For instance, different 
cultures use _dot_ (`.`), _comma_ (`,`), or _forward slash_ 
(`/`) characters as decimal separators. Accordingly, the following 
numbers formatted following different cultures are equal:

```
- 1.234
- 1,234
- 1/234
```

MSPC parses numbers according to the culture setting of the 
execution environment. For instance, on a operating system 
with its _region_ set to US, MSPC considers _dot_ (`.`) as 
a decimal separator.

In some scenarios input data format may not adhere with the 
culture setting of the operating system. For instance, a scientist
in Europe (where a _comma_ character (`,`) is used as decimal separator) 
analyzing data generated by a collaborator from US (where a _dot_ 
character('.') is used as decimal separator). For such scenarios
you may explicitly specify the culture info of the input data 
using the `Culture` attribute in parser configuration json file. 
For instance, the following configuration sets MSPC to read 
numbers formatted in US style independent from culture setting 
of the operating system: 

```json
{
   "Chr":0,
   "Left":1,
   "Right":2,
   "Name":3,
   "Value":4,
   "Culture":"en-US"
}
```

You may refer to the `Language tag` column in the list of language and region
names supported by Windows available in 
[this page](https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c).

:::info
The numbers written to the output files generated by MSPC are
formatted according to the operating system's settings, independent 
from the culture setting provided for the input parser.
:::
