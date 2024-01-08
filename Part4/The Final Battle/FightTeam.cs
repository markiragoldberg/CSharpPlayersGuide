using ConsoleIO;

public class FightTeam
{
    public bool Active
    {
        get
        {
            foreach (Fighter fighter in Fighters)
            {
                if (fighter.Health > 0)
                    return true;
            }
            return false;
        }
    }
    public List<Fighter> Fighters { get; }
    private ICommander _commander;

    public FightTeam(List<Fighter> fighters)
    {
        Fighters = fighters;
        _commander = new MindlessAICommander();
    }
    public void DoRound(Fight fight)
    {
        foreach (var fighter in Fighters)
        {
            TakeTurn(fighter, fight);
        }
    }
    private void TakeTurn(Fighter acting, Fight fight)
    {
        ColoredConsole.WriteLine($"It is {acting.Name}'s turn.", ConsoleColor.Yellow);
        Thread.Sleep(250);
        _commander.GetCombatAction(acting, out Fighter target, fight).Resolve(acting, target, fight);
        Thread.Sleep(400);
    }
}
