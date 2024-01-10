public class Fighter(string name, int maxHealth, FightTeam? fightTeam = null)
{
    public string Name { get; private set; } = name;
    public int Health { get; private set; } = maxHealth;
    public int MaxHealth { get; private set; } = maxHealth;
    public bool Alive { get => Health > 0; }
    public FightTeam? FightTeam { get; set; } = fightTeam;
    public List<IFightAction> Actions { get => new(_actions); }
    private List<IFightAction> _actions = new();
    public static Fighter CreateHero(string playerName, FightTeam? fightTeam = null)
    {
        Fighter hero = new(playerName, 25, fightTeam);
        hero._actions.Add(new AttackAction("PUNCH", "{0} used PUNCH on {1}.", 1, 1, 0));
        return hero;
    }
    public static Fighter CreateSkeleton(FightTeam? fightTeam = null)
    {
        Fighter skeleton = new("Skeleton", 4, fightTeam);
        skeleton._actions.Add(new AttackAction("BONE CRUNCH", "{0} used BONE CRUNCH on {1}.", 0, 1, 0));
        return skeleton;
    }
    public static Fighter CreateBoss(FightTeam? fightTeam = null)
    {
        Fighter boss = new("Uncoded One", 25, fightTeam);
        boss._actions.Add(new AttackAction("UNRAVELLING", "{0} used UNRAVELLING on {1}.", 0, 2, 0));
        return boss;
    }
    public void TakeDamage(int damage)
    {
        Health -= Math.Min(damage, Health);
    }
}
