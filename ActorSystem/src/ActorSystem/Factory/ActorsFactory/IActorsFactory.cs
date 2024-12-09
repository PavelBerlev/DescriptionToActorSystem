using ActorSystem.Actors;
using ActorSystem.Communication;
using ActorSystem.Entity;
namespace ActorSystem.Factory;

public interface IActorsFactory
{
    IDictionary<string, ActorBase> Create(IDictionary<string, IMailBox> mailBoxes, IMessageSystem messageSystem, IList<IEntity> entities);
}