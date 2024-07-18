using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Formatters;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;
using Pustalorc.Libraries.Logging.Formatters;
using Pustalorc.Libraries.Logging.LogLevels;
using Pustalorc.Libraries.Logging.Pipes.Abstractions;
using Pustalorc.Libraries.Logging.Pipes.Configuration;

namespace Pustalorc.Libraries.Logging.Pipes.Implementations;

/// <inheritdoc />
/// <summary>
///     An implementation of <see cref="T:Pustalorc.Libraries.Logging.Pipes.Abstractions.BasePipe" /> that outputs to a
///     file as per its configuration file
///     <see cref="T:Pustalorc.Libraries.Logging.API.Pipes.Configuration.IFilePipeConfiguration" />.
/// </summary>
[PublicAPI]
public class FilePipe : BasePipe
{
    /// <inheritdoc />
    protected override bool SupportsColors => false;

    /// <summary>
    ///     The assembly that owns this pipe. Used to generate the file name.
    /// </summary>
    protected virtual Assembly OwningAssembly { get; }

    /// <summary>
    ///     The formatter to use for the file name.
    /// </summary>
    protected virtual IPipeFormatter FileNameFormatter { get; }

    /// <summary>
    ///     The name of the file the pipe will write to.
    /// </summary>
    protected string FileName { get; set; }

    /// <inheritdoc />
    /// <summary>
    ///     Constructs the file pipe.
    /// </summary>
    /// <param name="owningAssembly">The assembly that owns this pipe. Used to generate the file name.</param>
    /// <param name="configuration">The configuration for this pipe.</param>
    public FilePipe(Assembly owningAssembly, IPipeConfiguration configuration) : base(configuration)
    {
        OwningAssembly = owningAssembly;
        var fileNameFormatter = new FileNamePipeFormatter();

        if (configuration is not IFilePipeConfiguration filePipeConfiguration)
            filePipeConfiguration = new DefaultFilePipeConfiguration();

        FileName = fileNameFormatter.Format(filePipeConfiguration.FileNameFormat,
            new LogLevel("File Naming", "FNM", byte.MaxValue), owningAssembly, "");
        FileNameFormatter = fileNameFormatter;
    }

    /// <inheritdoc />
    public override void UpdateConfiguration(IPipeConfiguration configuration)
    {
        base.UpdateConfiguration(configuration);

        if (configuration is not IFilePipeConfiguration filePipeConfiguration)
            filePipeConfiguration = new DefaultFilePipeConfiguration();

        FileName = FileNameFormatter.Format(filePipeConfiguration.FileNameFormat,
            new LogLevel("File Naming", "FNM", byte.MaxValue), OwningAssembly, "");
    }

    /// <inheritdoc />
    public override void Write(string formattedMessage)
    {
        var path = Path.GetDirectoryName(FileName);
        if (path != null && !Directory.Exists(path))
            Directory.CreateDirectory(path);

        File.AppendAllText(FileName, formattedMessage);
    }
}