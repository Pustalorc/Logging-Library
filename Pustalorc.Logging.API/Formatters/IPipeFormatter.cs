using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.LogLevels;

namespace Pustalorc.Libraries.Logging.API.Formatters;

/// <summary>
///     A pipe formatter to format a message to output to a pipe.
/// </summary>
[PublicAPI]
public interface IPipeFormatter
{
    /// <summary>
    ///     Formats the input format with all the necessary details.
    /// </summary>
    /// <param name="format">A string format to format with all the values.</param>
    /// <param name="logLevel">The level for the log to be included in the format.</param>
    /// <param name="assembly">The assembly that owns or called for the log.</param>
    /// <param name="message">The message to be included with the log.</param>
    /// <param name="includeColors">If colors should be included in this formatting.</param>
    /// <param name="colorCharacter">
    ///     If includeColors is true, then this character will be used to allow the pipe to know when to change or use colors.
    /// </param>
    /// <returns>A fully formatted string ready to be output by the pipe.</returns>
    public string Format(string format, ILogLevel logLevel, Assembly assembly, object message,
        bool includeColors = false, char colorCharacter = '§');
}