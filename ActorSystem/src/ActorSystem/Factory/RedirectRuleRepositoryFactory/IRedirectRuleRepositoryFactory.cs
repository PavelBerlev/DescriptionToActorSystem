using ActorSystem.Communication;

namespace ActorSystem.Factory;

public interface IRedirectRuleRepositoryFactory
{
    IRedirectRuleRepository Create(IDictionary<string, IMailBox> mailBoxes, ISet<RedirectRule> rules);
}