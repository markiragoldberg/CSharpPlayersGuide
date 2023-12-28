int parsedInt = Parser.AskForType<int>("Enter an integer: ");
Console.WriteLine($"You entered {parsedInt}.");

double parsedDouble = Parser.AskForType<double>("Enter a decimal number: ");
Console.WriteLine($"You entered {parsedDouble}.");

bool parsedBool = Parser.AskForType<bool>("Enter true or false: ");
Console.WriteLine($"You entered {parsedBool.ToString().ToLower()}.");

public static class Parser
{
    public static T AskForType<T>(string prompt, string? repeatPrompt = null) where T : IParsable<T>
    {
        Console.Write(prompt);
        while(true)
        {
            string? input = Console.ReadLine();
            if (T.TryParse(input, null, out T? result))
            {
                return result;
            }
            else
            {
                Console.Write(repeatPrompt ?? "Try again: ");
            }
        }
    }
}