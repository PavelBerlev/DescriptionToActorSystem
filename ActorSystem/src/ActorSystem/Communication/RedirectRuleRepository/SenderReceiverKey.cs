namespace ActorSystem.Communication;

public class SenderReceiverKey
{
    public string Sender{get;}
    public string Receiver{get;}
    public SenderReceiverKey(string Sender, string Receiver)
    {
        this.Sender = Sender;
        this.Receiver = Receiver;
    }
     public override int GetHashCode()
    {

        return HashCode.Combine(Sender, Receiver);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (SenderReceiverKey)obj;
        return Sender == other.Sender && Receiver == other.Receiver;
    }
}