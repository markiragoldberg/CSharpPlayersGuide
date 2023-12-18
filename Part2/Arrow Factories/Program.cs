Console.WriteLine("What kind of arrow do you want?");
Console.WriteLine($"1 - Elite Arrow: steel arrowhead, plastic fletching, 95 cm shaft");
Console.WriteLine($"2 - Beginner Arrow: wood arrowhead, goose fletching, 75 cm shaft");
Console.WriteLine($"3 - Marksman Arrow: steel arrowhead, goose fletching, 65 cm shaft");
Console.WriteLine($"4 - Custom Arrow: made to order");
Arrow yourArrow = AskForNumberInRange("?> ", 1, 4) switch
{
    1 => Arrow.CreateEliteArrow(),
    2 => Arrow.CreateBeginnerArrow(),
    3 => Arrow.CreateMarksmanArrow(),
    _ => AskForCustomArrow()
};

Console.WriteLine($"Your " +
    $"{yourArrow.ShaftLength} cm {yourArrow.Arrowhead.ToString().ToLower()} arrow with " +
    $"{yourArrow.Fletching.ToString().ToLower()} fletching will cost {yourArrow.Cost():0.##} gold.");

// ---------------------

Arrow AskForCustomArrow()
{
    Console.WriteLine("What kind of arrowhead do you want?");
    for (int i = 0; i < 3; i++)
    {
        Console.WriteLine($"{i + 1} - {(Arrowhead)i}");
    }
    Arrowhead arrowhead = (Arrowhead)(AskForNumberInRange("?> ", 1, 3) - 1);

    int shaftLength = AskForNumberInRange(
        "How long do you want the shaft to be, within 60 to 100 cm? ", 60, 100);

    Console.WriteLine("What kind of fletching do you want?");
    for (int i = 0; i < 3; i++)
    {
        Console.WriteLine($"{i + 1} - {(Fletching)i}");
    }
    Fletching fletching = (Fletching)(AskForNumberInRange("?> ", 1, 3) - 1);

    return new Arrow(arrowhead, fletching, shaftLength);
}

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
// ---------------------
class Arrow
{
    public Arrowhead Arrowhead { get; }
    public Fletching Fletching { get; }
    public int ShaftLength { get; }

    public Arrow(Arrowhead arrowhead, Fletching fletching, int shaftLength)
    {
        Arrowhead = arrowhead;
        Fletching = fletching;
        ShaftLength = shaftLength;
    }

    public static Arrow CreateEliteArrow() => new Arrow(Arrowhead.Steel, Fletching.Plastic, 95);
    public static Arrow CreateBeginnerArrow() => new Arrow(Arrowhead.Wood, Fletching.Goose, 75);
    public static Arrow CreateMarksmanArrow() => new Arrow(Arrowhead.Steel, Fletching.Goose, 65);
    

    public float Cost()
    {
        float cost = Arrowhead switch
        {
            Arrowhead.Steel => 10,
            Arrowhead.Wood => 3,
            Arrowhead.Obsidian => 5,
            _ => 0
        };
        cost += Fletching switch
        {
            Fletching.Plastic => 10,
            Fletching.Turkey => 5,
            Fletching.Goose => 3,
            _ => 0
        };
        cost += 0.05f * ShaftLength;
        return cost;
    }
}
enum Arrowhead { Steel, Wood, Obsidian }
enum Fletching { Plastic, Turkey, Goose }