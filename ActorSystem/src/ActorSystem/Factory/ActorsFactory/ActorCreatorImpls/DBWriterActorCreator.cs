using ActorSystem.Actors;
using ActorSystem.Communication;
using ActorSystem.Entity;
using MongoDB.Driver;

namespace ActorSystem.Factory;

public class DBWriterActorCreator(IServiceProvider serviceProvider) : IActorCreator
{
    public ActorBase Create(IMessageSystem messageSystem, IMailBox mailBox, IEntity entity)
    {
        var dataBase = serviceProvider.GetRequiredService<IMongoDatabase>();
        var actorName = (string)entity.Fields["Name"];
        var collectionName = (string)entity.Fields["Collection"];
        var mustSendToNext = entity.Fields.ContainsKey("Next");
        var keys = ((List<string>)entity.Fields["Keys"]).ToArray();

        return new DBWriterActor(messageSystem, mailBox, actorName,
        dataBase, collectionName, mustSendToNext
        ,keys);
    }
}
