using ConsoleIO;

string playerName = ColoredConsole.AskForType<string>("What is your name? ");
ColoredConsole.WriteLine($"Hello, {playerName}!",ConsoleColor.Cyan);

List<Fighter> playerFighters = new List<Fighter>();
List<Fighter> enemyFighters = new List<Fighter>();
playerFighters.Add(Fighter.CreateHero(playerName));
enemyFighters.Add(Fighter.CreateSkeleton());
FightTeam playerTeam = new FightTeam(playerFighters);
FightTeam enemyTeam = new FightTeam(enemyFighters);
Fight firstFight = new Fight(playerTeam, enemyTeam);

firstFight.Resolve(out FightTeam winningTeam);
if (winningTeam == playerTeam)
{
    Console.WriteLine("player team won game");
}
else
{
    Console.WriteLine("enemy team won game");
}
