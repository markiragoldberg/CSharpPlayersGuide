namespace The_Final_Battle
{
	public class SkillDef
	{
		public string DefName { get; }
		public Dice DamageDice { get; }
		public double HitChance { get; }
		public string? MessageFormat { get; }
		public SkillTarget Target { get; }
		//public List<ISkillEffect> Effects { get; }
		public SkillDef(string defName, Dice damageDice, double hitChance, string? messageFormat = null, SkillTarget? target = null)
		{
			DefName = defName;
			DamageDice = damageDice;
			HitChance = hitChance;
			MessageFormat = messageFormat;
			Target = target ?? SkillTarget.EnemyUnconditional;
		}
		public bool Usable(Creature user)
		{
			if (Target.TargetType == TargetType.Self)
				return Target.Valid(user, user);
			else if (Target.TargetType == TargetType.Enemy || Target.TargetType == TargetType.Ally)
			{
				FightTeam? targetTeam = Target.TargetType == TargetType.Enemy ? user.GetEnemyTeam() : user.FightTeam;
				if (targetTeam != null)
				{
					return targetTeam.Fighters.Any<Creature>(c => Target.Valid(user, c));
				}
			}
			throw new NotImplementedException("Unrecognized TargetType in SkillDef.Usable");
		}
	}
	public interface ISkillEffect
	{
		void Apply(Creature user, Creature target);
	}

	public class AttackEffect : ISkillEffect
	{
		public Dice DamageDice { get; }
		public double HitChance { get; }
		public List<ISkillEffect>? EffectsOnHit { get; }
		public List<ISkillEffect>? EffectsOnMiss { get; }
		public void Apply(Creature user, Creature target)
		{
			if (RNG.PercentChance(HitChance))
			{
				int damage = DamageDice.Roll();
				// ... implement damage modifiers here ...
				target.TakeDamage(damage);
				if (EffectsOnHit != null)
					foreach (var effect in EffectsOnHit)
						effect.Apply(user, target);
			}
			else
				if (EffectsOnMiss != null)
				foreach (var effect in EffectsOnMiss)
					effect.Apply(user, target);
		}
		public AttackEffect(Dice damageDice, double hitChance, List<ISkillEffect>? effectsOnHit, List<ISkillEffect>? effectsOnMiss)
		{
			DamageDice = damageDice;
			HitChance = hitChance;
			EffectsOnHit = effectsOnHit;
			EffectsOnMiss = effectsOnMiss;
		}
	}
}
