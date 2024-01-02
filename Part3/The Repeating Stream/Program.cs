Console.Title = "The Repeating Stream";

Console.WriteLine("Wait for the stream to emit the same number twice in a row,");
Console.WriteLine("then quickly press any key to seize the good omen!\n");
RecentNumbers recentNumbers = new RecentNumbers();
Thread generatorThread = new Thread(recentNumbers.GenerateNumbers);
generatorThread.Start();
while(true)
{
    Console.ReadKey(true);
    if (recentNumbers.CheckForMatch())
    {
        Console.WriteLine("Good luck obtained! Plus one good luck points! High score!");
    }
    else
    {
        Console.WriteLine("Sorry, the two most recent numbers do not match.");
    }
}

// -------------------------------------------------

public class RecentNumbers
{
    private readonly object _numberLock = new();
    private int _head;
    private int _tail;
    private readonly Random _rng;
    public RecentNumbers()
    {
        _rng = new Random();
        _tail = _rng.Next(10);
        _head = _rng.Next(10);
    }
    public void GenerateNumbers()
    {
        while(true)
        {
            Thread.Sleep(900 + _rng.Next(201));
            int next = _rng.Next(10);
            lock (_numberLock)
            {
                _tail = _head;
                _head = next;
            }
            Console.WriteLine($"The stream has generated the number {next}.");
        }
    }
    public bool CheckForMatch()
    {
        lock(_numberLock)
        {
            return _head == _tail;
        }
    }
}