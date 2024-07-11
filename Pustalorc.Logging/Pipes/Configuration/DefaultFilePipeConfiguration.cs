using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;
using Pustalorc.Libraries.Logging.LogLevels;
using Pustalorc.Libraries.Logging.Pipes.Implementations;

namespace Pustalorc.Libraries.Logging.Pipes.Configuration;

/// <inheritdoc />
/// <summary>
///     The default <see cref="FilePipe" /> configuration.
/// </summary>
[PublicAPI]
public class DefaultFilePipeConfiguration : IFilePipeConfiguration
{
    /// <inheritdoc />
    public virtual byte MaxLogLevel { get; } = LogLevel.Info.Level;

    /// <inheritdoc />
    public virtual byte MinLogLevel { get; } = LogLevel.Fatal.Level;

    /// <inheritdoc />
    public virtual string PipeName => nameof(FilePipe);

    /// <inheritdoc />
    public virtual string MessageFormat => "[{date} {time} UTC] [{logLevel}] [{assembly}]: {message}";

    /// <inheritdoc />
    public virtual string FileNameFormat => "Logs/{assembly:name}/{assembly:version}/{date}_{time}.log";
}