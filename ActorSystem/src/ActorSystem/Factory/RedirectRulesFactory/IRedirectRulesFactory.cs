using ActorSystem.Communication;
using ActorSystem.Entity;

namespace ActorSystem.Factory;

public interface IRedirectRulesFactory
{
    ISet<RedirectRule> Create(IList<IEntity> entities);
}