namespace The_Final_Battle;

public interface ICommander
{
    public IFightAction GetCombatAction(Fight fight, Creature acting, out Creature target);
}
