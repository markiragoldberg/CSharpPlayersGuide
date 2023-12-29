using TheSieve;
using SaferNumberCrunching;

Console.Write("Choose a filter from 'even', 'positive', or 'multiple of 10': ");
Predicate<int>? filter = null;
while (filter == null)
{
    string? choice = Console.ReadLine();
    filter = choice switch
    {
        "even" => x => x % 2 == 0,
        "positive" => x => x > 0,
        "multiple of 10" => x => x != 0 && x % 10 == 0,
        _ => null
    };
    if (filter == null)
    {
        Console.Write("Try again: ");
    }
}

Sieve sieve = new Sieve(filter);

while (true)
{
    int toSieve = Parser.AskForType<int>("Enter an integer to sieve: ");
    if (sieve.IsGood(toSieve))
    {
        Console.WriteLine("The integer is good.");
    }
    else
    {
        Console.WriteLine("The integer is bad.");
    }
    Console.Write("Keep going Y/n? ");
    if (Console.ReadLine() == "n")
    {
        break;
    }
}

/*
 * This doesn't make Program.cs shorter because the sieve filters are 
 * in a different file in a different project, but it does make the 
 * sieve filters mostly unnecessary so if they're counted then it 
 * does make the program shorter. Unless those specific filters are 
 * used so often to justify predefining them.
 * 
 * Using lambdas also makes the program slightly harder to read than 
 * if the predefined sieve filters have accurate and descriptive names. 
 * This is not so important for simple functions like "IsEven" but 
 * could be significant if the function is of greater complexity.
 * */