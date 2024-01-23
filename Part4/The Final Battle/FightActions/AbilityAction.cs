namespace The_Final_Battle.FightActions
{
	public class AbilityAction(SkillDef def) : IFightAction
	{
		public void Resolve(Fight fight, Messaging.Log log, Creature user, Creature target)
		{
			if(def.Target.Valid(fight, user, target))
			{
				foreach (var effect in def.Effects)
				{
					effect.Apply(log, user, target);
				}
				fight.GetAllyTeam(target).RemoveDead(fight, log);
			}
		}
	}
}
