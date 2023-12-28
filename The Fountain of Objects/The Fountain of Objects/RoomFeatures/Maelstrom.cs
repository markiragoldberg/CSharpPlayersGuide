using CavernOfObjects;

namespace CavernOfObjects.RoomFeatures;

public class Maelstrom(Point location, GameBoard gameBoard) : IRoomFeature, IMovable
{
    public Point Location { get; set; } = location;
    private readonly GameBoard _gameBoard = gameBoard;

    public bool ShootWithArrow()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("A maelstrom nearby shrieks deafeningly, then falls silent.");
        Console.ResetColor();
        _gameBoard.RemoveFeature(this);
        return true;
    }
    public void StepOn(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(
            "You stray into the raging winds of a maelstrom and are thrown a great distance."); ;
        Console.ResetColor();
        _gameBoard.MovePlayer(+2, -1);
        _gameBoard.Move(this, -2, +1);
    }
    public void Sense(int distance)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("You hear the growling and groaning of a maelstrom nearby.");
        Console.ResetColor();
    }
}
