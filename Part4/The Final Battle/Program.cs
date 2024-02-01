using ConsoleIO;
using The_Final_Battle;
using The_Final_Battle.Commanders;
using The_Final_Battle.Messaging;
using The_Final_Battle.Screens;
using The_Final_Battle.World;

Log log = new();
string playerName = ColoredConsole.AskForType<string>("What is your name? ");
ColoredConsole.WriteLine($"Hello, {playerName}!", ConsoleColor.Green);

ICommander playerCommander;
ICommander enemyCommander;
bool customize = ColoredConsole.AskToConfirmWithDefault(
	"Do you want to customize who controls which teams? ", false);
if (customize)
{
	bool aiPlayerTeam = ColoredConsole.AskForMenuOption<bool>("?> ",
		[("You", false), ("The Computer", true)],
		"Who will control the heroes?");
	Console.WriteLine();
	bool aiEnemyTeam = ColoredConsole.AskForMenuOption<bool>("?> ",
		[("You", false), ("The Computer", true)],
		"And who will control the villains?");

	// If both teams are AI one has to handle screen refreshes and wait for input
	if (aiPlayerTeam == true && aiEnemyTeam == true)
	{
		playerCommander = new WaitForInputAICommander();
		enemyCommander = new MindlessAICommander();
	}
	else
	{
		playerCommander = aiPlayerTeam ? new MindlessAICommander() : new HumanCommander();
		enemyCommander = aiEnemyTeam ? new MindlessAICommander() : new HumanCommander();
	}
}
else
{
	playerCommander = new HumanCommander();
	enemyCommander = new MindlessAICommander();
}

CrudeWorldGenerator worldGenerator = new CrudeWorldGenerator();
Vertex current = worldGenerator.GenerateWorld();

ThingFactory creatureFactory = new ThingFactory();
FightTeam playerTeam = new FightTeam(playerCommander);
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
		current.Interior.Enemies.Commander = enemyCommander;
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
	List<MenuOption> moveOptions = [];
	foreach (Direction eachDirection in Enum.GetValues(typeof(Direction)))
	{
		bool enabled = current.Neighbors.ContainsKey(eachDirection);
		moveOptions.Add(new(eachDirection.ToString(), () => moveDirection = eachDirection, enabled));
	}
	MenuScreen.Display(moveOptions, "Which direction do you want to go?");
	if (moveDirection != null)
	{
		current = current.Neighbors[moveDirection.Value];
	}
}
