---
title: (A)synchronous execution
---

The MSPC `Core` offers synchronous and asynchronous analysis
of datasets. In other words: 

- If executed synchronously, the caller is blocked until the 
analysis has concluded;

- If executed asynchronously, the caller is not blocked and can continue executing other logics, meanwhile, MSPC `Core` reports
the execution status and signals when the analysis has concluded. 

:::info
Regardless of analysis invocation method, MSPC _Core_
parallelizes the analysis of datasets (multi-threaded) whose 
degree-of-parallelism can be adjusted.
:::

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

A synchronous execution is easier to implement and invoke;
however, it renders the program irresponsive during the function 
execution, which can be disadvantageous for long-running 
analysis. Therefore, MSPC `Core` also implements _asynchronous_
execution of the analysis method.


## Asynchronous Execution
Long-running functions are commonly executed asynchronously,
which keeps the (graphical or command line) interface responsive,
and allows executing other logics (e.g., show elapsed time)
while the long-running function is being executed. For instance: 

```csharp
int y = AsynchFunc(x); // function invoked and execution is 
                       // continued to the next line regardless 
                       // of the function has returned or not.
var z = y * 2;         // executed even if `AsynchFunc` is busy, 
                       // and uses default value of `y`, which is `0`. 
```

The MSPC `Core` can analyze datasets _asynchronously_, which can 
be used as the following in its basic form:

```csharp
using Genometric.MSPC.Core;

// Setup:
var mspc = new Mspc();
foreach (var sample in samples) // where `samples` is a list of parsed input datasets.
    mspc.AddSample(sample.FileHashKey, sample);


// Invoke MSPC `Core` asynchronously
mspc.RunAsync(options);
// ...
// Here implement any logic to be executed while MSPC Core is running
// ...

// Wait for MSPC's signal on analysis completion
mspc.Done.WaitOne();

// Get results
var results = mspc.GetResults();
```

While MSPC `Core` is running asynchronously, it reports 
its progress, which you can display it as the following:

```csharp
void Run()
{
    var mspc = new Mspc();
    mspc.StatusChanged += _mspc_statusChanged;

    mspc.RunAsync(_options);
    mspc.Done.WaitOne();
}

private void _mspc_statusChanged(object sender, ValueEventArgs e)
{
    Console.WriteLine(e.Value.Step + "\t" + e.Value.StepCount + "\t" + e.Value.Message);
}
```

See [MSPC CLI Orchestrator](https://github.com/Genometric/MSPC/blob/edce42ecb18e7c447396f038e03f2fd7d911d70e/CLI/Orchestrator.cs#L19-L73) 
for a complete example of running MSPC `Core` 
_asynchronously_ and reporting its status. 
