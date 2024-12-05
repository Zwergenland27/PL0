namespace Pl0;

public class GraphCurve : Curve
{
    public int GraphIndex { get; private init; }
    public GraphCurve(
        int next,
        int alternative,
        Func<bool>? onAccept,
        int graphIndex) : base(next, alternative, onAccept)
    {
        GraphIndex = graphIndex;
    }
}