using The_Final_Battle;

public class Creature
{
	public string Name { get; private set; }
	public int Health { get; private set; }
	public int MaxHealth { get; private set; }
	public bool Alive { get => Health > 0; }
	public SkillTracker Skills { get; }

	public void AdjustHealth(int damage)
	{
		Health -= damage;
		Health = Math.Clamp(Health, 0, MaxHealth);
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
}
