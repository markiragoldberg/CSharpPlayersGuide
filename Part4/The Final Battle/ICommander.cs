public interface ICommander
{
    public IFightAction GetCombatAction(Fighter acting, out Fighter target, Fight fight);
}
