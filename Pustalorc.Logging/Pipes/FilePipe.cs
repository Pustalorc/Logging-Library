using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Messages;
using Pustalorc.Libraries.Logging.API.Pipes;
using Pustalorc.Libraries.Logging.Extensions;
using Pustalorc.Libraries.Logging.Messages;

namespace Pustalorc.Libraries.Logging.Pipes;

[PublicAPI]
public class FilePipe : IPipe
{
    protected const string LogFileNameFormat = "Logs/{0}/{1}.log";
    protected const string DateTimeFormat = "yyyy_MM_dd_HH_mm_ss";

    protected string FilePath { get; }

    public FilePipe(Assembly owningAssembly)
    {
        var identity = owningAssembly.GetAssemblyIdentity().Replace(" ", "/");
        var dateTime = DateTime.UtcNow.ToString(DateTimeFormat);
        FilePath = string.Format(LogFileNameFormat, identity, dateTime);
    }

    public void Write(List<MessagePiece> messagePieces)
    {
        var path = Path.GetDirectoryName(FilePath);
        if (path != null && !Directory.Exists(path))
            Directory.CreateDirectory(path);

        File.AppendAllText(FilePath, messagePieces.GetMessage());
    }
}