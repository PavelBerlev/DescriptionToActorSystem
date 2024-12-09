using ActorSystem.Communication;

namespace ActorSystem.Tests;

public class RedirectRuleTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        var sender = "Sender";
        var rule = "Rule";
        var receiver = "Receiver";

        var redirectRule = new RedirectRule(sender, rule, receiver);

        Assert.Equal(sender, redirectRule.Sender);
        Assert.Equal(rule, redirectRule.Rule);
        Assert.Equal(receiver, redirectRule.Receiver);
    }

    [Fact]
    public void Equals_ShouldReturnTrueForEqualObjects()
    {
        var redirectRule1 = new RedirectRule("Sender", "Rule", "Receiver");
        var redirectRule2 = new RedirectRule("Sender", "Rule", "Receiver");

        Assert.Equal(redirectRule1,redirectRule2);
    }

    [Fact]
    public void Equals_ShouldReturnFalseForDifferentObjects()
    {
        var redirectRule1 = new RedirectRule("Sender", "Rule", "Receiver");
        var redirectRule2 = new RedirectRule("DifferentSender", "Rule", "Receiver");

        Assert.NotEqual(redirectRule1, redirectRule2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameHashCodeForEqualObjects()
    {
        var redirectRule1 = new RedirectRule("Sender", "Rule", "Receiver");
        var redirectRule2 = new RedirectRule("Sender", "Rule", "Receiver");

        var hashCode1 = redirectRule1.GetHashCode();
        var hashCode2 = redirectRule2.GetHashCode();

        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnDifferentHashCodeForDifferentObjects()
    {
        var redirectRule1 = new RedirectRule("Sender", "Rule", "Receiver");
        var redirectRule2 = new RedirectRule("DifferentSender", "Rule", "Receiver");

        var hashCode1 = redirectRule1.GetHashCode();
        var hashCode2 = redirectRule2.GetHashCode();

        Assert.NotEqual(hashCode1, hashCode2);
    }
}