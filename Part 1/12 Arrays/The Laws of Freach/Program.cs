int[] array = new int[] { 4, 51, -7, 13, -99, 15, -8, 45, 90 };
Console.Write("The array is [ ");
foreach (int i in array)
{
    Console.Write($"{i} ");
}
Console.WriteLine("]");

int currentSmallest = int.MaxValue;
foreach (int i in array)
{
    if (i < currentSmallest)
        currentSmallest = i;
}
Console.WriteLine($"The smallest value in the array is {currentSmallest}.");

int total = 0;
foreach (int i in array)
{
    total += i;
}
float average = (float)total / array.Length;
Console.WriteLine($"The average of the array's values is {average}.");