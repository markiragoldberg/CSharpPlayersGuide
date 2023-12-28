using SaferNumberCrunching;

int parsedInt = Parser.AskForType<int>("Enter an integer: ");
Console.WriteLine($"You entered {parsedInt}.");

double parsedDouble = Parser.AskForType<double>("Enter a decimal number: ");
Console.WriteLine($"You entered {parsedDouble}.");

bool parsedBool = Parser.AskForType<bool>("Enter true or false: ");
Console.WriteLine($"You entered {parsedBool.ToString().ToLower()}.");
