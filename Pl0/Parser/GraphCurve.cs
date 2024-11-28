namespace Pl0;

public class GraphCurve : Curve
{
    public Curve[] Graph { get; private init; }
    public GraphCurve(
        int next,
        int alternative,
        Func<bool>? onAccept,
        Curve[] graph) : base(next, alternative, onAccept)
    {
        Graph = graph;
    }
}