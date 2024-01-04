using System.Dynamic;
using ConsoleIO;

int nextID = 1;
while (true)
{
    dynamic robot = new ExpandoObject();
    robot.id = nextID++;
    Console.WriteLine($"You are producing robot #{robot.id}.");

    // Define robot
    if (ColoredConsole.AskToConfirm("Do you want to name this robot? "))
        robot.name = ColoredConsole.AskForType<string>("What is its name? ");

    if (ColoredConsole.AskToConfirm("Does this robot have a specific size? "))
    {
        robot.height = ColoredConsole.AskForType<double>("What is the robot's height? ");
        robot.width = ColoredConsole.AskForType<double>("What is the robot's width? ");
    }
    if (ColoredConsole.AskToConfirm("Does this robot need to be a specific color? "))
        robot.color = ColoredConsole.AskForType<string>("What color? ");

    // Display robot
    foreach (KeyValuePair<string, object> property in (IDictionary<string, object>)robot)
        Console.WriteLine($"{property.Key}: {property.Value}");
}