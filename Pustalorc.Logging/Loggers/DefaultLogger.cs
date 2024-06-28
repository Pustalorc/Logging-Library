using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Loggers;
using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.LogLevels;
using Pustalorc.Libraries.Logging.API.Messages;
using Pustalorc.Libraries.Logging.API.Pipes;
using Pustalorc.Libraries.Logging.Extensions;

namespace Pustalorc.Libraries.Logging.Loggers;

[PublicAPI]
public class DefaultLogger(Assembly owningAssembly, ILoggerConfiguration loggerConfiguration, List<IPipe> pipes)
    : ILogger
{
    protected Assembly OwningAssembly { get; } = owningAssembly;

    protected List<IPipe> LogPipes { get; } = pipes;

    protected ILoggerConfiguration LoggerConfiguration { get; set; } = loggerConfiguration;

    public DefaultLogger(ILoggerConfiguration loggerConfiguration, List<IPipe> pipes) : this(
        Assembly.GetCallingAssembly(), loggerConfiguration, pipes)
    {
    }

    public virtual void Debug(object message)
    {
        Write(message, LogLevel.Debug);
    }

    public virtual void Trace(object message)
    {
        Write(message, LogLevel.Trace);
    }

    public virtual void Info(object message)
    {
        Write(message, LogLevel.Info);
    }

    public virtual void Warning(object message)
    {
        Write(message, LogLevel.Warning);
    }

    public virtual void Error(object message)
    {
        Write(message, LogLevel.Error);
    }

    public void Fatal(object message)
    {
        Write(message, LogLevel.Fatal);
    }

    public virtual void Write(object message, LogLevel level)
    {
        if (level.Level > LoggerConfiguration.MaxLogLevel)
            return;

        var pieces = GetMessagePieces(message, level, OwningAssembly);

        foreach (var logger in LogPipes)
            logger.Write(pieces);
    }

    public void UpdateConfiguration(ILoggerConfiguration configuration)
    {
        LoggerConfiguration = configuration;
    }

    protected virtual List<MessagePiece> GetMessagePieces(object message, LogLevel logLevel, Assembly callingAssembly)
    {
        return
        [
            new MessagePiece(DateTime.UtcNow.TimeOfDay.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture)),
            new MessagePiece(logLevel.ShortFormName, logLevel.Color),
            new MessagePiece(callingAssembly.GetAssemblyIdentity()),
            new MessagePiece(message.ToString(), logLevel.Color)
        ];
    }
}