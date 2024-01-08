public class MindlessAICommander : ICommander
{
    public IFightAction GetCombatAction(Fighter fighter, out Fighter target, Fight fight)
    {
        var actions = from a in fighter.Actions where a is AttackAction select a;
        IFightAction? chosenAction = actions.FirstOrDefault();
        if (chosenAction != null)
        {
            target = fight.GetEnemyTeam(fighter).Fighters[0];
            return chosenAction;
        }
        else
        {
            target = fighter;
            return new DoNothingFightAction();
        }
    }
}
