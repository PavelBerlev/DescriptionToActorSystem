namespace ActorSystem.Communication;

public interface IMessageQueue
{
    void Put(IMessage message);

    IMessage Take();
}