using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;
using Pustalorc.Libraries.Logging.LogLevels;
using Pustalorc.Libraries.Logging.Pipes.Implementations;

namespace Pustalorc.Libraries.Logging.Pipes.Configuration;

/// <inheritdoc />
/// <summary>
///     The default <see cref="ConsolePipe" /> configuration.
/// </summary>
[PublicAPI]
public class DefaultConsolePipeConfiguration : IPipeConfiguration
{
    /// <inheritdoc />
    public virtual byte MaxLogLevel { get; } = LogLevel.Info.Level;

    /// <inheritdoc />
    public virtual byte MinLogLevel { get; } = LogLevel.Fatal.Level;

    /// <inheritdoc />
    public virtual string PipeName => nameof(ConsolePipe);

    /// <inheritdoc />
    public virtual string MessageFormat => "[{time} UTC] [{logLevel}] [{assembly}]: {message}";
}