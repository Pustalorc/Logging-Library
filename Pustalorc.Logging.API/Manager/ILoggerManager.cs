using System.Reflection;
using Pustalorc.Libraries.Logging.API.Loggers;
using Pustalorc.Libraries.Logging.API.Loggers.Configuration;

namespace Pustalorc.Libraries.Logging.API.Manager;

/// <summary>
///     The logger manager manages which Loggers belong to which Assemblies, and gives them their logger when requested.
/// </summary>
public interface ILoggerManager
{
    /// <summary>
    ///     Retrieves or creates a Logger instance for the specified Assembly.
    /// </summary>
    /// <param name="callingAssembly">The assembly that wants a logger.</param>
    /// <param name="configuration">The configuration for the assembly, if provided.</param>
    /// <returns>An instance of <see cref="ILogger" />.</returns>
    public ILogger GetLogger(Assembly callingAssembly, ILoggerConfiguration? configuration = null);
}