namespace The_Final_Battle.Messaging
{
	public class Message(string text, MessageCategory category)
	{
		public string Text { get => text; }
		public MessageCategory Category { get => category; }
	}
	public enum MessageCategory
	{
		Good,
		VeryGood,
		Bad,
		VeryBad,
		Prompt,
		Warning,
		Info
	}
}
