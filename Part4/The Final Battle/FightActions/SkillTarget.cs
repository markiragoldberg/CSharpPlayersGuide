namespace The_Final_Battle
{
	public class SkillTarget
	{
		public static SkillTarget SelfUnconditional { get; } = new SkillTarget(TargetType.Self);
		public static SkillTarget AllyUnconditional { get; } = new SkillTarget(TargetType.Ally);
		public static SkillTarget EnemyUnconditional { get; } = new SkillTarget(TargetType.Enemy);
		public TargetType TargetType { get; }
		public List<ISkillTargetCondition> Conditions { get; } = new();
		public SkillTarget(TargetType targetType)
		{
			TargetType = targetType;
		}

		public bool Valid(Creature user, Creature target)
		{
			switch (TargetType)
			{
				case TargetType.Self:
					if (user != target)return false;
					break;
				case TargetType.Ally:
					if (user.FightTeam != target.FightTeam) return false;
					break;
				case TargetType.Enemy:
					if (user.FightTeam == target.FightTeam) return false;
					break;
			}
			foreach(var condition in Conditions)
			{
				if (!condition.Valid(user, target))
					return false;
			}
			return true;
		}
	}
	public enum TargetType { Self, Ally, Enemy }
}
