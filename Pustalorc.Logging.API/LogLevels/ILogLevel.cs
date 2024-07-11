using System;
using JetBrains.Annotations;

namespace Pustalorc.Libraries.Logging.API.LogLevels;

/// <summary>
///     A log level, with its full details.
/// </summary>
/// <remarks>
///     Log levels should not be mutable, ever.
///     You can create new LogLevel classes or structs as you see fit, and even new log levels, but a
///     log level should never change dynamically during runtime.
///     We recommend creating an immutable struct to inherit from ILogLevel.
/// </remarks>
[PublicAPI]
public interface ILogLevel
{
    /// <summary>
    ///     A byte representing the level for this log level.
    /// </summary>
    public byte Level { get; }

    /// <summary>
    ///     The full name for this log level.
    /// </summary>
    public string FullName { get; }

    /// <summary>
    ///     A short form name for this log level.
    /// </summary>
    /// <remarks>
    ///     For consistency, short form names should be maximum 3 characters.
    /// </remarks>
    public string ShortFormName { get; }

    /// <summary>
    ///     A color to be used by any pipe that supports it.
    /// </summary>
    public ConsoleColor Color { get; }
}