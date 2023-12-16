double baseLength = AskForDouble("Enter the length of the triangle's base: ");
double height = AskForDouble("Enter the triangle's height: ");

double area = (baseLength * height) / 2.0;

Console.WriteLine($"The triangle's area is {area}.");


// update from "Taking a Number" pg. 106
double AskForDouble(string text)
{
    Console.Write(text);
    string response = Console.ReadLine();
    double? result = null;
    while (result == null)
    {
        try
        {
            result = double.Parse(response);
        }
        catch (FormatException)
        {
            Console.Write("Try again: ");
            response = Console.ReadLine();
        }
    }
    return (double)result;
}