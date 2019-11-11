# UXIsk Filters Framework
[![Build Status](https://dev.azure.com/uxifiit/UXI.Libs/_apis/build/status/uxifiit.Filters?branchName=master)](https://dev.azure.com/uxifiit/UXI.Libs/_build/latest?definitionId=5&branchName=master) [![UXI.Filters package in Public feed in Azure Artifacts](https://feeds.dev.azure.com/uxifiit/905a1e2c-1aff-45b3-bc72-dba43be0a133/_apis/public/Packaging/Feeds/990007cf-a847-406c-9fa5-dec22ee2ccdc/Packages/ef1fe12b-6fac-41f4-bceb-0d03621f757e/Badge)](https://dev.azure.com/uxifiit/Packages/_packaging?_a=package&feed=990007cf-a847-406c-9fa5-dec22ee2ccdc&package=ef1fe12b-6fac-41f4-bceb-0d03621f757e&preferRelease=true)

UXI.Filters is a framework for developing console applications for data filtering and processing. UXI.Filters belongs to the common libraries in [UXI.Libs](https://github.com/uxifiit/UXI.Libs).

Main features:
* Simplifies wrapping a data filtering function into a command line application.
* Supports hosting multiple alternative filters in the same application.
* Integrates parsing of command line options using the [CommandLineParser](https://github.com/commandlineparser/commandline) library.
* Supports filter configuration based on the command line options.
* Allows creating data filtering pipelines.
* Serializes input and output data to JSON and CSV formats, using the [UXI.Serialization](https://github.com/uxifiit/UXI.Libs) library as a wrapper over [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) and [CsvHelper](https://github.com/JoshClose/CsvHelper) libraries.


## Usage

Create a new C#/.NET Console Application, add the UXI.Filters as a NuGet reference and replace the code in the `Program.cs` file with:

```csharp
public class Program 
{
    public static int Main(string[] args) 
    {
        return new SingleFilterHost<MyOptions>
        (
            new RelayFilter<MyInputData, MyOutputData, MyOptions>("My filter", (source, options, context) => MyFilterFunction(source, options))
        ).Execute(args);
    }
}
```

In this code we use `SingleFilterHost` which handles parsing command line arguments into the options.
If you would like to host multiple filters and differentiate between them based on subclasses of options, use `MultiFilterHost`. 

The `RelayFilter<TInput, TOutput, TOptions>` defines a filter for processing input data of `TInput` type into `TOutput` type using options of the type `TOptions`. The options should match the options of the filter host, or be its subclass, and can be used to configure the filtering function passed in the first argument.

The class for options `MyOptions` in the example code should implement interfaces from the `UXI.Filters.Options` namespace to allow configuration of filter host. Use property attributes from the CommandLineParser library when defining properties of these interfaces:
* `IInputOptions` - specifies input of the filter, path to the file with data (if empty, standard input is used), format of the input file format and default format. By default, the file format is resolved from the filename but is required when reading standard input.
* `IOutputOptions` - equivalent to the input options, but standard output is used if no file path is given.
* `ITimestampSerializationOptions` - if your data contains timestamps, use this option to specify format of the timestamps for deserialization. See below for timestamp formats.


### Timestamp formats

Timestamp formats are defined as a command line argument in the form of `(type)(:config)?`, where `type` defines resolution and config further precision (optional). Supported values for `type` with respective configurations:
* `date` (aliases: `dt`, `d`) - for date and time values. Default config value is `o` (equivalent to the .NET `o` string format for DateTime), e.g., `2018-09-19T08:18:12.692+02:00`.
* `time` (aliases: `tm`, `t`) - for time value, e.g., `08:18:12.692`.
* `ticks` (aliases: `tick`, `c`, `k`) - for ticks value. Default tick precision is hundred nanoseconds (default .NET DateTime tick resolution), e.g., `636729418926920000`. For other precisions, use format:
    * `us` for microseconds, i.e., `ticks:us`,
    * `ms` for milliseconds, i.e., `ticks:ms`,
    * `100ns` or `ns` - for default hundred nanoseconds format.  



## Installation

UXI.Filters is available as a NuGet package in the public Azure DevOps artifacts repository for all UXIsk packages:
```
https://pkgs.dev.azure.com/uxifiit/Packages/_packaging/Public/nuget/v3/index.json
```

### Add UXIsk Packages to package sources
First, add a new package source. Choose the way that fits you the best:
* Add new package source in [Visual Studio settings](https://docs.microsoft.com/en-us/azure/devops/artifacts/nuget/consume?view=azure-devops).
* Add new package source from command line:
```
nuget source Add -Name "UXIsk Packages" -Source "https://pkgs.dev.azure.com/uxifiit/Packages/_packaging/Public/nuget/v3/index.json"
```
* Create or edit `NuGet.config` file in your project's solution directory where you specify this package source:
```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="UXIsk Packages" value="https://pkgs.dev.azure.com/uxifiit/Packages/_packaging/Public/nuget/v3/index.json" />
    <!-- other package sources -->
  </packageSources>
  <disabledPackageSources />
</configuration>
```

### Install packages

Use the Visual Studio "Manage NuGet Packages..." window or the Package Manager Console:
```
PM> Install-Package UXI.Filters
```


## Contributing

Use [Issues](issues) to request features, report bugs, or discuss ideas.


## Dependencies

* [UXI.Serialization](https://github.com/uxifiit/UXI.Serialization)
* [Rx.NET](https://github.com/Reactive-Extensions/Rx.NET)
* [Json.NET](https://github.com/JamesNK/Newtonsoft.Json)
* [CsvHelper](https://github.com/JoshClose/CsvHelper)
* [CommandLineParser](https://github.com/commandlineparser/commandline)


## Contributors

* Martin Konopka ([@martinkonopka](https://github.com/martinkonopka))


## License

UXI.Filters is licensed under the 3-Clause BSD License - see [LICENSE.txt](LICENSE.txt) for details.

Copyright (c) 2019 Martin Konopka and Faculty of Informatics and Information Technologies, Slovak University of Technology in Bratislava.


## Contacts

* UXIsk
  * User eXperience and Interaction Research Center
  * Faculty of Informatics and Information Technologies, Slovak University of Technology in Bratislava
  * Web: https://www.uxi.sk/
* Martin Konopka
  * E-mail: martin (underscore) konopka (at) stuba (dot) sk
