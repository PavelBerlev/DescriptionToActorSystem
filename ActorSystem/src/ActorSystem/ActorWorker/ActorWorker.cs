namespace ActorSystem.Actors;

//Суть заключается в том, что мы устанавливаем количество
//единовременно выполняющихся задать по обработке сообщений
public class ActorWorker
{
    private int _maxConcurrency;
    private ActorBase _actor;
    private CancellationTokenSource _cts;
    private Task? _workerTask;

    public ActorWorker(ActorBase actor, int maxConcurrency)
    {
        _actor = actor;
        _maxConcurrency = maxConcurrency;
        _cts = new CancellationTokenSource();
    }

    public void Start()
    {
        if (_workerTask != null && !_workerTask.IsCompleted)
        {
            throw new InvalidOperationException("Worker is already running.");
        }
        
        _workerTask = Task.Run(async () => 
        {
            var cancellToken = _cts.Token;
            var workers = new List<Task>();

           try
           {
            //проверка необходимости прерывания
            while(!cancellToken.IsCancellationRequested)
            {
                while(workers.Count < _maxConcurrency)
                {
                    workers.Add(_actor.HandleMessage());
                }
                await Task.WhenAny(workers);
                workers.RemoveAll(s => s.IsCompleted);
            }
           }
           catch (OperationCanceledException)
           {

           }
           finally
            {
                // Wait for all tasks to complete
                await Task.WhenAll(workers);
            }
        }, _cts.Token);
    }

    public async Task Stop(int delay = 5000)
    {
        if (_workerTask == null || _workerTask.IsCompleted)
        {
            return;
        }

        _cts.Cancel();

        var delayTask = Task.Delay(delay);
        var completedTask = await Task.WhenAny(_workerTask, delayTask);

        // if (completedTask == delayTask)
        // {
            
        // }

        _cts.Dispose();
    }
}