using ConsoleIO;
using System;
using System.Collections.Generic;
using The_Final_Battle.FightActions;
using The_Final_Battle.Items;
using The_Final_Battle.Screens;

namespace The_Final_Battle.Commanders
{
	public class HumanCommander : ICommander
	{
		public IFightAction GetCombatAction(Fight fight, Messaging.Log log, Creature acting, out Creature target)
		{
			FightTeam actingTeam = fight.GetAllyTeam(acting);
			Inventory actingInventory = actingTeam.Inventory;

			IFightAction? action = null;
			Creature? possibleTarget = null;

			List<SkillAction> skillActions = acting.Skills.Actions;
			List<ItemAction> itemActions = actingInventory.ItemActions;

			List<MenuOption> topMenu = [];
			topMenu.Add(new("Nothing",
				new Action(() => { action = SkillAction.DoNothing; possibleTarget = acting; })));
			topMenu.Add(new("Use Item", 
				() => action = UseItem(fight, log, acting, itemActions),
				enabled: itemActions.Count > 0));
			foreach (SkillAction skillAction in skillActions)
				topMenu.Add(
					new(skillAction.Label.ToUpper(),
					() => action = skillAction,
					skillAction.Usable(fight, acting)));

			while (action == null || possibleTarget == null)
			{
				action = null;
				possibleTarget = null;

				Screens.FightScreen.Display(fight);
				Screens.MessagesScreen.Display(log, 13 - topMenu.Count);
				Screens.MenuScreen.Display(topMenu, $"{acting.Name}: What do you do?");


				if (action != null)
					possibleTarget = GetTarget(fight, log, acting, action);
				if (possibleTarget == null)
					action = null;
			}
			target = possibleTarget;
			return action;
		}

		private static IFightAction? UseItem(Fight fight, Messaging.Log log, Creature acting, List<ItemAction> actions)
		{
			if (actions.Count < 1)
				return null;

			ItemAction? possibleAction = null;
			List<MenuOption> itemMenu = [];
			foreach (ItemAction itemAction in actions)
				itemMenu.Add(new(itemAction.Item.Label, () => possibleAction = itemAction,
							  enabled : itemAction.Usable(fight, acting)));
			Screens.FightScreen.Display(fight);
			Screens.MessagesScreen.Display(log, 13 - itemMenu.Count);
			Screens.MenuScreen.Display(itemMenu, $"{acting.Name}: Use which item?", abortable: true);
			return possibleAction;
		}

		private static Creature? GetTarget(Fight fight, Messaging.Log log, Creature acting, IFightAction action)
		{
			SkillTarget skillTarget = action.Def.Target;
			if (skillTarget.TargetType == TargetType.Self)
				return skillTarget.Valid(fight, acting, acting) ? acting : null;

			Creature? possibleTarget = null;

			FightTeam targetTeam;
			if (skillTarget.TargetType == TargetType.Ally)
				targetTeam = fight.GetAllyTeam(acting);
			else if (skillTarget.TargetType == TargetType.Enemy)
				targetTeam = fight.GetEnemyTeam(acting);

			List<Creature> possibleTargets;
			if (skillTarget.TargetType == TargetType.Ally)
				possibleTargets = fight.GetAllyTeam(acting).Fighters;
			else // if(skillTarget.TargetType == TargetType.Enemy)
				possibleTargets = fight.GetEnemyTeam(acting).Fighters;

			if (possibleTargets.Count == 1)
				return possibleTargets[0];

			List<MenuOption> targetMenu = new();
			foreach (Creature targ in possibleTargets)
				targetMenu.Add(new(targ.Name, () => possibleTarget = targ,
					enabled: skillTarget.Valid(fight, acting, targ)));
			MenuScreen.Display(targetMenu,
				$"{acting.Name}: {action.Label}: Target who?", abortable: true);
			return possibleTarget;
		}
	}
}
