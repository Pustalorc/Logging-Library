using JetBrains.Annotations;

namespace Pustalorc.Libraries.Logging.API.Pipes.Configuration;

/// <summary>
///     Configuration for pipes.
/// </summary>
[PublicAPI]
public interface IPipeConfiguration
{
    /// <summary>
    ///     The name of the pipe this configuration will apply to.
    /// </summary>
    /// <remarks>
    ///     The value for PipeName should be the class name.
    /// </remarks>
    public string PipeName { get; }

    /// <summary>
    ///     The format for all messages written by the selected pipe to its output.
    /// </summary>
    public string MessageFormat { get; }

    /// <summary>
    ///     The maximum log level allowed to be written to this pipe.
    /// </summary>
    /// <remarks>
    ///     Each pipe could implement this restriction differently, but generally this value should be INCLUSIVE.
    /// </remarks>
    public byte MaxLogLevel { get; }

    /// <summary>
    ///     The minimum log level allowed to be written to this pipe.
    /// </summary>
    /// <remarks>
    ///     Each pipe could implement this restriction differently, but generally this value should be INCLUSIVE.
    /// </remarks>
    public byte MinLogLevel { get; }
}