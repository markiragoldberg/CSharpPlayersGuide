Console.WriteLine("What kind of thing are we talking about?");
// noun
string a = Console.ReadLine();

/* prefix */
Console.WriteLine("How would you describe it? Big? Azure? Tattered?");
string b = Console.ReadLine();

string c = "of Doom"; // first suffix
string d = "3000"; // second suffix
Console.WriteLine("The " + b + " " + a + " " + c + " " + d + "!");