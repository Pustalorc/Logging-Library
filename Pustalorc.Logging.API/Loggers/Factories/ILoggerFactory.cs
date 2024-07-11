using System.Collections.Generic;
using System.Reflection;
using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.Pipes;

namespace Pustalorc.Libraries.Logging.API.Loggers.Factories;

/// <summary>
///     A factory to instantiate new Loggers.
/// </summary>
public interface ILoggerFactory
{
    /// <summary>
    ///     Instantiates a new logger with the specified details.
    /// </summary>
    /// <param name="owningAssembly">The assembly that owns this new Logger.</param>
    /// <param name="configuration">The configuration for this Logger to use.</param>
    /// <param name="pipes">The pipes that this logger will output to.</param>
    /// <returns>A Logger instance ready to write to the specified pipes.</returns>
    public ILogger CreateNewLogger(Assembly owningAssembly, ILoggerConfiguration configuration,
        Dictionary<string, IPipe> pipes);
}