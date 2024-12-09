using ActorSystem.Communication;
namespace ActorSystem.Tests;

public class MailBoxTests
{
    [Fact]
    public async Task PutMessage_ShouldStoreMessage()
    {
        var mailBox = new MailBox();
        var mockMessage = new Mock<IMessage>();

        mailBox.PutMessage(mockMessage.Object);
        var receivedMessage = await mailBox.GetMessage();

        Assert.Equal(mockMessage.Object, receivedMessage);
    }

    [Fact]
        public async Task GetMessage_ShouldReturnCorrectMessage()
        {
            var mailBox = new MailBox();

            var mockMessage1 = new Mock<IMessage>();
            var mockMessage2 = new Mock<IMessage>();

            mailBox.PutMessage(mockMessage1.Object);
            mailBox.PutMessage(mockMessage2.Object);

            var receivedMessage1 = await mailBox.GetMessage();
            var receivedMessage2 = await mailBox.GetMessage();

            Assert.Equal(mockMessage1.Object, receivedMessage1);
            Assert.Equal(mockMessage2.Object, receivedMessage2);
        }

        [Fact]
        public async Task GetMessage_ShouldBlockUntilMessageIsAvailable()
        {
            var mailBox = new MailBox();
            var mockMessage = new Mock<IMessage>();

            var receivedMessage = mailBox.GetMessage();
            await Task.Delay(100);
            mailBox.PutMessage(mockMessage.Object);

            Assert.Equal(mockMessage.Object, await receivedMessage);
        }
}