BlockCoordinate bc = new(0, 0);
Console.WriteLine(bc);
foreach(Direction direction in Enum.GetValues(typeof(Direction)))
{
    Console.WriteLine($"Moving {direction}");
    bc += direction;
    Console.WriteLine(bc);
}
BlockOffset offset = new(3, 6);
Console.WriteLine($"Moving via {offset}");
bc += offset;
Console.WriteLine(bc);
offset = new(-3, -6);
Console.WriteLine($"Returning via {offset}");
bc += offset;
Console.WriteLine(bc);


// -------------------------------------------

public record BlockCoordinate(int Row, int Column)
{
    public static BlockCoordinate operator +(BlockCoordinate coord, BlockOffset change) =>
        new(coord.Row + change.RowOffset, coord.Column + change.ColumnOffset);
    public static BlockCoordinate operator +(BlockCoordinate coord, Direction direction)
    {
        switch(direction)
        {
            case Direction.North:
                return new BlockCoordinate(coord.Row-1, coord.Column);
            case Direction.South:
                return new BlockCoordinate(coord.Row+1, coord.Column);
            case Direction.East:
                return new BlockCoordinate(coord.Row, coord.Column+1);
            case Direction.West:
            default:
                return new BlockCoordinate(coord.Row, coord.Column-1);
        }
    }
}
public record BlockOffset(int RowOffset, int ColumnOffset);
public enum Direction { North, East, South, West }