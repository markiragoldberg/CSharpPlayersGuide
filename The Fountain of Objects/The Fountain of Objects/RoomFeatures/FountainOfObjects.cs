using CavernOfObjects;

namespace CavernOfObjects.RoomFeatures;

public class FountainOfObjects(Point location) : IRoomFeature, IEnablable
{
    public Point Location { get; } = location;
    public bool Enabled { get; private set; } = false;
    public void StepOn(Player player) { }
    public bool Enable()
    {
        if (!Enabled)
        {
            Enabled = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Sense(int distance)
    {
        if (distance > 0) return;
        Console.ForegroundColor = ConsoleColor.Blue;
        if (Enabled == false)
        {
            Console.WriteLine(
                "You hear water dripping in this room. The Fountain of Objects is here!");
        }
        else
        {
            Console.WriteLine(
                "You hear the rushing waters of the Fountain of Objects. " +
                "It has been reactivated!");
        }
        Console.ResetColor();
    }
}
