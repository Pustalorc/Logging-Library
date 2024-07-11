namespace Pustalorc.Libraries.Logging.API.Pipes.Configuration;

/// <inheritdoc />
/// <summary>
///     Configuration for any pipe that may write to any file.
/// </summary>
public interface IFilePipeConfiguration : IPipeConfiguration
{
    /// <summary>
    ///     The format for the name of the file, including the path to the file relative to the current directory.
    /// </summary>
    public string FileNameFormat { get; }
}