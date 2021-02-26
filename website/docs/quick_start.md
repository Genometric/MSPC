---
title: Quick Start
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

## Preparation

1. Install a self-contained release of MSPC, using either of following commands
depending on your runtime (see [installation](installation.md) page for detailed
installation options):

<Tabs
 groupId="operating-systems"
 defaultValue="win"
 values={[
  { label: 'Windows', value: 'win', },
  { label: 'Linux', value: 'linux', },
  { label: 'macOS', value: 'mac', },
 ]
}>
 <TabItem value="win">

 ```shell
 wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/win-x64.zip
 ```

 </TabItem>
 <TabItem value="linux">

 ```shell
 wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/linux-x64.zip
 ```

 </TabItem>
 <TabItem value="mac">

 ```shell
 wget -O mspc.zip https://github.com/Genometric/MSPC/releases/latest/download/osx-x64.zip
 ```

 </TabItem>
</Tabs>

2. Extract the downloaded archive and browse to the containing directory:

<Tabs
 groupId="operating-systems"
 defaultValue="win"
 values={[
  { label: 'Windows', value: 'win', },
  { label: 'Linux', value: 'linux', },
  { label: 'macOS', value: 'mac', },
 ]
}>
 <TabItem value="win">

 ```shell
 unzip mspc.zip -d mspc; cd mspc
 ```

 </TabItem>
 <TabItem value="linux">

 ```shell
 unzip mspc.zip -d mspc && cd mspc
 ```

 </TabItem>
 <TabItem value="mac">

 ```shell
 unzip mspc.zip -d mspc && cd mspc
 ```

 </TabItem>
</Tabs>

   Notice that if you are working on Windows x64, you will need to download the program unzip.

3. Download sample data:

<Tabs
 groupId="operating-systems"
 defaultValue="win"
 values={[
  { label: 'Windows', value: 'win', },
  { label: 'Linux', value: 'linux', },
  { label: 'macOS', value: 'mac', },
 ]
}>
 <TabItem value="win">

 ```shell
 wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip; unzip demo.zip -d .
 ```

 </TabItem>
 <TabItem value="linux">

 ```shell
 wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .
 ```

 </TabItem>
 <TabItem value="mac">

 ```shell
 wget -O demo.zip https://github.com/Genometric/Annotations/raw/master/SampleFiles/demo.zip && unzip demo.zip -d .
 ```

 </TabItem>
</Tabs>

## Run

To run MSPC use the following command depending on your runtime:

<Tabs
 groupId="operating-systems"
 defaultValue="win"
 values={[
  { label: 'Windows', value: 'win', },
  { label: 'Linux', value: 'linux', },
  { label: 'macOS', value: 'mac', },
 ]
}>
 <TabItem value="win">

 ```shell
 .\mspc.exe -i .\rep1.bed -i .\rep2.bed -r bio -w 1e-4 -s 1e-8
 ```

 </TabItem>
 <TabItem value="linux">

 ```shell
 ./mspc.dll -i .\rep1.bed -i .\rep2.bed -r bio -w 1e-4 -s 1e-8
 ```

 </TabItem>
 <TabItem value="mac">

 ```shell
 ./mspc.dll -i .\rep1.bed -i .\rep2.bed -r bio -w 1e-4 -s 1e-8
 ```

 </TabItem>
</Tabs>

## Output

MSPC creates a folder in the current execution path named `session_X_Y`, where `X` and `Y` are execution date and time respectively, which contains the following files and folders:

```bash
.
└── session_20191126_222131330
    ├── ConsensusPeaks.bed
	├── ConsensusPeaks_mspc_peaks.txt
	├── EventsLog_20191126_2221313409928.txt
    ├── rep1
    │   ├── Background.bed
    │   ├── Background_mspc_peaks.txt
    │   ├── Confirmed.bed
    │   ├── Confirmed_mspc_peaks.txt
    │   ├── Discarded.bed
    │   ├── Discarded_mspc_peaks.txt
    │   ├── FalsePositive.bed
    │   ├── FalsePositive_mspc_peaks.txt
    │   ├── Stringent.bed
    │   ├── Stringent_mspc_peaks.txt
    │   ├── TruePositive.bed
    │   ├── TruePositive_mspc_peaks.txt
    │   └── Weak.bed
    │   ├── Weak_mspc_peaks.txt
    └── rep2
        ├── Background.bed
        ├── Background_mspc_peaks.txt
        ├── Confirmed.bed
        ├── Confirmed_mspc_peaks.txt
        ├── Discarded.bed
        ├── Discarded_mspc_peaks.txt
        ├── FalsePositive.bed
        ├── FalsePositive_mspc_peaks.txt
        ├── Stringent.bed
        ├── Stringent_mspc_peaks.txt
        ├── TruePositive.bed
        ├── TruePositive_mspc_peaks.txt
        └── Weak.bed
        └── Weak.bed
```

## See Also

- [Welcome page](welcome.md)
- [Input file format](cli/input.md)
- [Output files](cli/output.md)
