using ConsoleIO;
using The_Final_Battle.Items;

namespace The_Final_Battle.Screens
{
	public static class LootScreen
	{
		public static void Display(Inventory loot)
		{
			ColoredConsole.WriteLine("You recover: ", ConsoleColor.Gray);
			foreach (var item in loot.Items)
			{
				ColoredConsole.WriteLine(item.Label, ConsoleColor.Cyan);
			}
			Console.ReadKey(true);
		}
	}
}
