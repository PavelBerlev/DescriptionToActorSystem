using ActorSystem.Communication;
using ActorSystem.Actors;
using MongoDB.Driver;

public class ActorWorkerTests
{
    // Mock for ActorBase
    public class MockActor : ActorBase
    {
        private int _messageCount = 0;

        public MockActor(IMessageSystem messageSystem, IMailBox mailBox, string ID)
            : base(messageSystem, mailBox, ID) {}

        public override async Task HandleMessage()
        {
            Interlocked.Increment(ref _messageCount);
            await Task.Delay(10);
        }

        public int GetMessageCount()
        {
            return _messageCount;
        }
    }

    [Fact]
    public async Task ActorWorker_RespectsMaxConcurrency()
    {
        var messageSystemMock = new Mock<IMessageSystem>();
        var mailBoxMock = new Mock<IMailBox>();
        var actor = new MockActor(messageSystemMock.Object, mailBoxMock.Object, "testActor");
        var actorWorker = new ActorWorker(actor, 2);

        actorWorker.Start();
        await Task.Delay(40);
        await actorWorker.Stop();

        Assert.InRange(actor.GetMessageCount(), 2, 10);
    }

    [Fact]
    public async Task ActorWorker_ThrowsExceptionIfAlreadyRunning()
    {
        var messageSystemMock = new Mock<IMessageSystem>();
        var mailBoxMock = new Mock<IMailBox>();
        var actor = new MockActor(messageSystemMock.Object, mailBoxMock.Object, "testActor");
        var actorWorker = new ActorWorker(actor, 5);

        actorWorker.Start();

        Assert.Throws<InvalidOperationException>(() => actorWorker.Start());
        await actorWorker.Stop();
    }

    [Fact]
    public void ActorWorkerASD()
    {
        var mailBox = new MailBox();
        var redirectRule = new RedirectRule("1", "2","2");
        var set = new HashSet<RedirectRule>{redirectRule};
        var dict = new Dictionary<string, IMailBox>{ {"2", mailBox},
                                                     {"1", mailBox}};
        var redirectRuleRep = new RedirectRuleRepository(dict, set);
        var MessageQueue = new MessageQueue();
        var MessageSystem = new MessageSystemEventLoopBased(MessageQueue);
        var EventLoop = new EventLoop(redirectRuleRep, MessageQueue);
        EventLoop.Start();

        string connectionURI = "mongodb://root:password@localhost:27017";
        string dataBaseName = "ActorSystem";
        var client =  new MongoClient(connectionURI);
        var database = client.GetDatabase(dataBaseName);

        var actor = new DBWriterActor(MessageSystem,mailBox,"2", database,"StudentHomeWorks",false,"Group", "TaskName", "Description", "Files");

        var actorWorker = new ActorWorker(actor, 1);

        actorWorker.Start();

        var message = new Message();
        message.Sender = "1";
        message.Receiver = "2";
        message.Context["Group"] = "ФИТ-201";
        message.Context["TaskName"] = "ФИТ-201";
        message.Context["Description"] = "ФИТ-201";
        //message.Context["Files"] = files;
        MessageSystem.requestMessage(message);
        Assert.True(false);
    }
}