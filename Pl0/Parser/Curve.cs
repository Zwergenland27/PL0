namespace Pl0;

public abstract class Curve
{
    public int Next { get; private init; }
    public int Alternative { get; private init; }
    
    public Func<bool>? OnAccept { get; private init; }

    protected Curve(
        int next,
        int alternative,
        Func<bool>? onAccept)
    {
        Next = next;
        Alternative = alternative;
        OnAccept = onAccept;
    }
}