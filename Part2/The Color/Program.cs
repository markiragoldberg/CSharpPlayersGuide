Color offBlack = new(40, 40, 40);
Color black = Color.Black;

Console.WriteLine($"Off-black: {offBlack}");
Console.WriteLine($"Black: {black}");

// ---------------------------

class Color
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public static Color White => new(255, 255, 255);
    public static Color Black => new(0, 0, 0);
    public static Color Red => new(255, 0, 0);
    public static Color Orange => new(255, 165, 0);
    public static Color Yellow => new(255, 255, 0);
    public static Color Green => new(0, 128, 0);
    public static Color Blue => new(0, 0, 255);
    public static Color Purple => new(128, 0, 128);

    public Color(int red, int green, int blue) { R = red; G = green; B = blue; }
    public Color(Color color) { R = color.R; G = color.G; B = color.B; }
    public override string ToString() => $"(R: {R}, G: {G}, B: {B})";
}
