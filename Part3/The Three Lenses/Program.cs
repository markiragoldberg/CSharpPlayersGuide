int[] lenniksArray = [9, 2, 8, 3, 7, 4, 6, 5];
Console.WriteLine("Lennik's array:");
foreach (int i in lenniksArray)
    Console.Write(i + " ");
Console.WriteLine();

Console.WriteLine("The array after procedural Lennikization:");
var proceduralResult = proceduralLennikization(lenniksArray);
foreach (int i in proceduralResult)
    Console.Write(i + " ");
Console.WriteLine();

Console.WriteLine("The array after keyword-based query Lennikization:");
var keywordQueryResult = keywordQueryLennikization(lenniksArray);
foreach (int i in keywordQueryResult)
    Console.Write(i + " ");
Console.WriteLine();

Console.WriteLine("The array after method-based query Lennikization:");
var methodQueryResult = methodQueryLennikization(lenniksArray);
foreach (int i in methodQueryResult)
    Console.Write(i + " ");
Console.WriteLine();

IEnumerable<int> proceduralLennikization(int[] array)
{
    List<int> result = new List<int>();
    foreach (int i in array)
        if(i % 2 == 0)
            result.Add(i * 2);
    result.Sort();
    return result;
}

IEnumerable<int> keywordQueryLennikization(int[] array) =>
    from i in array where i % 2 == 0 orderby i select i * 2;

IEnumerable<int> methodQueryLennikization(int[] array) =>
    array.Where(i => i % 2 == 0).Order().Select(i => i * 2);

/*
 * The procedural approacb is substantially worse for readibility 
 * and seems bad for that reason. The keyword-based and method-based 
 * queries are much more concise. The keyword-based query is marginally 
 * more readable than the method-based query; both have the same length 
 * but the keyword approach uses fewer symbols like parentheses and has 
 * more whitespace.
 * 
 * For these reasons I prefer the keyword-based query approach.
 * 
 * 
 */