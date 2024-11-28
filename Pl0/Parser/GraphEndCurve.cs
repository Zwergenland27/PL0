namespace Pl0;

public class GraphEndCurve : Curve
{
    public GraphEndCurve(int next, int alternative, Func<bool>? onAccept) : base(next, alternative, onAccept)
    {
    }
}