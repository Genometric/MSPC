---
title: Instalation
---

Installing MSPC is as simple as downloading it and unzipping it.
The zip archive contains executables and all the requirement.

## Prerequisites
- Install [.NET Core 2.0 or newer](https://dotnet.microsoft.com/download).

## Install MSPC

You may install MSPC using either of the following methods: 

### Method 1: 
- Goto the [Releases page](https://github.com/Genometric/MSPC/releases);
- click on the `zip` file of the latest release to download it, the file is named as `mspc_vx.y.z.zip` where `X`, `y`, and `z` are version numbers;
- Extract the downloaded zip archive (you may extract the file to the path where it is downloaded, or extract it to path along side other tools installed on your computer).

### Method 2:

- Download latest release: 
    ```shell
    wget -O mspc.zip https://github.com/genometric/mspc/releases/latest/download/mspc_v3.3.1.zip -UseBasicParsing
    ```
    
- Unzip the archive: 
    ```shell
    unzip mspc.zip -d ./mspc/
    ```
