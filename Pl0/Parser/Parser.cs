namespace Pl0;

public class Parser
{
    private static Curve[] _program = [
        /*00*/new GraphCurve(1, 0, null, 1),
        /*01*/new SymbolCurve(2, 0, null, Symbol.ProgramEnd),
        /*02*/new GraphEndCurve(0, 0, null)
    ];
    
    private static Curve[] _block = [
        /*00*/new SymbolCurve(1, 6, null, Symbol.Const),
        /*01*/new MorphCurve(2, 0, null, MorphType.Identifier),
        /*02*/new SymbolCurve(3, 0, null, Symbol.Equal),
        /*03*/new MorphCurve(4, 0, null, MorphType.Number),
        /*04*/new SymbolCurve(1, 5, null, Symbol.Comma),
        /*05*/new SymbolCurve(7, 0, null, Symbol.Semicolon),
        /*06*/new NilCurve(7, 0, null),
        /*07*/new SymbolCurve(8, 11, null, Symbol.Var),
        /*08*/new MorphCurve(9, 0, null, MorphType.Identifier),
        /*09*/new SymbolCurve(8, 10, null, Symbol.Comma),
        /*10*/new SymbolCurve(12, 0, null, Symbol.Semicolon),
        /*11*/new NilCurve(12, 0, null),
        /*12*/new SymbolCurve(13, 17, null, Symbol.Procedure),
        /*13*/new MorphCurve(14, 0, null, MorphType.Identifier),
        /*14*/new SymbolCurve(15, 0, null, Symbol.Semicolon),
        /*15*/new GraphCurve(16, 0, null, 1),
        /*16*/new SymbolCurve(12, 0, null, Symbol.Semicolon),
        /*17*/new NilCurve(18, 0, null),
        /*18*/new GraphCurve(19, 0, null, 4),
        /*19*/new GraphEndCurve(0, 0, null),
    ];
    
    private static Curve[] _expression = [
        /*00*/new SymbolCurve(1, 2, null, Symbol.Minus),
        /*01*/new GraphCurve(3, 0, null, 3),
        /*02*/new GraphCurve(3, 0, null, 3),
        /*03*/new NilCurve(4, 0, null),
        /*04*/new SymbolCurve(6, 5, null, Symbol.Plus),
        /*05*/new SymbolCurve(7, 8, null, Symbol.Minus),
        /*06*/new GraphCurve(3, 0, null, 3),
        /*07*/new GraphCurve(3, 0, null, 3),
        /*08*/new NilCurve(9, 0, null),
        /*09*/new GraphEndCurve(0, 0, null),
    ];
    
    private static Curve[] _term = [
        /*00*/new GraphCurve(1, 0, null, 5),
        /*01*/new NilCurve(2, 0, null),
        /*02*/new SymbolCurve(4, 3, null, Symbol.Multiply),
        /*03*/new SymbolCurve(5, 6, null, Symbol.Divide),
        /*04*/new GraphCurve(1, 0, null, 5),
        /*05*/new GraphCurve(1, 0, null, 5),
        /*06*/new NilCurve(7, 2, null),
        /*07*/new GraphEndCurve(0, 0, null),
    ];
    
    private static Curve[] _statement = [
        /*00*/new MorphCurve(1, 3, null, MorphType.Identifier),
        /*01*/new SymbolCurve(2, 0, null, Symbol.Assignment),
        /*02*/new GraphCurve(22, 0, null, 2),
        /*03*/new SymbolCurve(4, 7, null, Symbol.If),
        /*04*/new GraphCurve(5, 0, null, 6),
        /*05*/new SymbolCurve(6, 0, null, Symbol.Then),
        /*06*/new GraphCurve(22, 0, null, 4),
        /*07*/new SymbolCurve(8, 11, null, Symbol.While),
        /*08*/new GraphCurve(9, 0, null, 6),
        /*09*/new SymbolCurve(10, 0, null, Symbol.Do),
        /*10*/new GraphCurve(22, 0, null, 4),
        /*11*/new SymbolCurve(12, 15, null, Symbol.Begin),
        /*12*/new GraphCurve(14, 0, null, 4),
        /*13*/new SymbolCurve(12, 0, null, Symbol.Semicolon),
        /*14*/new SymbolCurve(22, 13, null, Symbol.End),
        /*15*/new SymbolCurve(16, 17, null, Symbol.Call),
        /*16*/new MorphCurve(22, 0, null, MorphType.Identifier),
        /*17*/new SymbolCurve(18, 19, null, Symbol.Input),
        /*18*/new MorphCurve(22, 0, null, MorphType.Identifier),
        /*19*/new SymbolCurve(20, 21, null, Symbol.Output),
        /*20*/new GraphCurve(22, 0, null, 2),
        /*21*/new NilCurve(22, 0, null),
        /*22*/new GraphEndCurve(0, 0, null)];
    
