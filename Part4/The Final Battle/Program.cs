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

ThingFactory factory = new ThingFactory();
CrudeWorldGenerator worldGenerator = new CrudeWorldGenerator(factory);
Vertex currentVertex = worldGenerator.GenerateWorld();

FightTeam playerTeam = new FightTeam(playerCommander);
playerTeam.Add(factory.CreateCreature("hero", weapon: factory.CreateWeapon("sword"), name: playerName));
playerTeam.Add(factory.CreateCreature("fletcher", weapon: factory.CreateWeapon("fine bow"), name: "Vin Fletcher"));
playerTeam.Inventory.Add(factory.CreateItem("bandage", 3));

ConsoleKeyInfo? input = null;
while (input?.Key != ConsoleKey.Q
	|| (input?.Modifiers & ConsoleModifiers.Shift) == 0)
{
	LocationScreen.Display(currentVertex);
	FightTeam? enemyTeam = currentVertex.Interior.Enemies;
	if (enemyTeam != null)
	{
		enemyTeam.Commander = enemyCommander;
		log.AddTemporaryMessage("You have encountered enemies.", MessageCategory.Bad);
		MessagesScreen.Display(log);
		Console.ReadKey(true);
		Fight fight = new(playerTeam, enemyTeam);
		fight.Resolve(out FightTeam winningTeam, log);
		if (winningTeam != playerTeam)
		{
			GameLostScreen.Display();
			return;
		}
		else if (currentVertex.Interior.Name == "Consolas Crypt")
		{
			The_Final_Battle.Screens.GameWonScreen.Display();
			return;
		}
		log.AddTemporaryMessage("You have defeated the enemy.", MessageCategory.Good);
		if(enemyTeam.Inventory.Count > 0)
		{
			LootScreen.Display(enemyTeam.Inventory);
		}
		playerTeam.Inventory.TakeAll(enemyTeam.Inventory);
		log.ClearAllMessages();
		LocationScreen.Display(currentVertex);
	}
	Direction? moveDirection = null;
	List<MenuOption> moveOptions = [];
	foreach (Direction eachDirection in Enum.GetValues(typeof(Direction)))
	{
		bool enabled = currentVertex.Neighbors.ContainsKey(eachDirection);
		moveOptions.Add(new(eachDirection.ToString(), () => moveDirection = eachDirection, enabled));
	}
	MenuScreen.Display(moveOptions, "Which direction do you want to go?");
	if (moveDirection != null)
	{
		currentVertex = currentVertex.Neighbors[moveDirection.Value];
	}
}
