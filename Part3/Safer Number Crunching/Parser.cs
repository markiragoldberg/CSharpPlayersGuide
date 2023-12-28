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
    }
}
