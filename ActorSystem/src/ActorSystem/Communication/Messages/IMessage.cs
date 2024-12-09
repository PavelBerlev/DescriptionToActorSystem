namespace ActorSystem.Communication;

public interface IMessage
{
    String Receiver{get; set;}
    String Sender{get;set;}
    IDictionary<string, object> Context{set;get;} 
}

