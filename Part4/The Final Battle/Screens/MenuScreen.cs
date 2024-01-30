using ConsoleIO;

namespace The_Final_Battle.Screens
{
	public static class MenuScreen
	{
		public static void Display(IList<(string text, Action whenPicked)> options,
			string? header = null, string? footer = null, bool abortable = false)
		{
			if (header != null) // do not add line if no header
				Console.WriteLine(header);
			for (int i = 0; i < options.Count; i++)
			{
				Console.WriteLine($"{i+1} - {options[i].text}");
			}
			while (true)
			{
				// todo: implement 0-9 a-z input stitching
				if (options.Count > 9)
					throw new NotImplementedException("MenuScreen can't handle >9 options yet!");

				var input = Console.ReadKey();
				if (int.TryParse(input.ToString(), null, out int result))
				{
					if (1 <= result && result <= options.Count)
					{
						options[result - 1].whenPicked();
						return;
					}
				}
				else if (abortable && input.Key == ConsoleKey.Escape)
					return;
			}
		}
	}
}
