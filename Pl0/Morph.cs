namespace Pl0;

//TODO: Number, Text, Symbol should be implemented in a better way
public class Morph
{
    private Symbol? _symbol;

    private float _number;

    private string? _identifier;
    public MorphType Type { get; private set; }
    
    public int LineNr { get; private init; }
    
    public int ColNr { get; private init; }
    
    public int Length { get; private set; }

    private string _value;

    public float Number
    {
        get
        {
            if(Type != MorphType.Number)
                throw new InvalidOperationException("Morph is not a number");

            return _number;
        }
    }

    public Symbol Symbol
    {
        get
        {
            if(Type != MorphType.Symbol)
                throw new InvalidOperationException("Morph is not a symbol");

            return _symbol!.Value;
        }
    }

    public String Identifier
    {
        get
        {
            if(Type != MorphType.Identifier)
                throw new InvalidOperationException("Morph is not an identifier");
            
            return _identifier!;
        }
    }
    
    private void _handleState2()
    {
        Type = MorphType.Symbol;
        switch (_value)
        {
            case "CALL":
                _symbol = Symbol.Call;
                break;
            case "IF":
                _symbol = Symbol.If;
                break;
            case "BEGIN":
                _symbol = Symbol.Begin;
                break;
            case "END":
                _symbol = Symbol.End;
                break;
            case "DO":
                _symbol = Symbol.Do;
                break;
            case "WHILE":
                _symbol = Symbol.While;
                break;
            case "THEN":
                _symbol = Symbol.Then;
                break;
            case "ODD":
                _symbol = Symbol.Odd;
                break;
            case "VAR":
                _symbol = Symbol.Var;
                break;
            case "CONST":
                _symbol = Symbol.Const;
                break;
            case "PROCEDURE":
                _symbol = Symbol.Procedure;
                break;
            default:
                Type = MorphType.Identifier;
                _identifier = _value;
                break;
                
        }
    }

    public Morph(int lineNr, int colNr)
    {
        Type = MorphType.Empty;
        LineNr = lineNr;
        ColNr = colNr;
        Length = 0;
        _value = "";
    }

    public void AppendChar(char character)
    {
        _value += character;
    }

    public void Finish(int lastState)
    {
        switch (lastState)
        {
            case 0:
                Type = MorphType.Symbol;
                _symbol = Symbol.Equal;
                break;
            case 1:
                Type = MorphType.Number;
                _number = float.Parse(_value);
                break;
            case 2:
                _handleState2();
                break;
            case 3:
                Type = MorphType.Symbol;
                _symbol = Symbol.Colon;
                break;
            case 4:
                Type = MorphType.Symbol;
                _symbol = Symbol.SmallerThan;
                break;
            case 5:
                Type = MorphType.Symbol;
                _symbol = Symbol.GreaterThan;
                break;
            case 6:
                Type = MorphType.Symbol;
                _symbol = Symbol.Assignment;
                break;
            case 7:
                Type = MorphType.Symbol;
                _symbol = Symbol.SmallerOrEqual;
                break;
            case 8:
                Type = MorphType.Symbol;
                _symbol = Symbol.GreaterOrEqual;
                break;
                
        }
        if (lastState is 3 or 4 or 5 or 0)
        {
            Type = MorphType.Symbol;
            return;
        }
        
        if (lastState is 1)
        {
            Type = MorphType.Number;
            return;
        }

        if (lastState is 6 or 7 or 8)
        {
            Type = MorphType.Symbol;
            
        }
    }
}