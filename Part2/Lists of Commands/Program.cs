Robot robot = new Robot();

while (true)
{
    Console.WriteLine("Enter any number of commands for the robot, then \"stop\" to run them:");
    string? input = null;
    while (input != "stop")
    {
        input = AskForString($"{robot.Commands.Count + 1}?> ");
        if (input == "quit" || input == "exit")
        {
            return;
        }
        IRobotCommand? nextCommand = input switch
        {
            "on" => new OnCommand(),
            "off" => new OffCommand(),
            "north" => new NorthCommand(),
            "south" => new SouthCommand(),
            "east" => new EastCommand(),
            "west" => new WestCommand(),
            _ => null
        };
        if(nextCommand != null)
        {
            robot.Commands.Add(nextCommand);
        }
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
    public List<IRobotCommand> Commands { get; } = new List<IRobotCommand>();
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