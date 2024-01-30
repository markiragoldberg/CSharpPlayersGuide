using The_Final_Battle.FightActions;

namespace The_Final_Battle.Items
{
	public class Inventory
	{
		private List<Item> _items = new();
		public List<Item> Items { get => _items; }
		public int Count { get => _items.Count; }
		public Item this[int index] => _items[index];
		public List<ItemAction> ItemActions { get; } = [];

		public void Add(Item item)
		{
			Item? duplicate = _items.Where(i => i.Def == item.Def).FirstOrDefault();
			if (duplicate != null)
				duplicate.Charges += item.Charges;
			else
			{
				_items.Add(item);
				item.Parent = this;
				if (item.OnUseAbility != null)
					ItemActions.Add(new ItemAction(item));
			}
		}
		public void Remove(Item item)
		{
			_items.Remove(item);
			ItemActions.RemoveAll(action => action.Item == item);
		}
	}
}
