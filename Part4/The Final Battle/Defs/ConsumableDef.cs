namespace The_Final_Battle.Defs
{
    public class ConsumableDef : Def
    {
        public override string Label { get; }
        public AbilityDef? OnUseAbility { get; }

        public ConsumableDef(string defName, string label, AbilityDef? skillWhenUsed = null) : base(defName)
        {
            Label = label;
            OnUseAbility = skillWhenUsed;
        }
    }
}