    private static Curve[] _factor = [
        /*00*/new MorphCurve(5, 1, null, MorphType.Number),
        /*01*/new SymbolCurve(2, 4, null, Symbol.OpenBraces),
        /*02*/new GraphCurve(3, 0, null, 2),
        /*03*/new SymbolCurve(5, 0, null, Symbol.CloseBraces),
        /*04*/new MorphCurve(5, 0, null, MorphType.Identifier),
        /*05*/new GraphEndCurve(0, 0, null),
    ];
    
    private static Curve[] _condition = [
        /*00*/new SymbolCurve(1, 2, null, Symbol.Odd),
        /*01*/new GraphCurve(10, 0, null, 2),
        /*02*/new GraphCurve(3, 0, null, 2),
        /*03*/new SymbolCurve(9, 4, null, Symbol.Equal),
        /*04*/new SymbolCurve(9, 5, null, Symbol.Unequal),
        /*05*/new SymbolCurve(9, 6, null, Symbol.SmallerThan),
        /*06*/new SymbolCurve(9, 7, null, Symbol.GreaterThan),
        /*07*/new SymbolCurve(9, 8, null, Symbol.SmallerOrEqual),
        /*08*/new SymbolCurve(9, 0, null, Symbol.GreaterOrEqual),
        /*09*/new GraphCurve(10, 0, null, 2),
        /*10*/new GraphEndCurve(0, 0, null),
    ];

    private static Curve[][] _graphes =
    [
        _program,
        _block,
        _expression,
        _term,
        _statement,
        _factor,
        _condition,
    ];

    private Automata _lexer;
    private Morph _currentMorph;

    public Parser(string filePath)
    {
        _lexer = new Automata(filePath);
        _currentMorph = _lexer.Lex();
    }

    public bool Start()
    {
        return _parse(_program);
    }

    private bool _parse(Curve[] graph)
    {
        bool success = false;
        Curve currentCurve = graph[0];
        Console.WriteLine("----------");
        while (true)
        {
            switch (currentCurve)
            {
                case NilCurve:
                    Console.WriteLine("NIL");
                    success = true;
                    break;
                case SymbolCurve c:
                    Console.WriteLine($"SYMBOL {c.Symbol}");
                    success = false;
                    if (_currentMorph.Type == MorphType.Symbol)
                    {
                        success = (_currentMorph.Symbol == c.Symbol);   
                    }
                    break;
                case MorphCurve c:
                    Console.WriteLine($"MORPH {c.Type}");
                    success = (_currentMorph.Type == c.Type);
                    break;
                case GraphCurve c:
                    Console.WriteLine($"START GRAPH {c.GraphIndex}");
                    success = _parse(_graphes[c.GraphIndex]);
                    break;
                case GraphEndCurve:
                    Console.WriteLine($"End GRAPH");
                    Console.WriteLine("----------");
                    return true;
            }

            if (success && currentCurve.OnAccept is not null)
            {
                success = currentCurve.OnAccept();
            }

            if (!success)
            {
                Console.WriteLine("NO");
                if (currentCurve.Alternative != 0)
                {
                    currentCurve = graph[currentCurve.Alternative];
                }
                else
                {
                    Console.WriteLine($"Parse error at {_currentMorph.LineNr}.{_currentMorph.ColNr}");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("YES");
                if (currentCurve is SymbolCurve or MorphCurve)
                {
                    _currentMorph = _lexer.Lex();
                }
                currentCurve = graph[currentCurve.Next];
            }
        }
    }
}