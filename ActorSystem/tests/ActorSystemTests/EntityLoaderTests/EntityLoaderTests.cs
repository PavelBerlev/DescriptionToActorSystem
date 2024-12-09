using System.Xml.Linq;
using ActorSystem.DI;
using ActorSystem.Entity;
using ActorSystem.Factory;
namespace ActorSystemTests;
public class EntityLoaderTests
{
    [Fact]
    public void ReadFromXmlDbWriterSuccess()
    {
        string xmlContent = @"
        <ActorSystem>
            <DBWriter>
                <Name>WriteInDB</Name>
                <Collection>StudentHomeWorks</Collection>
                <Next>TaskStudentNotion</Next>
                <Keys>
                    <Key>Group</Key>
                    <Key>TaskName</Key>
                    <Key>Description</Key>
                </Keys>
            </DBWriter>
        <TaskNotificator>
            <Name>TaskStudentNotion</Name>
            <Collection>Students</Collection>
        </TaskNotificator>
        </ActorSystem>";

        XDocument doc = XDocument.Parse(xmlContent);
        var actors = doc.Element("ActorSystem");
        XmlEntityLoader entityLoader = IoC.Resolve<XmlEntityLoader>();
        var entities = entityLoader.Load(actors!);
        Assert.Equal("DBWriter: Name=WriteInDB, Collection=StudentHomeWorks, Keys=Group, TaskName, Description",entities[0].ToString());
        Assert.Equal("TaskNotificator: Name=TaskStudentNotion, Collection=Students",entities[1].ToString());
    }
    [Fact]
    public void ThrowNotSupportedException()
    {
        string xmlContent = @"
        <ActorSystem>
            <UnSupported>
            </UnSupported>
        </ActorSystem>";

        XDocument doc = XDocument.Parse(xmlContent);
        var actors = doc.Element("ActorSystem");
        XmlEntityLoader entityLoader =IoC.Resolve<XmlEntityLoader>();
        Assert.Throws<NotSupportedException>(()=>entityLoader.Load(actors!));
    }

    [Fact]
    public void correctRedirectRules()
    {
        string xmlContent = @"
        <ActorSystem>
            <DBWriter>
                <Name>WriteInDB</Name>
                <Collection>StudentHomeWorks</Collection>
                <Next>TaskStudentNotion</Next>
                <Keys>
                    <Key>Group</Key>
                    <Key>TaskName</Key>
                    <Key>Description</Key>
                </Keys>
            </DBWriter>
        <TaskNotificator>
            <Name>TaskStudentNotion</Name>
            <Collection>Students</Collection>
        </TaskNotificator>
        </ActorSystem>";

        XDocument doc = XDocument.Parse(xmlContent);
        var actors = doc.Element("ActorSystem");
        XmlEntityLoader entityLoader = IoC.Resolve<XmlEntityLoader>();
        var entities = entityLoader.Load(actors!);
        var rules = IoC.Resolve<IRedirectRulesFactory>().Create(entities);
        Assert.Equal(1, rules.Count);
    }
}