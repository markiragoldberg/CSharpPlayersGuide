using CavernOfObjects;

namespace CavernOfObjects.RoomFeatures;

public class Entrance(Point location) : IRoomFeature
{
    public Point Location { get; } = location;
    public void StepOn(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("You see light coming from the cavern entrance.");
        Console.ResetColor();
    }
}
