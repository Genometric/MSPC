---
title: Installation
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

A prerequisite for MSPC installation is [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
or newer. We provide two methods for MSPC installation depending on whether
.NET 5.0 is installed on your machine or you can install it,
or .NET 5.0 is not installed and you cannot install it, respectively
[Method A](#method-a) or [Method B](#method-b).


## Method A: Framework Dependent

First we check if .NET 5.0 is installed (not to be confused with .NET Framework), and install it if it is not, then we install MSPC.

### Install .NET 5.0
Open a command line shell (e.g., PowerShell) and run the following command:

```shell
$ dotnet --info
.NET SDK (reflecting any global.json):
 Version:   5.0.102
 Commit:    71365b4d42

Runtime Environment:
 OS Name:     Windows
 OS Version:  10.0.19042
 OS Platform: Windows
 RID:         win10-x64
 Base Path:   C:\Program Files\dotnet\sdk\5.0.102\
```

If the output is not as shown above, you would need to install
.NET 5.0 (or newer) [following these instructions](https://dotnet.microsoft.com/download/dotnet/5.0).


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

  </TabItem>
  <TabItem value="mac">

	$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/osx-x64.zip"
	$ unzip mspc.zip -d mspc

  </TabItem>
</Tabs>
