using PlayersGuide;

#pragma warning disable CS0618 // Type or member is obsolete
string thing = ConsoleInput.AskForType<string>("Enter something to say: ");
#pragma warning restore CS0618 // Type or member is obsolete
Console.WriteLine($"You entered {thing}.");