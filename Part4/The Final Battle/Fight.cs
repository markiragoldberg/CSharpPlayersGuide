using ConsoleIO;
using System.Xml.Linq;

public class Fight
{
    public FightTeam LeftTeam { get; }
    public FightTeam RightTeam { get; }
    public Fight(FightTeam leftTeam, FightTeam rightTeam)
    {
        LeftTeam = leftTeam;
        RightTeam = rightTeam;
    }
    public void Resolve(out FightTeam winningTeam)
    {
        while (LeftTeam.HasAliveFighters && RightTeam.HasAliveFighters)
        {
            LeftTeam.DoRound(this);
            RightTeam.DoRound(this);
        }
        winningTeam = LeftTeam.HasAliveFighters ? LeftTeam : RightTeam;
    }
    public FightTeam GetEnemyTeam(Fighter fighter)
    {
        if (LeftTeam.Contains(fighter))
            return RightTeam;
        return LeftTeam;
    }
    public void WriteStatus()
    {
        Console.Clear();
        Console.WriteLine("==================================== BATTLE ====================================");
        for(int i = 0; i < LeftTeam.Count || i < RightTeam.Count; ++i)
        {
            if (i < LeftTeam.Count)
            {
                ColoredConsole.Write($"{LeftTeam[i].Name,-25}", ConsoleColor.Cyan);
                string healthString = $"({LeftTeam[i].Health}/{LeftTeam[i].MaxHealth}) HP";
                ColoredConsole.Write($"{healthString,15}", ConsoleColor.Green);
            }
            else
                Console.Write($"{"",40}");
            if (i < RightTeam.Count)
            {
                ColoredConsole.Write($"{RightTeam[i].Name,25}", ConsoleColor.Red);
                string healthString = $"({RightTeam[i].Health}/{RightTeam[i].MaxHealth}) HP";
                ColoredConsole.Write($"{healthString,15}", ConsoleColor.Magenta);
            }
            Console.WriteLine();
        }
        Console.WriteLine("================================================================================");
    }
}
