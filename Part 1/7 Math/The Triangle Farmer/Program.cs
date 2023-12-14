Console.Write("Enter the length of the triangle's base: ");
double baseLength = Convert.ToDouble(Console.ReadLine());

Console.Write("Enter the triangle's height': ");
double height = Convert.ToDouble(Console.ReadLine());

double area = (baseLength * height) / 2.0;

Console.WriteLine($"The triangle's area is {area}.");