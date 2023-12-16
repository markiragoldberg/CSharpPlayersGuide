Console.WriteLine("Choose a recipe:");
for(int i = 0; i < 3; i++)
{
    Console.WriteLine($"{i+1} - {(Recipe) i}");
}
Recipe recipe = (Recipe)(AskForNumberInRange("?> ", 1, 3) - 1);

Console.Clear();
Console.WriteLine("Choose an ingredient:");
for (int i = 0; i < 4; i++)
{
    Console.WriteLine($"{i + 1} - {(Ingredient)i}");
}
Ingredient ingredient = (Ingredient)(AskForNumberInRange("?> ", 1, 4) - 1);

Console.Clear();
Console.WriteLine("Choose a seasoning:");
for (int i = 0; i < 3; i++)
{
    Console.WriteLine($"{i + 1} - {(Seasoning)i}");
}
Seasoning seasoning = (Seasoning)(AskForNumberInRange("?> ", 1, 3) - 1);

(Recipe recipe, Ingredient ingredient, Seasoning seasoning) soup = (recipe, ingredient, seasoning);

Console.WriteLine($"You cooked {soup.seasoning} {soup.ingredient} {soup.recipe}!");

// ------------------------------------------------------

int AskForNumber(string text)
{
    Console.Write(text);
    string response = Console.ReadLine();
    int? result = null;
    while (result == null)
    {
        try
        {
            result = int.Parse(response);
        }
        catch (FormatException)
        {
            Console.Write("Try again: ");
            response = Console.ReadLine();
        }
    }
    return (int)result;
}

int AskForNumberInRange(string text, int min, int max)
{
    int result = AskForNumber(text);
    while (result < min || result > max)
    {
        result = AskForNumber("Try again: ");
    }
    return result;
}

// -----------------------------------------------

enum Recipe { Soup, Stew, Gumbo }
enum Ingredient { Mushrooms, Chicken, Carrots, Potatoes }
enum Seasoning { Spicy, Salty, Sweet }
