namespace The_Final_Battle
{
	public class AttackEffect : ISkillEffect
	{
		public Dice DamageDice { get; }
		public double HitChance { get; }
		public string HitMessageFormat { get; }
		public string MissMessageFormat { get; }
		public List<ISkillEffect>? EffectsOnHit { get; }
		public List<ISkillEffect>? EffectsOnMiss { get; }
		public void Apply(TFBConsole display, Creature user, Creature target)
		{
			if (RNG.PercentChance(HitChance))
			{
				int damage = DamageDice.Roll();
				// ... implement damage modifiers here ...
				target.AdjustHealth(damage);
				display.AddMessage(
					String.Format(HitMessageFormat, user.Name, target.Name, damage), MessageCategory.Warning);
				if (EffectsOnHit != null)
					foreach (var effect in EffectsOnHit)
						effect.Apply(display, user, target);
			}
			else
			{
				display.AddMessage(
					String.Format(MissMessageFormat, user.Name, target.Name), MessageCategory.Info);
				if (EffectsOnMiss != null)
					foreach (var effect in EffectsOnMiss)
						effect.Apply(display, user, target);
			}
		}
		public AttackEffect(Dice damageDice, double hitChance,
			List<ISkillEffect>? effectsOnHit, List<ISkillEffect>? effectsOnMiss,
			string? hitMessageFormat = null, string? missMessageFormat = null)
		{
			DamageDice = damageDice;
			HitChance = hitChance;
			EffectsOnHit = effectsOnHit;
			EffectsOnMiss = effectsOnMiss;
			HitMessageFormat = hitMessageFormat ?? "{0} hit {1} for {2} damage.";
			MissMessageFormat = missMessageFormat ?? "{0} missed {1}.";
		}
	}
}
