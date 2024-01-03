using System.Dynamic;
using PlayersGuide;

int nextID = 1;
while (true)
{
    dynamic robot = new ExpandoObject();
    robot.id = nextID++;
    Console.WriteLine($"You are producing robot #{robot.id}.");

    // Define robot
    if (ConsoleInput.AskToConfirm("Do you want to name this robot? "))
        robot.name = ConsoleInput.AskForType<string>("What is its name? ");

    if (ConsoleInput.AskToConfirm("Does this robot have a specific size? "))
    {
        robot.height = ConsoleInput.AskForType<double>("What is the robot's height? ");
        robot.width = ConsoleInput.AskForType<double>("What is the robot's width? ");
    }
    if (ConsoleInput.AskToConfirm("Does this robot need to be a specific color? "))
        robot.color = ConsoleInput.AskForType<string>("What color? ");

    // Display robot
    foreach (KeyValuePair<string, object> property in (IDictionary<string, object>)robot)
        Console.WriteLine($"{property.Key}: {property.Value}");
}