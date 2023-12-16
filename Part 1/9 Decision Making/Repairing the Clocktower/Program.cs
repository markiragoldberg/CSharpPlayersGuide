Console.Write("Put a number into the clock: ");
int number = Convert.ToInt32(Console.ReadLine());

if(number % 2 == 0)
{
    Console.WriteLine("Tick...");
}
else
{
    Console.WriteLine("Tock...");
}
