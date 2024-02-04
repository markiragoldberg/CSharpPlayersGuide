using The_Final_Battle.Defs;
using The_Final_Battle.FightActions;

namespace The_Final_Battle.Items
{
	public class Weapon
	{
		public WeaponDef Def { get; }
		public string Label => Def.Label;
		public List<SkillAction> Actions { get; }

		public Weapon(WeaponDef def)
		{
			Def = def;
			Actions = new();
			foreach (var attack in Def.Attacks)
			{
				Actions.Add(new SkillAction(attack));
			}
		}
	}
}
