using Antlr4.Runtime;
using ActorSystem.Grammar;
namespace ActorSystem.Tests;
public class GrammarTest
{
    [Fact]
    public void Current_string_respond_grammar()
    {
        string pathToTextFile = "../../../ParserTests/ProcessText.txt";
        string str = File.ReadAllText(pathToTextFile);

        // Создание входного потока ANTLR
        AntlrInputStream input = new AntlrInputStream(str);
        // Создание лексера
        ActorsGrammarLexer lexer = new ActorsGrammarLexer(input);
        // Создание токен-стрима на основе лексера
        CommonTokenStream tokens = new CommonTokenStream(lexer);
        // Создание парсера
        ActorsGrammarParser parser = new ActorsGrammarParser(tokens);
        parser.actorSystem();
        Assert.Equal(0, parser.NumberOfSyntaxErrors);
    }

    [Fact]
    public void Custom_grammar_error_listener_respond_grammar()
    {
        string pathToTextFile = "../../../ParserTests/ProcessText.txt";
        string str = File.ReadAllText(pathToTextFile);
        var grammarValidator = new GrammarValidator();
        
        Assert.True(grammarValidator.isCorrectInput(str));
    }
}
