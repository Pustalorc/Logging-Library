using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Loggers;
using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.Loggers.Factories;
using Pustalorc.Libraries.Logging.API.Pipes;

namespace Pustalorc.Libraries.Logging.Loggers.Factories;

/// <inheritdoc />
/// <summary>
///     The default logger factory that only supports generating <see cref="DefaultLogger" />.
/// </summary>
[PublicAPI]
public class DefaultLoggerFactory : ILoggerFactory
{
    /// <inheritdoc />
    public ILogger CreateNewLogger(Assembly owningAssembly, ILoggerConfiguration configuration,
        Dictionary<string, IPipe> pipes)
    {
        return new DefaultLogger(owningAssembly, configuration, pipes);
    }
}