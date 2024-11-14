using Pl0;

var automata = new Automata("C:\\Users\\koenigsf\\source\\repos\\Pl0\\Pl0\\test.pl0");

bool end = false;

do
{
    var morp = automata.Lex();
    if (morp.Type == MorphType.Symbol)
    {
        Console.WriteLine($"SYMBOL {morp.Symbol} at {morp.LineNr}, {morp.ColNr}");
        if (morp.Symbol == Symbol.EndOfFile)
        {
            end = true;
        }
    }
    if(morp.Type == MorphType.Number)
    {
        Console.WriteLine($"NUMBER {morp.Number} at {morp.LineNr}, {morp.ColNr}");
    }

    if (morp.Type == MorphType.Identifier)
    {
        Console.WriteLine($"IDENTIFIER {morp.Identifier} at {morp.LineNr}, {morp.ColNr}");
    }
}while(!end);