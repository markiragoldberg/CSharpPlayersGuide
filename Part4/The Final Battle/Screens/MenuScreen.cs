using ConsoleIO;

namespace The_Final_Battle.Screens
{
	public static class MenuScreen
	{
		public static void Display(IList<MenuOption> options,
			string? header = null, string? footer = null, bool abortable = false)
		{
			if (header != null) // do not add line if no header
				Console.WriteLine(header);
			for (int i = 0; i < options.Count; i++)
			{
				ColoredConsole.WriteLine($"{i+1} - {options[i].Text}", options[i].Color);
			}
			while (true)
			{
				// todo: implement 0-9 a-z input stitching
				if (options.Count > 9)
					throw new NotImplementedException("MenuScreen can't handle >9 options yet!");

				var input = Console.ReadKey(true);
				if (int.TryParse(input.KeyChar.ToString(), null, out int result))
				{
					if (1 <= result && result <= options.Count && options[result - 1].Enabled)
					{
						options[result - 1].OnPicked();
						return;
					}
				}
				else if (abortable && input.Key == ConsoleKey.Escape)
					return;
			}
		}
	}

	public class MenuOption
	{
		public Action OnPicked { get; }
		public string Text { get; set; }
		public bool Enabled { get; set; }
		public ConsoleColor Color => Enabled ? _color : ConsoleColor.DarkGray;
		private ConsoleColor _color;

		public MenuOption(string text, Action onPicked,
			bool enabled = true, ConsoleColor color = ConsoleColor.Gray)
		{
			OnPicked = onPicked;
			Text = text;
			Enabled = enabled;
			_color = color;
		}
	}
}
