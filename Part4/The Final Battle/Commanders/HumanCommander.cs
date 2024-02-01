﻿using ConsoleIO;
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

			List<SkillAction> skillActions = acting.Skills.Actions.FindAll(s => s.Usable(fight, acting));
			List<ItemAction> itemActions = actingInventory.ItemActions.FindAll(i => i.Usable(fight, acting));

			List<(string, Action)> topMenu = [];
			topMenu.Add(("Nothing",
				new Action(() => { action = SkillAction.DoNothing; possibleTarget = acting; })));
			if (itemActions.Count > 0)
				topMenu.Add(("Use Item", () => action = UseItem(fight, log, acting, itemActions)));
			foreach (SkillAction skillAction in skillActions)
				topMenu.Add((skillAction.Def.DefName.ToUpper(), () => action = skillAction));

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
			List<(string, Action)> itemMenu = [];
			foreach (ItemAction itemAction in actions)
				itemMenu.Add(($"{itemAction.Item.Def.DefName} x{itemAction.Item.Charges}", 
					          () => possibleAction = itemAction));
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
				possibleTargets = fight.GetAllyTeam(acting).Fighters.Where(f => skillTarget.Valid(fight, acting, f)).ToList();
			else // if(skillTarget.TargetType == TargetType.Enemy)
				possibleTargets = fight.GetEnemyTeam(acting).Fighters.Where(f => skillTarget.Valid(fight, acting, f)).ToList();

			if (possibleTargets.Count == 1)
				return possibleTargets[0];

			List<(string, Action)> targetMenu = new();
			foreach (Creature targ in possibleTargets)
				targetMenu.Add((targ.Name, () => possibleTarget = targ));
			MenuScreen.Display(targetMenu,
				$"{acting.Name}: {action.Def.DefName}: Target who?", abortable: true);
			return possibleTarget;
		}
	}
}
