namespace Pl0;

public class Automata : IDisposable
{
    private StreamReader _reader;

    private int _currentState = 0;
    private int _followState = 0;
    private bool _finished = false;
    private Morph? _currentMorph;
    
    private int _currentLine = 1;
    private int _currentColumn = 0;
    private char _currentChar = ' ';
    
    private CharacterClass _getCharacterClass(char character)
    {
        CharacterClass characterClass = CharacterClass.Other;
        
        //Numbers
        if (char.IsDigit(character))
        {
            characterClass = CharacterClass.Digit;
        }
        
        //Letters
        if (char.IsLetter(character))
        {
            characterClass = CharacterClass.Letter;
        }
        
        //Colon
        if(character == ':')
        {
            characterClass = CharacterClass.Colon;
        }
        
        //Equal
        if(character == '=')
        {
            characterClass = CharacterClass.Equal;
        }
        
        //SmallerThan
        if(character == '<')
        {
            characterClass = CharacterClass.SmallerThan;
        }
        
        //GreaterThan
        if(character == '>')
        {
            characterClass = CharacterClass.GreaterThan;
        }

        return characterClass;
    }
    
    //TODO: Implement the automata table using delegates to the automate methods read, writeRead etc. and add the followState as int parameter
    //[State][CharacterClass as int]
    private Action[][] _automataTable;

    public Automata(String filePath)
    {
        _reader = new StreamReader(filePath);
        _initTable();
    }

    private void _initTable()
    {
        //Write read exit states
        void WRE0() => WRE(0);
        
        //Write read states
        void WR1() => WR(1);
        void WR2() => WR(2);
        void WR3() => WR(3);
        void WR4() => WR(4);
        void WR5() => WR(6);
        void WR6() => WR(6);
        void WR7() => WR(7);
        void WR8() => WR(8);
        
        //Write upper read states
        void WUR2() => WUR(2);
        
        //Exit states
        void E0() => E(0);
        void E1() => E(1);
        void E2() => E(2);
        void E3() => E(3);
        void E4() => E(4);
        void E5() => E(5);
        void E6() => E(6);
        void E7() => E(7);
        void E8() => E(8);
        _automataTable =
        [
            /*State Spec  Dig  Let   Col  Equa  LT   GT   Ot*/
            /* 0*/ [WRE0, WR1, WUR2, WR3, WRE0, WR4, WR5, E0],
            /* 1*/ [E1,   WR1, E1,   E1,  E1,   E1,  E1,  E1],
            /* 2*/ [E2,   WR2, WUR2, E2,  E2,   E2,  E2,  E2],
            /* 3*/ [E3,   E3,  E3,   E3,  WR6,  E3,  E3,  E3],
            /* 4*/ [E4,   E4,  E4,   E4,  WR7,  E4,  E4,  E4],
            /* 5*/ [E5,   E5,  E5,   E5,  WR8,  E5,  E5,  E5],
            /* 6*/ [E6,   E6,  E6,   E6,  E6,   E6,  E6,  E6],
            /* 7*/ [E7,   E7,  E7,   E7,  E7,   E7,  E7,  E7],
            /* 8*/ [E8,   E8,  E8,   E8,  E8,   E8,  E8,  E8],
        ];
    }

    public Morph Lex()
    {
        _currentState = 0;
        _currentMorph = null;
        _finished = false;
        _read();
        do
        {
            var nextState = _automataTable[_currentState][(int)_getCharacterClass(_currentChar)];
            _currentState = _followState;
            nextState();
        } while (!_finished);

        if (_currentMorph is null)
        {
            throw new InvalidDataException("No morph was created");
        }

        return _currentMorph;
    }

    public void R(int followState)
    {
        _followState = followState;
        _read();
    }
    
    private void _read()
    {
        var characterRead = _reader.Read();
        if (characterRead == -1)
        {
            return;
        }
        
        _currentChar = (char) characterRead;
        if (_currentChar == '\r')
        {
            _currentLine++;
            _currentColumn = 0;
        }
        _currentColumn++;
    }

    public void E(int followState)
    {
        _followState = followState;
        _exit();
    }
    
    private void _exit()
    {
        if (_currentMorph is null)
        {
            throw new InvalidOperationException("No morph was created");
        }
        _currentMorph.Finish(_currentState);
        _finished = true;
    }

    public void WUR(int followState)
    {
        _followState = followState;
        _writeUpperRead();
    }
    
    private void _writeUpperRead()
    {
        if (_currentMorph is null)
        {
            _currentMorph = new Morph(_currentLine, _currentColumn);
        }
        
        _currentMorph.AppendChar(char.ToUpper(_currentChar));

        _read();
    }

    public void WR(int followState)
    {
        _followState = followState;
        _writeRead();
    }
    
    private void _writeRead()
    {
        if (_currentMorph is null)
        {
            _currentMorph = new Morph(_currentLine, _currentColumn);
        }
        
        _currentMorph.AppendChar(_currentChar);

        _read();
    }

    public void WRE(int followState)
    {
        _followState = followState;
        _writeReadExit();
    }
    
    private void _writeReadExit()
    {
        _writeRead();
        _exit();
    }

    public void Dispose()
    {
        _reader.Dispose();
    }
}