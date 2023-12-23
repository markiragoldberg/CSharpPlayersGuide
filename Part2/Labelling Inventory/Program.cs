Pack pack = new(15f, 15f, 26);

int choice = -1;
while (choice != 0)
{
    Console.WriteLine($"You have a {pack}.");
    Console.WriteLine("What would you like to add to it?");
    Console.WriteLine("1 - Arrow");
    Console.WriteLine("2 - Bow");
    Console.WriteLine("3 - Rope");
    Console.WriteLine("4 - Water");
    Console.WriteLine("5 - Food Ration");
    Console.WriteLine("6 - Sword");
    Console.WriteLine("Or enter 0 to quit.");
    choice = AskForNumberInRange("?> ", 0, 6);
    if (choice != 0)
    {
        string? reasonItDidntWork;
        InventoryItem item = choice switch
        {
            1 => new Arrow(),
            2 => new Bow(),
            3 => new Rope(),
            4 => new Water(),
            5 => new FoodRation(),
            _ => new Sword()
        };
        bool worked = pack.Add(item, out reasonItDidntWork);
        if (worked)
        {
            Console.WriteLine($"You add the {item} to your pack.");
        }
        else
        {
            Console.WriteLine($"You cannot add the {item} because {reasonItDidntWork}.");
        }
    }
}
Console.WriteLine("You set out...");

// ------------------------------------------------------

int AskForNumber(string text)
{
    Console.Write(text);
    string? response = Console.ReadLine();
    int? result = null;
    while (result == null)
    {
        try
        {
            result = int.Parse(response!);
        }
        catch
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

//--------------------------------------

class InventoryItem
{
    public float Weight { get; }
    public float Volume { get; }

    public InventoryItem(float weight, float volume)
    {
        Weight = weight;
        Volume = volume;
    }
}

// arrow bow rope water "food rations" sword
class Arrow : InventoryItem
{
    public Arrow() : base(0.1f, 0.05f) { }
}
class Bow : InventoryItem
{
    public Bow() : base(1f, 4f) { }
}
class Rope : InventoryItem
{
    public Rope() : base(1f, 1.5f) { }
}
class Water : InventoryItem
{
    public Water() : base(2f, 3f) { }
}
class FoodRation : InventoryItem
{
    public FoodRation() : base(1, 0.5f) { }
    public override string ToString() => "Food Ration";
}
class Sword : InventoryItem
{
    public Sword() : base(5f, 3f) { }
}

class Pack(float weightLimit, float volumeLimit, int ItemLimit)
{
    public float Weight { get; private set; }
    public float Volume { get; private set; }
    public int ItemCount { get; private set; }
    public float WeightLimit { get; } = weightLimit;
    public float VolumeLimit { get; } = volumeLimit;
    public int ItemLimit { get => _items.Length; }
    private readonly InventoryItem[] _items = new InventoryItem[ItemLimit];

    public bool Add(InventoryItem item, out string? reason)
    {
        if (Weight + item.Weight > WeightLimit + 0.0001f)
        {
            reason = "it weighs too much";
            return false;
        }
        else if (Volume + item.Volume > VolumeLimit + 0.0001f)
        {
            reason = "it is too bulky";
            return false;
        }
        else if (ItemCount + 1 > ItemLimit)
        {
            reason = "the pack cannot hold more items";
            return false;
        }
        _items[ItemCount++] = item;
        Weight += item.Weight;
        Volume += item.Volume;
        reason = null;
        return true;
    }

    public override string ToString()
    {
        string result = $"pack ({Weight:#.##}/{WeightLimit:#.##} weight, " +
            $"{Volume:#.##}/{VolumeLimit:#.##} volume, {ItemCount}/{ItemLimit} items)";
        if (ItemCount > 0)
        {
            result += " containing ";
            for(int i = 0; i < ItemCount - 1; i++)
            {
                result += $"{_items[i]}, ";
            }
            result += _items[ItemCount - 1].ToString();
        }
        return result;
    }
}
