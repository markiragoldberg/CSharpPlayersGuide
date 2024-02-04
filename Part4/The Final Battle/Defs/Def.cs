namespace The_Final_Battle.Defs
{
	public abstract class Def
	{
		public string DefName { get; }
		public abstract string? Label { get; }

		public Def(string defName)
		{
			DefName = defName;
		}
	}
}
