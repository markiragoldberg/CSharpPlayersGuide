Console.Write("Enter the number of chocolate eggs collected today: ");
int total_eggs = Convert.ToInt32(Console.ReadLine());
int eggs_per_sister = total_eggs / 4; // 4 sisters
int eggs_for_duckbear = total_eggs % 4;
Console.WriteLine(
    $"Each sister should get {eggs_per_sister} eggs, " +
    $"and the duckbear should get {eggs_for_duckbear} eggs.");

// The duckbear gets more eggs than any one sister if they collect one to three eggs,
// or six to seven eggs, or eleven eggs.