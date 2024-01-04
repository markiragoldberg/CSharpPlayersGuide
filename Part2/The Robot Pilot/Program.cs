Console.Title = "Hunting the Manticore";
Console.BackgroundColor = ConsoleColor.DarkRed;
Console.ForegroundColor = ConsoleColor.White;

List<string> options = new() { "human", "robot" };
string? response = ConsoleInput.AskForString(
    "Who will pilot the manticore, a second HUMAN or a ROBOT? ", options);
IManticorePilot pilot = response switch
{
    "human" => new HumanManticorePilot(),
    _ => new RobotManticorePilot()
};
int manticore_location = pilot.PickManticoreLocation();

Console.ResetColor();
Console.Clear();

int city_hp = 15;
int manticore_hp = 10;
int current_round = 1;

while (city_hp > 0 && manticore_hp > 0)
{
    int damage = GetDamage(current_round);
    Console.WriteLine("-----------------------------------------------------------");
    Console.Write($"STATUS: Round: {current_round} City: ");
    WriteCityHP();
    Console.Write("/15 Manticore: ");
    WriteManticoreHP();
    Console.WriteLine("/10");
    Console.Write($"The cannon is expected to deal ");
    WriteCannonDamage(damage);
    Console.WriteLine(" damage this round.");
    int target_range = ConsoleInput.AskForNumberInRange(
        "Enter desired cannon range from 0 to 100: ", 0, 100);
    ShowRangeFeedback(target_range);
    if (target_range == manticore_location)
        manticore_hp -= damage;
    if (manticore_hp > 0)
        city_hp -= 1;
    current_round += 1;
}
if (city_hp <= 0)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Oh no! The Manticore has destroyed Consolas!");
}
else
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("The Manticore has been destroyed! The city of Consolas has been saved!");
}
Console.WriteLine("Press any key...");
Console.ReadKey(true);
Console.ResetColor();

void WriteCityHP()
{
    if (city_hp < 8)
        Console.ForegroundColor = ConsoleColor.Red;
    else if (city_hp < 15)
        Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(city_hp);
    Console.ResetColor();
}

void WriteManticoreHP()
{
    if (manticore_hp < 5)
        Console.ForegroundColor = ConsoleColor.DarkRed;
    else if (manticore_hp < 10)
        Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write(manticore_hp);
    Console.ResetColor();
}

void WriteCannonDamage(int damage)
{
    if (damage > 3)
        Console.ForegroundColor = ConsoleColor.Blue;
    else if (damage > 1)
        Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(damage);
    Console.ResetColor();
}

int GetDamage(int round)
{
    if (round % 3 == 0 && round % 5 == 0)
        return 10;
    else if (round % 3 == 0 || round % 5 == 0)
        return 3;
    else
        return 1;
}

void ShowRangeFeedback(int target_range)
{
    if (target_range == manticore_location)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("That round was a DIRECT HIT!");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (target_range < manticore_location)
            Console.WriteLine("That round FELL SHORT of the target.");
        else
            Console.WriteLine("That round OVERSHOT the target.");
    }
    Console.ResetColor();
}
public class HumanManticorePilot : IManticorePilot
{
    public int PickManticoreLocation()
    {
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.White;
        int manticore_location = ConsoleInput.AskForNumberInRange(
            "Player 1, how far away from the city, from 0 to 100, " +
            "do you want to station the Manticore? ", 0, 100);
        Console.ResetColor();
        Console.Clear();
        return manticore_location;
    }
}

public class RobotManticorePilot : IManticorePilot
{
    public int PickManticoreLocation()
    {
        Random rng = new();
        return rng.Next(101);
    }
}

public static class ConsoleInput
{
    public static int AskForNumber(string text)
    {
        Console.Write(text);
        string? response = Console.ReadLine();
        int? result = null;
        while (result == null)
        {
            try
            {
                result = int.Parse(response!);
            }
            catch
            {
                Console.Write("Try again: ");
                response = Console.ReadLine();
            }
        }
        return (int)result;
    }
    public static int AskForNumberInRange(string text, int min, int max)
    {
        int result = AskForNumber(text);
        while (result < min || result > max)
        {
            result = AskForNumber("Try again: ");
        }
        return result;
    }
    public static string? AskForString(string prompt,
           List<string> acceptableInputs, bool caseSensitive = false)
    {
        if (acceptableInputs.Count == 0)
            throw new ArgumentException("acceptableInputs cannot be empty");
        int resultIndex = -1;
        if (!caseSensitive)
        {
            for (int i = 0; i < acceptableInputs.Count; i++)
                acceptableInputs[i] = acceptableInputs[i].ToLower();
        }
        Console.Write(prompt);
        string? input;
        while (resultIndex == -1)
        {
            input = Console.ReadLine();
            if (!caseSensitive)
                input = input?.ToLower();
            if (input != null)
            {
                resultIndex = acceptableInputs.IndexOf(input);
                if(resultIndex != -1)
                    return acceptableInputs[resultIndex];
            }
            Console.Write("Try again: ");
        }
        return null;
    }
}

public interface IManticorePilot
{
    int PickManticoreLocation();
}

// To allow the game to be single or multiplayer, I could define an interface 
// that provides a method to assign the manticore's location, then create two 
// classes that implement the interface and either assign the location by 
// getting it from the second human player or by randomly generating it.
// Then I would need to replace the code in the main function that generates 
// the manticore's location with code that obtains an object that implements 
// the interface and calls the method that assigns the manticore a location.
// I would also need to write some code that chooses which class should be 
// used; for this simple example it would probably be "ask the user".