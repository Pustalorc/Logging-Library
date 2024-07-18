using System;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Pipes.Configuration;
using Pustalorc.Libraries.Logging.Pipes.Abstractions;

namespace Pustalorc.Libraries.Logging.Pipes.Implementations;

/// <inheritdoc />
/// <summary>
///     An implementation of <see cref="T:Pustalorc.Libraries.Logging.Pipes.Abstractions.BasePipe" /> that outputs directly
///     to the console.
/// </summary>
/// <param name="configuration">The configuration for this pipe.</param>
[PublicAPI]
public class ConsolePipe(IPipeConfiguration configuration) : BasePipe(configuration)
{
    /// <inheritdoc />
    protected override bool SupportsColors => true;

    /// <inheritdoc />
    public override void Write(string formattedMessage)
    {
        var messagePieces = formattedMessage.Split(ColorCharacter);

        Console.ForegroundColor = ConsoleColor.Gray;
        foreach (var piece in messagePieces)
            if (Enum.TryParse(piece, out ConsoleColor color))
                Console.ForegroundColor = color;
            else if (string.IsNullOrWhiteSpace(piece))
                Console.ForegroundColor = ConsoleColor.Gray;
            else
                Console.Write(piece);
        Console.ResetColor();
        Console.Write(Environment.NewLine);
    }
}