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
        while (LeftTeam.Active && RightTeam.Active)
        {
            LeftTeam.DoRound(this);
            RightTeam.DoRound(this);
        }
        winningTeam = LeftTeam.Active ? LeftTeam : RightTeam;
    }
    public FightTeam GetEnemyTeam(Fighter fighter)
    {
        if (LeftTeam.Fighters.Contains(fighter))
            return RightTeam;
        return LeftTeam;
    }
}
