namespace Pl0;

public class Parser
{
    private static Curve[] _expressionGraph = [];

    private static Curve[] _factorGraph = [
        new MorphCurve(5, 1, null, MorphType.Identifier),
        new MorphCurve(5, 2, null, MorphType.Number),
        new SymbolCurve(3, 0, null, Symbol.OpenBraces),
        new GraphCurve(4, 0, null, _expressionGraph),
        new SymbolCurve(5, 0, null, Symbol.CloseBraces),
        new GraphEndCurve(0, 0, null),
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
        return _parse(_factorGraph);
    }

    private bool _parse(Curve[] graph, int startIndex = 0)
    {
        bool success = false;
        Curve currentCurve = graph[startIndex];
        while (true)
        {
            switch (currentCurve)
            {
                case NilCurve:
                    success = true;
                    break;
                case SymbolCurve c:
                    success = (_currentMorph.Symbol == c.Symbol);
                    break;
                case MorphCurve c:
                    success = (_currentMorph.Type == c.Type);
                    break;
                case GraphCurve c:
                    success = _parse(c.Graph);
                    break;
                case GraphEndCurve:
                    return true;
            }

            if (success && currentCurve.OnAccept is not null)
            {
                success = currentCurve.OnAccept();
            }

            if (!success)
            {
                if (currentCurve.Alternative != 0)
                {
                    currentCurve = graph[currentCurve.Alternative];
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (currentCurve is SymbolCurve or MorphCurve)
                {
                    _currentMorph = _lexer.Lex();
                }
                currentCurve = graph[currentCurve.Next];
            }
        }
    }
}