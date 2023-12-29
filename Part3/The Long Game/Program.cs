Console.Write("What is your name? ");
string? name = null;
string? reason;
while (name == null)
{
    string? input = Console.ReadLine();
    if (ValidateName(input, out reason))
    {
        name = input;
        break;
    }
    Console.WriteLine($"That name can't be used because it {reason}.");
    Console.Write("Try again: ");
}

int score = 0;
string filePath = $"./saves/{name}.sav";
if (Path.Exists(filePath))
{
    try
    {
        score = Convert.ToInt32(File.ReadAllText(filePath));
    }
    catch
    {
        score = 0;
    }
}

while (true)
{
    Console.Clear();
    Console.Write($"Your score is ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(score.ToString());
    Console.ResetColor();
    Console.WriteLine(".");
    var key = Console.ReadKey(false);
    if (key.Key == ConsoleKey.Enter)
        break;
    else
        score += 1;
}

if (!Path.Exists($"./saves/"))
    Directory.CreateDirectory("./saves/");
File.WriteAllText(filePath, score.ToString());
Console.WriteLine($"Goodbye, {name}!");


    bool ValidateName(string? name, out string? reason)
{
    char[] bannedChars = ['\\', '/', ':', '*', '?', '\"', '<', '>', '|', '\''];
    if (name == null)
    {
        reason = "is null";
        return false;
    }
    if (name.Length == 0)
    {
        reason = "is empty";
        return false;
    }
    foreach (char c in bannedChars)
    {
        if (name.Contains(c))
        {
            reason = $"filenames can't contain '{c}'";
            return false;
        }
    }
    reason = null;
    return true;
}


