using The_Final_Battle.Commanders;

namespace The_Final_Battle.World
{
	public class CrudeWorldGenerator(ThingFactory factory) : IWorldGenerator
	{
		public Vertex GenerateWorld()
		{
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

#if DEBUG
			Location testAIItemUseLocation = new("Consolas Ripoff",
				"This ruined hospital is marked by explosions.");
			FightTeam hammerAndAnvil = new(enemyCommander);
			hammerAndAnvil.Add(factory.CreateCreature("skeleton hammerer", "Hammer the Skeleton"));
			hammerAndAnvil.Add(factory.CreateCreature("skeleton armored", "Anvil the Skeleton"));
			hammerAndAnvil.Inventory.Add(factory.CreateItem("bandage", 5));
			testAIItemUseLocation.Enemies = hammerAndAnvil;
			Vertex testAIIItemUse = new(testAIItemUseLocation);
			origin.AddEdge(Direction.North, testAIIItemUse);
#endif

			Location bossCrypt = new("Consolas Crypt",
				"You are in the Crypt of Consolas. The UNCODED ONE is here but you can beat him, easy-peasy.");
			FightTeam bossTeam = new(enemyCommander);
			bossTeam.Add(factory.CreateCreature("boss"));
			bossTeam.Add(factory.CreateCreature("skeleton healer"));
			bossCrypt.Enemies = bossTeam;
			Vertex east2x = new(bossCrypt);
			east.AddEdge(Direction.East, east2x);

			return origin;
		}
	}
}
