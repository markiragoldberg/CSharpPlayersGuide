public class Fighter(string name, int maxHealth)
{
    public string Name { get; private set; } = name;
    public int Health { get; private set; } = maxHealth;
    public int maxHealth { get; private set; } = maxHealth;
    public List<IFightAction> Actions { get; } = new();
    public static Fighter CreateHero(string playerName)
    {
        Fighter hero = new(playerName, 25);
        hero.Actions.Add(new AttackAction("{0} used PUNCH on {1}."));
        return hero;
    }
    public static Fighter CreateSkeleton()
    {
        Fighter skeleton = new("Skeleton", 4);
        skeleton.Actions.Add(new AttackAction("{0} used BONE CRUNCH on {1}."));
        return skeleton;
    }
}
