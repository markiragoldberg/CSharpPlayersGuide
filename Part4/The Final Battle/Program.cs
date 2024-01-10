using ConsoleIO;
using The_Final_Battle;

string playerName = ColoredConsole.AskForType<string>("What is your name? ");
ColoredConsole.WriteLine($"Hello, {playerName}!",ConsoleColor.Cyan);

List<Fighter> playerFighters = new List<Fighter>();
playerFighters.Add(Fighter.CreateHero(playerName));
FightTeam playerTeam = new FightTeam(playerFighters, new HumanCommander());

List<Fight> fights = new();
fights.Add(new(playerTeam, new([Fighter.CreateSkeleton()], new MindlessAICommander())));
fights.Add(new(playerTeam, new([Fighter.CreateSkeleton(), Fighter.CreateSkeleton()], new MindlessAICommander())));
fights.Add(new(playerTeam, new([Fighter.CreateBoss()], new MindlessAICommander())));

int f = 0;
while (f < fights.Count)
{
    fights[f].Resolve(out FightTeam winningTeam);
    if(winningTeam != playerTeam)
    {
        ColoredConsole.WriteLine("The heroes have fallen! The Uncoded One has prevailed...", ConsoleColor.Magenta);
        Console.ReadKey(true);
        return;
    }
    f++;
}
ColoredConsole.WriteLine("The Uncoded One has been defeated! The Realms of C# are saved!", ConsoleColor.Green);
Console.ReadKey(true);
