using System.Numerics;

namespace PlayersGuide
{
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
            List<T>? exclusions = null, string? repeatPrompt = null,
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
        public static string AskForOption(string prompt, List<string> options, 
            string? repeatPrompt = null, bool caseSensitive = false)
        {
            if (options.Count == 0)
                throw new ArgumentException("options cannot be empty");
            if (!caseSensitive)
            {
                for (int i = 0; i < options.Count; i++)
                    options[i] = options[i].ToLower();
            }
            Console.Write(prompt);
            string? input;
            int resultIndex = -1; // return value if input not found
            while (true)
            {
                input = Console.ReadLine();
                if (!caseSensitive)
                    input = input?.ToLower();
                if (input != null)
                {
                    resultIndex = options.IndexOf(input);
                    if (resultIndex != -1)
                        return options[resultIndex];
                }
                Console.Write(repeatPrompt ?? "Try again: ");
            }
        }
        public static bool AskToConfirm(string prompt, string? repeatPrompt = null)
        {
            string yesNo = AskForOption(prompt, ["yes", "no"], repeatPrompt);
            return yesNo == "yes" ? true : false;
        }
        public static bool AskToConfirm(string prompt, bool defaultResult, string? repeatPrompt = null)
        {
            string yesNo = AskForType<string>(prompt, repeatPrompt);
            if (yesNo == "yes") return true;
            else if (yesNo == "no") return false;
            else return defaultResult;
        }
    }
}