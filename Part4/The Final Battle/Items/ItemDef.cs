namespace The_Final_Battle.Items
{
	public class ItemDef
	{
		public string DefName { get; }
		public AbilityDef? OnUseAbility { get; }

		public ItemDef(string defName, AbilityDef? skillWhenUsed = null)
		{
			DefName = defName;
			OnUseAbility = skillWhenUsed;
		}
	}
}
