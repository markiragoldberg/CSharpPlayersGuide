namespace The_Final_Battle.Items
{
	public class Inventory
	{
		public List<Item> _items = new List<Item>();
		public int Count { get => _items.Count }

		public void Add(Item item)
		{
			Item? duplicate = _items.Where(i => i.Def == item.Def).FirstOrDefault();
			if (duplicate != null)
				duplicate.StackCount += item.StackCount;
			else
				_items.Add(item);
		}
		public void Remove(Item item) => _items.Remove(item);
	}
}
