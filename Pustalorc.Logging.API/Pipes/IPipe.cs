using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.LogLevels;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;

namespace Pustalorc.Libraries.Logging.API.Pipes;

/// <summary>
///     A pipe is a class that will write to one output exactly (eg: Console, a file, a stream, etc.)
/// </summary>
[PublicAPI]
public interface IPipe
{
    /// <summary>
    ///     Updates the configuration for this pipe with a new instance of <see cref="IPipeConfiguration" />.
    /// </summary>
    /// <param name="configuration">The new instance of the configuration.</param>
    public void UpdateConfiguration(IPipeConfiguration configuration);

    /// <summary>
    ///     Writes a message with a specific log level and from a specific input assembly to the pipe's output.
    /// </summary>
    /// <param name="logLevel">The level for this log.</param>
    /// <param name="assembly">The assembly that requested the message to be written.</param>
    /// <param name="message">The message that needs to be written.</param>
    public void Write(ILogLevel logLevel, Assembly assembly, object message);
}