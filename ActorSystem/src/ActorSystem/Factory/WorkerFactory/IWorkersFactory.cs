using ActorSystem.Actors;

namespace ActorSystem.Factory;

public interface IWorkersFactory
{
    IDictionary<string, ActorWorker> Create(IDictionary<string, ActorBase> actors, int maxConcurrency);
}