namespace Pl0;

public class SymbolCurve : Curve
{
    public Symbol Symbol { get; private init; }
    public SymbolCurve(
        int next,
        int alternative,
        Func<bool>? onAccept,
        Symbol symbol) : base(next, alternative, onAccept)
    {
        Symbol = symbol;
    }
}