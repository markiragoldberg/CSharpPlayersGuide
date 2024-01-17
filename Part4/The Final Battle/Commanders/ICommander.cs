namespace The_Final_Battle;

public interface ICommander
{
    public IFightAction GetCombatAction(Creature acting, out Creature target, Fight fight);
}
