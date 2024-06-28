using System.Collections.Generic;
using JetBrains.Annotations;
using Pustalorc.Libraries.Logging.API.Messages;

namespace Pustalorc.Libraries.Logging.API.Pipes;

[PublicAPI]
public interface IPipe
{
    public void Write(List<MessagePiece> messagePieces);
}