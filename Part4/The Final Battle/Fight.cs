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
        LeftTeam.Fight = this;
        RightTeam.Fight = this;
    }
    public void Resolve(out FightTeam winningTeam)
    {
        while (LeftTeam.HasAliveFighters && RightTeam.HasAliveFighters)
        {
            LeftTeam.DoRound();
            RightTeam.DoRound();
		}
		Display.UpdateDisplay(this);
		winningTeam = LeftTeam.HasAliveFighters ? LeftTeam : RightTeam;
    }
    public FightTeam? GetEnemyTeam(Creature fighter)
    {
        if (fighter.FightTeam == LeftTeam)
            return RightTeam;
        else if (fighter.FightTeam == RightTeam)
            return LeftTeam;
        return null;
	}
	public FightTeam? GetEnemyTeam(FightTeam team)
	{
		if (team == LeftTeam)
			return RightTeam;
		else if (team == RightTeam)
			return LeftTeam;
		return null;
	}
}
