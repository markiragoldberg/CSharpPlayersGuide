using ConsoleIO;
namespace The_Final_Battle;
public class FightTeam
{
    public bool HasAliveFighters
    {
        get
        {
            foreach (Creature fighter in _fighters)
            {
                if (fighter.Health > 0)
                    return true;
            }
            return false;
        }
    }
    public int Count { get => _fighters.Count; }
    public Fight? Fight { get; set; }
    public Creature this[int index] => _fighters[index];
    public List<Creature> Fighters { get => new(_fighters); }
    private List<Creature> _fighters;
    private ICommander _commander;

    public FightTeam(ICommander commander)
    {
        _fighters = new();
        _commander = commander;
    }
    public void DoRound()
    {
        foreach (var fighter in _fighters)
        {
            if (Fight == null || GetEnemyTeam()?.HasAliveFighters == false)
                break;
            TakeTurn(fighter, Fight);
        }
    }
    private void TakeTurn(Creature acting, Fight fight)
    {
        fight.Display.AddMessage($"It is {acting.Name}'s turn.", MessageCategory.Info);
        fight.Display.UpdateDisplay(fight);
        //Thread.Sleep(250);
        _commander.GetCombatAction(acting, out Creature target, fight).Resolve(acting, target, fight);
        //Thread.Sleep(400);
    }
    public bool Contains(Creature fighter) => _fighters.Contains(fighter);
    public void AddFighter(Creature fighter)
    {
        fighter.FightTeam = this;
        _fighters.Add(fighter);
    }
    public void RemoveDead(Fight fight)
    {
        int i = 0;
        while (i < _fighters.Count)
        {
            if (!_fighters[i].Alive)
            {
                fight.Display.AddMessage($"{_fighters[i].Name} has died!", MessageCategory.VeryBad);
                _fighters.RemoveAt(i);
            }
            else
                i += 1;
        }
    }

    public FightTeam? GetEnemyTeam()
    {
        return Fight?.GetEnemyTeam(this);
    }
}
