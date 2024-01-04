string? word = null;
while (word != "quit")
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    if (word != null)
    {
        RandomlyRecreateAndReportAsync(word);
        Console.Write("Working on ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(word);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("...");        
    }
    Console.Write("What word do you want to recreate randomly? ");
    Console.ForegroundColor = ConsoleColor.Cyan;

    word = Console.ReadLine();
}
Console.ResetColor();

// -------------------------
async void RandomlyRecreateAndReportAsync(string word)
{
    DateTime start = DateTime.Now;
    Task<int> attempts = Task.Run(() => RandomlyRecreate(word));
    await attempts;
    TimeSpan time = DateTime.Now - start;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write($"\nIt took ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write($"{attempts.Result} attempts");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(" and ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write($"{time.TotalSeconds:0.0} seconds");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(" to randomly recreate the word ");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write(word);
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(".");
    Console.ForegroundColor = ConsoleColor.Cyan;
}

int RandomlyRecreate(string word)
{
    Random rng = new Random();
    int length = word.Length;
    System.Text.StringBuilder randomWord = new();
    int attempts = 0;
    while (randomWord.ToString() != word)
    {
        attempts += 1;
        randomWord.Clear();
        for (int i = 0; i < length; i++)
        {
            randomWord.Append((char)('a' + rng.Next(26)));
        }
    }
    return attempts;
}
