namespace ActorSystem.Communication;


public class Message : IMessage
{
    string _receiver = "undefined";
    string _sender = "undefined";
    IDictionary<string, object> _context = new Dictionary<string, object>();

    public string Receiver{get=> _receiver; set => _receiver = value;}
    public string Sender{get => _sender; set=> _sender = value;}
    public IDictionary<string, object> Context{get => _context; set=> _context = value;}
}