using ActorSystem.Communication;

namespace ActorSystem.Tests;

public class RedirectRuleRepositoryTests
{
    [Fact]
    public void Constructor_ShouldInitializeEndPointReceivers()
    {
        var senderMailbox = new Mock<IMailBox>().Object;
        var receiverMailbox = new Mock<IMailBox>().Object;

        var mailBoxes = new Dictionary<string, IMailBox>
        {
            { "Sender", senderMailbox },
            { "Receiver", receiverMailbox }
        };

        var redirectRules = new HashSet<RedirectRule>
        {
            new RedirectRule("Sender", "Rule", "Receiver")
        };

        var repository = new RedirectRuleRepository(mailBoxes, redirectRules);
        var key = new SenderReceiverKey("Sender", "Rule");

        Assert.Equal(receiverMailbox, repository.GetAdressReceiver(key));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenMailBoxIsMissing()
    {
        var mailBoxes = new Dictionary<string, IMailBox>
        {
            { "Sender", new Mock<IMailBox>().Object }
        };

        var redirectRules = new HashSet<RedirectRule>
        {
            new RedirectRule("Sender", "Rule", "Receiver")
        };

        Assert.Throws<ArgumentException>(() => new RedirectRuleRepository(mailBoxes, redirectRules));
    }

    [Fact]
    public void GetAdressReceiver_ShouldThrowKeyNotFoundException_WhenKeyIsNotFound()
    {
        var mailBoxes = new Dictionary<string, IMailBox>();
        var redirectRules = new HashSet<RedirectRule>();
        var repository = new RedirectRuleRepository(mailBoxes, redirectRules);
        var key = new SenderReceiverKey("NonExistentSender", "NonExistentReceiver");

        Assert.Throws<KeyNotFoundException>(() => repository.GetAdressReceiver(key));
    }

    [Fact]
    public void AddOrUpdateAdressReceiver_ShouldAddOrUpdateMailbox()
    {
        var senderMailbox = new Mock<IMailBox>().Object;
        var receiverMailbox = new Mock<IMailBox>().Object;

        var mailBoxes = new Dictionary<string, IMailBox>
        {
            { "Sender", senderMailbox },
            { "Receiver", receiverMailbox }
        };

        var redirectRules = new HashSet<RedirectRule>();

        var repository = new RedirectRuleRepository(mailBoxes, redirectRules);
        var key = new SenderReceiverKey("Sender", "Rule");

        repository.AddOrUpdateAdressReceiver(new RedirectRule("Sender", "Rule", "Receiver"));

        Assert.Equal(receiverMailbox, repository.GetAdressReceiver(key));
    }

    [Fact]
    public void RemoveAdressReceiver_ShouldRemoveMailbox()
    {
        var senderMailbox = new Mock<IMailBox>().Object;
        var receiverMailbox = new Mock<IMailBox>().Object;

        var mailBoxes = new Dictionary<string, IMailBox>
        {
            { "Sender", senderMailbox },
            { "Receiver", receiverMailbox }
        };

        var redirectRules = new HashSet<RedirectRule>
        {
            new RedirectRule("Sender", "Rule", "Receiver")
        };

        var repository = new RedirectRuleRepository(mailBoxes, redirectRules);
        var key = new SenderReceiverKey("Sender", "Rule");

        repository.RemoveAdressReceiver(key);

        Assert.Throws<KeyNotFoundException>(() => repository.GetAdressReceiver(key));
    }
}