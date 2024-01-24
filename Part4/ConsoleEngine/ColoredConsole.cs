using System.Numerics;

namespace ConsoleIO
{
	public class ColoredConsole
	{
		public static T AskForType<T>(string prompt, string? repeatPrompt = null) where T : IParsable<T>
		{
			Console.Write(prompt);
			string? input;
			while (true)
			{
				input = ReadLine();
				if (T.TryParse(input, null, out T? result))
					return result;
				else
					Console.Write(repeatPrompt ?? "Try again: ");
			}
		}
		public static string Prompt(string question)
		{
			return AskForType<string>(question);
		}
		public static T AskForTypeInRange<T>(
			string prompt, T min, T max,
			ICollection<T>? excludedValues = null, string? repeatPrompt = null,
			string? outOfRangePrompt = null, string? excludedPrompt = null)
			where T : IParsable<T>, IComparisonOperators<T, T, bool>
		{
			Console.Write(prompt);
			while (true)
			{
				string? input = ReadLine();
				if (T.TryParse(input, null, out T? result))
				{
					if (min <= result && result <= max)
					{
						if (excludedValues == null || !excludedValues.Contains(result))
							return result;
						else
							Console.Write(excludedPrompt ?? "That is excluded from the range. Try again: ");
					}
					else
						Console.Write(outOfRangePrompt ?? "That is outside the range. Try again: ");
				}
				else
					Console.Write(repeatPrompt ?? "Try again: ");
			}
		}
		public static string AskForOption(string prompt, ICollection<string> options,
			string? repeatPrompt = null, bool caseSensitive = false)
		{
			if (options.Count == 0)
				throw new ArgumentException("options cannot be empty");
			Console.Write(prompt);
			string? input;
			while (true)
			{
				input = ReadLine();
				if (input != null && options.Contains(input,
					caseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase))
				{
					return caseSensitive ? input : input.ToLower();
				}
				Console.Write(repeatPrompt ?? "Try again: ");
			}
		}
		public static T AskForMenuOption<T>(string postPrompt, IList<(string text, T value)> options, string? prePrompt = null, string? repeatPrompt = null)
		{
			if (options.Count == 0)
				throw new ArgumentException("options cannot be empty");
			if (prePrompt != null)
				Console.WriteLine(prePrompt);
			for (int i = 0; i < options.Count; i++)
			{
				Console.WriteLine($"{i + 1} - {options[i].text}");
			}
			Console.Write(postPrompt);
			while (true)
			{
				var input = Console.ReadKey(true).KeyChar.ToString();
				if (int.TryParse(input, null, out int result) && result >= 1 && result <= options.Count)
				{
					return options[result - 1].value;
				}
				else if (repeatPrompt != null)
					Console.WriteLine(repeatPrompt);
			}
		}

		public static T? AskForMenuOptionAbortable<T>(string postPrompt, IList<(string text, T value)> options, string? prePrompt = null, string? repeatPrompt = null)
		{
			if (options.Count == 0)
				throw new ArgumentException("options cannot be empty");
			if (prePrompt != null)
				Console.WriteLine(prePrompt);
			for (int i = 0; i < options.Count; i++)
			{
				Console.WriteLine($"{i + 1} - {options[i].text}");
			}
			Console.Write(postPrompt);
			while (true)
			{
				var input = Console.ReadKey(true);
				if (input.Key == ConsoleKey.Escape)
					return default;
				else if (int.TryParse(input.KeyChar.ToString(), null, out int result) && result >= 1 && result <= options.Count)
				{
					return options[result - 1].value;
				}
				else if (repeatPrompt != null)
					Console.WriteLine(repeatPrompt);
			}
		}

		public static bool AskToConfirm(string prompt, string? repeatPrompt = null)
		{
			string yesNo = AskForOption(prompt, ["yes", "no"], repeatPrompt);
			return yesNo.Equals("yes");
		}
		public static bool AskToConfirmWithDefault(string prompt, bool defaultResult)
		{
			Console.Write(prompt);
			string? yesNo = ReadLine();
			if (yesNo == null)
				return defaultResult;
			if (defaultResult == false && yesNo.Equals("yes", StringComparison.OrdinalIgnoreCase))
				return true;
			else if (defaultResult == true && yesNo.Equals("no", StringComparison.OrdinalIgnoreCase))
				return false;
			return defaultResult;
		}
		public static void Write(string output, ConsoleColor color)
		{
			ConsoleColor oldColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.Write(output);
			Console.ForegroundColor = oldColor;
		}
		public static void WriteLine(string output, ConsoleColor color)
		{
			Write(output, color);
			Console.WriteLine();
		}
		private static string? ReadLine(ConsoleColor color = ConsoleColor.Cyan)
		{
			ConsoleColor oldColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			string? result = Console.ReadLine();
			Console.ForegroundColor = oldColor;
			return result;
		}
	}
}
