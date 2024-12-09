using ActorSystem.Communication;

namespace ActorSystem.Factory;

public interface IDictMailBoxFactory
{
    IDictionary<string, IMailBox> Create(ISet<string> names);
}