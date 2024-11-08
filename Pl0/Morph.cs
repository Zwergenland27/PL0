namespace Pl0;

//TODO: Number, Text, Symbol should be implemented in a better way
public class Morph
{
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
            
            return float.Parse(_value);
        }
    }

    public Symbol Symbol
    {
        get
        {
            if(Type != MorphType.Symbol)
                throw new InvalidOperationException("Morph is not a symbol");

            return _getSymbol();
        }
    }

    public String Identifier
    {
        get
        {
            if (Type != MorphType.Identifier)
                throw new InvalidOperationException("Morph is not an identifier");

            return _value;
        }
    }
    
    private Symbol _getSymbol()
    {
        return _value switch
        {
            "call" => Symbol.Call,
            "if" => Symbol.If,
            "begin" => Symbol.Begin,
            "end" => Symbol.End,
            "do" => Symbol.Do,
            "while" => Symbol.While,
            "then" => Symbol.Then,
            "odd" => Symbol.Odd,
            "var" => Symbol.Var,
            "const" => Symbol.Const,
            ":=" => Symbol.Assignment,
            ":" => Symbol.Colon,
            "<=" => Symbol.SmallerOrEqual,
            "<" => Symbol.SmallerThan,
            "=" => Symbol.Equal,
            ">=" => Symbol.GreaterOrEqual,
            ">" => Symbol.GreaterThan,
            _ => throw new Exception($"{_value} is no valid symbol")
        };
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