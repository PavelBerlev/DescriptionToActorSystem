using ActorSystem.Actors;
using ActorSystem.Communication;
using ActorSystem.Entity;

namespace ActorSystem.Factory;

public interface IActorCreator
{
    ActorBase Create(IMessageSystem messageSystem, IMailBox mailBox, IEntity entity);
}