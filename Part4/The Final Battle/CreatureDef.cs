using System.Xml.Linq;

namespace The_Final_Battle
{
	public class CreatureDef
	{
		public string DefName { get; }
		public string? DefaultName { get; }
		public int MaxHealth { get; }
		public HashSet<SkillDef> StartingSkills { get; }
		public CreatureDef(string defName, int maxHealth, string? defaultName, params SkillDef[] startingSkills)
		{
			DefName = defName;
			MaxHealth = maxHealth;
			StartingSkills = [.. startingSkills];
			DefaultName = defaultName;
		}
	}
}
