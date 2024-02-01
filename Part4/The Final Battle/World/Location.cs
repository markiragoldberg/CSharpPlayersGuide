namespace The_Final_Battle.World
{
	public class Location
	{
		public string Name { get; }
		public string Description { get; }
		public FightTeam? Enemies { get; set; }

		public Location(string name, string description, FightTeam? enemies = null)
		{
			Name = name;
			Description = description;
			Enemies = enemies;
		}
	}
}
