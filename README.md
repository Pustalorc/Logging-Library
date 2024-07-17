# Logging Library API [![NuGet](https://img.shields.io/nuget/v/Pustalorc.Logging.API.svg)](https://www.nuget.org/packages/Pustalorc.Logging.API/)

This library exposes the minimum API for the logging library as interfaces.
If you wish to create your own logging utilities, pipes, managers, etc. you should use this API.
You can install it using NuGet, and for its usage, please refer to the Logging Library in this repository.

# Logging Library [![NuGet](https://img.shields.io/nuget/v/Pustalorc.Logging.svg)](https://www.nuget.org/packages/Pustalorc.Logging/)

This library is the core of the functionality for logging. You can install it using NuGet.

## Usage

There are many ways this library can be used.  
You can instantiate a pipe directly and log to console (`ConsolePipe`) or to a file (`FilePipe`).  
You can instantiate a logger directly to log to multiple pipes of your chosing at once (`DefaultLogger`).  
Or you can use the `LogManager` class and let the library handle all the instantiation for you.  
Due to this, I will be focusing on the main case of just letting the `LogManager` class handle all the instantiation.

### If you do not wish to configure the outputs
If you do not wish to configure where your logs will go to, you can directly call the following example methods to log with the preset log levels:
```cs
LogManager.Trace(object message);
LogManager.Debug(object message);
LogManager.Information(object message);
LogManager.Warning(object message);
LogManager.Error(object message);
LogManager.Fatal(object message);
```
All of these log levels are documented for their expected purpose, but you are free to use them however you see fit.  
Additionally, if you implement a custom log level, or wish to specify an exact level yourself without the helper methods, you can use `LogManager.Write(object message, ILogLevel level)`

The reason all of these methods use `object` for the messages, rather than `string` is to have a similar functionality to `Console.WriteLine()`, to which you can pass an exception object, and the .NET runtime will still write the exception to console, rather than needing developers to call `.ToString()` on any object they wish to log to console.

The `LogManager` is a static class, so no instantiation is needed, but this class instantiates an `ILoggerManager` (specifically `DefaultLoggerManager`) in the field `LoggerManager` which handles creating new Loggers for each assembly, linking a logger to a calling assembly, and constructing any new pipes and loggers.  
This field can be overwritten if you wish to extend which `ILogPipe`s the library can create, as well as any internal functionality for loggers.


### If you do wish to configure the outputs
When not configuring the logger for your assembly before calling it, the pipes will limit their output to the `Information` level.  
This might not be desired, or you might want to limit output to one pipe, or have multiple file outputs for each log level, so this is where you can call the following method:
```cs
LogManager.UpdateConfiguration(ILoggerConfiguration configuration);
```
With this method, you can update the `ILogger` instance that is linked to your `Assembly` and change which pipes it uses, the maximum and minimum log levels for each pipe, as well as the log format for each pipe.

To construct a configuration, you will have to create a few classes, starting with a class that inherits from `ILoggerConfiguration`:
```cs
public class LoggerConfiguration : ILoggerConfiguration
{
    
    public List<IPipeConfiguration> PipeSettings =>
    [
        new ConsolePipeConfiguration(),
        new FilePipeConfiguration()
    ];

    public sealed class ConsolePipeConfiguration : DefaultConsolePipeConfiguration
    {
        public override byte MaxLogLevel => LogLevel.Debug.Level;
    }

    public sealed class FilePipeConfiguration : DefaultFilePipeConfiguration
    {
        public override byte MaxLogLevel => LogLevel.Debug.Level;
    }
}
```
In this configuration class, you can set which pipes to use by using a new `IPipeConfiguration` and setting `PipeName` to the name of the class you wish to use.
In the example, we want to increase the log level to allow `Debug` level logs to be written to both the `ConsolePipe` and the `FilePipe` without changing any of the other values, so we use their default configuration types and we override `MaxLogLevel`.
