namespace ActorSystem.Communication;

public interface IMessageSystem
{
    void requestMessage(IMessage message);
}