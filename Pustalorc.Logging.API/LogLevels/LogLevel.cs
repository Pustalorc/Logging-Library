using System;
using JetBrains.Annotations;

namespace Pustalorc.Libraries.Logging.API.LogLevels;

[PublicAPI]
public struct LogLevel(
    string fullName,
    string shortFormName,
    byte level,
    ConsoleColor color = ConsoleColor.Gray)
{
    public byte Level { get; } = level;

    public string FullName { get; } = fullName;
    public string ShortFormName { get; } = shortFormName;

    public ConsoleColor Color { get; } = color;

    public static LogLevel Trace { get; } = new("Trace", "TRC", 255, ConsoleColor.Magenta);
    public static LogLevel Debug { get; } = new("Debug", "DBG", 127, ConsoleColor.Cyan);
    public static LogLevel Info { get; } = new("Info", "INF", 30);
    public static LogLevel Warning { get; } = new("Warning", "WRN", 20, ConsoleColor.Yellow);
    public static LogLevel Error { get; } = new("Error", "ERR", 10, ConsoleColor.Red);
    public static LogLevel Fatal { get; } = new("Fatal", "FTL", 0, ConsoleColor.DarkRed);
}