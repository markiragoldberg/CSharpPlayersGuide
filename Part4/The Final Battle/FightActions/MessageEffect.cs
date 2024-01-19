namespace The_Final_Battle
{
	public class MessageEffect : ISkillEffect
	{
		public LogMessage MessageFormat { get; }
		public void Apply(TFBConsole display, Creature user, Creature target)
		{
			display.AddMessage(String.Format(MessageFormat.Text, user.Name, target.Name), MessageFormat.Category);
		}
		public MessageEffect(string text, MessageCategory category = MessageCategory.Info)
		{
			MessageFormat = new(text, category);
		}
	}
}
