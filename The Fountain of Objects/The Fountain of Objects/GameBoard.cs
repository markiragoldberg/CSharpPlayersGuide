using CavernOfObjects.RoomFeatures;

namespace CavernOfObjects;

public class GameBoard
{
    public Player Player { get; private set; }
    public FountainOfObjects FountainOfObjects { get; private set; }
    private List<IRoomFeature> _features;
    public int Size { get; }
    public GameBoard(int size)
    {
        Size = size;
        _features = [new Entrance(new Point(0, 0))];
        Player = new Player(new Point(0, 0), this);
        if (size <= 4)
        {
            FountainOfObjects = new FountainOfObjects(new Point(0, 2));
            _features.Add(FountainOfObjects);
            _features.Add(new Pit(new Point(0, 1)));
            _features.Add(new Maelstrom(new Point(1, 2), this));
            _features.Add(new Amarok(new Point(2, 1), this));
        }
        else if (size <= 6)
        {
            FountainOfObjects = new FountainOfObjects(new Point(4, 5));
            _features.Add(FountainOfObjects);
            _features.Add(new Pit(new Point(2, 4)));
            _features.Add(new Pit(new Point(1, 1)));
            _features.Add(new Maelstrom(new Point(2, 2), this));
            _features.Add(new Amarok(new Point(3, 5), this));
            _features.Add(new Amarok(new Point(4, 3), this));
        }
        else
        {
            FountainOfObjects = new FountainOfObjects(new Point(7, 6));
            _features.Add(FountainOfObjects);
            _features.Add(new Pit(new Point(2, 1)));
            _features.Add(new Pit(new Point(0, 3)));
            _features.Add(new Pit(new Point(7, 5)));
            _features.Add(new Pit(new Point(3, 4)));
            _features.Add(new Maelstrom(new Point(2, 4), this));
            _features.Add(new Maelstrom(new Point(5, 1), this));
            _features.Add(new Amarok(new Point(3, 5), this));
            _features.Add(new Amarok(new Point(4, 3), this));
            _features.Add(new Amarok(new Point(6, 6), this));
        }
    }

    public void MovePlayer(int changeInX, int changeInY)
    {
        Move(Player, changeInX, changeInY);
        foreach (IRoomFeature feature in _features)
        {
            if (Player.Location == feature.Location)
            {
                feature.StepOn(Player);
            }
        }
    }
    public void Move(IMovable moving, int changeInX, int changeInY)
    {
        moving.Location = new Point(
            Math.Clamp(moving.Location.X + changeInX, 0, Size - 1),
            Math.Clamp(moving.Location.Y + changeInY, 0, Size - 1));
    }

    public bool Shoot(Point target)
    {
        bool shotSomething = false;
        foreach (IRoomFeature feature in _features)
        {
            if (feature.Location == target)
            {
                shotSomething = shotSomething || feature.ShootWithArrow();
                if (shotSomething)
                    break;
            }
        }
        return shotSomething;
    }
    public void Enable(Point location)
    {
        foreach(IRoomFeature feature in _features)
        {
            if(feature is IEnablable enablable && feature.Location == location)
            {
                bool worked = enablable.Enable();
                if(!worked)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"You have already restored the Fountain of Objects. Now you should return to the entrance!");
                    Console.ResetColor();
                }
                return;
            }
        }
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("The Fountain of Objects is not here.");
        Console.ResetColor();
    }
    public bool GameOver() => !Player.IsAlive
        || (Player.Location.Is(0, 0) && FountainOfObjects.Enabled);
    public void RemoveFeature(IRoomFeature toRemove)
    {
        _features.Remove(toRemove);
    }
    public void DisplaySenses()
    {
        foreach (IRoomFeature feature in _features)
        {
            if (Player.Location.AdjacentTo(feature.Location))
            {
                feature.Sense(feature.Location == Player.Location? 0 : 1);
            }
        }
    }
}
