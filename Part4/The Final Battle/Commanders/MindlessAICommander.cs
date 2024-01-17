namespace The_Final_Battle;

public class MindlessAICommander : ICommander
{
    public IFightAction GetCombatAction(Creature fighter, out Creature target, Fight fight)
    {
        SkillDef? actionDef = fighter.Skills.Actions.FirstOrDefault();
        FightTeam enemyTeam = fight.GetEnemyTeam(fighter);
        if (actionDef != null && enemyTeam.Count > 0)
        {
            target = fight.GetEnemyTeam(fighter)[0];
            return new AttackAction(actionDef);
		}
        else
        {
            target = fighter;
            return new DoNothingFightAction();
        }
    }
}
