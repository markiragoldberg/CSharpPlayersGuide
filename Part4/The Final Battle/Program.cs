using ConsoleIO;
using The_Final_Battle;
using The_Final_Battle.Commanders;
using The_Final_Battle.Messaging;

Log log = new();
//string playerName;
#if DEBUG
    string playerName = "Brad";
#else
    string playerName = ColoredConsole.AskForType<string>("What is your name? ");
#endif
ColoredConsole.WriteLine($"Hello, {playerName}!", ConsoleColor.Green);

ICommander playerCommander;
ICommander enemyCommander;
#if DEBUG
    bool customize = false;
#else
    bool customize = ColoredConsole.AskToConfirm("Do you want to customize who controls which teams? ");
#endif
if(customize)
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


ThingFactory creatureFactory = new ThingFactory();
FightTeam playerTeam = new FightTeam(playerCommander);
playerTeam.Add(creatureFactory.CreateCreature("hero", name: playerName));
playerTeam.Add(creatureFactory.CreateCreature("fletcher", name: "Vin Fletcher"));
playerTeam.Inventory.Add(creatureFactory.CreateItem("bandage", 3));

List<FightTeam> enemyTeams = [];

FightTeam enemy1 = new FightTeam(enemyCommander);
enemy1.Add(creatureFactory.CreateCreature("skeleton", "skelly sentry"));
enemyTeams.Add(enemy1);

FightTeam enemy2 = new FightTeam(enemyCommander);
enemy2.Add(creatureFactory.CreateCreature("skeleton", "skelly warrior"));
enemy2.Add(creatureFactory.CreateCreature("skeleton", "skelly soldja"));
enemyTeams.Add(enemy2);


FightTeam enemyLast = new FightTeam(enemyCommander);
enemyLast.Add(creatureFactory.CreateCreature("boss"));
enemyTeams.Add(enemyLast);

Fight nextFight;
foreach (FightTeam enemyTeam in enemyTeams)
{
    nextFight = new(playerTeam, enemyTeam);
    nextFight.Resolve(out FightTeam winningTeam, log);
    if(winningTeam != playerTeam)
    {
        The_Final_Battle.Screens.GameLostScreen.Display();
        return;
    }
}
The_Final_Battle.Screens.GameWonScreen.Display();