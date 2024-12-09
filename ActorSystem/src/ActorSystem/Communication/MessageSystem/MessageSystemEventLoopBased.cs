using System.Collections.Concurrent;

namespace ActorSystem.Communication;

public class MessageSystemEventLoopBased(IMessageQueue messages) : IMessageSystem
{
    public void requestMessage(IMessage message)
    {
        messages.Put(message);
    }
}