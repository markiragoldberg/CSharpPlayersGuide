using CavernOfObjects.RoomFeatures;

namespace CavernOfObjects;

public class Player(Point location, GameBoard gameBoard) : IMovable
{
    public Point Location { get; set; } = location;
    public int Arrows { get; private set; } = 5;
    public bool IsAlive { get; private set; } = true;
    private GameBoard _gameBoard = gameBoard;

    public void Shoot(int x, int y)
    {
        if (Arrows > 0)
        {
            Arrows -= 1;
            _gameBoard.Shoot(new Point(Location.X + x, Location.Y + y));
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You are out of arrows.");
            Console.ResetColor();
        }
    }
    public void EnableFountain()
    {
        _gameBoard.Enable(Location);
    }
    public void Kill() => IsAlive = false;
}
