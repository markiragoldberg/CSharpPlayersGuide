using ConsoleIO;
using The_Final_Battle.FightActions;

namespace The_Final_Battle
{
    public class HumanCommander : ICommander
    {
        public IFightAction GetCombatAction(Fight fight, Creature acting, out Creature target)
        {
			//var doNothing = new DoNothingFightAction();
			List<(string text, SkillDef value)> options = [];
            foreach (SkillDef skillDef in acting.Skills.Actions.FindAll(d => d.Usable(fight, acting)))
                options.Add((skillDef.DefName.ToUpper(), skillDef));
            options.Add(("Do Nothing", SkillDef.DoNothing));
            SkillDef def = fight.Display.AskForMenuOption<SkillDef>("?> ", options, "Perform which action?");
			if(def.Target.TargetType == TargetType.Self)
			{
				target = acting;
				return new AbilityAction(def);
			}
			FightTeam targetTeam;
			if (def.Target.TargetType == TargetType.Ally)
			{
				targetTeam = fight.GetAllyTeam(acting);
			}
			else // if (def.Target.TargetType == TargetType.Enemy)
			{
				targetTeam = fight.GetEnemyTeam(acting);
			}
			fight.Display.UpdateDisplay(fight); // clear last menu
			List<(string text, Creature value)> targetOptions = (from t in targetTeam.Fighters
									where def.Target.Valid(fight, acting, t)
									select (t.Name, t)).ToList();
			if (targetOptions.Count > 1)
			{
				target = fight.Display.AskForMenuOption("?> ", targetOptions, "Target who?");
			}
			else
				target = targetOptions[0].value;
			return new AbilityAction(def);
		}
	}
}
