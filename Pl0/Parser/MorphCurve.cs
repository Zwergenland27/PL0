namespace Pl0;

public class MorphCurve : Curve
{
    public MorphType Type { get; private init; }
    public MorphCurve(
        int next,
        int alternative,
        Func<bool>? onAccept,
        MorphType type) : base(next, alternative, onAccept)
    {
        Type = type;
    }
}