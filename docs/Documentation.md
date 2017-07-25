### Call Example
{{
// on Windows
MSPC -i rep1.bed -i rep2.bed -i rep3.bed -r biological -s 1E-8 -w 1E-4

//on Linux/Mac
mono MSPC -i rep1.bed -i rep2.bed -i rep3.bed -r biological -s 1E-8 -w 1E-4
}}


# Requirements

* Microsoft Windows users: Update to [.NET 4.5](http://www.microsoft.com/en-sg/download/details.aspx?id=30653) or later.
* Mac and Linux users: Download and install [Mono](http://www.mono-project.com) and remember to invoke it before our MSPC command.

# Input
A BED file for each replicate, containing enriched regions (i.e. peaks) called with a permissive p-value threshold. The columns of the BED files should comply the field order defined on Ensembl: (1) chrom, (2) chromStart, (3) chromEnd, (4) name, and (5) score (p-value). The p-value needs to be in **-Log10(p-value)** format. More columns can be present, but they are not considered.

# Output
MSPC outputs different classifications of peaks (e.g., stringent, weak, stringent-confirmed, weak-discarded) into merged and separated BED files. An output BED file contains all parsed information of a peak (i.e., chr, chromStart, chromEnd, name, and p-value) plus the combined significance and corresponding right-tail probability. An example is as follows: 

from input:
{{
chr1 32600 32680 MACS_peak_4 4.08
}}
in the output:
{{
chr1 32600 32680 MACS_peak_4 4.08 222.936 46.373
}}


# Arguments
You may consider following table for quick start, a detailed description of arguments are available at [this page](Arguments-in-details).

|| Argument || Type || short argument || long argument || valid values || default value ||
| Input | Required | -i | -input | BED file | none |
| Replicate Type | Required | -r | -replicate | Bio, Biological, Tec, Technical | none |
| Stringency threshold | Required | -s | -tauS | Double | none |
| Weak threshold | Required | -w | -tauW | Double | none |
| Gamma (combined stringency threshold) | Optional | -g | -gamma | Double | Stringency threshold |
| C (minimum required overlapping peaks) | Optional | -c | none | Integer | 1 |
| Alpha (BH multiple testing correction threshold) | Optional | -a | -alpha | Double | 0.05 |
 

 

.

 