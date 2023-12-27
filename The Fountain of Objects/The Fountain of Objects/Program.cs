using System.ComponentModel.DataAnnotations;

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
DateTime gameStartTime = DateTime.Now;
while(!board.GameOver())
{
    Console.WriteLine("" +
        "--------------------------------------------------------------------------------");
    Console.WriteLine($"You are in the room at {board.Player.Location}.");
    board.DisplaySenses();
    Console.Write("What do you want to do? ");
    string? input = Console.ReadLine();
    switch (input?.ToLower())
    {
        case "help":
            HelpSystem.DisplayHelp();
            break;
        case "move north":
            board.MovePlayer(0, -1);
            break;
        case "move south":
            board.MovePlayer(0, 1);
            break;
        case "move east":
            board.MovePlayer(1, 0);
            break;
        case "move west":
            board.MovePlayer(-1, 0);
            break;
        case "shoot north":
            board.Player.Shoot(0, -1);
            break;
        case "shoot south":
            board.Player.Shoot(0, 1);
            break;
        case "shoot east":
            board.Player.Shoot(1, 0);
            break;
        case "shoot west":
            board.Player.Shoot(-1, 0);
            break;
        case "enable fountain":
            board.Player.EnableFountain();
            break;
        case "quit":
            board.Player.Kill();
            break;
        default:
            Console.WriteLine("Enter \"help\" for help.");
            break;
    }
}
if (board.FountainOfObjects.Enabled)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("You have restored the Fountain of Objects and escaped!\nYou win!");
    Console.ResetColor();
}
TimeSpan totalGameTime = DateTime.Now - gameStartTime;
Console.Write($"The game lasted ");
List<string> timeItems = new List<string>();
if (totalGameTime.Days > 0)
    timeItems.Add($"{totalGameTime.Days} days");
if (totalGameTime.Hours > 0)
    timeItems.Add($"{totalGameTime.Hours} hours");
if (totalGameTime.Minutes > 0)
    timeItems.Add($"{totalGameTime.Minutes} minutes");
if (totalGameTime.Seconds > 0)
    timeItems.Add($"{totalGameTime.Seconds} seconds");
for(int i = 0; i < timeItems.Count -1; i++)
    Console.Write(timeItems[i] + ", ");
Console.WriteLine("and " + timeItems[^1] + ".");


// ------------------------

public class GameBoard
{
    public Player Player { get; private set; }
    public FountainOfObjects FountainOfObjects { get; private set; }
    private List<IRoomFeature> _features;
    //private FountainOfObjects fountainOfObjects;
    public int Size { get; }
    public GameBoard(int size)
    {
        Size = size;
        _features = new List<IRoomFeature>();
        _features.Add(new Entrance(new Point(0, 0)));
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
public readonly record struct Point(int X, int Y)
{
    public bool Is(int x, int y) => X == x && Y == y;
    public bool AdjacentTo(Point other)
    {
        return Math.Abs(X - other.X) <= 1
            && Math.Abs(Y - other.Y) <= 1;
    }
    public override string ToString()
    {
        return $"(Row={X}, Column={Y})";
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
    public bool Enabled { get; private set; } = false;
    public void StepOn(Player player) { }
    public bool Enable()
    {
        if(!Enabled)
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

public interface IRoomFeature
{
    public Point Location { get; }
    public bool ShootWithArrow() { return false; }
    public void StepOn(Player player) { }
    public void Sense(int distance) { }
}

public interface IMovable
{
    public Point Location { get; set; }
}

public interface IEnablable
{
    public bool Enabled { get; }
    public bool Enable();
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
