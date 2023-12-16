int total_eggs = AskForNumber("Enter the number of chocolate eggs collected today: ");
int eggs_per_sister = total_eggs / 4; // 4 sisters
int eggs_for_duckbear = total_eggs % 4;
Console.WriteLine(
    $"Each sister should get {eggs_per_sister} eggs, " +
    $"and the duckbear should get {eggs_for_duckbear} eggs.");

// The duckbear gets more eggs than any one sister if they collect one to three eggs,
// or six to seven eggs, or eleven eggs.

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
