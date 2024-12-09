using System.Threading.Tasks.Dataflow;

namespace ActorSystem.Communication;

public class MailBox : IMailBox
{
    BufferBlock<IMessage> _messages = new();

   public void PutMessage(IMessage message)
   {
        _messages.Post(message);
   }

    public Task<IMessage> GetMessage()
    {   
        return _messages.ReceiveAsync();
    }
}