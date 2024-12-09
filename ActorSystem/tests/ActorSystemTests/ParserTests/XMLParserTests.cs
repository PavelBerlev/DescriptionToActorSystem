using System.Xml;
using ActorSystem.Parser;
namespace ActorSystem.Tests;
public class XMLParserTests
{
    [Fact]
    public void ConvertToString_returns_expected_result_actors_system()
    {

        //Как это нормально указать ???
        string pathToXML = "../../../ParserTests/ProcessXML.xml";
        string pathToText = "../../../ParserTests/ProcessText.txt";

        var document = new XmlDocument();
        document.Load(pathToXML);
        IParserFileToString parser = new XMLParser(document);
        string currentValue = parser.ConvertToString();

        string expectedValue = File.ReadAllText(pathToText);
        Assert.Equal(expectedValue, currentValue);
    }
}