---
title: (A)synchronous execution
---

The MSPC `Core` offers synchronous and asynchronous analysis
of datasets. In other words: 

- If executed synchronously, the caller is blocked until the 
analysis has concluded;

- If executed asynchronously, the caller is not blocked and can 
continue executing other logics, meanwhile, MSPC `Core` reports
the execution status, and signals when the analysis has 
concluded. 

> Note that regardless of analysis invocation method, MSPC `Core`
parallelizes the analysis of datasets (multi-threaded) whose 
degree-of-parallelisim can be adjusted.

Both methods are discussed in details in the following sections. 

## Synchronous Execution

A synchronous execution is the common function invocation, where 
the command after function call is not executed until the called 
function returns. For instance: 

```csharp
var y = Func(x); // Blocked here until Func(x) returns
var z = y * 2;   // Not executed until Func(x) returns
```

The MSPC `Core` can analyze datasets _synchronously_, which 
can be invoked as the following:

```csharp
using Genometric.MSPC.Core;

// Setup:
var mspc = new Mspc();
foreach (var sample in samples) // where `samples` is a list of parsed input datasets.
	mspc.AddSample(sample.FileHashKey, sample);

// Method 1:
var results = mspc.Run(options);

// Method 2:
mspc.Run(options);
var results = mspc.GetResults();
```

