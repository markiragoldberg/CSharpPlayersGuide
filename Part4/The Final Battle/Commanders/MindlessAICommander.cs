namespace The_Final_Battle.Commanders;

public class MindlessAICommander : ICommander
{
    public IFightAction GetCombatAction(Fight fight, Messaging.Log _, Creature fighter, out Creature target)
    {
        SkillDef? actionDef = fighter.Skills.Actions
                              .Where(s => s.Usable(fight, fighter) && s.IsAttack)
                              .ToList().RandomElement();
        Creature? enemy = fight.GetEnemyTeam(fighter).RandomTarget();
		if (actionDef != null && enemy != null)
        {
            target = enemy;
            return new FightActions.AbilityAction(actionDef);
		}
        else
        {
            target = fighter;
            return new FightActions.AbilityAction(SkillDef.DoNothing);
        }
    }
}
