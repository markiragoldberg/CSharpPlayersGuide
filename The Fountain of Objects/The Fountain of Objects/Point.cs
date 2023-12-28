namespace CavernOfObjects;

public readonly record struct Point(int X, int Y)
{
    public bool Is(int x, int y) => X == x && Y == y;
    public bool AdjacentTo(Point other)
    {
        return Math.Abs(X - other.X) <= 1
            && Math.Abs(Y - other.Y) <= 1;
    }
    public override string ToString()
    {
        return $"(Row={X}, Column={Y})";
    }
}
