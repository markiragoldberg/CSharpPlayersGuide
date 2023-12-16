Console.Write("The following items are available:\n" +
    "1 - Rope\n" +
    "2 - Torches\n" +
    "3 - Climbing Equipment\n" +
    "4 - Clean Water\n" +
    "5 - Machete\n" +
    "6 - Canoe\n" +
    "7 - Food Supplies\n" +
    "What number do you want to see the price of? ");
int choice = Convert.ToInt32(Console.ReadLine());

Console.WriteLine(choice switch
{
    1 => "Rope costs 10 gold.",
    2 => "Torches cost 16 gold.",
    3 => "Climbing equipment costs 24 gold.",
    4 => "Clean water costs 2 gold.",
    5 => "Machetes cost 20 gold.",
    6 => "Canoes cost 200 gold.",
    7 => "Food supplies cost 2 gold.",
    _ => throw new Exception("Choice does not correspond to an item")
});
