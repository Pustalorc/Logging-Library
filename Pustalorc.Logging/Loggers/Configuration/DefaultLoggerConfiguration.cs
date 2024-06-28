using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.LogLevels;

namespace Pustalorc.Libraries.Logging.Loggers.Configuration;

[PublicAPI]
public class DefaultLoggerConfiguration : ILoggerConfiguration
{
    public byte MaxLogLevel => LogLevel.Info.Level;
}