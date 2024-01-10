﻿using ConsoleIO;
using The_Final_Battle;

string playerName = ColoredConsole.AskForType<string>("What is your name? ");
ColoredConsole.WriteLine($"Hello, {playerName}!",ConsoleColor.Cyan);

FightTeam playerTeam = new FightTeam(new HumanCommander());
playerTeam.AddFighter(Fighter.CreateHero(playerName, playerTeam));

List<Fight> fights = new();

FightTeam enemy1 = new FightTeam(new MindlessAICommander());
enemy1.AddFighter(Fighter.CreateSkeleton());
fights.Add(new(playerTeam, enemy1));

FightTeam enemy2 = new FightTeam(new MindlessAICommander());
enemy2.AddFighter(Fighter.CreateSkeleton());
enemy2.AddFighter(Fighter.CreateSkeleton());
fights.Add(new(playerTeam, enemy2));


FightTeam enemyLast = new FightTeam(new MindlessAICommander());
enemyLast.AddFighter(Fighter.CreateBoss());
fights.Add(new(playerTeam, enemyLast));

fights.Add(new(playerTeam, enemy1));
fights.Add(new(playerTeam, enemy2));
fights.Add(new(playerTeam, enemyLast));

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
