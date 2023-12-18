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

Arrow yourArrow = new Arrow(arrowhead, fletching, shaftLength);
Console.WriteLine($"Your arrow will cost {yourArrow.Cost():0.##} gold.");

// ---------------------

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
    private Arrowhead _arrowhead;
    private Fletching _fletching;
    private int _shaftLength;

    public Arrow(Arrowhead arrowhead, Fletching fletching, int shaftLength)
    {
        _arrowhead = arrowhead;
        _fletching = fletching;
        _shaftLength = shaftLength;
    }

    public float Cost()
    {
        float cost = _arrowhead switch
        {
            Arrowhead.Steel => 10,
            Arrowhead.Wood => 3,
            Arrowhead.Obsidian => 5,
            _ => 0
        };
        cost += _fletching switch
        {
            Fletching.Plastic => 10,
            Fletching.Turkey => 5,
            Fletching.Goose => 3,
            _ => 0
        };
        cost += 0.05f * _shaftLength;
        return cost;
    }
    public Arrowhead GetArrowhead() { return _arrowhead; }
    public Fletching GetFletching() { return _fletching; }
    public int GetShaftLength() { return _shaftLength; }
}
enum Arrowhead { Steel, Wood, Obsidian }
enum Fletching { Plastic, Turkey, Goose }