Robot robot = new Robot();

while (true)
{
    Console.WriteLine("Enter three commands for the robot:");
    string? input;
    for (int i = 0; i < 3; i++)
    {
        input = AskForString($"{i + 1}?> ");
        if (input == "quit" || input == "exit")
        {
            return;
        }
        robot.Commands[i] = input switch
        {
            "on" => new OnCommand(),
            "off" => new OffCommand(),
            "north" => new NorthCommand(),
            "south" => new SouthCommand(),
            "east" => new EastCommand(),
            "west" => new WestCommand(),
            _ => null
        };
    }
    robot.Run();
}


// ---------------------------------------------

string? AskForString(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine();
}

public class OnCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        robot.IsPowered = true;
    }
}
public class OffCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        robot.IsPowered = false;
    }
}

public class NorthCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.Y += 1;
        }
    }
}
public class SouthCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.Y -= 1;
        }
    }
}
public class EastCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.X += 1;
        }
    }
}
public class WestCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.X -= 1;
        }
    }
}
public interface IRobotCommand
{
    void Run(Robot robot);
}

// ---------------------------------------------

public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }
    public IRobotCommand?[] Commands { get; } = new IRobotCommand?[3];
    public void Run()
    {
        foreach (IRobotCommand? command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}

// Answer: I think this is an improvement over an abstract base class, 
// mostly because it allows the robot commands to inherit a different 
// base class and inherit useful behavior. The robot command concept
// is very simple and doesn't need any fields or definitions so there 
// are no downsides to representing it as an interface.