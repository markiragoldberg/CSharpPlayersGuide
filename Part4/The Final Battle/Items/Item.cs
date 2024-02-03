using The_Final_Battle.FightActions;

namespace The_Final_Battle.Items
{
	public class Item : IItem
	{
		public ItemDef Def { get; }
		public int Charges { get; set; }
		public Inventory? Parent { get; set; }
		public AbilityDef? OnUseAbility { get => Def.OnUseAbility; }
		public string Name => $"{Def.DefName} {(Charges > 1 ? $"({Charges})" : "" )}";

		public Item(ItemDef def, int charges = 1)
		{
			Def = def;
			Charges = charges;
			Parent = null;
		}

		public void AfterUse()
		{
			Charges -= 1;
			if (Charges <= 0 && Parent != null)
				Parent.Remove(this);
		}
	}
}
