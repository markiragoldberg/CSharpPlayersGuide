using The_Final_Battle.Defs;

namespace The_Final_Battle.FightActions
{
    public class SkillAction : IFightAction
	{
		public AbilityDef Def { get; }
		public string Label => Def.Label;
		public bool IsAttack { get => Def.IsAttack; }
		public void Resolve(Fight fight, Messaging.Log log, Creature user, Creature target)
		{
			if(Def.Target.Valid(fight, user, target))
			{
				foreach (var effect in Def.Effects)
				{
					effect.Apply(log, user, target);
				}
				fight.GetAllyTeam(target).RemoveDead(fight, log);
			}
		}
		public bool Usable(Fight fight, Creature user) => Def.Usable(fight, user);

		public static SkillAction DoNothing = new(AbilityDef.DoNothing);
		public SkillAction(AbilityDef def)
		{
			Def = def;
		}
	}
}
