using The_Final_Battle.Screens;

namespace The_Final_Battle;
public class Fight
{
    public FightTeam LeftTeam { get; }
    public FightTeam RightTeam { get; }
    public Fight(FightTeam leftTeam, FightTeam rightTeam)
    {
        LeftTeam = leftTeam;
        RightTeam = rightTeam;
    }
    public void Resolve(out FightTeam winningTeam, Messaging.Log log)
    {
        while (LeftTeam.CanFight && RightTeam.CanFight)
        {
            LeftTeam.DoRound(this, log);
            RightTeam.DoRound(this, log);
		}
        // todo: review when/where screen display calls should be made
		FightScreen.Display(this);
		MessagesScreen.Display(log);
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
