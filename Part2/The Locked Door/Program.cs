Door door = new(AskForPasscode("Enter an initial four-digit passcode for the door: "));
string? response = null;
while(response == null || (response != "quit" && response != "exit"))
{
    Console.WriteLine($"\nThe door is {door.DoorState.ToString().ToLower()}." +
        $" What do you want to do?");
    Console.Write("Unlock, open, close, lock, change password, quit? ");
    response = Console.ReadLine();
    switch(response)
    {
        case "unlock":
            if(door.Unlock(AskForPasscode("Enter the passcode: ")))
            {
                Console.WriteLine("The door unlocks.");
            }
            else
            {
                Console.WriteLine("The passcode is incorrect.");
            }
            break;
        case "open":
            if (door.Open())
            {
                Console.WriteLine("The door opens.");
            }
            break;
        case "close":
            if (door.Close())
            {
                Console.WriteLine("The door closes.");
            }
            break;
        case "lock":
            if (door.Lock())
            {
                Console.WriteLine("The door locks.");
            }
            break;
        case "change passcode":
            if (door.ChangePasscode(AskForPasscode("Enter the current passcode: "),
                AskForPasscode("Enter the new passcode: ")))
            {
                Console.WriteLine("The passcode has been changed.");
            }
            else
            {
                Console.WriteLine("The passcode is incorrect.");
            }
            break;
        case "quit":
            break;
        default:
            Console.WriteLine("That's not an option.");
            break;
    }
}

// -----------------
int AskForPasscode(string prompt)
{
    Console.Write(prompt);
    string? response = Console.ReadLine();
    while (true)
    {
        if (response != null && response.Length == 4)
        {
            try
            {
                return int.Parse(response, System.Globalization.NumberStyles.None);
            }
            catch { }
        }
        Console.Write("Try again: ");
        response = Console.ReadLine();
    }
}
// -----------------
class Door
{
    public DoorState DoorState { get; private set; }
    private int _Passcode { get; set; }
    public Door(int passcode)
    {
        _Passcode = passcode;
        DoorState = DoorState.Locked;
    }
    public bool ChangePasscode(int currentPasscode, int newPasscode)
    {
        if(currentPasscode == _Passcode)
        {
            _Passcode = newPasscode;
            return true;
        }
        return false;
    }
    public bool Unlock(int passcode)
    {
        if(DoorState == DoorState.Locked && passcode == _Passcode)
        {
            DoorState = DoorState.Closed;
            return true;
        }
        return false;
    }
    public bool Open()
    {
        if(DoorState == DoorState.Closed)
        {
            DoorState = DoorState.Open;
            return true;
        }
        return false;
    }
    public bool Close()
    {
        if(DoorState == DoorState.Open)
        {
            DoorState = DoorState.Closed;
            return true;
        }
        return false;
    }
    public bool Lock()
    {
        if (DoorState == DoorState.Closed)
        {
            DoorState = DoorState.Locked;
            return true;
        }
        return false;
    }
}

enum DoorState { Open, Closed, Locked }
