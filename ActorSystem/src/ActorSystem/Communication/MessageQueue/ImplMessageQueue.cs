using System.Collections.Concurrent;

namespace ActorSystem.Communication;

public class MessageQueue : IMessageQueue
{
    BlockingCollection<IMessage> _queue;

    public MessageQueue()
    {
        _queue = new();
    }

    void IMessageQueue.Put(IMessage message)
    {
        _queue.Add(message);
    }

    IMessage IMessageQueue.Take()
    {
        return _queue.Take();
    }
}