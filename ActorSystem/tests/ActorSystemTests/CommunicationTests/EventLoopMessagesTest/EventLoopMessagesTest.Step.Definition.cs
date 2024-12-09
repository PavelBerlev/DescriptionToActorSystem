using TechTalk.SpecFlow;
using ActorSystem.Communication;
using System.Collections.Concurrent;

namespace ActorSystem.Tests;

[Binding]
class EventLoopTest
{

    IRedirectRuleRepository? ruleRepo;
    IMessageSystem? messageSystem;
    IMailBox? MailBox1;
    IMailBox? MailBox2;

    IMessageQueue? messages;

    EventLoop? eventLoop;


    [Given("Система состоящая их почтовых ящиков и EventLoop")]
    public void Address_For_MailBoxes()
    {   
        //Почтовые ящики
        MailBox1 = new MailBox();
        MailBox2 = new MailBox();
        var mailBoxes = new Dictionary<string, IMailBox> {
            ["Актор№1"] = MailBox1,
            ["Актор№2"] = MailBox2,
        };

        //Правила перессылки сообщений
        var rules = new HashSet<RedirectRule>{
            new RedirectRule("Актор№1","Актор№2","Актор№2"),
            new RedirectRule("Актор№2","Актор№1","Актор№1")
        };

        ruleRepo = new RedirectRuleRepository(mailBoxes, rules);
        
        //Система обмена сообщениями
        messages = new MessageQueue();
        messageSystem = new MessageSystemEventLoopBased(messages);

        eventLoop = new EventLoop(ruleRepo, messages);
        eventLoop.Start();
    }

    [When("Из ящика№1 отправляется сообщение в ящик№2 с EventLoopBased")]
    public void Send_To_Mail2()
    {   
        IMessage msg = new Message();
        msg.Sender = "Актор№1";
        msg.Receiver = "Актор№2";
        messageSystem!.requestMessage(msg);
    }
    [Then("В ящике№2 появляется сообщение и EventLoop завершается")]
    public async void Mail2_Contains_messages(){
        Assert.NotNull(await MailBox2!.GetMessage());
        eventLoop!.Stop();
    }

}