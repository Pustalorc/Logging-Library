using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Formatters;
using Pustalorc.Libraries.Logging.API.LogLevels;
using Pustalorc.Libraries.Logging.API.Pipes;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;
using Pustalorc.Libraries.Logging.Formatters;

namespace Pustalorc.Libraries.Logging.Pipes.Abstractions;

/// <inheritdoc />
/// <summary>
///     A base class for the pipes to have most of their functionality performed and managed.
/// </summary>
/// <param name="config">The configuration that this pipe instance uses.</param>
[PublicAPI]
public abstract class BasePipe(IPipeConfiguration config) : IPipe
{
    /// <summary>
    ///     Sets if this pipe supports displaying colors.
    /// </summary>
    protected abstract bool SupportsColors { get; }

    /// <summary>
    ///     The character that will be added around colors to signify to the pipe what should be output with a color vs what
    ///     shouldn't.
    /// </summary>
    protected virtual char ColorCharacter => '§';

    /// <summary>
    ///     The configuration that this pipe instance uses.
    /// </summary>
    protected virtual IPipeConfiguration Configuration { get; set; } = config;

    /// <summary>
    ///     The formatter that this pipe will use.
    /// </summary>
    protected virtual IPipeFormatter Formatter { get; set; } = new DefaultPipeFormatter();

    /// <inheritdoc />
    public virtual void UpdateConfiguration(IPipeConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <inheritdoc />
    public virtual void Write(ILogLevel logLevel, Assembly assembly, object message)
    {
        if (logLevel.Level > Configuration.MaxLogLevel || logLevel.Level < Configuration.MinLogLevel)
            return;

        var formattedMessage = Formatter.Format(Configuration.MessageFormat, logLevel, assembly, message,
            SupportsColors, ColorCharacter);
        Write(formattedMessage);
    }

    /// <summary>
    ///     Tells the implementation exactly what message it should write to its output.
    /// </summary>
    /// <param name="formattedMessage">The message, already formatted with is values, to output.</param>
    public abstract void Write(string formattedMessage);
}