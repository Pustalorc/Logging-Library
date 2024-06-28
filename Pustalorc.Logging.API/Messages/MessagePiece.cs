using System;
using JetBrains.Annotations;

namespace Pustalorc.Libraries.Logging.API.Messages;

[PublicAPI]
public struct MessagePiece(string message, ConsoleColor color = ConsoleColor.Gray)
{
    public string Message { get; } = message;
    public ConsoleColor Color { get; } = color;

    public override string ToString()
    {
        return Message;
    }
}