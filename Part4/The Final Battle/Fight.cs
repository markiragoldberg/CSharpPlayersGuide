using ConsoleIO;
using The_Final_Battle;

public class Fight
{
    public FightTeam LeftTeam { get; }
    public FightTeam RightTeam { get; }
    public TFBConsole Display { get; }
    public Fight(FightTeam leftTeam, FightTeam rightTeam, TFBConsole display)
    {
        LeftTeam = leftTeam;
        RightTeam = rightTeam;
        Display = display;
    }
    public void Resolve(out FightTeam winningTeam)
    {
        while (LeftTeam.HasAliveFighters && RightTeam.HasAliveFighters)
        {
            LeftTeam.DoRound(this);
            RightTeam.DoRound(this);
		}
		Display.UpdateDisplay(this);
		winningTeam = LeftTeam.HasAliveFighters ? LeftTeam : RightTeam;
    }
    public FightTeam GetEnemyTeam(Fighter fighter)
    {
        if (LeftTeam.Contains(fighter))
            return RightTeam;
        return LeftTeam;
    }
}
