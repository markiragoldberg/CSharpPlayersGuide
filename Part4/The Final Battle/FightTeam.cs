namespace The_Final_Battle;
using Commanders;
using Items;

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
    public Inventory Inventory { get; } = new();
    public ICommander Commander { get; set; }

    public FightTeam(ICommander commander)
    {
        Fighters = new();
        Commander = commander;
    }
    public void DoRound(Fight fight, Messaging.Log log)
    {
        foreach (var fighter in Fighters)
        {
            TakeTurn(fighter, fight, log);
            if (!fight.GetEnemyTeam(this).CanFight)
                break;
		}
    }
    private void TakeTurn(Creature acting, Fight fight, Messaging.Log log)
    {
        var action = Commander.GetCombatAction(fight, log, acting, out Creature target);
        action.Resolve(fight, log, acting, target);
    }
    public bool Contains(Creature fighter) => Fighters.Contains(fighter);
    public void Add(Creature fighter) => Fighters.Add(fighter);
    public void RemoveDead(Fight fight, Messaging.Log log)
    {
        int i = 0;
        while (i < Fighters.Count)
        {
            if (!Fighters[i].Alive)
            {
                log.AddMessage($"{Fighters[i].Name} has died!", Messaging.MessageCategory.VeryBad);
				Fighters.RemoveAt(i);
            }
            else
                i += 1;
        }
    }
}
