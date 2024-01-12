namespace The_Final_Battle;
public class AttackAction(
    string name, string messageFormat, 
    int minDamage, int maxDamage, int bonusDamage) : IFightAction
{
    public string Name { get => name; }
    public int MinDamage { get; } = minDamage;
    public int MaxDamage { get; } = maxDamage;
    public int BonusDamage { get; } = bonusDamage;

    public void Resolve(Fighter user, Fighter target, Fight fight)
    {
        fight.Display.AddMessage(
            String.Format(messageFormat, user.Name, target.Name), MessageCategory.Warning);
        int damage = RNG.Roll(MinDamage, MaxDamage, BonusDamage);
        target.TakeDamage(damage);
        fight.Display.AddMessage(
            $"{target.Name} was hit for {damage} damage.", MessageCategory.Warning);
        fight.Display.AddMessage(
            $"{target.Name} is now at {target.Health}/{target.MaxHealth} HP.", MessageCategory.Warning);
        target.FightTeam?.RemoveDead(fight);
        //Thread.Sleep(500);
    }
}