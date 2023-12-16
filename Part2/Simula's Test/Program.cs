ChestState currentState = ChestState.Locked;

while(true)
{
    Console.Write($"The chest is {currentState.ToString().ToLower()}. What do you want to do? ");
    string response = Console.ReadLine();
    switch (response)
    {
        case "unlock":
            if (currentState == ChestState.Locked)
            {
                currentState = ChestState.Closed;
            }
            break;
        case "open":
            if (currentState == ChestState.Closed)
            {
                currentState = ChestState.Open;
            }
            break;
        case "lock":
            if (currentState == ChestState.Closed)
            {
                currentState = ChestState.Locked;
            }
            break;
        case "close":
            if (currentState == ChestState.Open)
            {
                currentState = ChestState.Closed;
            }
            break;
        case "quit":
        case "exit":
            return;
        default:
            break;
    }
}

// -------------------------------------

enum ChestState { Locked, Closed, Open }