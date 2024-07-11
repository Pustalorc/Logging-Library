using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Loggers;
using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.LogLevels;
using Pustalorc.Libraries.Logging.API.Pipes;
using Pustalorc.Libraries.Logging.LogLevels;

namespace Pustalorc.Libraries.Logging.Loggers;

/// <inheritdoc />
/// <summary>
///     A default logger that allows to write to many pipes.
/// </summary>
/// <param name="owningAssembly">The assembly that owns this logger instance.</param>
/// <param name="loggerConfiguration">The configuration for this logger.</param>
/// <param name="pipes">The pipes that this logger will use.</param>
[PublicAPI]
public class DefaultLogger(
    Assembly owningAssembly,
    ILoggerConfiguration loggerConfiguration,
    Dictionary<string, IPipe> pipes) : ILogger
{
    /// <summary>
    ///     The Assembly that owns this logger.
    /// </summary>
    protected Assembly OwningAssembly { get; } = owningAssembly;

    /// <summary>
    ///     The configuration that this logger is currently using.
    /// </summary>
    protected ILoggerConfiguration LoggerConfiguration { get; set; } = loggerConfiguration;

    /// <summary>
    ///     The pipes that this logger is currently using.
    /// </summary>
    protected Dictionary<string, IPipe> LogPipes { get; } = pipes;

    /// <inheritdoc />
    public DefaultLogger(ILoggerConfiguration loggerConfiguration, Dictionary<string, IPipe> pipes) : this(
        Assembly.GetCallingAssembly(), loggerConfiguration, pipes)
    {
    }

    /// <inheritdoc />
    public virtual void Debug(object message)
    {
        Write(message, LogLevel.Debug);
    }

    /// <inheritdoc />
    public virtual void Trace(object message)
    {
        Write(message, LogLevel.Trace);
    }

    /// <inheritdoc />
    public virtual void Info(object message)
    {
        Write(message, LogLevel.Info);
    }

    /// <inheritdoc />
    public virtual void Warning(object message)
    {
        Write(message, LogLevel.Warning);
    }

    /// <inheritdoc />
    public virtual void Error(object message)
    {
        Write(message, LogLevel.Error);
    }

    /// <inheritdoc />
    public void Fatal(object message)
    {
        Write(message, LogLevel.Fatal);
    }

    /// <inheritdoc />
    public virtual void Write(object message, ILogLevel logLevel)
    {
        foreach (var logger in LogPipes.Values)
            logger.Write(logLevel, OwningAssembly, message);
    }

    /// <inheritdoc />
    public void UpdateConfiguration(ILoggerConfiguration configuration)
    {
        LoggerConfiguration = configuration;
        UpdatePipeConfigurations();
    }

    /// <summary>
    ///     Updates each pipe's configuration directly.
    /// </summary>
    protected virtual void UpdatePipeConfigurations()
    {
        foreach (var config in LoggerConfiguration.PipeSettings)
        {
            if (!LogPipes.TryGetValue(config.PipeName, out var pipe))
                continue;

            pipe.UpdateConfiguration(config);
        }
    }
}