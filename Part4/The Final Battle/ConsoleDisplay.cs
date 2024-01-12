using ConsoleIO;

namespace The_Final_Battle
{
	public class ConsoleDisplay(int maxMessages = 15)
	{
		public Queue<LogMessage> log = new();
		public int MaxMessages { get => maxMessages; }

		public void AddMessage(string message, MessageCategory category)
		{
			log.Enqueue(new LogMessage(message, category));
			while (log.Count > maxMessages)
				log.Dequeue();
		}

		public void UpdateDisplay(Fight fight)
		{
			Console.Clear();
			Console.WriteLine("==================================== BATTLE ====================================");
			FightTeam left = fight.LeftTeam;
			FightTeam right = fight.RightTeam;
			for (int i = 0; i < left.Count || i < right.Count; ++i)
			{
				if (i < left.Count)
				{
					ColoredConsole.Write($"{left[i].Name,-25}", ConsoleColor.Cyan);
					string healthString = $"({left[i].Health}/{left[i].MaxHealth}) HP";
					ColoredConsole.Write($"{healthString,-15}", ConsoleColor.Green);
				}
				else
					Console.Write($"{"",40}");
				if (i < right.Count)
				{
					ColoredConsole.Write($"{right[i].Name,25}", ConsoleColor.Red);
					string healthString = $"({right[i].Health}/{right[i].MaxHealth}) HP";
					ColoredConsole.Write($"{healthString,15}", ConsoleColor.Magenta);
				}
				Console.WriteLine();
			}
			Console.WriteLine("================================================================================");
			WriteLog();
		}

		private void WriteLog()
		{
			foreach (LogMessage message in log)
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
	public class LogMessage(string text, MessageCategory category)
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
