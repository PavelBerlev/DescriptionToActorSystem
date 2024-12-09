using ActorSystem.Communication;
using ActorSystem.Entity;

namespace ActorSystem.Factory;

public class TaskNotionActorRules : IRuleProcces
{
    public ISet<RedirectRule> GetRules(IEntity entity)
    {
        return new HashSet<RedirectRule>();
    }
}