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

/*
 * It would probably be better to make an AdvancedRandom class than to
 * make a lot of extensions to System.Random, because that way your
 * random generator isn't necessarily dependent on System.Random. It 
 * could use any other code to get its random numbers and meanwhile 
 * whatever code uses AdvancedRandom doesn't have to know or care.
 * 
 * But that depends on how many and how complex your extensions are.
 * It is probably fine to make extensions for small things.
 */