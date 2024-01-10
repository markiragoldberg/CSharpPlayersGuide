using ConsoleIO;

public class FightTeam
{
    public bool HasAliveFighters
    {
        get
        {
            foreach (Fighter fighter in _fighters)
            {
                if (fighter.Health > 0)
                    return true;
            }
            return false;
        }
    }
    public int Count { get => _fighters.Count; }
    public Fighter this[int index] => _fighters[index];
    public List<Fighter> Fighters { get => new(_fighters); }
    private List<Fighter> _fighters;
    private ICommander _commander;

    public FightTeam(ICommander commander)
    {
        _fighters = new();
        _commander = commander;
    }
    public void DoRound(Fight fight)
    {
        foreach (var fighter in _fighters)
        {
            fight.WriteStatus();
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
    public bool Contains(Fighter fighter) => _fighters.Contains(fighter);
    public void AddFighter(Fighter fighter)
    {
        fighter.FightTeam = this;
        _fighters.Add(fighter);
    }
    public void RemoveDead()
    {
        int i = 0;
        while (i < _fighters.Count)
        {
            if (!_fighters[i].Alive)
            {
                ColoredConsole.WriteLine($"{_fighters[i].Name} has died!", ConsoleColor.Red);
                _fighters.RemoveAt(i);
            }
            else
                i += 1;
        }
    }
}
