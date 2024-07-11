using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.LogLevels;

namespace Pustalorc.Libraries.Logging.API.Loggers;

/// <summary>
///     A logger has a set of pipes it can write to, and methods exposed to write differently to each.
/// </summary>
public interface ILogger
{
    /// <summary>
    ///     Writes a log to the pipes of level Trace.
    /// </summary>
    /// <param name="message">The message to write to the pipes.</param>
    /// <remarks>
    ///     A trace level log tries to output the maximum amount of information in order to follow the application's execution
    ///     at all steps.
    /// </remarks>
    public void Trace(object message);

    /// <summary>
    ///     Writes a log to the pipes of level Debug.
    /// </summary>
    /// <param name="message">The message to write to the pipes.</param>
    /// <remarks>
    ///     A debug level log outputs extra information that might not be necessary for the user, but may be of use when fixing
    ///     bugs or debugging.
    /// </remarks>
    public void Debug(object message);

    /// <summary>
    ///     Writes a log to the pipes of level Information.
    /// </summary>
    /// <param name="message">The message to write to the pipes.</param>
    /// <remarks>
    ///     An info level log gives enough information about the state of the application, and what it is currently doing.
    /// </remarks>
    public void Info(object message);

    /// <summary>
    ///     Writes a log to the pipes of level Warning.
    /// </summary>
    /// <param name="message">The message to write to the pipes.</param>
    /// <remarks>
    ///     A warning level log gives information when unexpected inputs, outputs, or results are encountered that might affect
    ///     the software's functionality
    /// </remarks>
    public void Warning(object message);

    /// <summary>
    ///     Writes a log to the pipes of level Error.
    /// </summary>
    /// <param name="message">The message to write to the pipes.</param>
    /// <remarks>
    ///     An error level log gives information about any errors that prevented functionality of the software from completing
    ///     or executing correctly, but the software still functions overall.
    /// </remarks>
    public void Error(object message);

    /// <summary>
    ///     Writes a log to the pipes of level Fatal.
    /// </summary>
    /// <param name="message">The message to write to the pipes.</param>
    /// <remarks>
    ///     A fatal level log gives information about complete software failure, where the software can no longer function or
    ///     recover and the issue must be investigated and the software restarted.
    /// </remarks>
    public void Fatal(object message);

    /// <summary>
    ///     Writes directly to the pipes. This could be used to write with a custom log level for different information (eg: an
    ///     Audit level for auditing purposes of changes in the application).
    /// </summary>
    /// <param name="message">The message to write to the pipes.</param>
    /// <param name="level">The log level for this message.</param>
    public void Write(object message, ILogLevel level);

    /// <summary>
    ///     Updates the configuration for this logger, and all pipes the logger has.
    /// </summary>
    /// <param name="configuration">The new configuration to be used by the logger and the pipes.</param>
    public void UpdateConfiguration(ILoggerConfiguration configuration);
}