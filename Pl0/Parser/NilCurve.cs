namespace Pl0;

public class NilCurve : Curve
{
    public NilCurve(int next, int alternative, Func<bool>? onAccept) : base(next, alternative, onAccept)
    {
    }
}