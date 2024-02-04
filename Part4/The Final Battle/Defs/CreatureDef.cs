namespace The_Final_Battle.Defs
{
	public class CreatureDef : Def
    {
        public override string Label { get; }
        public int MaxHealth { get; }
        public HashSet<AbilityDef> StartingSkills { get; }

        public CreatureDef(string defName, string label, int maxHealth, params AbilityDef[] startingSkills)
            : base(defName)
        {
            Label = label;
            MaxHealth = maxHealth;
            StartingSkills = [.. startingSkills];
        }
    }
}
