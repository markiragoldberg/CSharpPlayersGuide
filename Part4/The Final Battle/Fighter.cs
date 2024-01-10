public class Fighter(string name, int maxHealth)
{
    public string Name { get; private set; } = name;
    public int Health { get; private set; } = maxHealth;
    public int maxHealth { get; private set; } = maxHealth;
    public bool Alive { get => Health > 0; }
    public FightTeam? FightTeam { get; set; }
    public List<IFightAction> Actions { get => new(_actions); }
    private List<IFightAction> _actions = new();
    public static Fighter CreateHero(string playerName)
    {
        Fighter hero = new(playerName, 25);
        hero._actions.Add(new AttackAction("{0} used PUNCH on {1}.", 1, 1, 0));
        return hero;
    }
    public static Fighter CreateSkeleton()
    {
        Fighter skeleton = new("Skeleton", 4);
        skeleton._actions.Add(new AttackAction("{0} used BONE CRUNCH on {1}.", 0, 1, 0));
        return skeleton;
    }
    public static Fighter CreateBoss()
    {
        Fighter boss = new("Uncoded One", 25);
        boss._actions.Add(new AttackAction("{0} used UNRAVEL on {1}.", 0, 2, 0));
        return boss;
    }
    public void TakeDamage(int damage)
    {
        Health -= Math.Min(damage, Health);
    }
}
