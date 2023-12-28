using System.Numerics;

namespace SaferNumberCrunching
{
    public static class Parser
    {
        public static T AskForType<T>(string prompt, string? repeatPrompt = null) where T : IParsable<T>
        {
            Console.Write(prompt);
            while (true)
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
        public static T AskForTypeInRange<T>(
            string prompt, T min, T max, string? repeatPrompt = null, string? outOfRangePrompt = null)
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
                        return result;
                    }
                    else
                    {
                        Console.Write(outOfRangePrompt ?? "That is out of range. Try again: ");
                    }
                }
                else
                {
                    Console.Write(repeatPrompt ?? "Try again: ");
                }
            }
        }
        public static T AskForTypeInRangeWithExclusions<T>(
            string prompt, T min, T max, List<T> exclusions, string? repeatPrompt = null, string? outOfRangePrompt = null, string? excludedPrompt = null)
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
                        if (!exclusions.Contains(result))
                        {
                            return result;
                        }
                        else
                        {
                            Console.Write(excludedPrompt ?? "That is excluded from the range. Try again: ");
                        }
                    }
                    else
                    {
                        Console.Write(outOfRangePrompt ?? "That is out of range. Try again: ");
                    }
                }
                else
                {
                    Console.Write(repeatPrompt ?? "Try again: ");
                }
            }
        }
    }
}
