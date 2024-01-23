namespace The_Final_Battle
{
	using Messaging;
	public class MessageEffect : ISkillEffect
	{
		public Message MessageFormat { get; }
		public void Apply(Log log, Creature user, Creature target)
		{
			log.AddMessage(String.Format(MessageFormat.Text, user.Name, target.Name), MessageFormat.Category);
		}
		public MessageEffect(string text, MessageCategory category = MessageCategory.Info)
		{
			MessageFormat = new(text, category);
		}
	}
}
