using static System.Net.Mime.MediaTypeNames;

namespace The_Final_Battle
{
	public class SkillDef
	{
		public string DefName { get; }
		public SkillTarget Target { get; }
		public List<ISkillEffect> Effects { get; }
		public static SkillDef DoNothing { get; } = new("doNothing", new MessageEffect("{0} does nothing.", MessageCategory.Info), SkillTarget.SelfUnconditional);
		public SkillDef(string defName, List<ISkillEffect> effects, SkillTarget? target = null)
		{
			DefName = defName;
			Effects = effects;
			Target = target ?? SkillTarget.EnemyUnconditional;
		}
		public SkillDef(string defName, ISkillEffect onlyEffect, SkillTarget? target = null)
		{
			DefName = defName;
			Effects = [onlyEffect];
			Target = target ?? SkillTarget.EnemyUnconditional;
		}
		public bool IsAttack => Effects.Any<ISkillEffect>(e => e is AttackEffect);
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
