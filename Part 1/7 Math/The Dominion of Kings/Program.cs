int provinces = AskForNumber("Enter the total number of provinces you own: ");
int duchies = AskForNumber("Do the same for duchies: ");
int estates = AskForNumber("Finally do the same for estates: ");

int points = provinces * 6 + duchies * 3 + estates;

Console.WriteLine($"Your holdings are worth {points} points.");


// update from "Taking a Number" pg. 106
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
