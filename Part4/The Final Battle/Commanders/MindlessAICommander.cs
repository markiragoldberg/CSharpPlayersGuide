namespace The_Final_Battle;

public class MindlessAICommander : ICommander
{
    public IFightAction GetCombatAction(Fight fight, Messaging.Log _, Creature fighter, out Creature target)
    {
        SkillDef? actionDef = fighter.Skills.Actions.FirstOrDefault(s => s.IsAttack);
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
