using Pl0;

var filePath = args[0];

var automata = new Automata(filePath);

bool end = false;

do
{
    var morph = automata.Lex();
    if (morph.Type == MorphType.Symbol)
    {
        Console.WriteLine($"SYMBOL {morph.Symbol} at {morph.LineNr}, {morph.ColNr}");
        if (morph.Symbol == Symbol.EndOfFile)
        {
            end = true;
        }
    }
    if(morph.Type == MorphType.Number)
    {
        Console.WriteLine($"NUMBER {morph.Number} at {morph.LineNr}, {morph.ColNr}");
    }

    if (morph.Type == MorphType.Identifier)
    {
        Console.WriteLine($"IDENTIFIER {morph.Identifier} at {morph.LineNr}, {morph.ColNr}");
    }
}while(!end);