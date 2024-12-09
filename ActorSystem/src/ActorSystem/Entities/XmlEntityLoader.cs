using System.Xml.Linq;

namespace ActorSystem.Entity;

public class XmlEntityLoader(IServiceProvider provider)
{
    public List<IEntity> Load(XElement actorSystem)
    {
        var factory = provider.GetRequiredService<XmlEntityFactory>();
        List<IXmlEntity> entities = new();
        foreach(var element in actorSystem.Elements())
        {
            try
            {
                var entity = factory.CreateEntity(element.Name.LocalName);
                entity.ReadFromXml(element);
                entities.Add(entity);
            }
            catch(NotSupportedException)
            {
                throw;
            }
        }
        return entities.Cast<IEntity>().ToList();
    }
}