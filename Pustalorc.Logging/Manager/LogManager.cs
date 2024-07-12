using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Loggers;
using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.LogLevels;
using Pustalorc.Libraries.Logging.API.Manager;

namespace Pustalorc.Libraries.Logging.Manager;

/// <summary>
///     A static entrypoint for access to a LoggerManager, as well as writing messages.
/// </summary>
/// <remarks>
///     Since static classes can't be overridden, a LoggerManager property with get and set was set-up so that the
///     functionality can be overridden for any software that uses this library to log.
/// </remarks>
[PublicAPI]
public static class LogManager
{
    /// <summary>
    ///     The logger manager that any software calling this library should use.
    /// </summary>
    public static ILoggerManager LoggerManager { get; set; } = new DefaultLoggerManager();

    /// <summary>
    ///     Gets or creates a logger for the assembly that called this method.
    /// </summary>
    /// <param name="configuration">The configuration that should be used if creating a logger is necessary.</param>
    /// <returns>An instance of <see cref="ILogger" />.</returns>
    public static ILogger GetLogger(ILoggerConfiguration? configuration = null)
    {
        return LoggerManager.GetLogger(Assembly.GetCallingAssembly(), configuration);
    }

    /// <summary>
    ///     Updates the configuration for the logger for the assembly that called this method.
    /// </summary>
    /// <param name="configuration">The configuration that will be used.</param>
    public static void UpdateConfiguration(ILoggerConfiguration configuration)
    {
        var logger = LoggerManager.GetLogger(Assembly.GetCallingAssembly(), configuration);
        logger.UpdateConfiguration(configuration);
    }

    /// <summary>
    ///     Writes a log to the logger for the calling Assembly of level Trace.
    /// </summary>
    /// <param name="message">The message to write to the logger for the calling Assembly.</param>
    /// <remarks>
    ///     A trace level log tries to output the maximum amount of information in order to follow the application's execution
    ///     at all steps.
    /// </remarks>
    public static void Trace(object message)
    {
        LoggerManager.GetLogger(Assembly.GetCallingAssembly()).Trace(message);
    }

    /// <summary>
    ///     Writes a log to the logger for the calling Assembly of level Debug.
    /// </summary>
    /// <param name="message">The message to write to the logger for the calling Assembly.</param>
    /// <remarks>
    ///     A debug level log outputs extra information that might not be necessary for the user, but may be of use when fixing
    ///     bugs or debugging.
    /// </remarks>
    public static void Debug(object message)
    {
        LoggerManager.GetLogger(Assembly.GetCallingAssembly()).Debug(message);
    }

    /// <summary>
    ///     Writes a log to the logger for the calling Assembly of level Information.
    /// </summary>
    /// <param name="message">The message to write to the logger for the calling Assembly.</param>
    /// <remarks>
    ///     An info level log gives enough information about the state of the application, and what it is currently doing.
    /// </remarks>
    public static void Information(object message)
    {
        LoggerManager.GetLogger(Assembly.GetCallingAssembly()).Info(message);
    }

    /// <summary>
    ///     Writes a log to the logger for the calling Assembly of level Warning.
    /// </summary>
    /// <param name="message">The message to write to the logger for the calling Assembly.</param>
    /// <remarks>
    ///     A warning level log gives information when unexpected inputs, outputs, or results are encountered that might affect
    ///     the software's functionality
    /// </remarks>
    public static void Warning(object message)
    {
        LoggerManager.GetLogger(Assembly.GetCallingAssembly()).Warning(message);
    }

    /// <summary>
    ///     Writes a log to the logger for the calling Assembly of level Error.
    /// </summary>
    /// <param name="message">The message to write to the logger for the calling Assembly.</param>
    /// <remarks>
    ///     An error level log gives information about any errors that prevented functionality of the software from completing
    ///     or executing correctly, but the software still functions overall.
    /// </remarks>
    public static void Error(object message)
    {
        LoggerManager.GetLogger(Assembly.GetCallingAssembly()).Error(message);
    }

    /// <summary>
    ///     Writes a log to the logger for the calling Assembly of level Fatal.
    /// </summary>
    /// <param name="message">The message to write to the logger for the calling Assembly.</param>
    /// <remarks>
    ///     A fatal level log gives information about complete software failure, where the software can no longer function or
    ///     recover and the issue must be investigated and the software restarted.
    /// </remarks>
    public static void Fatal(object message)
    {
        LoggerManager.GetLogger(Assembly.GetCallingAssembly()).Fatal(message);
    }

    /// <summary>
    ///     Writes directly to the logger for the calling assembly.
    ///     This could be used to write with a custom log level for different information (eg: an Audit level for auditing
    ///     purposes of changes in the application).
    /// </summary>
    /// <param name="message">The message to write to the logger for the calling Assembly.</param>
    /// <param name="level">The log level for this message.</param>
    public static void Write(object message, ILogLevel level)
    {
        LoggerManager.GetLogger(Assembly.GetCallingAssembly()).Write(message, level);
    }
}