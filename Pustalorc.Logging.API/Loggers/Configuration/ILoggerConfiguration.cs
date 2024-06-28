using JetBrains.Annotations;

namespace Pustalorc.Libraries.Logging.API.Loggers.Configuration;

[PublicAPI]
public interface ILoggerConfiguration
{
    public byte MaxLogLevel { get; }
}