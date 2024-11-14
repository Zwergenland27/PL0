//#define VERBOSE

namespace Pl0;

public class Automata : IDisposable
{
    private StreamReader _reader;

    private int _currentState = 0;
    private bool _finished = false;
    private Morph? _currentMorph;
    
    private int _currentLine = 1;
    private int _currentColumn = 1;
    private char _currentChar = ' ';
    
    private Action[][] _automataTable;

    private int[] _characterVector;

    public Automata(String filePath)
    {
        _reader = new StreamReader(filePath);
        _initAutomateTable();
        _initCharacterVector();
        _read();
    }

    private void _initAutomateTable()
    {
        //Read
        void R0() => R(0);
        //Write read exit states
        void WRE0() => WRE(0);
        
        //Write read states
        void WR1() => WR(1);
        void WR2() => WR(2);
        void WR3() => WR(3);
        void WR4() => WR(4);
        void WR5() => WR(5);
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
            /* 0*/ [WRE0, WR1, WUR2, WR3, WRE0, WR4, WR5, R0],
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

    private void _initCharacterVector()
    {
        _characterVector =
        [
            /*     0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F*/
            /*00*/ 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
            /*10*/ 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
            /*20*/ 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            /*30*/ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 0, 5, 4, 6, 0,
            /*40*/ 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
            /*50*/ 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0,
            /*60*/ 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
            /*70*/ 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0
        ];
    }

    public Morph Lex()
    {
        _currentState = 0;
        _currentMorph = new Morph(_currentLine, _currentColumn);
        _finished = false;
        do
        {
            #if VERBOSE
            Console.WriteLine($"currently in state {_currentState}");
            #endif
            var nextState = _automataTable[_currentState][_characterVector[_currentChar]];
            nextState();
        } while (!_finished);

        return _currentMorph;
    }

    public void R(int followState)
    {
        #if VERBOSE
        Console.WriteLine($"Read and switch to state {followState}");
        #endif
        _read();
        _currentState = followState;
    }
    
    private void _read()
    {
        var characterRead = _reader.Read();
        if (characterRead == -1)
        {
            _currentChar = '\0';
            return;
        }
        
        _currentChar = (char) characterRead;
        #if VERBOSE
        Console.WriteLine($"Read character {_currentChar}");
        #endif
        if (_currentChar == '\r')
        {
            _currentLine++;
            _currentColumn = 0;
        }
        _currentColumn++;
    }
    
    public void E(int followState)
    {
        #if VERBOSE
        Console.WriteLine($"Exit and switch to state {followState}");
        #endif
        _exit();
        _currentState = followState;
    }
    
    private void _exit()
    {
        _currentMorph!.Finish(_currentState);
        _finished = true;
    }

    public void WUR(int followState)
    {
        #if VERBOSE
        Console.WriteLine($"Write upper read and switch to state {followState}");
        #endif
        _writeUpperRead();
        _currentState = followState;
    }
    
    private void _writeUpperRead()
    {
        _currentMorph!.Append(char.ToUpper(_currentChar));
        
        _read();
    }

    public void WR(int followState)
    {
        #if VERBOSE
        Console.WriteLine($"Write read and switch to state {followState}");
        #endif
        _writeRead();
        _currentState = followState;
    }
    
    private void _writeRead()
    {
        _currentMorph!.Append(_currentChar);
        _read();
    }

    public void WRE(int followState)
    {
        #if VERBOSE
        Console.WriteLine($"Write read exit and switch to state {followState}");
        #endif
        _writeReadExit();
        _currentState = followState;
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