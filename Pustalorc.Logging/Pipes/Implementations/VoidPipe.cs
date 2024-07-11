using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.LogLevels;
using Pustalorc.Libraries.Logging.API.Pipes;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;

namespace Pustalorc.Libraries.Logging.Pipes.Implementations;

/// <summary>
///     An implementation of IPipe that does not output to anywhere.
/// </summary>
/// <inheritdoc />
[PublicAPI]
public class VoidPipe : IPipe
{
    /// <inheritdoc />
    public void UpdateConfiguration(IPipeConfiguration configuration)
    {
    }

    /// <inheritdoc />
    public void Write(ILogLevel logLevel, Assembly assembly, object message)
    {
    }
}