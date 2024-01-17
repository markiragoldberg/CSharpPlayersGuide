namespace The_Final_Battle
{
	public interface ISkillTargetCondition
	{
		public bool Valid(Creature user, Creature target);
	}
	public class TargetIsInjured : ISkillTargetCondition
	{
		public bool Valid(Creature user, Creature target)
		{
			return target.Health < target.MaxHealth;
		}
	}
}
