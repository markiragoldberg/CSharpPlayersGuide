namespace CavernOfObjects;
internal class Program
{
    static void Main(string[] args)
    {
        Console.Title = "The Cavern of Objects";
        GameBoard? board = null;

        Console.Write("Do you want a small, medium, or large game? ");
        while (board == null)
        {
            string? input = Console.ReadLine();
            switch (input)
            {
                case "small":
                    board = new GameBoard(4);
                    break;
                case "medium":
                    board = new GameBoard(6);
                    break;
                case "large":
                    board = new GameBoard(8);
                    break;
                default:
                    Console.Write("Try again: ");
                    break;
            }
        }

        HelpSystem.DisplayIntro();
        DateTime gameStartTime = DateTime.Now;
        while (!board.GameOver())
        {
            Console.WriteLine("" +
                "--------------------------------------------------------------------------------");
            Console.WriteLine($"You are in the room at {board.Player.Location}.");
            board.DisplaySenses();
            Console.Write("What do you want to do? ");
            string? input = Console.ReadLine();
            switch (input?.ToLower())
            {
                case "help":
                    HelpSystem.DisplayHelp();
                    break;
                case "move north":
                    board.MovePlayer(0, -1);
                    break;
                case "move south":
                    board.MovePlayer(0, 1);
                    break;
                case "move east":
                    board.MovePlayer(1, 0);
                    break;
                case "move west":
                    board.MovePlayer(-1, 0);
                    break;
                case "shoot north":
                    board.Player.Shoot(0, -1);
                    break;
                case "shoot south":
                    board.Player.Shoot(0, 1);
                    break;
                case "shoot east":
                    board.Player.Shoot(1, 0);
                    break;
                case "shoot west":
                    board.Player.Shoot(-1, 0);
                    break;
                case "enable fountain":
                    board.Player.EnableFountain();
                    break;
                case "quit":
                    board.Player.Kill();
                    break;
                default:
                    Console.WriteLine("Enter \"help\" for help.");
                    break;
            }
        }
        if (board.FountainOfObjects.Enabled)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have restored the Fountain of Objects and escaped!\nYou win!");
            Console.ResetColor();
        }
        TimeSpan totalGameTime = DateTime.Now - gameStartTime;
        Console.Write($"The game lasted ");
        List<string> timeItems = new List<string>();
        if (totalGameTime.Days > 0)
            timeItems.Add($"{totalGameTime.Days} days");
        if (totalGameTime.Hours > 0)
            timeItems.Add($"{totalGameTime.Hours} hours");
        if (totalGameTime.Minutes > 0)
            timeItems.Add($"{totalGameTime.Minutes} minutes");
        // always report seconds even if game was <1 second
        timeItems.Add($"{totalGameTime.Seconds} seconds");
        for (int i = 0; i < timeItems.Count - 1; i++)
            Console.Write(timeItems[i] + ", ");
        if (timeItems.Count > 2)
            Console.Write("and ");
        Console.WriteLine(timeItems[^1] + ".");
    }
}

// I prefer traditional entry points to top-level statements,
// mostly because I unthinkingly expect the statements at the 
// top of a file with no indentation to be of low importance.
