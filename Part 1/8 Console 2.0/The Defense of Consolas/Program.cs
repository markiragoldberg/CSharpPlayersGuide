Console.Title = "The Defense of Consolas";

Console.Write("Target row? ");
int row = Convert.ToInt32(Console.ReadLine());
Console.Write("Target column? ");
int column = Convert.ToInt32(Console.ReadLine());

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Deploy to:");
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine($"({row},{column-1})");
Console.WriteLine($"({row-1},{column})");
Console.WriteLine($"({row},{column+1})");
Console.WriteLine($"({row+1},{column})");
Console.ForegroundColor = ConsoleColor.White;
Console.Beep();