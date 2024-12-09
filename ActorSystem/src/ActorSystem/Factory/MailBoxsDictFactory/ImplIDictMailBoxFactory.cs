using ActorSystem.Communication;

namespace ActorSystem.Factory;

public class DictMailBoxFactory(IServiceProvider provider) : IDictMailBoxFactory
{
    public IDictionary<string, IMailBox> Create(ISet<string> names)
    {
        var mailBoxes = new Dictionary<string, IMailBox>();
        foreach(var name in names)
        {
            mailBoxes[name] = provider.GetRequiredService<IMailBox>();
        }
        return mailBoxes;
    }
}