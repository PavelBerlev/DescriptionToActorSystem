using ActorSystem.Communication;
using ActorSystem.Entity;

namespace ActorSystem.Factory;

public class DBWriterActorRules : IRuleProcces
{
    public ISet<RedirectRule> GetRules(IEntity entity)
    {
        var set = new HashSet<RedirectRule>();
        if(entity.Fields.ContainsKey("Next"))
        {
            set.Add(new RedirectRule((string)entity.Fields["Name"], "Next", (string)entity.Fields["Next"]));
        }
        return set;
    }
}