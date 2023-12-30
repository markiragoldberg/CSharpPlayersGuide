Console.Title = "Potion Crafter";
Potion workInProgress = Potion.Water;

while(true)
{
    Console.WriteLine($"You have a {PotionString(workInProgress)}.");
    Console.Write("Ingredients available: ");
    for (int i = 0; i < (int)Ingredient.EyeshineGem; i++)
        Console.Write(IngredientString((Ingredient)i) + ", ");
    Console.WriteLine(IngredientString(Ingredient.EyeshineGem));
    Console.Write("What ingredient do you want to add to the potion? ");
    Ingredient? ingredientToAdd = null;
    while (ingredientToAdd == null)
    {
        string? input = Console.ReadLine();
        ingredientToAdd = input?.ToLower() switch
        {
            "stardust" => Ingredient.Stardust,
            "snake venom" => Ingredient.SnakeVenom,
            "dragon breath" => Ingredient.DragonBreath,
            "shadow glass" => Ingredient.ShadowGlass,
            "eyeshine gem" => Ingredient.EyeshineGem,
            _ => null
        };
        if (ingredientToAdd == null)
            Console.Write("Try again: ");
    }
    Potion? newPotion = (workInProgress, ingredientToAdd) switch
    {
        (Potion.Water, Ingredient.Stardust) => Potion.Elixir,
        (Potion.Elixir, Ingredient.SnakeVenom) => Potion.Poison,
        (Potion.Elixir, Ingredient.DragonBreath) => Potion.Flying,
        (Potion.Elixir, Ingredient.ShadowGlass) => Potion.Invisibility,
        (Potion.Elixir, Ingredient.EyeshineGem) => Potion.NightSight,
        (Potion.NightSight, Ingredient.ShadowGlass) => Potion.CloudyBrew,
        (Potion.Invisibility, Ingredient.EyeshineGem) => Potion.CloudyBrew,
        (Potion.CloudyBrew, Ingredient.Stardust) => Potion.Wraith,
        _ => null
    };
    if (newPotion != null)
    {
        Console.WriteLine($"Your {PotionString(workInProgress)} has become {PotionString(newPotion)}.");
        workInProgress = (Potion) newPotion;
        Console.Write($"Would you like to 'continue', or 'accept' your {PotionString(workInProgress)}? ");
        string? input = Console.ReadLine()?.ToLower();
        while (input != "continue" && input != "accept")
        {
            Console.Write("'Continue' or 'accept'? ");
            input = Console.ReadLine()?.ToLower();
        }
        if (input == "accept")
        {
            Console.WriteLine($"You finish up and take your {PotionString(workInProgress)}.");
            return;
        }
    }
    else
    {
        Console.WriteLine("That ingredient has ruined your potion.\nYou must start over.");
        workInProgress = Potion.Water;
    }
}

string? PotionString(Potion? p) => p switch
{
    Potion.Water => "water potion",
    Potion.Elixir => "elixir",
    Potion.Poison => "poison potion",
    Potion.Flying => "flying potion",
    Potion.Invisibility => "invisibility potion",
    Potion.NightSight => "night sight potion",
    Potion.CloudyBrew => "cloudy brew",
    Potion.Wraith => "wraith potion",
    _ => null
};
string? IngredientString(Ingredient? i) => i switch
{
    Ingredient.Stardust => "stardust",
    Ingredient.SnakeVenom => "snake venom",
    Ingredient.DragonBreath => "dragon breath",
    Ingredient.ShadowGlass => "shadow glass",
    Ingredient.EyeshineGem => "eyeshine gem",
    _ => null
};

enum Potion { Water, Elixir, Poison, Flying, Invisibility, NightSight, CloudyBrew, Wraith }
enum Ingredient { Stardust, SnakeVenom, DragonBreath, ShadowGlass, EyeshineGem }
