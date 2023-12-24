ColoredItem<Sword> greenSword = new(ConsoleColor.Green, new Sword());
ColoredItem<Bow> redBow = new(ConsoleColor.Red, new Bow());
ColoredItem<Axe> blueAxe = new(ConsoleColor.Blue, new Axe());
Console.Write("I have a ");
greenSword.Display();
Console.Write(", a ");
redBow.Display();
Console.Write(", and an ");
blueAxe.Display();
Console.WriteLine(".");

// --------------------------------
public class ColoredItem<T>
{
    public ConsoleColor Color { get; set; }
    public T Item { get; }
    public ColoredItem(ConsoleColor color, T item)
    {
        Color = color;
        Item = item;
    }
    public void Display()
    {
        Console.ForegroundColor = Color;
        Console.Write(Item);
        Console.ResetColor();
    }
}
public class Sword { }
public class Bow { }
public class Axe { }