using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Loggers;
using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.Loggers.Factories;
using Pustalorc.Libraries.Logging.API.Manager;
using Pustalorc.Libraries.Logging.API.Pipes;
using Pustalorc.Libraries.Logging.API.Pipes.Factories;
using Pustalorc.Libraries.Logging.Extensions;
using Pustalorc.Libraries.Logging.Loggers.Configuration;
using Pustalorc.Libraries.Logging.Loggers.Factories;
using Pustalorc.Libraries.Logging.Pipes.Factories;

namespace Pustalorc.Libraries.Logging.Manager;

/// <inheritdoc />
/// <summary>
///     The default implementation of <see cref="ILoggerManager" />
/// </summary>
[PublicAPI]
public class DefaultLoggerManager : ILoggerManager
{
    /// <summary>
    ///     A dictionary of the instantiated loggers for each Assembly by the Assembly's identity.
    /// </summary>
    protected virtual Dictionary<string, ILogger> Loggers { get; } = new();

    /// <summary>
    ///     The factory that will generate new pipes.
    /// </summary>
    protected virtual IPipeFactory PipeFactory { get; } = new DefaultPipeFactory();

    /// <summary>
    ///     The factory that will generate new loggers.
    /// </summary>
    protected virtual ILoggerFactory LoggerFactory { get; } = new DefaultLoggerFactory();

    /// <summary>
    ///     The default comparer for the pipe dictionaries that the Loggers will use.
    /// </summary>
    protected virtual StringComparer PipeComparer { get; } = StringComparer.OrdinalIgnoreCase;

    /// <inheritdoc />
    public virtual ILogger GetLogger(Assembly callingAssembly, ILoggerConfiguration? configuration = null)
    {
        var assemblyIdentity = callingAssembly.GetAssemblyIdentity();

        if (Loggers.TryGetValue(assemblyIdentity, out var logger))
            return logger;

        var loggerConfiguration = configuration ?? new DefaultLoggerConfiguration();
        var pipes = new Dictionary<string, IPipe>(PipeComparer);

        foreach (var pipeConfig in loggerConfiguration.PipeSettings)
            pipes.Add(pipeConfig.PipeName, PipeFactory.CreateNewPipeFromConfig(callingAssembly, pipeConfig));

        logger = LoggerFactory.CreateNewLogger(callingAssembly, loggerConfiguration, pipes);
        Loggers.Add(assemblyIdentity, logger);
        return logger;
    }
}