namespace The_Final_Battle.Defs
{
    public class AbilityDef : Def
    {
        public override string Label { get; }
        public SkillTarget Target { get; }
        public List<ISkillEffect> Effects { get; }
        public bool IsAttack => Effects.Any(e => e is AttackEffect);

        public static AbilityDef DoNothing { get; } =
            new("doNothing", "Do Nothing",
                [new MessageEffect("{0} does nothing.", Messaging.MessageCategory.Info)],
                SkillTarget.SelfUnconditional);

        public AbilityDef(string defName, string label, List<ISkillEffect> effects, SkillTarget? target = null)
            : base(defName)
        {
            Label = label;
            Effects = effects;
            Target = target ?? SkillTarget.EnemyUnconditional;
        }
        public bool Usable(Fight fight, Creature user)
        {
            if (Target.TargetType == TargetType.Self)
                return Target.Valid(fight, user, user);
            else if (Target.TargetType == TargetType.Enemy || Target.TargetType == TargetType.Ally)
            {
                FightTeam? targetTeam = Target.TargetType == TargetType.Enemy ? fight.GetEnemyTeam(user) : fight.GetAllyTeam(user);
                if (targetTeam != null)
                {
                    return targetTeam.Fighters.Any(c => Target.Valid(fight, user, c));
                }
            }
            throw new NotImplementedException("Unrecognized TargetType in SkillDef.Usable");
        }
    }
}
