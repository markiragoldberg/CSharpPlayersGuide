using The_Final_Battle.Commanders;

namespace The_Final_Battle.World
{
	public class CrudeWorldGenerator : IWorldGenerator
	{
		public Vertex GenerateWorld()
		{
			ThingFactory factory = new();
			MindlessAICommander enemyCommander = new();

			Location originLoc = new("Consolas", 
				"The peerless city of Consolas stretches beyond sight in all directions, up and down.");
			Vertex origin = new(originLoc);

			Location westLoc = new("Consolas Palace", 
				"You are in the palace of Consolas.\nThe KING is here, but his mind is in another castle.");
			Vertex west = new(westLoc);
			origin.AddEdge(Direction.West, west);

			Location eastLoc = new("Consolas Graveyard",
				"The dead do not rest easy in the fog-soaked mud of Consolas's graveyard.");
			FightTeam twoSkeletons = new FightTeam(enemyCommander);
			twoSkeletons.Add(factory.CreateCreature("skeleton", "skelly warrior"));
			twoSkeletons.Add(factory.CreateCreature("skeleton", "skelly soldja"));
			eastLoc.Enemies = twoSkeletons;
			Vertex east = new(eastLoc);
			origin.AddEdge(Direction.East, east);

			Location bossCrypt = new("Consolas Crypt",
				"You are in the Crypt of Consolas. The UNCODED ONE is here but you can beat him, easy-peasy.");
			FightTeam bossTeam = new(enemyCommander);
			bossTeam.Add(factory.CreateCreature("boss"));
			bossCrypt.Enemies = bossTeam;
			Vertex east2x = new(bossCrypt);
			east.AddEdge(Direction.East, east2x);

			return origin;
		}
	}
}
