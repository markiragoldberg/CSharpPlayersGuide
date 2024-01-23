using ConsoleIO;
using The_Final_Battle.FightActions;

namespace The_Final_Battle
{
    public class HumanCommander : ICommander
    {
        public IFightAction GetCombatAction(Fight fight, Messaging.Log log, Creature acting, out Creature target)
        {
			//var doNothing = new DoNothingFightAction();
			List<(string text, SkillDef value)> options = [];
            foreach (SkillDef skillDef in acting.Skills.Actions.FindAll(d => d.Usable(fight, acting)))
                options.Add((skillDef.DefName.ToUpper(), skillDef));
            options.Add(("Do Nothing", SkillDef.DoNothing));

			Screens.FightScreen.Display(fight);
			Screens.MessagesScreen.Display(log, 13 - options.Count);
			SkillDef def = ColoredConsole.AskForMenuOption<SkillDef>("?> ", options, "Perform which action?");

			if(def.Target.TargetType == TargetType.Self)
			{
				target = acting;
				return new AbilityAction(def);
			}
			FightTeam targetTeam = def.Target.TargetType == TargetType.Ally
															? fight.GetAllyTeam(acting)
															: fight.GetEnemyTeam(acting);
			List<(string text, Creature value)> targetOptions = (from t in targetTeam.Fighters
																 where def.Target.Valid(fight, acting, t)
																 select (t.Name, t)).ToList();

			Screens.FightScreen.Display(fight); // clear last menu
			Screens.MessagesScreen.Display(log, 13 - targetOptions.Count);
			if (targetOptions.Count > 1)
			{
				target = ColoredConsole.AskForMenuOption("?> ", targetOptions, "Target who?");
			}
			else
				target = targetOptions[0].value;
			return new AbilityAction(def);
		}
	}
}
