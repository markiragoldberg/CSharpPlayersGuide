int number = AskForNumber("I won't let you go until you give me an integer! : ");
Console.WriteLine($"Thank you for this {number}.");

double number2 = AskForDouble("Now give me a floating point number (or an integer is fine too.): ");
Console.WriteLine($"Thank you for this {number2:0.00}.");

int min = -3;
int max = 3;
int numberInRange = AskForNumberInRange($"Now I want a number between {min} and {max}: ", min, max);
Console.WriteLine($"Thank you for this {numberInRange}.");


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
    return (int) result;
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

double AskForDouble(string text)
{
    Console.Write(text);
    string response = Console.ReadLine();
    double? result = null;
    while (result == null)
    {
        try
        {
            result = double.Parse(response);
        }
        catch (FormatException)
        {
            Console.Write("Try again: ");
            response = Console.ReadLine();
        }
    }
    return (double)result;
}