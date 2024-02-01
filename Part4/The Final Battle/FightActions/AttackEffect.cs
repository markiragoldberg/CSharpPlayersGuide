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
		public double AverageResult => HitChance * DamageDice.AverageResult;

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
		public void Apply(Messaging.Log log, Creature user, Creature target)
		{
			if (RNG.PercentChance(HitChance))
			{
				int damage = DamageDice.Roll();
				// ... implement damage modifiers here ...
				target.AdjustHealth(damage);
				// todo: invert MessageCategory based on team
				log.AddMessage(String.Format(HitMessageFormat, user.Name, target.Name, damage),
					           Messaging.MessageCategory.Warning);
				if (EffectsOnHit != null)
					foreach (var effect in EffectsOnHit)
						effect.Apply(log, user, target);
			}
			else
			{
				log.AddMessage(String.Format(MissMessageFormat, user.Name, target.Name),
							   Messaging.MessageCategory.Info);
				if (EffectsOnMiss != null)
					foreach (var effect in EffectsOnMiss)
						effect.Apply(log, user, target);
			}
		}
	}
}
