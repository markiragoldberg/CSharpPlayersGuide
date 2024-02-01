using The_Final_Battle.World;

namespace The_Final_Battle.Screens
{
	public static class LocationScreen
	{
		public static void Display(Vertex here)
		{
			Console.Clear();
			Console.WriteLine(here.Interior.Name);
			Console.WriteLine();
			Console.WriteLine(here.Interior.Description);
		}
	}
}
