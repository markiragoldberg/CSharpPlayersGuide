using ConsoleIO;
using The_Final_Battle;

public interface IFightAction
{
    string Name { get; }
    void Resolve(Fighter user, Fighter target, Fight fight);
}

public class DoNothingFightAction : IFightAction
{
    public readonly string messageFormat = "{0} does nothing.";
    public string Name { get =>  "NOTHING"; }

    public void Resolve(Fighter user, Fighter target, Fight fight)
    {
        ColoredConsole.WriteLine(
            String.Format(messageFormat, user.Name), ConsoleColor.Yellow);
        Thread.Sleep(500);
    }
}

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
        ColoredConsole.WriteLine(
            String.Format(messageFormat, user.Name, target.Name), ConsoleColor.Yellow);
        int damage = RNG.Roll(MinDamage, MaxDamage, BonusDamage);
        target.TakeDamage(damage);
        ColoredConsole.WriteLine($"{target.Name} was hit for {damage} damage.", ConsoleColor.Red);
        ColoredConsole.WriteLine($"{target.Name} is now at {target.Health}/{target.MaxHealth} HP.", ConsoleColor.Magenta);
        target.FightTeam?.RemoveDead();
        Thread.Sleep(500);
    }
}