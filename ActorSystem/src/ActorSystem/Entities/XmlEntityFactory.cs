namespace ActorSystem.Entity;

public class XmlEntityFactory(IServiceProvider provider)
{
    public IXmlEntity CreateEntity(string entityName)
    {
        var entity = provider.GetKeyedService<IXmlEntity>(entityName);
        if (entity != null)
        {
            return entity;
        }
        
        throw new NotSupportedException($"Entity '{entityName}' is not supported.");
    }
}