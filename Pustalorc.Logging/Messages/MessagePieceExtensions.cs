using System.Collections.Generic;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Messages;

namespace Pustalorc.Libraries.Logging.Messages;

[PublicAPI]
public static class MessagePieceExtensions
{
    public static string GetMessage(this List<MessagePiece> messagePieces)
    {
        var message = "";
        var lastIndex = messagePieces.Count - 1;
        for (var index = 0; index < messagePieces.Count; index++)
        {
            var messagePiece = messagePieces[index];
            GetStartAndEndSeparators(index, lastIndex, out var startSeparator, out var endSeparator);
            message += $"{startSeparator}{messagePiece.Message}{endSeparator}";
        }

        return message;
    }

    public static void GetStartAndEndSeparators(int index, int lastIndex, out string startSeparator,
        out string endSeparator)
    {
        if (index == lastIndex)
        {
            startSeparator = "";
            endSeparator = "\n";
        }
        else if (index == lastIndex - 1)
        {
            startSeparator = "[";
            endSeparator = "]: ";
        }
        else
        {
            startSeparator = "[";
            endSeparator = "] ";
        }
    }
}