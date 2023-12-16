Console.Write("Welcome to Tortuga's shop, restored by Mark. What is your name? ");
string input_name = Console.ReadLine();

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

string noun_verb;
int cost;
switch(choice)
{
    case 1:
        noun_verb = "Rope costs";
        cost = 10;
        break;
    case 2:
        noun_verb = "Torches cost";
        cost = 16;
        break;
    case 3:
        noun_verb = "Climbing equipment costs";
        cost = 24;
        break;
    case 4:
        noun_verb = "Clean water costs";
        cost = 2;
        break;
    case 5:
        noun_verb = "Machetes cost";
        cost = 20;
        break;
    case 6:
        noun_verb = "Canoes cost";
        cost = 200;
        break;
    case 7:
        noun_verb = "Food supplies cost";
        cost = 2;
        break;
    default:
        noun_verb = "Nothing costs";
        cost = 0;
        break;
}

if (input_name == "Mark")
    cost /= 2;

Console.WriteLine($"{noun_verb} {cost} gold.");
