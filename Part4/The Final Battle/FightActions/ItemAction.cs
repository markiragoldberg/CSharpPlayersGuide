using The_Final_Battle.Defs;
using The_Final_Battle.Items;

namespace The_Final_Battle.FightActions
{
    public class ItemAction : IFightAction
	{
		public string Label => Def.Label;
		public Item Item { get; }
		public AbilityDef Def { get; }
		public bool IsAttack { get => Item.OnUseAbility != null && Item.OnUseAbility.IsAttack; }

		public void Resolve(Fight fight, Messaging.Log log, Creature user, Creature target)
		{
			AbilityDef? def = Item.OnUseAbility;
			if (def == null) return;

			if (def.Target.Valid(fight, user, target))
			{
				foreach (var effect in def.Effects)
				{
					effect.Apply(log, user, target);
				}
				fight.GetAllyTeam(target).RemoveDead(fight, log);
			}
			Item.AfterUse();
		}
		public bool Usable(Fight fight, Creature user) =>
			Item.OnUseAbility != null && Item.Charges > 0 && Item.OnUseAbility.Usable(fight, user);

		public ItemAction(Item item)
		{
			if (item.OnUseAbility == null)
				throw new ArgumentException("Item in ItemAction constructor has null OnUseAbility");
			Item = item;
			Def = item.OnUseAbility;
		}
	}
}
