Console.Title = "Hunting the Manticore";
Console.BackgroundColor = ConsoleColor.DarkRed;
Console.ForegroundColor = ConsoleColor.White;
int manticore_location = AskForNumberInRange("Player 1, how far away from the city, from 0 to 100, do you want to station the Manticore? ", 0, 100);
ResetColors();
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
    int target_range = AskForNumber("Enter desired cannon range: ");
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
ResetColors();

void WriteCityHP()
{
    if(city_hp < 8)
        Console.ForegroundColor = ConsoleColor.Red;
    else if (city_hp < 15)
        Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(city_hp);
    ResetColors();
}

void WriteManticoreHP()
{
    if (manticore_hp < 5)
        Console.ForegroundColor = ConsoleColor.DarkRed;
    else if (manticore_hp < 10)
        Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write(manticore_hp);
    ResetColors();
}

void WriteCannonDamage(int damage)
{
    if (damage > 3)
        Console.ForegroundColor = ConsoleColor.Blue;
    else if (damage > 1)
        Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(damage);
    ResetColors();
}

void ResetColors()
{
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.BackgroundColor = ConsoleColor.Black;
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
    ResetColors();
}

int AskForNumber(string text)
{
    Console.Write(text);
    string response = Console.ReadLine();
    int? result = null;
    while (result == null)
    {
        try
        {
            result = int.Parse(response);
        }
        catch (FormatException)
        {
            Console.Write("Try again: ");
            response = Console.ReadLine();
        }
    }
    return (int)result;
}
int AskForNumberInRange(string text, int min, int max)
{
    int result = AskForNumber(text);
    while (result < min || result > max)
    {
        result = AskForNumber("Try again: ");
    }
    return result;
}