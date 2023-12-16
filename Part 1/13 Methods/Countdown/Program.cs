Countdown(10);

void Countdown(int start)
{
    if (start <= 0)
        return;
    Console.WriteLine(start);
    Countdown(start - 1);
}