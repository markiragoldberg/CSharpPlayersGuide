using TheSieve;
using SaferNumberCrunching;

Console.Write("Choose a filter from 'even', 'positive', or 'multiple of 10': ");
Predicate<int>? filter = null;
while (filter == null)
{
    string? choice = Console.ReadLine();
    filter = choice switch
    {
        "even" => Sieve.IsEven,
        "positive" => Sieve.IsPositive,
        "multiple of 10" => Sieve.IsMultipleOfTen,
        _ => null
    };
    if (filter == null)
    {
        Console.Write("Try again: ");
    }
}

Sieve sieve = new Sieve(filter);

while(true)
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
 * I could have solved this problem using inheritance and polymorphism by 
 * creating an interface that has an bool IsGood(int) method and classes
 * that implement that method differently. Alternatively I could have made 
 * an abstract base class with a similar method and inherited it.
 * 
 * The delegate / predicate approach is probably better because while it 
 * may be more work to set up the sieve using a delegate, it is easier to 
 * write a new filter predicate for the existing sieve class than to write 
 * an entire class that derives from the abstract class or interface, even 
 * if the class is very simple as it is here. In a larger class with more 
 * complexity being able to reuse the entire rest of the class while only 
 * changing the predicate would obviously be much better.
 * 
 */