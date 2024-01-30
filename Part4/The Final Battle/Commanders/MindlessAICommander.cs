using The_Final_Battle.FightActions;

namespace The_Final_Battle.Commanders;

public class MindlessAICommander : ICommander
{
    public IFightAction GetCombatAction(Fight fight, Messaging.Log _, Creature fighter, out Creature target)
    {
        IFightAction? action = fighter.Skills.Actions
                              .Where(s => s.Usable(fight, fighter) && s.IsAttack)
                              .ToList().RandomElement();
        Creature? enemy = fight.GetEnemyTeam(fighter).RandomTarget();
		if (action != null && enemy != null)
        {
            target = enemy;
            return action;
		}
        else
        {
            target = fighter;
            return SkillAction.DoNothing;
        }
    }
}
