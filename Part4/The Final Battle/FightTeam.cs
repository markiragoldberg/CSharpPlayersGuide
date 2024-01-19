using ConsoleIO;
namespace The_Final_Battle;
public class FightTeam
{
    public bool CanFight
    {
        get
        {
            foreach (Creature fighter in Fighters)
            {
                if (fighter.Health > 0)
                    return true;
            }
            return false;
        }
    }
    public Creature? RandomTarget()
    {
		return Fighters.RandomElement<Creature>();
	}
    public int Count { get => Fighters.Count; }
    public Creature this[int index] => Fighters[index];
    public List<Creature> Fighters { get; }
    private ICommander _commander;

    public FightTeam(ICommander commander)
    {
        Fighters = new();
        _commander = commander;
    }
    public void DoRound(Fight fight)
    {
        foreach (var fighter in Fighters)
        {
            TakeTurn(fighter, fight);
        }
    }
    private void TakeTurn(Creature acting, Fight fight)
    {
        // future me: you can probably delete this
        //fight.Display.AddMessage($"It is {acting.Name}'s turn.", MessageCategory.Info);
        fight.Display.UpdateDisplay(fight);
        _commander.GetCombatAction(fight, acting, out Creature target).Resolve(fight, acting, target);
    }
    public bool Contains(Creature fighter) => Fighters.Contains(fighter);
    public void Add(Creature fighter) => Fighters.Add(fighter);
    public void RemoveDead(Fight fight)
    {
        int i = 0;
        while (i < Fighters.Count)
        {
            if (!Fighters[i].Alive)
            {
                fight.Display.AddMessage($"{Fighters[i].Name} has died!", MessageCategory.VeryBad);
				Fighters.RemoveAt(i);
            }
            else
                i += 1;
        }
    }
}
