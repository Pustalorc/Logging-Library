using System;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.LogLevels;

namespace Pustalorc.Libraries.Logging.LogLevels;

/// <inheritdoc />
/// <summary>
///     A struct to define a log level
/// </summary>
/// <param name="fullName">The full name for this log level</param>
/// <param name="shortFormName">A short form name for this log level</param>
/// <param name="level">A byte representing the level for this log level</param>
/// <param name="color">A color to use when printing to console</param>
[PublicAPI]
public struct LogLevel(
    string fullName,
    string shortFormName,
    byte level,
    ConsoleColor color = ConsoleColor.Gray) : ILogLevel
{
    /// <inheritdoc />
    public byte Level { get; } = level;

    /// <inheritdoc />
    public string FullName { get; } = fullName;

    /// <inheritdoc />
    public string ShortFormName { get; } = shortFormName;

    /// <inheritdoc />
    public ConsoleColor Color { get; } = color;

    /// <summary>
    ///     A default log level instantiated statically. This log level represents a Trace log.
    /// </summary>
    public static LogLevel Trace { get; } = new("Trace", "TRC", 255, ConsoleColor.Magenta);

    /// <summary>
    ///     A default log level instantiated statically. This log level represents a Debug log.
    /// </summary>
    public static LogLevel Debug { get; } = new("Debug", "DBG", 127, ConsoleColor.Cyan);

    /// <summary>
    ///     A default log level instantiated statically. This log level represents an Info log.
    /// </summary>
    public static LogLevel Info { get; } = new("Info", "INF", 30);

    /// <summary>
    ///     A default log level instantiated statically. This log level represents a Warning log.
    /// </summary>
    public static LogLevel Warning { get; } = new("Warning", "WRN", 20, ConsoleColor.Yellow);

    /// <summary>
    ///     A default log level instantiated statically. This log level represents a Error log.
    /// </summary>
    public static LogLevel Error { get; } = new("Error", "ERR", 10, ConsoleColor.Red);

    /// <summary>
    ///     A default log level instantiated statically. This log level represents a Fatal log.
    /// </summary>
    public static LogLevel Fatal { get; } = new("Fatal", "FTL", 0, ConsoleColor.DarkRed);
}