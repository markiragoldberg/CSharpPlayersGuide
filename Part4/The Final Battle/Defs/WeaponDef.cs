namespace The_Final_Battle.Defs
{
	public class WeaponDef : Def
	{
		public override string Label { get; }
		public List<AbilityDef> Attacks { get; }

		public WeaponDef(string defName, string label, List<AbilityDef> attacks) : base(defName)
		{
			Label = label;
			Attacks = attacks;
		}
	}
}
