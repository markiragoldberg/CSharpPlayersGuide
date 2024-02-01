using The_Final_Battle.FightActions;

namespace The_Final_Battle.Commanders;

public class MindlessAICommander : ICommander
{
    public IFightAction GetCombatAction(Fight fight, Messaging.Log _, Creature fighter, out Creature target)
    {
        FightTeam allyTeam = fight.GetAllyTeam(fighter);

		List<IFightAction> allActions = [];
        allActions.AddRange(fighter.Skills.Actions.Where(s => s.Usable(fight, fighter)));
        allActions.AddRange(allyTeam.Inventory.ItemActions.Where(s => s.Usable(fight, fighter)));

        var actionPriorities = new (double priority, IFightAction action)[allActions.Count];
        for (int i = 0; i < allActions.Count; i++)
            actionPriorities[i] = (ActionPriority(fight, fighter, allActions[i]), allActions[i]);

        IFightAction? action = actionPriorities.MaxBy(it => it.priority).action;
        Creature? possibleTarget = null;
        if (action.IsAttack)
			possibleTarget = fight.GetEnemyTeam(fighter).RandomTarget();
        else // healing
			possibleTarget = fight.GetAllyTeam(fighter).Fighters.MaxBy(a => a.MaxHealth - a.Health);
		if (action != null && possibleTarget != null)
        {
            target = possibleTarget;
            return action;
		}
        else
        {
            target = fighter;
            return SkillAction.DoNothing;
        }
    }

    private double ActionPriority(Fight fight, Creature fighter, IFightAction action)
    {
        // only use consumables sometimes
        if (action is ItemAction itemAction && RNG.Roll(1,6,0) > itemAction.Item.Charges)
			return 0f;
        // estimate value as damage dealt/healed
        double expectedValue = 0;
        foreach (ISkillEffect effect in action.Def.Effects)
        {
            if (effect is AttackEffect attack)
				expectedValue += attack.AverageResult;
            else if (effect is HealEffect heal)
				expectedValue -= heal.AverageResult;
        }
        if(expectedValue < 0f) // cap healing at missing health of allies
		{
			expectedValue = Math.Abs(expectedValue);
            int missingHealth = 0;
            foreach (Creature ally in fight.GetAllyTeam(fighter).Fighters)
				missingHealth = Math.Max(missingHealth, ally.MaxHealth - ally.Health);
			expectedValue = Math.Min(expectedValue, missingHealth);
		}
        // Randomize final value somewhat
        expectedValue *= RNG.RandomMultiplier(0.5, 1.5);
        return expectedValue;
    }
}
