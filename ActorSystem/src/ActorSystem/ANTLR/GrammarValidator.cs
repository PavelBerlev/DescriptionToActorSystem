using Antlr4.Runtime;

namespace ActorSystem.Grammar;

public class GrammarValidator
{
    public bool isCorrectInput(string input)
    {
        AntlrInputStream inputStream = new AntlrInputStream(input);

        var lexer = new ActorsGrammarLexer(inputStream);

        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);

        var parser = new ActorsGrammarParser(commonTokenStream);

        //Обработчик ошибок
        var errorListener = new ErrorListener();
        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);

        parser.actorSystem();
        if(errorListener.HasErrors)
        {
            Console.WriteLine("Найдены ошибки грамматики");
            foreach(var error in  errorListener.Errors)
            {
                Console.WriteLine(error);
            }
        }
        return !errorListener.HasErrors;
    }

    private class ErrorListener : BaseErrorListener
    {
        public List<string> Errors {get;} = new List<string>();
        public bool HasErrors => Errors.Count > 0;
        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
           Errors.Add($"Строка {line} : {charPositionInLine} - {msg}");
        }
    }
}