Console.WriteLine("Provide the first array.");
int[] array = new int[5];
for(int i = 0; i < 5; i++)
{
    Console.Write($"Enter the value at index {i}: ");
    array[i] = Convert.ToInt32(Console.ReadLine());
}

Console.WriteLine("\nReplicating the array...");
int[] array2 = new int[5];
for(int i = 0;i < array.Length; i++)
{
    array2[i] = array[i];
}

Console.Write("Array 1: \n[");
for (int i = 0; i < array.Length-1; i++)
{
    Console.Write($"{array[i]}, ");
}
Console.WriteLine($"{array[^1]}]");

Console.Write("\nArray 2: \n[");
for (int i = 0; i < array2.Length-1; i++)
{
    Console.Write($"{array2[i]}, ");
}
Console.WriteLine($"{array2[^1]}]");