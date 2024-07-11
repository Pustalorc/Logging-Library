using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Pipes;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;
using Pustalorc.Libraries.Logging.API.Pipes.Factories;
using Pustalorc.Libraries.Logging.Pipes.Implementations;

namespace Pustalorc.Libraries.Logging.Pipes.Factories;

/// <inheritdoc />
/// <summary>
///     The default pipe factory that only supports generating <see cref="ConsolePipe" />, <see cref="FilePipe" /> and
///     <see cref="VoidPipe" />
/// </summary>
[PublicAPI]
public class DefaultPipeFactory : IPipeFactory
{
    /// <inheritdoc />
    public IPipe CreateNewPipeFromConfig(Assembly owningAssembly, IPipeConfiguration configuration)
    {
        return configuration.PipeName switch
        {
            nameof(ConsolePipe) => new ConsolePipe(configuration),
            nameof(FilePipe) => new FilePipe(owningAssembly, configuration),
            _ => new VoidPipe()
        };
    }
}