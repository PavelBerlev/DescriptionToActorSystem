using System.Xml.Linq;

namespace ActorSystem.Entity;

public class TaskNotificatorEntity : IXmlEntity
{
    public IDictionary<string,object> Fields{get;set;}
    public TaskNotificatorEntity()
    {
        Fields = new Dictionary<string, object>();
    }

    public void ReadFromXml(XElement element)
    {
        Fields["ActorType"] = "TaskNotificator";
        Fields["Name"] = element.Element("Name")?.Value!;
        Fields["Collection"] = element.Element("Collection")?.Value!;
    }
    
    public override string ToString()
    {
        return $"TaskNotificator: Name={Fields["Name"]}, Collection={Fields["Collection"]}";
    }
}