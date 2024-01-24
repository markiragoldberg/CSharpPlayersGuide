namespace The_Final_Battle.Items
{
	public class ItemDef
	{
		public string DefName { get; }
		public SkillDef? SkillWhenUsed { get; }

		public ItemDef(string defName, SkillDef? skillWhenUsed = null)
		{
			DefName = defName;
			SkillWhenUsed = skillWhenUsed;
		}
	}
}
