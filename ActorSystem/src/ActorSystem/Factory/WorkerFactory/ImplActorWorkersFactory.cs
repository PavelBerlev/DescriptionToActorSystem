using ActorSystem.Actors;

namespace ActorSystem.Factory;

public class ActorWorkersFactory : IWorkersFactory
{
    public IDictionary<string, ActorWorker> Create(IDictionary<string, ActorBase> actors, int maxConcurrency)
    {
        IDictionary<string, ActorWorker> actorWorkers = new Dictionary<string, ActorWorker>();

        foreach(var key in actors.Keys)
        {
            actorWorkers[key] = new ActorWorker(actors[key], maxConcurrency);
        }

        return actorWorkers;
    }
}