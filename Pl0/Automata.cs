namespace Pl0;

public class Automata : IDisposable
{
    private StreamReader _reader;

    private int _currentState = 0;
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
    private AC[][] _automataTable =
    [
        /*State Special    Digit     Letter     Colon     Equal      LessThan  GreaterThan Other*/
        /* 0*/ [AC.WRE(0), AC.WR(1), AC.WUR(2), AC.WR(3), AC.WRE(0), AC.WR(4), AC.WR(5), AC.E(0)],
        /* 1*/ [AC.E(1),   AC.WR(1), AC.E(1),   AC.E(1),  AC.E(1),   AC.E(1),  AC.E(1),  AC.E(1)],
        /* 2*/ [AC.E(2),   AC.WR(2), AC.WUR(2), AC.E(2),  AC.E(2),   AC.E(2),  AC.E(2),  AC.E(2)],
        /* 3*/ [AC.E(3),   AC.E(3),  AC.E(3),   AC.E(3),  AC.WR(6),  AC.E(3),  AC.E(3),  AC.E(3)],
        /* 4*/ [AC.E(4),   AC.E(4),  AC.E(4),   AC.E(4),  AC.WR(7),  AC.E(4),  AC.E(4),  AC.E(4)],
        /* 5*/ [AC.E(5),   AC.E(5),  AC.E(5),   AC.E(5),  AC.WR(8),  AC.E(5),  AC.E(5),  AC.E(5)],
        /* 6*/ [AC.E(6),   AC.E(6),  AC.E(6),   AC.E(6),  AC.E(6),   AC.E(6),  AC.E(6),  AC.E(6)],
        /* 7*/ [AC.E(7),   AC.E(7),  AC.E(7),   AC.E(7),  AC.E(7),   AC.E(7),  AC.E(7),  AC.E(7)],
        /* 8*/ [AC.E(8),   AC.E(8),  AC.E(8),   AC.E(8),  AC.E(8),   AC.E(8),  AC.E(8),  AC.E(8)],
    ];

    public Automata(String filePath)
    {
        _reader = new StreamReader(filePath);
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
            _currentState = nextState.FollowState;
            switch (nextState.Action)
            {
                case AutomataAction.Read:
                    _read();
                    break;
                case AutomataAction.Exit:
                    _exit();
                    break;
                case AutomataAction.WriteUpperRead:
                    _writeUpperRead();
                    break;
                case AutomataAction.WriteRead:
                    _writeRead();
                    break;
                case AutomataAction.WriteReadExit:
                    _writeReadExit();
                    break;
            }
        } while (!_finished);

        if (_currentMorph is null)
        {
            throw new InvalidDataException("No morph was created");
        }

        return _currentMorph;
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
    
    private void _exit()
    {
        if (_currentMorph is null)
        {
            throw new InvalidOperationException("No morph was created");
        }
        _currentMorph.Finish(_currentState);
        _finished = true;
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

    private void _writeRead()
    {
        if (_currentMorph is null)
        {
            _currentMorph = new Morph(_currentLine, _currentColumn);
        }
        
        _currentMorph.AppendChar(_currentChar);

        _read();
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