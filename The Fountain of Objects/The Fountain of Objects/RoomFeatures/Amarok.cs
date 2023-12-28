using CavernOfObjects;

namespace CavernOfObjects.RoomFeatures;

public class Amarok(Point location, GameBoard board) : IRoomFeature
{
    public Point Location { get; } = location;
    private readonly GameBoard _gameBoard = board;
    public bool ShootWithArrow()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Your arrow strikes an amarok and kills it.");
        Console.ResetColor();
        _gameBoard.RemoveFeature(this);
        return true;
    }
    public void StepOn(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("An amarok lunges at you from the darkness and swiftly kills you.");
        player.Kill();
        Console.ResetColor();
    }
    public void Sense(int distance)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("You can smell the rotten stench of an amarok in a nearby room.");
        Console.ResetColor();
    }
}
