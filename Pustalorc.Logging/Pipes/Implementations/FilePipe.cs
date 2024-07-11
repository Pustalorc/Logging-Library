using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;
using Pustalorc.Libraries.Logging.LogLevels;
using Pustalorc.Libraries.Logging.Pipes.Abstractions;
using Pustalorc.Libraries.Logging.Pipes.Configuration;

namespace Pustalorc.Libraries.Logging.Pipes.Implementations;

/// <inheritdoc />
/// <summary>
///     An implementation of <see cref="T:Pustalorc.Libraries.Logging.Pipes.Abstractions.BasePipe" /> that outputs to a file as per its configuration file
///     <see cref="T:Pustalorc.Libraries.Logging.API.Pipes.Configuration.IFilePipeConfiguration" />.
/// </summary>
/// <param name="owningAssembly">The assembly that owns this pipe. Used to generate the file name.</param>
/// <param name="configuration">The configuration for this pipe.</param>
[PublicAPI]
public class FilePipe(Assembly owningAssembly, IPipeConfiguration configuration) : BasePipe(configuration)
{
    /// <inheritdoc />
    protected override bool SupportsColors => false;

    /// <summary>
    ///     The assembly that owns this pipe. Used to generate the file name.
    /// </summary>
    protected virtual Assembly OwningAssembly { get; } = owningAssembly;

    /// <inheritdoc />
    public override void Write(string formattedMessage)
    {
        if (Configuration is not IFilePipeConfiguration filePipeConfiguration)
            filePipeConfiguration = new DefaultFilePipeConfiguration();

        var filePath = Formatter.Format(filePipeConfiguration.FileNameFormat,
            new LogLevel("File Naming", "FNM", byte.MaxValue), OwningAssembly, "");

        var path = Path.GetDirectoryName(filePath);
        if (path != null && !Directory.Exists(path))
            Directory.CreateDirectory(path);

        File.AppendAllText(filePath, formattedMessage);
    }
}