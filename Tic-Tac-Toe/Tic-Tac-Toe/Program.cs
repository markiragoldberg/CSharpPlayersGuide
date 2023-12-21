using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

TicTacToeGame game = new();
game.Run();

// -----------------------------------------

class TicTacToeGame
{
    private TicTacToeMatch _match;
    private TicTacToeDisplay _display;
    public TicTacToeGame()
    { 
        _match = new TicTacToeMatch();
        _display = new TicTacToeDisplay(_match);
    }

    public void Run()
    {
        TicTacToeDisplay.ShowIntro();
        char currentPlayer = 'X';
        // enum of gamestates instead?
        char? winner = null;
        while(true)
        {
            _display.ShowMatch(currentPlayer);
            if(_match.GameOver)
            {
                break; 
            }
            string? input = Console.ReadLine();
            int x = -1;
            int y = -1;
            if (input != null && input.Length == 2)
            {
                try
                {
                    int coords = int.Parse(input);
                    x = coords / 10;
                    y = coords % 10;
                }
                catch
                {
                }
            }
            if(x != -1 && y != -1)
            {
                string? reason = _match.InvalidMoveReason(x, y);
                if(reason == null)
                {
                    _match.ClaimCell(x, y, currentPlayer);
                    currentPlayer = (currentPlayer == 'X' ? 'O' : 'X');
                }
                else
                {
                    TicTacToeDisplay.ShowBadMove(x, y, reason);
                }
            }
            else
            {
                TicTacToeDisplay.BadInput();
            }
        }
    }
}

class TicTacToeMatch
{
    public bool GameOver { get; private set; }
    public char? Winner { get; private set; }
    private readonly char[,] _cells;
    public TicTacToeMatch()
    {
        GameOver = false;
        Winner = null;
        _cells = new char[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _cells[i, j] = ' ';
            }
        }
    }

    public char CellAt(int x, int y)
    {
        if (0 <= x && x <= 2 && 0 <= y && y <= 2)
        {
            return _cells[x, y];
        }
        return '?';
    }

    public string? InvalidMoveReason(int x, int y)
    {
        if (0 <= x && x <= 2 && 0 <= y && y <= 2)
        {
            if (_cells[x, y] == ' ')
            {
                return null;
            }
            return "it has already been claimed";
        }
        else
        {
            return "it isn't on the board";
        }
    }

    public bool ClaimCell(int x, int y, char player)
    {
        if (InvalidMoveReason(x, y) == null)
        {
            _cells[x, y] = player;
            CheckForGameOver();
            return true;
        }
        return false;
    }
    private void CheckForGameOver()
    {
        // check center cell for win (4/8 possibilities)
        if (_cells[1, 1] != ' ')
        {
            if ((_cells[1, 1] == _cells[0, 0] && 
                _cells[1, 1] == _cells[2, 2]) ||
                (_cells[1, 1] == _cells[2, 0] && 
                _cells[1, 1] == _cells[0, 2]) ||
                (_cells[1, 1] == _cells[0, 1] &&
                _cells[1, 1] == _cells[2, 1]) ||
                (_cells[1, 1] == _cells[1, 0] &&
                _cells[1, 1] == _cells[1, 2]))
            {
                GameOver = true;
                Winner = _cells[1, 1];
            }
        }
        // then check corners 0, 0 and 2, 2 for wins
        if (_cells[0, 0] != ' ')
        {
            if ((_cells[0, 0] == _cells[1, 0] &&
                _cells[0, 0] == _cells[2, 0]) ||
                (_cells[0, 0] == _cells[0, 1] &&
                _cells[0, 0] == _cells[0, 2]))
            {
                GameOver = true;
                Winner = _cells[0, 0];
            }
        }
        if (_cells[2, 2] != ' ')
        {
            if ((_cells[2, 2] == _cells[1, 2] &&
                _cells[2, 2] == _cells[0, 2]) ||
                (_cells[2, 2] == _cells[2, 1] &&
                _cells[2, 2] == _cells[2, 0]))
            {
                GameOver = true;
                Winner = _cells[2, 2];
            }
        }
        // Early exit if any cells unoccupied
        for(int i = 0; i < _cells.GetLength(0); i++)
        {
            for(int j = 0; j < _cells.GetLength(1); j++)
            {
                if(_cells[i, j] == ' ')
                {
                    return;
                }
            }
        }
        // All cells are occupied so it is a draw
        GameOver = true;
    }
}

class TicTacToeDisplay
{
    private readonly TicTacToeMatch _Match;

    public TicTacToeDisplay(TicTacToeMatch match)
    {
        _Match = match;
    }

    public void ShowMatch(char currentPlayer)
    {
        Console.WriteLine($"It is {currentPlayer}'s turn.");
        Console.WriteLine($" {_Match.CellAt(0,2)} | {_Match.CellAt(1, 2)} | {_Match.CellAt(2, 2)} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {_Match.CellAt(0, 1)} | {_Match.CellAt(1, 1)} | {_Match.CellAt(2, 1)} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {_Match.CellAt(0, 0)} | {_Match.CellAt(1, 0)} | {_Match.CellAt(2, 0)} ");
        if (_Match.Winner == null)
        {
            if(_Match.GameOver)
            {
                Console.WriteLine("The game is a draw. Game over!");
            }
            else
            {
                Console.Write("What square do you want to play in? ");
            }
        }
        else
        {
            Console.WriteLine($"{_Match.Winner} has won the game!");
        }
    }

    public static void ShowBadMove(int x, int y, string reason)
    {
        Console.WriteLine($"You cannot claim ({x},{y}) because {reason}.");
    }
    public static void ShowIntro()
    {
        Console.WriteLine("Enter two digits, for example '11', to claim a cell on the board.");
    }
    public static void BadInput()
    {
        Console.WriteLine("Sorry. Please enter two digits, such as \"11\".");
    }
}
