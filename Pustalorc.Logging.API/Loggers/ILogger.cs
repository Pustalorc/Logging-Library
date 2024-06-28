using Pustalorc.Libraries.Logging.API.Loggers.Configuration;
using Pustalorc.Libraries.Logging.API.LogLevels;

namespace Pustalorc.Libraries.Logging.API.Loggers;

public interface ILogger
{
    public void Trace(object message);
    public void Debug(object message);
    public void Info(object message);
    public void Warning(object message);
    public void Error(object message);
    public void Fatal(object message);
    public void Write(object message, LogLevel level);
    public void UpdateConfiguration(ILoggerConfiguration configuration);
}