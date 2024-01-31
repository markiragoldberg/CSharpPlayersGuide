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

		//else if (actingInventory.Count > 0 && choice == skills.Count) // "use item"
		//{
		//	List<string> itemOptions = [];
		//	for (int j = 0; j < actingInventory.ItemActions.Count; j++)
		//	{
		//		itemOptions.Add($"{j + 1} - {actingInventory.ItemActions[j].Item.Def.DefName} x{actingInventory.ItemActions[j].Item.Charges}");
		//	}
		//	itemOptions.Add($"{actingInventory.ItemActions.Count + 1} - Cancel");
		//	Screens.FightScreen.Display(fight);
		//	Screens.MessagesScreen.Display(log, 13 - itemOptions.Count);
		//	Screens.MenuScreen.Display(itemOptions, $"{acting.Name}: Use which item?");
		//	int itemChoice = ColoredConsole.AskForTypeInRange<int>("?> ", 1, itemOptions.Count) - 1;
		//	if (itemChoice < actingInventory.ItemActions.Count)
		//		action = actingInventory.ItemActions[itemChoice];
		////}
		//else // do nothing
		//{
		//	possibleTarget = acting;
		//	action = SkillAction.DoNothing;
		//}
		//if (action != null && possibleTarget == null)
		//{
		//SkillTarget skillTarget = action.Def.Target;
		//if (skillTarget.TargetType == TargetType.Self && skillTarget.Valid(fight, acting, acting))
		//	possibleTarget = acting;
		//else
		//{
		//	FightTeam targetTeam = skillTarget.TargetType == TargetType.Ally
		//							? fight.GetAllyTeam(acting)
		//							: fight.GetEnemyTeam(acting);
		//	List<Creature> possibleTargets = (from t in targetTeam.Fighters
		//									  where skillTarget.Valid(fight, acting, t)
		//									  select (t)).ToList();
		//	if (possibleTargets.Count == 1)
		//		possibleTarget = possibleTargets[0];
		//	else if (possibleTargets.Count > 1)
		//	{
		//		List<string> targetOptions = (from t in possibleTargets select t.Name).ToList();
		//		for (int k = 0; k < possibleTargets.Count; k++)
		//			targetOptions.Add($"{k + 1} - {possibleTargets[k].Name}");
		//		targetOptions.Add($"{possibleTargets.Count + 1} - Cancel");
		//		Screens.FightScreen.Display(fight);
		//		Screens.MessagesScreen.Display(log, 13 - targetOptions.Count);
		//		Screens.MenuScreen.Display(targetOptions, $"{acting.Name}: {action.Def.DefName}: Target who?");
		//		int targetChoice = ColoredConsole.AskForTypeInRange<int>("?> ", 1, targetOptions.Count) - 1;
		//		if (targetChoice < possibleTargets.Count)
		//			possibleTarget = possibleTargets[targetChoice];
		//	}
		//}
		//}
	}
}
