namespace ActorSystem.Communication;

//Нужно по заданному получателю, получить адрес почтового ящика
public interface IRedirectRuleRepository
{
    IMailBox GetAdressReceiver(SenderReceiverKey key);
    void AddOrUpdateAdressReceiver(RedirectRule rule);
    void RemoveAdressReceiver(SenderReceiverKey key);
}