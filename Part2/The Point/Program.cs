Point point1 = new Point( 2, 3);
Point point2 = new Point(-4, 0);

Console.WriteLine($"Point 1: {point1}");
Console.WriteLine($"Point 2: {point2}");

// ---------------------------

class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) { X = x; Y = y; }
    public Point(Point toCopy) { X = toCopy.X; Y = toCopy.Y; }
    public Point() { X = 0; Y = 0; }
    public Point Plus(int x, int y) => new(X + x, Y + y);
    public Point Plus(Point point) => new(X + point.X, Y + point.Y);
    public Point Minus(int x, int y) => new(X - x, Y - y);
    public Point Minus(Point point) => new(X - point.X, Y - point.Y);
    public override string ToString() => $"({X}, {Y})";
}

// I made the X and Y properties immutable, because:
// 1 - It makes it less likely the fields will be erroneously changed by other code.
// 2 - Points are very small, so destroying and replacing them is probably fine.
// 3 - If necessary, making the class mutable in the future will be easier than the opposite.
