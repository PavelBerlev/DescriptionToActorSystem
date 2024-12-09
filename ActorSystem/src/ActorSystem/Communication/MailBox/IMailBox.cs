namespace ActorSystem.Communication;

public interface IMailBox
{
    void PutMessage(IMessage message);
    Task<IMessage> GetMessage();
}
