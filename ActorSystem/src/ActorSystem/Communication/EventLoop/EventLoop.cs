namespace ActorSystem.Communication;

public class EventLoop
{
    IRedirectRuleRepository Repository;
    IMessageQueue Queue;
    Thread thread;
    CancellationTokenSource cancellationTokenSource;

    readonly IMessage emptyMsg = new emptyMessage();

    public EventLoop(IRedirectRuleRepository repository, IMessageQueue queue)
    {
        Repository = repository;
        Queue = queue;
        cancellationTokenSource = new CancellationTokenSource();
        thread = new Thread(() => loopQueue(cancellationTokenSource.Token));
    }

    public void Start()
    {
        thread.Start();
    }
    public void Stop()
    {
        Queue.Put(emptyMsg);
        cancellationTokenSource.Cancel();
        thread.Join();
    }

    private void loopQueue(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var message = Queue.Take();
                if(message == emptyMsg)
                {
                    break;
                }
                var key = new SenderReceiverKey(message.Sender, message.Receiver);
                var endAddres = Repository.GetAdressReceiver(key);
                endAddres!.PutMessage(message);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    private class emptyMessage : IMessage
    {
        string _receiver = "";
        string _sender = "";
        IDictionary<string, object> _context = new Dictionary<string, object>();

        public string Receiver{get=> _receiver; set => _receiver = value;}
        public string Sender{get => _sender; set=> _sender = value;}
        public IDictionary<string, object> Context{get => _context; set=> _context = value;}
    }
}
