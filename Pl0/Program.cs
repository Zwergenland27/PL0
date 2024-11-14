using Pl0;

var automata = new Automata("C:\\Users\\koenigsf\\source\\repos\\Pl0\\Pl0\\test.pl0");
do
{
    var morp = automata.Lex();
    Console.WriteLine("===================================");
    Console.WriteLine(morp.Type);
    if (morp.Type == MorphType.Symbol)
    {
        Console.WriteLine($"{morp.Symbol} at {morp.LineNr}, {morp.ColNr}");
    }
    if(morp.Type == MorphType.Number)
    {
        Console.WriteLine($"{morp.Number} at {morp.LineNr}, {morp.ColNr}");
    }
    Console.WriteLine("===================================");
}while(true);