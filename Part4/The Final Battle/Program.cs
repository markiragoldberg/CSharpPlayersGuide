using ConsoleIO;
using The_Final_Battle;

TFBConsole display = new();
//string playerName = display.AskForType<string>("What is your name? ");
List<string> possibleNames = ["Alice", "Bob", "Brad", "Charlie"];
int nameIndex = ColoredConsole.AskForMenuOption("?> ", possibleNames, "What is your name?");
string playerName = possibleNames[nameIndex];
display.AddMessage($"Hello, {playerName}!", MessageCategory.VeryGood);

CreatureFactory creatureFactory = new CreatureFactory();
FightTeam playerTeam = new FightTeam(new HumanCommander());
playerTeam.AddFighter(creatureFactory.CreateCreature("hero", name: playerName));
playerTeam.AddFighter(creatureFactory.CreateCreature("fletcher", name: "Vin Fletcher"));

List<FightTeam> enemyTeams = new();

FightTeam enemy1 = new FightTeam(new MindlessAICommander());
enemy1.AddFighter(creatureFactory.CreateCreature("skeleton"));
enemyTeams.Add(enemy1);

FightTeam enemy2 = new FightTeam(new MindlessAICommander());
enemy2.AddFighter(creatureFactory.CreateCreature("skeleton"));
enemy2.AddFighter(creatureFactory.CreateCreature("skeleton"));
enemyTeams.Add(enemy2);


FightTeam enemyLast = new FightTeam(new MindlessAICommander());
enemyLast.AddFighter(creatureFactory.CreateCreature("boss"));
enemyTeams.Add(enemyLast);

Fight nextFight;
foreach (FightTeam enemyTeam in enemyTeams)
{
    nextFight = new(playerTeam, enemyTeam, display);
    nextFight.Resolve(out FightTeam winningTeam);
    if(winningTeam != playerTeam)
    {
        display.GameLost();
        return;
    }
}
display.GameWon();