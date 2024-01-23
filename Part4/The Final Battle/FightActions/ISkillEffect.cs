namespace The_Final_Battle
{
	public interface ISkillEffect
	{
		void Apply(Messaging.Log log, Creature user, Creature target);
	}
}
