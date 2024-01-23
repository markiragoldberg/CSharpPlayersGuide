namespace The_Final_Battle.Messaging
{
	public class Message(string text, MessageCategory category)
	{
		public string Text { get => text; }
		public MessageCategory Category { get => category; }

		public static void InvertCategory(ref MessageCategory category)
		{
			category = category switch
			{
				MessageCategory.Good => MessageCategory.Bad,
				MessageCategory.Bad => MessageCategory.Good,
				MessageCategory.VeryGood => MessageCategory.VeryBad,
				MessageCategory.VeryBad => MessageCategory.VeryGood,
				_ => category
			};
		}
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
