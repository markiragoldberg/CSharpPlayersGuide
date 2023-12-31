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
Console.WriteLine($"bc[0]: {bc[0]}");
Console.WriteLine($"bc[1]: {bc[1]}");
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
    public int this[int index]
    {
        get
        {
            if (index == 0) return Row;
            else if (index == 1) return Column;
            else throw new IndexOutOfRangeException();
        }

        // I do not think the indexer is a good addition to the BlockCoordinate 
        // unless it is used extremely heavily and needs extremely concise syntax.
        // Otherwise you are simply inviting confusion over which index is the 
        // row and which is the column. If the user is more familiar with 
        // the form (x, y) where x = col, y = row then their intuition will be
        // opposite of how the BlockCoordinate is supposed to be used.
    }
}
public record BlockOffset(int RowOffset, int ColumnOffset);
public enum Direction { North, East, South, West }