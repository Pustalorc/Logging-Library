using System;
using System.Globalization;
using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Formatters;
using Pustalorc.Libraries.Logging.API.LogLevels;
using Pustalorc.Libraries.Logging.Extensions;

namespace Pustalorc.Libraries.Logging.Formatters;

/// <inheritdoc />
/// <summary>
///     The default pipe formatter. Supports the following formats:
///     - {date} - For the current UTC date in yyyy/MM/dd format
///     - {time} - For the current UTC time in hh\:mm\:ss format
///     - {logLevel:short} - For the log level with its short form name
///     - {logLevel:full} - For the log level with its full name
///     - {logLevel:level} - For the numerical representation of the log level
///     - {logLevel} - For the log level with its short form name
///     - {assembly:name} - For the assembly's name.
///     - {assembly:version} - For the assembly's version.
///     - {assembly} - For the assembly's identity (name + version)
///     - {message} - For the input message.
/// </summary>
[PublicAPI]
public class DefaultPipeFormatter : IPipeFormatter
{
    /// <inheritdoc />
    public virtual string Format(string format, ILogLevel logLevel, Assembly assembly, object message,
        bool includeColors = false, char colorCharacter = '§')
    {
        var now = DateTime.UtcNow;

        var result = format;
        // Date formatter:
        result = FormatDate(result, now);

        // Time formatter:
        result = FormatTime(result, now.TimeOfDay);

        // LogLevel formatters:
        var colorAdditionFormat = includeColors
            ? $"{colorCharacter}{logLevel.Color}{colorCharacter}{{0}}{colorCharacter}{colorCharacter}"
            : "{0}";
        result = FormatLogLevel(result, colorAdditionFormat, logLevel);

        // Assembly Identity formatter:
        result = FormatAssembly(result, assembly);

        // Message formatter:
        result = FormatMessage(result, colorAdditionFormat, message);

        return result;
    }

    /// <summary>
    ///     Replaces {date} in the format with the date.
    /// </summary>
    /// <param name="format">The string which we want to replace {date} from it.</param>
    /// <param name="now">The current UTC DateTime</param>
    /// <returns>format but with {date} replaced.</returns>
    protected virtual string FormatDate(string format, DateTime now)
    {
        return format.Replace("{date}", now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture));
    }

    /// <summary>
    ///     Replaces {time} in the format with the time.
    /// </summary>
    /// <param name="format">The string which we want to replace {time} from it.</param>
    /// <param name="now">The current UTC DateTime</param>
    /// <returns>format but with {time} replaced.</returns>
    protected virtual string FormatTime(string format, TimeSpan now)
    {
        return format.Replace("{time}", now.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture));
    }

    /// <summary>
    ///     Replaces {logLevel} and its modifiers in the format with the log level.
    /// </summary>
    /// <param name="format">The string which we want to replace {logLevel} and its modifiers from it.</param>
    /// <param name="colorAdditionFormat">The format for handling colors.</param>
    /// <param name="logLevel">The log level</param>
    /// <returns>format but with {logLevel} and its modifiers replaced.</returns>
    /// <remarks>
    ///     Supports the following format modifiers:
    ///     - {logLevel:short} - For the log level with its short form name
    ///     - {logLevel:full} - For the log level with its full name
    ///     - {logLevel:level} - For the numerical representation of the log level
    ///     - {logLevel} - For the log level with its short form name
    /// </remarks>
    protected virtual string FormatLogLevel(string format, string colorAdditionFormat, ILogLevel logLevel)
    {
        var result = format.Replace("{logLevel:short}", colorAdditionFormat.Replace("{0}", logLevel.ShortFormName));
        result = result.Replace("{logLevel:full}", colorAdditionFormat.Replace("{0}", logLevel.FullName));
        result = result.Replace("{logLevel:level}", colorAdditionFormat.Replace("{0}", logLevel.Level.ToString()));
        return result.Replace("{logLevel}", colorAdditionFormat.Replace("{0}", logLevel.ShortFormName));
    }

    /// <summary>
    ///     Replaces {assembly} and its modifiers in the format with the assembly's identity.
    /// </summary>
    /// <param name="format">The string which we want to replace {assembly} and its modifiers from it.</param>
    /// <param name="assembly">The assembly.</param>
    /// <returns>format but with {assembly} and its modifiers replaced.</returns>
    /// <remarks>
    ///     Supports the following format modifiers:
    ///     - {assembly:name} - For the assembly's name.
    ///     - {assembly:version} - For the assembly's version.
    ///     - {assembly} - For the assembly's identity (name + version)
    /// </remarks>
    protected virtual string FormatAssembly(string format, Assembly assembly)
    {
        var result = format.Replace("{assembly:name}", assembly.GetAssemblyName());
        result = result.Replace("{assembly:version}", assembly.GetAssemblyVersion());
        return result.Replace("{assembly}", assembly.GetAssemblyIdentity());
    }

    /// <summary>
    ///     Replaces {message} in the format with the message.
    /// </summary>
    /// <param name="format">The string which we want to replace {message} from it.</param>
    /// <param name="colorAdditionFormat">The format for handling colors.</param>
    /// <param name="message">The message to output</param>
    /// <returns>format but with {message} replaced.</returns>
    protected virtual string FormatMessage(string format, string colorAdditionFormat, object message)
    {
        return format.Replace("{message}", colorAdditionFormat.Replace("{0}", message.ToString()));
    }
}