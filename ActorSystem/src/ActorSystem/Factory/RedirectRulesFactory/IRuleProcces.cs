using ActorSystem.Communication;
using ActorSystem.Entity;

namespace ActorSystem.Factory;

public interface IRuleProcces
{
    ISet<RedirectRule> GetRules(IEntity entity);
}
