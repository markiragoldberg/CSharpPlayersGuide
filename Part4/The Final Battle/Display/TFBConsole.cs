using ConsoleIO;
using System.Numerics;

namespace The_Final_Battle
{
	public class TFBConsole(int maxMessages = 15)
	{
		public Queue<LogMessage> log = new();
		public Queue<LogMessage> temporaryLog = new();
		public int MaxMessages { get => maxMessages; }

		public void AddMessage(string message, MessageCategory category)
		{
			temporaryLog.Clear();
			log.Enqueue(new LogMessage(message, category));
			while (log.Count > maxMessages)
				log.Dequeue();
		}
		public void AddTemporaryMessage(string message, MessageCategory category)
		{
			temporaryLog.Enqueue(new LogMessage(message, category));
		}

		public T AskForType<T>(string prompt, string? repeatPrompt = null) where T : IParsable<T>
			=> ColoredConsole.AskForType<T>(prompt, repeatPrompt);

		public T AskForMenuOption<T>(string postPrompt, IList<(string, T)> options, string? prePrompt = null, string? repeatPrompt = null)
			=> ColoredConsole.AskForMenuOption(postPrompt, options, prePrompt, repeatPrompt);

		public T AskForTypeInRange<T>(
			string prompt, T min, T max,
			ICollection<T>? excludedValues = null, string? repeatPrompt = null,
			string? outOfRangePrompt = null, string? excludedPrompt = null)
			where T : IParsable<T>, IComparisonOperators<T, T, bool>
			=> ColoredConsole.AskForTypeInRange<T>(
				prompt, min, max, excludedValues, repeatPrompt, outOfRangePrompt);

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
		public void GameWon()
		{
			ColoredConsole.WriteLine("The Uncoded One has been defeated! The Realms of C# are saved!", ConsoleColor.Green);
			Console.ReadKey(true);
		}

		public void GameLost()
		{
			ColoredConsole.WriteLine("The heroes have fallen! The Uncoded One has prevailed...", ConsoleColor.Magenta);
			Console.ReadKey(true);
		}

		private void WriteLog()
		{
			foreach (LogMessage message in log)
			{
				WriteMessage(message);
			}
			foreach (LogMessage message in temporaryLog)
			{
				WriteMessage(message);
			}
		}

		private void WriteMessage(LogMessage message)
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
