namespace The_Final_Battle.World
{
	public class Vertex
    {
        public Dictionary<Direction, Vertex> Neighbors { get; } = new();
        public Location Interior { get; }

        public Vertex(Location interior)
        {
            Interior = interior;
        }
        public bool AddEdge(Direction toThem, Vertex them)
		{
			// Ensure neither vertex already has a conflicting edge
			Direction toThis = ReverseDirection(toThem);
			this.Neighbors.TryGetValue(toThem, out Vertex? maybeThem);
			them.Neighbors.TryGetValue(toThis, out Vertex? maybeThis);
            if ((maybeThis != null && maybeThis != this)
                || (maybeThem != null && maybeThis != them))
                return false;
            // Conditions are met. Create the edge.
            if (maybeThem == null)
                this.Neighbors.Add(toThem, them);
            if (maybeThis == null)
				them.Neighbors.Add(toThis, this);
            return true;
		}
        public bool AddDirectedEdge(Direction direction, Vertex neighbor)
            => Neighbors.TryAdd(direction, neighbor);

		public static Direction ReverseDirection(Direction direction)
		    => direction switch
		{
			Direction.North => Direction.South,
			Direction.East => Direction.West,
			Direction.South => Direction.North,
			Direction.West => Direction.East,
            _ => direction
		};
	}

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}
