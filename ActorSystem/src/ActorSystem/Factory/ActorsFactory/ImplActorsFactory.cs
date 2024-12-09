using ActorSystem.Actors;
using ActorSystem.Communication;
using ActorSystem.Entity;

namespace ActorSystem.Factory;

public class ActorsFactory(IServiceProvider serviceProvider) : IActorsFactory
{
    public IDictionary<string, ActorBase>  Create(IDictionary<string, IMailBox> mailBoxes, IMessageSystem messageSystem, IList<IEntity> entities)
    {
        var actors = new Dictionary<string, ActorBase>();
        foreach(var entity in entities)
        {
            var actorName = (string)entity.Fields["Name"];
            var actorCreator = serviceProvider.GetKeyedService<IActorCreator>((string)entity.Fields["ActorType"]);
            actors[actorName] = actorCreator!.Create(messageSystem, mailBoxes[actorName],entity);
        }
        return actors;
    }
}