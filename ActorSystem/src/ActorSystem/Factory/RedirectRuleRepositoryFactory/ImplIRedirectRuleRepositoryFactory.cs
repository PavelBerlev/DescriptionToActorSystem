using ActorSystem.Communication;

namespace ActorSystem.Factory;

public class RedirectRuleRepositoryFactory : IRedirectRuleRepositoryFactory
{
    public IRedirectRuleRepository Create(IDictionary<string, IMailBox> mailBoxes, ISet<RedirectRule> rules)
    {
        return new RedirectRuleRepository(mailBoxes,rules);
    }
}