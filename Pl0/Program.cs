using Pl0;

var automata = new Automata("C:\\Users\\koenigsf\\source\\repos\\Pl0\\Pl0\\test.pl0");
do
{
    var morp = automata.Lex();
    Console.WriteLine(morp.Type);
    if (morp.Type == MorphType.Symbol)
    {
        Console.WriteLine($"{morp.Symbol} at {morp.LineNr}, {morp.ColNr}");
    }
}while(Console.ReadLine() != "e");