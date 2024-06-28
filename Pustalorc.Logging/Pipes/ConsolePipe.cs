using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Messages;
using Pustalorc.Libraries.Logging.API.Pipes;
using Pustalorc.Libraries.Logging.Messages;

namespace Pustalorc.Libraries.Logging.Pipes;

[PublicAPI]
public class ConsolePipe : IPipe
{
    public virtual void Write(List<MessagePiece> message)
    {
        var messagePieces = message.ToList();
        var lastIndex = messagePieces.Count - 1;

        for (var index = 0; index < messagePieces.Count; index++)
        {
            var messagePiece = messagePieces[index];

            MessagePieceExtensions.GetStartAndEndSeparators(index, lastIndex, out var startSeparator,
                out var endSeparator);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(startSeparator);

            Console.ForegroundColor = messagePiece.Color;
            Console.Write(messagePiece.Message);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(endSeparator);

            Console.ResetColor();
        }
    }
}