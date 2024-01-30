namespace The_Final_Battle
{
	public class AbilityDef
	{
		public string DefName { get; }
		public SkillTarget Target { get; }
		public List<ISkillEffect> Effects { get; }
		public bool IsAttack => Effects.Any<ISkillEffect>(e => e is AttackEffect);

		public static AbilityDef DoNothing { get; } = 
			new("doNothing", new MessageEffect("{0} does nothing.", Messaging.MessageCategory.Info), 
				SkillTarget.SelfUnconditional);

		public AbilityDef(string defName, List<ISkillEffect> effects, SkillTarget? target = null)
		{
			DefName = defName;
			Effects = effects;
			Target = target ?? SkillTarget.EnemyUnconditional;
		}
		public AbilityDef(string defName, ISkillEffect onlyEffect, SkillTarget? target = null)
		{
			DefName = defName;
			Effects = [onlyEffect];
			Target = target ?? SkillTarget.EnemyUnconditional;
		}
		public bool Usable(Fight fight, Creature user)
		{
			if (Target.TargetType == TargetType.Self)
				return Target.Valid(fight, user, user);
			else if (Target.TargetType == TargetType.Enemy || Target.TargetType == TargetType.Ally)
			{
				FightTeam? targetTeam = Target.TargetType == TargetType.Enemy ? fight.GetEnemyTeam(user) : fight.GetAllyTeam(user);
				if (targetTeam != null)
				{
					return targetTeam.Fighters.Any<Creature>(c => Target.Valid(fight, user, c));
				}
			}
			throw new NotImplementedException("Unrecognized TargetType in SkillDef.Usable");
		}
	}
}
