Console.WriteLine("What kind of thing are we talking about?");
// noun
string noun = Console.ReadLine();

/* prefix */
Console.WriteLine("How would you describe it? Big? Azure? Tattered?");
string prefix = Console.ReadLine();

string suffix1 = "of Doom"; // first suffix

string suffix2 = "3000"; // second suffix

Console.WriteLine($"The {prefix} {noun} {suffix1} {suffix2}!");

// Answer: You could make this code more readable by changing the variable names to reflect their purpose.
// This would mostly make the comments redundant, which is fine.
// Using string interpolation might(?) make bugs like the duplicate "of" bug easier to spot and avoid.
// Adding empty lines between different parts of the name helps, or at least looks nice to me.
// But this isn't a very complicated program so these steps are not very necessary...
