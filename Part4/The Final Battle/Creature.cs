using The_Final_Battle;
using The_Final_Battle.Defs;
using The_Final_Battle.Items;

public class Creature
{
	public string Name { get; private set; }
	public int Health { get; private set; }
	public int MaxHealth { get; private set; }
	public bool Alive { get => Health > 0; }
	public SkillTracker Skills { get; }
	public Weapon? Weapon { get; set; }
	public List<IFightAction> Actions
	{
		get
		{
			List<IFightAction> actions = new();
			if (Weapon?.Actions != null)
				actions.AddRange(Weapon.Actions);
			actions.AddRange(Skills.Actions);
			return actions;
		}
	}

	public void AdjustHealth(int damage)
	{
		Health -= damage;
		Health = Math.Clamp(Health, 0, MaxHealth);
	}
	public Creature(CreatureDef def, Weapon? weapon = null, string? name = null)
	{
		Name = name ?? def.Label;
		Weapon = weapon;
		Health = MaxHealth = def.MaxHealth;
		Skills = new();
		foreach (AbilityDef skillDef in def.StartingSkills)
		{
			Skills.AddSkill(skillDef);
		}
	}
}
