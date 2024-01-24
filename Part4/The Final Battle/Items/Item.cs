using The_Final_Battle.FightActions;

namespace The_Final_Battle.Items
{
	public class Item : IItem
	{
		public ItemDef Def { get; }
		public int StackCount { get; set; }
		public AbilityAction? UseAction
		{
			get
			{
				if (Def.SkillWhenUsed != null)
				{
					return new AbilityAction(Def.SkillWhenUsed);
				}
				return null;
			}
		}

		public Item(ItemDef def, int stackCount = 1)
		{
			Def = def;
			StackCount = stackCount;
		}
	}
}
