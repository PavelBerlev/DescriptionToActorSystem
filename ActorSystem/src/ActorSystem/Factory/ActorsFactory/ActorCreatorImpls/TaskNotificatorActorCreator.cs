using System.Net.Mail;
using ActorSystem.Actors;
using ActorSystem.Communication;
using ActorSystem.Entity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ActorSystem.Factory;

public class TaskNotificatorActorCreator(IServiceProvider serviceProvider) : IActorCreator
{
    public ActorBase Create(IMessageSystem messageSystem, IMailBox mailBox, IEntity entity)
    {
        var dataBase = serviceProvider.GetService<IMongoDatabase>();
        var collectionName = (string)entity.Fields["Collection"];
        var collection = dataBase!.GetCollection<BsonDocument>(collectionName);

        var client = serviceProvider.GetService<SmtpClient>();

        var actorName = (string)entity.Fields["Name"];
        return new TaskNotificatorActor(messageSystem, mailBox,actorName,collection, client!);
    }
}