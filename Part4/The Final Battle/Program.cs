using ConsoleIO;
using The_Final_Battle;

string playerName = ColoredConsole.AskForType<string>("What is your name? ");
ColoredConsole.WriteLine($"Hello, {playerName}!",ConsoleColor.Cyan);

FightTeam playerTeam = new FightTeam(new HumanCommander());
playerTeam.AddFighter(Fighter.CreateHero(playerName, playerTeam));
playerTeam.AddFighter(Fighter.CreateFletcher(playerTeam));

List<Fight> fights = new();
TFBConsole display = new();

FightTeam enemy1 = new FightTeam(new MindlessAICommander());
enemy1.AddFighter(Fighter.CreateSkeleton());
fights.Add(new(playerTeam, enemy1, display));

FightTeam enemy2 = new FightTeam(new MindlessAICommander());
enemy2.AddFighter(Fighter.CreateSkeleton());
enemy2.AddFighter(Fighter.CreateSkeleton());
fights.Add(new(playerTeam, enemy2, display));


FightTeam enemyLast = new FightTeam(new MindlessAICommander());
enemyLast.AddFighter(Fighter.CreateBoss());
fights.Add(new(playerTeam, enemyLast, display));

int f = 0;
while (f < fights.Count)
{
    fights[f].Resolve(out FightTeam winningTeam);
    if(winningTeam != playerTeam)
    {
        display.GameLost();
        return;
    }
    f++;
}
display.GameWon();