using ConsoleIO;
using The_Final_Battle.Messaging;

namespace The_Final_Battle.Screens
{
	public static class MessagesScreen
	{
		public static void WriteRecentMessages(Log log, int messageCount = 15)
		{
			IEnumerable<Message> messages = log.MostRecent(messageCount);
			foreach (Message message in messages)
			{
				WriteMessage(message);
			}
		}

		private static void WriteMessage(Messaging.Message message)
		{
			ColoredConsole.WriteLine(message.Text, message.Category switch
			{
				MessageCategory.Good => ConsoleColor.Green,
				MessageCategory.VeryGood => ConsoleColor.Cyan,
				MessageCategory.Bad => ConsoleColor.Red,
				MessageCategory.VeryBad => ConsoleColor.Magenta,
				MessageCategory.Prompt => ConsoleColor.White,
				MessageCategory.Warning => ConsoleColor.Yellow,
				MessageCategory.Info => ConsoleColor.Gray,
				_ => ConsoleColor.Gray
			});
		}
	}
}
