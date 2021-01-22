---
title: Installation
---

A prerequisite for MSPC installation is [.NET Core 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0)
or newer. We provide two methods for MSPC installation depending on whether
.NET Core 3.0 is installed on your machine or you can install it,
or .NET Core 3.0 is not installed and you cannot install it, respectively
[Method A](#method-a) or [Method B](#method-b).


## Method A: Framework Dependent

First we check if .NET Core 3.0 is installed (not to be confused with .NET Framework), and install it if it is not, then we install MSPC.

### Install .NET Core 3.0
Open a command line shell (e.g., PowerShell) and run the following command:

```shell
$ dotnet --info
.NET Core SDK (reflecting any global.json):
 Version:   3.0.100
 Commit:    04339c3a26

Runtime Environment:
 OS Name:     Windows
 OS Version:  10.0.18362
 OS Platform: Windows
 RID:         win10-x64
 Base Path:   C:\Program Files\dotnet\sdk\3.0.100\
```

If the output is not as shown above, you would need to install
.NET Core 3.0 (or newer) [following these instructions](https://dotnet.microsoft.com/download/dotnet-core/3.0).


### Install MSPC

You may install MSPC using either of the following methods:
- goto [this page](https://github.com/Genometric/MSPC/releases/latest) and download `mspc.zip`
and extract it to a path on your computer;
- type the following command in your command line shell:

	```shell
	$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/mspc.zip"
	$ unzip mspc.zip -d mspc
	```


## Method B: Self-Contained

Install MSPC using either of the following commands depending on your runtime:

- Windows x64:

	```shell
	$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/win-x64.zip"
	$ unzip mspc.zip -d mspc
	```

- Linux x64:

	```shell
	$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/linux-x64.zip"
	$ unzip mspc.zip -d mspc
	```

- macOS x64:

	```shell
	$ wget -O mspc.zip "https://github.com/Genometric/MSPC/releases/latest/download/osx-x64.zip"
	$ unzip mspc.zip -d mspc
	```
