namespace The_Final_Battle.Commanders;

public interface ICommander
{
    public IFightAction GetCombatAction(Fight fight, Messaging.Log log, Creature acting, out Creature target);
}
