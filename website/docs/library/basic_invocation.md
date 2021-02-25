---
title: Basic Invocation
---

In general, a basic invocation on MSPC `Core` consists of three steps: 
1. Parse input samples;
2. Setup a `Config` object, specifying basic analysis parameters;
3. Initialize an instance of `Mspc` class, add all input samples, and call `Run` function.

```csharp
// First;
// parse input samples using Genometric.GeUtilities
// available on NuGet:  https://www.nuget.org/packages/Genometric.GeUtilities/
// you may use a code similar to the following:
var bedParser = new BedParser();
var sample = bedParser.Parse(fileName);

// Second;
// Configure options
var config = new Config(
    c: 2,
    tauW: 1E-4,
    tauS: 1E-8,
    gamma: 1E-8,
    alpha: 0.05F,
    replicateType: ReplicateType.Biological,
    multipleIntersections: MultipleIntersections.UseLowestPValue);

//
// Third;
// Initialize and call MSPC Core; you may use 
// a code similar to the following:
var mspc = new Mspc();
mspc.AddSample(sample.FileHashKey, sample);
var results = mspc.Run(config);
```


## Remarks
The afore-mentioned example uses **conceret** versions of the 
`BedParser` and `Mspc` class:

- [`BedParser`](https://github.com/Genometric/GeUtilities/blob/30bb4691fc2ad37eda6131c6e3f3714c5464dbb4/GeUtilities/Intervals/Parsers/Bed/BedParser.cs#L10-L17)
parses each peak in the given input sample file as a 
[`Peak`](https://github.com/Genometric/GeUtilities/blob/30bb4691fc2ad37eda6131c6e3f3714c5464dbb4/GeUtilities/Intervals/Model/Peak.cs#L9-L54) 
object, and returns a [`Bed`](https://github.com/Genometric/GeUtilities/blob/30bb4691fc2ad37eda6131c6e3f3714c5464dbb4/GeUtilities/Intervals/Parsers/Model/Bed.cs#L9-L15)
object representing the sample file;
- [`Mspc`](https://github.com/Genometric/MSPC/blob/be51df1fa2f37a0ded44cc8b5769864fd8c75bc9/Core/Mspc.cs#L9-L13)
processes [`Peak`](https://github.com/Genometric/GeUtilities/blob/30bb4691fc2ad37eda6131c6e3f3714c5464dbb4/GeUtilities/Intervals/Model/Peak.cs#L9-L54) 
objects in a [`Bed`](https://github.com/Genometric/GeUtilities/blob/30bb4691fc2ad37eda6131c6e3f3714c5464dbb4/GeUtilities/Intervals/Parsers/Model/Bed.cs#L9-L15) object per sample,
and returns analysis results in as `ReadOnlyDictionary<uint,`[`Result`](https://github.com/Genometric/MSPC/blob/be51df1fa2f37a0ded44cc8b5769864fd8c75bc9/Core/Model/Result.cs#L10-L26)`<Peak>>`.

This design is to facilitate using MSPC for developers, since
they do not need to define/implement mspc-specific models and 
can benefit from simpler function invocations.

However, each of these classes implement **generic** versions
that allow developers to define their own types, which makes 
it simpler to integrate MSPC into their application. For instance, 
if your peaks are represented in a different type (you use a different
class for your peaks), you can still use MSPC to analyze your peaks
without having to convert them to a different type, if your objects
implement the same interface as MSPC requires.
