namespace The_Final_Battle;

public class MindlessAICommander : ICommander
{
    public IFightAction GetCombatAction(Fighter fighter, out Fighter target, Fight fight)
    {
        var actions = from a in fighter.Actions where a is AttackAction select a;
        IFightAction? chosenAction = actions.FirstOrDefault();
        FightTeam enemyTeam = fight.GetEnemyTeam(fighter);
        if (chosenAction != null && enemyTeam.Count > 0)
        {
            target = fight.GetEnemyTeam(fighter)[0];
            return chosenAction;
        }
        else
        {
            target = fighter;
            return new DoNothingFightAction();
        }
    }
}
