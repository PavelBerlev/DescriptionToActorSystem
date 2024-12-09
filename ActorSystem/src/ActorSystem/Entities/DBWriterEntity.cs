using System.Xml.Linq;

namespace ActorSystem.Entity;

public class DBWriterEntity : IXmlEntity
{
    public IDictionary<string,object> Fields{get;set;}
    public DBWriterEntity()
    {
        Fields = new Dictionary<string, object>();
    }

    public void ReadFromXml(XElement element)
    {
        Fields["ActorType"] = "DBWriter";
        Fields["Name"] = element.Element("Name")!.Value;
        Fields["Collection"] = element.Element("Collection")!.Value;
        Fields["Keys"] = element.Element("Keys")!.Elements("Key").Select(k => k.Value).ToList()!;
        if(element.Element("Next") != null)
        {
            Fields["Next"] = element.Element("Next")!.Value;
        }
    }
    
    public override string ToString()
    {
        return $"DBWriter: Name={Fields["Name"]}, Collection={Fields["Collection"]}, Keys={string.Join(", ", (List<string>)Fields["Keys"])}";
    }
}