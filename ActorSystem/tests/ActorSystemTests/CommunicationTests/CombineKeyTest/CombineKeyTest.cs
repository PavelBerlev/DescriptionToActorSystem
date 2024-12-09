using ActorSystem.Communication;

namespace ActorSystem.Tests;

public class CombieKeyTest
{
    [Fact]
    public void Two_Key_Have_Same_Hashcode_And_They_Are_Equal()
    {
        var key1 = new SenderReceiverKey("Sender", "Receiver");
        var key2 = new SenderReceiverKey("Sender", "Receiver");
        Assert.Equal(key1.GetHashCode(), key2.GetHashCode());
        Assert.True(key1.Equals(key2));
    }

    [Fact]
    public void Two_Key_Have_Diff_Hashcode_And_They_Are_Not_Equal()
    {
        var key1 = new SenderReceiverKey("Sender", "Receiver");
        var key2 = new SenderReceiverKey("Sender", "DiffReceiver");
        Assert.NotEqual(key1.GetHashCode(), key2.GetHashCode());
        Assert.False(key1.Equals(key2));
    }
}