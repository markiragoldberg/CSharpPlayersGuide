GameBoard? board = null;

Console.Write("Do you want a small, medium, or large game? ");
while (board == null)
{
    string? input = Console.ReadLine();
    switch(input)
    {
        case "small":
            board = new GameBoard(4);
            break;
        case "medium":
            board = new GameBoard(6);
            break;
        case "large":
            board = new GameBoard(8);
            break;
        default:
            Console.Write("Try again: ");
            break;
    }
}

HelpSystem.DisplayIntro();
board.MovePlayer(0, 0);
while(!board.GameOver())
{
    board.Player.Kill();
}

// ------------------------

public class GameBoard
{
    public Player Player { get; private set; }
    private List<IRoomFeature> _features;
    private FountainOfObjects fountainOfObjects;
    public int Size { get; }
    public GameBoard(int size)
    {
        Size = size;
        _features = new List<IRoomFeature>();
        _features.Add(new Entrance(new Point(0, 0)));
        Player = new Player(new Point(0, 0));
        if(size <= 4)
        {
            fountainOfObjects = new FountainOfObjects(new Point(0, 2));
            // ... add pits, amaroks, maelstroms ...
            _features.Add(new Pit(new Point(0, 1)));
            _features.Add(new Maelstrom(new Point(1, 2), this));
            _features.Add(new Amarok(new Point(2, 1), this));
        }
        else if (size <= 6)
        {
            fountainOfObjects = new FountainOfObjects(new Point(4, 5));
            _features.Add(new Pit(new Point(2, 4)));
            _features.Add(new Pit(new Point(1, 1)));
            _features.Add(new Maelstrom(new Point(2, 2), this));
            _features.Add(new Amarok(new Point(3, 5), this));
            _features.Add(new Amarok(new Point(4, 3), this));
        }
        else
        {
            fountainOfObjects = new FountainOfObjects(new Point(7, 6));
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
        foreach(IRoomFeature feature in _features)
        {
            if(Player.Location ==  feature.Location)
            {
                feature.StepOn(Player);
            }
            else if(Player.Location.AdjacentTo(feature.Location))
            {
                feature.Sense();
            }
        }
    }
    public void Move(IMovable moving, int changeInX, int changeInY)
    {
        moving.Location = new Point(
            Math.Clamp(moving.Location.X + changeInX, 0, Size - 1),
            Math.Clamp(moving.Location.Y + changeInY, 0, Size - 1));
    }

    public bool GameOver() => !Player.IsAlive
        || (Player.Location.Is(0,0)  && fountainOfObjects.Enabled);

    public void RemoveFeature(IRoomFeature toRemove)
    {
        _features.Remove(toRemove);
    }
}

public class Player : IMovable
{
    public Point Location { get; set; }
    public int Arrows { get; private set; }
    public bool IsAlive { get; private set; }
    public Player(Point location)
    {
        Location = location;
        Arrows = 5;
    }
    public void UseArrow()
    {
        if (Arrows > 0)
            Arrows -= 1;
    }
    public void Kill() => IsAlive = false;
}
public readonly record struct Point(int X, int Y)
{
    public bool Is(int x, int y) => X == x && Y == y;
    public bool AdjacentTo(Point other)
    {
        return Math.Abs(X - other.X) <= 1
            && Math.Abs(Y - other.Y) <= 1;
    }
}

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

public class FountainOfObjects(Point location) : IRoomFeature, IEnablable
{
    public Point Location { get; } = location;
    public bool Enabled { get; set; }
    public void StepOn(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        if (Enabled == false)
        {
            Console.WriteLine(
                "You hear water dripping in this room. The Fountain of Objects is here!");
        }
        else
        {
            Console.WriteLine(
                "You hear the rushing waters from the Fountain of Objects. " +
                "It has been reactivated!");
        }
        Console.ResetColor();
    }
}

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
    public void Sense()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("You feel a draft. There is a pit in a nearby room.");
        Console.ResetColor();
    }
}

public class Amarok(Point location, GameBoard board) : IRoomFeature
{
    public Point Location { get; } = location;
    private GameBoard _gameBoard = board;
    public void ShootWithArrow()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Your arrow strikes an amarok and kills it.");
        Console.ResetColor();
        _gameBoard.RemoveFeature(this);
    }
    public void StepOn(Player player)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("An amarok lunges at you from the darkness and swiftly kills you.");
        player.Kill();
        Console.ResetColor();
    }
    public void Sense()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("You can smell the rotten stench of an amarok in a nearby room.");
        Console.ResetColor();
    }
}

public class Maelstrom(Point location, GameBoard gameBoard) : IRoomFeature, IMovable
{
    public Point Location { get; set; } = location;
    private readonly GameBoard _gameBoard = gameBoard;

    public void ShootWithArrow()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("A maelstrom nearby shrieks deafeningly, then falls silent.");
        Console.ResetColor();
        _gameBoard.RemoveFeature(this);
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
    public void Sense()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("You hear the growling and groaning of a maelstrom nearby.");
        Console.ResetColor();
    }
}

public interface IRoomFeature
{
    public Point Location { get; }
    public void ShootWithArrow() { }
    public void StepOn(Player player) { }
    public void Sense() { }
}

public interface IMovable
{
    public Point Location { get; set; }
}

public interface IEnablable
{
    public bool Enabled { get; set;}
}

public static class HelpSystem
{
    public static void DisplayIntro()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(
            "You enter the Cavern of Objects, a maze of rooms filled with dangerous pits, " +
            "in search of the Fountain of Objects. Light is visible only in the entrance, " +
            "and no other light is seen anywhere in the caverns. You must navigate the " +
            "Caverns with your other senses. Find the Fountain of Objects, activate it, " +
            "and return to the entrance.");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(
            "\nLook out for pits. You will feel a breeze if a pit is in an adjacent room. " +
            "If you enter a room with a pit, you will die.");
        Console.WriteLine(
            "\nMaelstroms are violent forces of sentient wind. Entering a room with one " +
            "could transport you to any other location in the caverns. You will be able to " +
            "hear their growling and groaning in nearby rooms.");
        Console.WriteLine(
            "\nAmaroks roam the caverns. Encountering one is certain death, but you can " +
            "smell their rotten stench in nearby rooms.");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(
            "\nYou carry with you a bow and a quiver of arrows. You can use them to shoot " +
            "monsters in the caverns, but be warned: you have a limited supply.");
        Console.ResetColor();
    }

    public static void DisplayHelp()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("To move, enter \"move\" followed by \"north\",\"east\",\"south\", or \"west\".");
        Console.WriteLine("To fire an arrow, enter \"shoot\" followed by \"north\",\"east\",\"south\", or \"west\".");
        Console.WriteLine("To activate the fountain should you find it, enter \"enable fountain\".");
        Console.WriteLine("To quit, enter \"quit\".");
        Console.ResetColor();
    }
}
