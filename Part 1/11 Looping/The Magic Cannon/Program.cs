for(int i = 1; i < 101; i++)
{
    Console.Write($"{i}: ");
    string shot = "Normal";
    if (i % 3 == 0)
    {
        if (i % 5 == 0)
        {
            shot = "Combined Fire/Electric";
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        else
        {
            shot = "Fire";
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
    else if (i % 5 == 0)
    {
        shot = "Electric";
        Console.ForegroundColor = ConsoleColor.Yellow;
    }
    Console.WriteLine(shot);
    Console.ForegroundColor = ConsoleColor.Gray;
}