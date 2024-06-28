using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Loggers;
using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.LogLevels;
using Pustalorc.Libraries.Logging.API.Pipes;
using Pustalorc.Libraries.Logging.Extensions;
using Pustalorc.Libraries.Logging.Loggers;
using Pustalorc.Libraries.Logging.Loggers.Configuration;
using Pustalorc.Libraries.Logging.Pipes;

namespace Pustalorc.Libraries.Logging.Manager;

[PublicAPI]
public static class LogManager
{
    private static Dictionary<string, ILogger> Loggers { get; }

    private static List<IPipe> StaticLogPipes { get; }

    static LogManager()
    {
        Loggers = new Dictionary<string, ILogger>();
        StaticLogPipes = new List<IPipe>
        {
            new ConsolePipe()
        };
    }

    public static ILogger GetLogger(ILoggerConfiguration? configuration = null)
    {
        return GetLogger(Assembly.GetCallingAssembly(), configuration);
    }

    public static void UpdateConfiguration(ILoggerConfiguration configuration)
    {
        var logger = GetLogger(Assembly.GetCallingAssembly(), configuration);
        logger.UpdateConfiguration(configuration);
    }

    public static void AddStaticLogPipe(IPipe logPipe)
    {
        StaticLogPipes.Add(logPipe);
    }

    private static ILogger GetLogger(Assembly callingAssembly, ILoggerConfiguration? configuration = null)
    {
        var assemblyIdentity = callingAssembly.GetAssemblyIdentity();

        if (Loggers.TryGetValue(assemblyIdentity, out var logger))
            return logger;

        logger = new DefaultLogger(callingAssembly, configuration ?? new DefaultLoggerConfiguration(),
            StaticLogPipes.Concat([new FilePipe(callingAssembly)]).ToList());
        Loggers.Add(assemblyIdentity, logger);
        return logger;
    }

    public static void Trace(object message)
    {
        GetLogger(Assembly.GetCallingAssembly()).Trace(message);
    }

    public static void Debug(object message)
    {
        GetLogger(Assembly.GetCallingAssembly()).Debug(message);
    }

    public static void Information(object message)
    {
        GetLogger(Assembly.GetCallingAssembly()).Info(message);
    }

    public static void Warning(object message)
    {
        GetLogger(Assembly.GetCallingAssembly()).Warning(message);
    }

    public static void Error(object message)
    {
        GetLogger(Assembly.GetCallingAssembly()).Error(message);
    }

    public static void Fatal(object message)
    {
        GetLogger(Assembly.GetCallingAssembly()).Fatal(message);
    }

    public static void Write(object message, LogLevel level)
    {
        GetLogger(Assembly.GetCallingAssembly()).Write(message, level);
    }
}