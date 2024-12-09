namespace ActorSystem.Parser;
using System.Xml;
public class XMLParser : IParserFileToString
{

    private readonly XmlDocument _xmlDocument;
    public XMLParser(XmlDocument xmlDocument)
    {
        _xmlDocument = xmlDocument;
    }
    public string ConvertToString()
    {
        string sequence = "";
        // Получаем последовательность значений элементов
        foreach (XmlNode child in _xmlDocument.DocumentElement!.ChildNodes)
        {
            sequence += ProcessElement(child);
        }
        return sequence;
    }

    private string ProcessElement(XmlNode element)
    {
        string result = "";
        //если это конечный узел, то нужно записать его значение
        if (!element.HasChildNodes)
        {
            //Либо это текст либо пустой элемент
            string textValueElement = element.InnerText.Trim();
            //Значит это пустой элемент
            if (textValueElement == "")
            {
                result += element.LocalName + " ";
            }
            //Значит это текст
            else
            {
                result += textValueElement + " ";
            }
        }
        else
        {
            result += element.LocalName + " ";
            // Обрабатываем дочерние элементы
            foreach (XmlNode child in element.ChildNodes)
            {
                result += ProcessElement(child);
            }
        }
        return result;
    }
}