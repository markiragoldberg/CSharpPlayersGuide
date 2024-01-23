using ConsoleIO;
using The_Final_Battle;
using The_Final_Battle.Messaging;

Log log = new();
string playerName = ColoredConsole.AskForType<string>("What is your name? ");
log.AddMessage($"Hello, {playerName}!", MessageCategory.VeryGood);

CreatureFactory creatureFactory = new CreatureFactory();
FightTeam playerTeam = new FightTeam(new HumanCommander());
playerTeam.Add(creatureFactory.CreateCreature("hero", name: playerName));
playerTeam.Add(creatureFactory.CreateCreature("fletcher", name: "Vin Fletcher"));

List<FightTeam> enemyTeams = new();

FightTeam enemy1 = new FightTeam(new MindlessAICommander());
enemy1.Add(creatureFactory.CreateCreature("skeleton", "skelly sentry"));
enemyTeams.Add(enemy1);

FightTeam enemy2 = new FightTeam(new MindlessAICommander());
enemy2.Add(creatureFactory.CreateCreature("skeleton", "skelly warrior"));
enemy2.Add(creatureFactory.CreateCreature("skeleton", "skelly soldja"));
enemyTeams.Add(enemy2);


FightTeam enemyLast = new FightTeam(new MindlessAICommander());
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