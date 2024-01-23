using ConsoleIO;
using The_Final_Battle.Messaging;

namespace The_Final_Battle
{
	public static class FightScreen
	{
		public static void Display(Fight fight, Log log)
		{
			FightTeam left = fight.LeftTeam;
			FightTeam right = fight.RightTeam;
			Console.Clear();
			Console.WriteLine("==================================== BATTLE ====================================");
			for (int i = 0; i < left.Count || i < right.Count; ++i)
			{
				if (i < left.Count)
				{
					ColoredConsole.Write($"{left[i].Name,-25}", ConsoleColor.Cyan);
					string healthString = $"({left[i].Health}/{left[i].MaxHealth}) HP";
					ColoredConsole.Write($"{healthString,-15}", ConsoleColor.Green);
				}
				else
					// if right team is larger ensure they're written on left side
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
		}
	}
}
