using System.Xml.Linq;

namespace ActorSystem.Entity;

public interface IXmlEntity : IEntity
{
    void ReadFromXml(XElement element);
}