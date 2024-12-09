using ActorSystem.Communication;
using ActorSystem.Entity;

namespace ActorSystem.Factory;

public class RedirectRulesFactory(IServiceProvider provider) : IRedirectRulesFactory
{
  
    public ISet<RedirectRule> Create(IList<IEntity> entities)
    {
        ISet<RedirectRule> rules = new HashSet<RedirectRule>();
        foreach(var entity in entities)
        {
            rules.UnionWith(provider.GetKeyedService<IRuleProcces>((string)entity.Fields["ActorType"])!.GetRules(entity));
        }
        return rules;
    }
}