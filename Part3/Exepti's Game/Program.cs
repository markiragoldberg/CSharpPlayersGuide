using SaferNumberCrunching;

const int MAXCOOKIE = 10;
Random rng = new Random();
int oatmealRaisinCookieID = rng.Next(MAXCOOKIE);
List<int> takenCookies = new List<int>();
int currentPlayer = 1;
try
{
    while (true)
    {
        int cookie = Parser.AskForTypeInRangeWithExclusions<int>(
            prompt: $"Player {currentPlayer}, pick one of the cookies from 0 to 9: ",
            exclusions: takenCookies,
            excludedPrompt: "That cookie has already been taken. Try again: ",
            min: 0, max: 9);
        if (cookie != oatmealRaisinCookieID)
        {
            Console.WriteLine("YUM! The cookie is delicious!");
            takenCookies.Add(cookie);
            currentPlayer = (currentPlayer % 2) + 1;
        }
        else
        {
            throw new CookieException("Expected delicious chocolate chip, received oatmeal raisin");
        }
    }
}
catch (CookieException e)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(e.ToString());
    Console.ResetColor();
}

public class CookieException : Exception
{
    public CookieException() { }
    public CookieException(string message) : base(message) { }
    public CookieException(string message, Exception e) : base(message, e) { }
}

/*
 * I used a custom exception type because it felt appropriate for this 
 * odd use of exceptions to receive a specific type with an explanatory name.
 * 
 * If exceptions weren't the purpose of this exercise, I would probably 
 * simply end the program when the one oatmeal raisin cookie is found.
 */