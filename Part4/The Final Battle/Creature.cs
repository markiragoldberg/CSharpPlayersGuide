using The_Final_Battle;

public class Creature
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public bool Alive { get => Health > 0; }
    public FightTeam? FightTeam { get; set; } = null;
    public SkillTracker Skills { get; }
    public void TakeDamage(int damage)
    {
        Health -= Math.Min(damage, Health);
    }

    public Creature(CreatureDef def, string? name = null)
    {
        Name = name ?? def.DefaultName ?? Namer.Random();
		Health = MaxHealth = def.MaxHealth;
        Skills = new();
        foreach (SkillDef skillDef in def.StartingSkills)
        {
            Skills.AddSkill(skillDef);
        }
	}
	public FightTeam? GetEnemyTeam()
    {
        return FightTeam?.GetEnemyTeam();
    }
}
