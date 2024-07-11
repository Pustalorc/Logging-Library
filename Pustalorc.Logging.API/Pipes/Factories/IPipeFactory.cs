using System.Reflection;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;

namespace Pustalorc.Libraries.Logging.API.Pipes.Factories;

/// <summary>
///     Factory interface to create new pipes.
/// </summary>
public interface IPipeFactory
{
    /// <summary>
    ///     Creates a new pipe with the specified owning assembly, and configuration.
    /// </summary>
    /// <param name="owningAssembly">The assembly that owns this pipe.</param>
    /// <param name="configuration">The configuration for this pipe to use.</param>
    /// <returns>A new instance of an <see cref="IPipe" /> implementation.</returns>
    public IPipe CreateNewPipeFromConfig(Assembly owningAssembly, IPipeConfiguration configuration);
}