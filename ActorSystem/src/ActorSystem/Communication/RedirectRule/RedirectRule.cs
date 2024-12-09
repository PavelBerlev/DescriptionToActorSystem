namespace ActorSystem.Communication;

public class RedirectRule
{
    public string Sender { get; }
    public string Rule { get; }
    public string Receiver { get; }

    public RedirectRule(string sender, string rule, string receiver)
    {
        Sender = sender;
        Rule = rule;
        Receiver = receiver;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != this.GetType())
        {
            return false;
        }

        var other = (RedirectRule)obj;
        return Sender == other.Sender && Rule == other.Rule && Receiver == other.Receiver;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Sender,Rule,Receiver);
    }
}