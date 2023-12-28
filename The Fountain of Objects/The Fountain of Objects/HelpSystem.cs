namespace CavernOfObjects;

public static class HelpSystem
{
    public static void DisplayIntro()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(
            "You enter the Cavern of Objects, a maze of rooms filled with dangerous pits, " +
            "in search of the Fountain of Objects. Light is visible only in the entrance, " +
            "and no other light is seen anywhere in the caverns. You must navigate the " +
            "Caverns with your other senses. Find the Fountain of Objects, activate it, " +
            "and return to the entrance.");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(
            "\nLook out for pits. You will feel a breeze if a pit is in an adjacent room. " +
            "If you enter a room with a pit, you will die.");
        Console.WriteLine(
            "\nMaelstroms are violent forces of sentient wind. Entering a room with one " +
            "could transport you to any other location in the caverns. You will be able to " +
            "hear their growling and groaning in nearby rooms.");
        Console.WriteLine(
            "\nAmaroks roam the caverns. Encountering one is certain death, but you can " +
            "smell their rotten stench in nearby rooms.");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(
            "\nYou carry with you a bow and a quiver of arrows. You can use them to shoot " +
            "monsters in the caverns, but be warned: you have a limited supply.");
        Console.ResetColor();
    }

    public static void DisplayHelp()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("To move, enter \"move\" followed by \"north\",\"east\",\"south\", or \"west\".");
        Console.WriteLine("To fire an arrow, enter \"shoot\" followed by \"north\",\"east\",\"south\", or \"west\".");
        Console.WriteLine("To activate the fountain should you find it, enter \"enable fountain\".");
        Console.WriteLine("To quit, enter \"quit\".");
        Console.ResetColor();
    }
}
