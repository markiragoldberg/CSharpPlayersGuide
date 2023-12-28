using BetterRandom;

Random rng = new Random();
Console.WriteLine($"Doubles less than 10.0: ");
for (int i = 0; i < 15; i++)
    Console.Write($"{rng.NextDouble(10.0):0.0} ");

Console.WriteLine($"\nStrings from up, down, left, right:");
string[] directions = {"up", "down", "left", "right"};
for (int i = 0; i < 12; i++)
    Console.Write($"{rng.NextString(directions)} ");

Console.WriteLine($"\nCoinflips:");
for (int i = 0; i < 40; i++)
    Console.Write($"{(rng.CoinFlip() == true ? "1" : "0")} ");

Console.WriteLine($"\nCoinflips with 85% chance of 0:");
for (int i = 0; i < 40; i++)
    Console.Write($"{(rng.CoinFlip(0.15) == true ? "1" : "0")} ");