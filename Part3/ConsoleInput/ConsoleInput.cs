using System.Numerics;

namespace PlayersGuide
{
    [Obsolete("Use ConsoleIO.ColoredConsole")]
    public static class ConsoleInput
    {
        public static T AskForType<T>(string prompt, string? repeatPrompt = null) where T : IParsable<T>
        {
            Console.Write(prompt);
            string? input;
            while (true)
            {
                input = Console.ReadLine();
                if (T.TryParse(input, null, out T? result))
                    return result;
                else
                    Console.Write(repeatPrompt ?? "Try again: ");
            }
        }
        public static T AskForTypeInRange<T>(
            string prompt, T min, T max, 
            ICollection<T>? exclusions = null, string? repeatPrompt = null,
            string? outOfRangePrompt = null, string? excludedPrompt = null)
            where T : IParsable<T>, IComparisonOperators<T, T, bool>
        {
            Console.Write(prompt);
            while (true)
            {
                string? input = Console.ReadLine();
                if (T.TryParse(input, null, out T? result))
                {
                    if (min <= result && result <= max)
                    {
                        if (exclusions == null || !exclusions.Contains(result))
                            return result;
                        else
                            Console.Write(excludedPrompt ?? "That is excluded from the range. Try again: ");
                    }
                    else
                        Console.Write(outOfRangePrompt ?? "That is out of range. Try again: ");
                }
                else
                    Console.Write(repeatPrompt ?? "Try again: ");
            }
        }
        public static string AskForOption(string prompt, ICollection<string> options, 
            string? repeatPrompt = null, bool caseSensitive = false)
        {
            if (options.Count == 0)
                throw new ArgumentException("options cannot be empty");
            Console.Write(prompt);
            string? input;
            while (true)
            {
                input = Console.ReadLine();
                if(input != null && options.Contains(input, 
                    caseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase))
                {
                    return caseSensitive ? input : input.ToLower();
                }
                Console.Write(repeatPrompt ?? "Try again: ");
            }
        }
        public static bool AskToConfirm(string prompt, string? repeatPrompt = null)
        {
            string yesNo = AskForOption(prompt, ["yes", "no"], repeatPrompt);
            return yesNo.Equals("yes");
        }
        public static bool AskToConfirm(string prompt, bool defaultResult, string? repeatPrompt = null)
        {
            Console.Write(prompt);
            string? yesNo = Console.ReadLine();
            if (yesNo == null)
                return defaultResult;
            if (defaultResult == false && yesNo.Equals("yes", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (defaultResult == true && yesNo.Equals("no", StringComparison.OrdinalIgnoreCase))
                return false;
            else return defaultResult;
        }
    }
}