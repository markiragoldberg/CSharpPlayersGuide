namespace The_Final_Battle.FightActions
{
	public class AbilityAction(SkillDef def) : IFightAction
	{
		public void Resolve(Fight fight, Creature user, Creature target)
		{
			if(def.Target.Valid(fight, user, target))
			{
				foreach (var effect in def.Effects)
				{
					effect.Apply(fight.Display, user, target);
				}
				fight.GetAllyTeam(target).RemoveDead(fight);
			}
		}
	}
}
