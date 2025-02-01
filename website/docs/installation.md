---
title: Installation
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

MSPC can be used as command-line application, a C# library 
([distributed via nuget](https://www.nuget.org/packages/Genometric.MSPC.Core)), 
or an R package 
([distributed via Bioconductor](https://bioconductor.org/packages/release/bioc/html/rmspc.html)). 
This page documents installing MSPC as a command-line application, for 
installing it as a C# library, please refer to [this page](library/install), 
or [Bioconductor user guide](https://bioconductor.org/packages/release/bioc/vignettes/rmspc/inst/doc/rmpsc.html)
for installing/using it in R programming language.


A prerequisite for MSPC installation is [.NET 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
or newer. We provide two methods for MSPC installation depending on whether
.NET 9.0 is installed on your machine or you can install it,
or .NET 9.0 is not installed and you cannot install it, respectively
[Method A](#method-a) or [Method B](#method-b).


## Method A: Framework Dependent

First we check if .NET 9.0 is installed (not to be confused with .NET Framework), and install it if it is not, then we install MSPC.

### Install .NET 9.0
Open a command line shell (e.g., PowerShell) and run the following command:

```shell
$ dotnet --info
.NET SDK:
 Version:           9.0.102
 Commit:            cb83cd4923
 Workload version:  9.0.100-manifests.4a54b1a6
 MSBuild version:   17.12.18+ed8c6aec5
```

If the output is not as shown above, you would need to install
.NET 9.0 (or newer) [following these instructions](https://dotnet.microsoft.com/download/dotnet/9.0).


### Install MSPC

You may install MSPC using either of the following methods:
- Goto [this page](https://github.com/Genometric/MSPC/releases/latest) and download `mspc.zip`
and extract it to a path on your computer;
- Type the following command in your command line shell:

	```shell
	$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/mspc.zip"
	$ unzip mspc.zip -d mspc
	```


## Method B: Self-Contained

Install MSPC using either of the following commands depending on your runtime:


<Tabs
  defaultValue="win"
  values={[
    {label: 'Windows x64', value: 'win'},
    {label: 'Linux x64', value: 'linux'},
    {label: 'macOS x64', value: 'mac'},
  ]}>
  <TabItem value="win">

	$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/win-x64.zip"
	$ unzip mspc.zip -d mspc
  
  </TabItem>
  <TabItem value="linux">

	$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/linux-x64.zip"
	$ unzip mspc.zip -d mspc
	$ cd mspc
	$ chmod 777 mspc

  </TabItem>
  <TabItem value="mac">

	$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/osx-x64.zip"
	$ unzip mspc.zip -d mspc
	$ cd mspc
	$ chmod 777 mspc

  </TabItem>
</Tabs>
