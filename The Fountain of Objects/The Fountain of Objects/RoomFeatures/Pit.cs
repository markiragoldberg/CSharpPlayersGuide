using CavernOfObjects;

namespace CavernOfObjects.RoomFeatures;

public class Pit(Point location) : IRoomFeature
{
    public Point Location { get; } = location;
    public void StepOn(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You fall into a pit and die.");
        player.Kill();
        Console.ResetColor();
    }
    public void Sense(int distance)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("You feel a draft. There is a pit in a nearby room.");
        Console.ResetColor();
    }
}
