using ConsoleIO;
using The_Final_Battle;
using The_Final_Battle.Commanders;
using The_Final_Battle.Messaging;
using The_Final_Battle.Screens;
using The_Final_Battle.World;

Log log = new();
#if DEBUG
string playerName = "The True Debugger";
#else
string playerName = ColoredConsole.AskForType<string>("What is your name? ");
#endif
ColoredConsole.WriteLine($"Hello, {playerName}!", ConsoleColor.Green);

CrudeWorldGenerator worldGenerator = new CrudeWorldGenerator();
Vertex current = worldGenerator.GenerateWorld();

ThingFactory creatureFactory = new ThingFactory();
FightTeam playerTeam = new FightTeam(new HumanCommander());
playerTeam.Add(creatureFactory.CreateCreature("hero", name: playerName));
playerTeam.Add(creatureFactory.CreateCreature("fletcher", name: "Vin Fletcher"));
playerTeam.Inventory.Add(creatureFactory.CreateItem("bandage", 3));

ConsoleKeyInfo? input = null;
while (input?.Key != ConsoleKey.Q
	|| (input?.Modifiers & ConsoleModifiers.Shift) == 0)
{
	LocationScreen.Display(current);
	if (current.Interior.Enemies != null)
	{
		log.AddTemporaryMessage("You have encountered enemies.", MessageCategory.Bad);
		MessagesScreen.Display(log);
		Console.ReadKey(true);
		Fight fight = new(playerTeam, current.Interior.Enemies);
		fight.Resolve(out FightTeam winningTeam, log);
		if (winningTeam != playerTeam)
		{
			GameLostScreen.Display();
			return;
		}
		else if (current.Interior.Name == "Consolas Crypt")
		{
			The_Final_Battle.Screens.GameWonScreen.Display();
			return;
		}
		current.Interior.Enemies = null;
		log.AddTemporaryMessage("You have defeated the enemy.", MessageCategory.Good);
		Console.ReadKey(true);
		log.ClearAllMessages();
		LocationScreen.Display(current);
	}
	Direction? moveDirection = null;
	List<(string, Action)> moveOptions = [];
	foreach (Direction validMove in current.Neighbors.Keys)
	{
		moveOptions.Add((validMove.ToString(), () => moveDirection = validMove));
	}
	MenuScreen.Display(moveOptions, "Which direction do you want to go?");
	if (moveDirection != null)
	{
		current = current.Neighbors[moveDirection.Value];
	}
}
