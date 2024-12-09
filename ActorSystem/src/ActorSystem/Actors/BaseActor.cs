using ActorSystem.Communication;

namespace ActorSystem.Actors;

//Базовый класс подразумевает наличие почтового ящика, откуда берутся сообщения
//и наличие системы сообщений куда уже сам актор будет их поссылать. 
public abstract class ActorBase
{
    protected string _id;
    protected IMessageSystem _messageSystem;
    protected IMailBox _mailbox;
    protected ActorBase(IMessageSystem messageSystem, IMailBox mailBox, string ID)
    {
        _messageSystem = messageSystem;
        _mailbox = mailBox;
        _id = ID;
    }

    public abstract Task HandleMessage();
}