namespace Pl0;

public class Graph
{
    private Curve[] _curves;

    public Graph(Curve[] curves)
    {
        _curves = curves;
    }

    public Curve this[int i] => _curves[i];
}