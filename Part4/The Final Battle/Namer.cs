namespace The_Final_Battle
{
	public static class Namer
	{
		private static List<string> _names = ["Alice", "Bob"];
		public static string Random()
		{
			return _names.RandomElement();
		}
	}
}
