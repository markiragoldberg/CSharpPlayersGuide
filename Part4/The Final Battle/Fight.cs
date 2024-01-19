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
        while (LeftTeam.CanFight && RightTeam.CanFight)
        {
            LeftTeam.DoRound(this);
            RightTeam.DoRound(this);
		}
		Display.UpdateDisplay(this);
		winningTeam = LeftTeam.CanFight ? LeftTeam : RightTeam;
    }
    public FightTeam GetAllyTeam(Creature fighter)
	{
		if (LeftTeam.Contains(fighter))
			return LeftTeam;
		else if (RightTeam.Contains(fighter))
			return RightTeam;
        throw new ArgumentException("GetAllyTeam: creature argument isn't in the fight");
	}
    public FightTeam GetEnemyTeam(Creature fighter)
    {
        if (LeftTeam.Contains(fighter))
            return RightTeam;
        else if (RightTeam.Contains(fighter))
            return LeftTeam;
        throw new ArgumentException("GetEnemyTeam: creature argument isn't in the fight");
	}
	public FightTeam GetEnemyTeam(FightTeam team)
	{
		if (team == LeftTeam)
			return RightTeam;
		else if (team == RightTeam)
			return LeftTeam;
		throw new ArgumentException("GetEnemyTeam: fightteam argument isn't in the fight");
	}
}
