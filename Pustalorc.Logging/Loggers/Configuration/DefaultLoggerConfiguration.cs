using System.Collections.Generic;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;
using Pustalorc.Libraries.Logging.Pipes.Configuration;

namespace Pustalorc.Libraries.Logging.Loggers.Configuration;

/// <inheritdoc />
/// <summary>
///     The default configuration for loggers.
/// </summary>
[PublicAPI]
public class DefaultLoggerConfiguration : ILoggerConfiguration
{
    /// <inheritdoc />
    public List<IPipeConfiguration> PipeSettings { get; } =
    [
        new DefaultConsolePipeConfiguration(),
        new DefaultFilePipeConfiguration()
    ];
}