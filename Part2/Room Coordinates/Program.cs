ReportAdjacency(0, 0);
ReportAdjacency(0, 1);
ReportAdjacency(0, -1);
ReportAdjacency(1, 0);
ReportAdjacency(-1, 0);
ReportAdjacency(-1, -1);
ReportAdjacency(-1, 1);
ReportAdjacency(1, -1);
ReportAdjacency(1, 1);
ReportAdjacency(-2, 0);
ReportAdjacency(2, 0);
ReportAdjacency(0, 2);
ReportAdjacency(0, -2);

// ----------------------------------
static void ReportAdjacency(int X = 0, int Y = 0)
{
    Coordinate origin = new();
    Coordinate other = new(X, Y);
    Console.WriteLine($"Origin is {(origin.IsAdjacent(other) ? "" : "not ")}" +
        $"adjacent to {other}.");
}

// ----------------------------------

public readonly struct Coordinate
{
    public int X { get; init; }
    public int Y { get; init; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
    public readonly bool IsAdjacent(Coordinate other)
    {
        int x_diff = Math.Abs(X - other.X);
        int y_diff = Math.Abs(Y - other.Y);
        return (x_diff == 1 && y_diff == 0) || (x_diff == 0 && y_diff == 1);
    }
    public readonly override string ToString()
    {
        return $"({X,2},{Y,2})";
    }
}