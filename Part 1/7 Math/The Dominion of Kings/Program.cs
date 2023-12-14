Console.WriteLine("Enter the total number of provinces you own: ");
int provinces = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Do the same for duchies: ");
int duchies = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Finally do the same for estates: ");
int estates = Convert.ToInt32(Console.ReadLine());

int points = provinces * 6 + duchies * 3 + estates;

Console.WriteLine($"Your holdings are worth {points} points.");