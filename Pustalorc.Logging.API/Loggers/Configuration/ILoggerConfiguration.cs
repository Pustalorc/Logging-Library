using System.Collections.Generic;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;

namespace Pustalorc.Libraries.Logging.API.Loggers.Configuration;

/// <summary>
///     The configuration for a Logger
/// </summary>
[PublicAPI]
public interface ILoggerConfiguration
{
    /// <summary>
    ///     The settings for each pipe that this logger will use.
    /// </summary>
    public List<IPipeConfiguration> PipeSettings { get; }
}