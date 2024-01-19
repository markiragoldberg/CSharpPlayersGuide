using ConsoleIO;

namespace The_Final_Battle
{
    public class HumanCommander : ICommander
    {
        public IFightAction GetCombatAction(Creature acting, out Creature target, Fight fight)
        {
			//         SkillDef[] options = new SkillDef[acting.Skills.Actions.Count];
			//         int i = 0;
			//         foreach (SkillDef skill in acting.Skills.Actions)
			//{
			//             options[i] = skill;
			//	fight.Display.AddTemporaryMessage($"{i + 1} - {options[i].DefName.ToUpper()}", MessageCategory.Info);
			//             i += 1;
			//}
			var doNothing = new DoNothingFightAction();
			//         fight.Display.AddTemporaryMessage($"{i + 1} - {doNothing.Name.ToUpper()}", MessageCategory.Warning);
			//         fight.Display.UpdateDisplay(fight);
			//         int choice = fight.Display.AskForTypeInRange<int>(
			//             "Perform which action? ", 1, options.Length + 1) - 1;

			//         if(choice == options.Length)
			//         {
			//             target = acting;
			//             return doNothing;
			//         }
			//         else
			//         {
			//             target = fight.GetEnemyTeam(acting).Fighters[0];
			//             return new AttackAction(options[choice]);
			//         }

			List<string> options = [];
            foreach (SkillDef def in acting.Skills.Actions)
                options.Add(def.DefName.ToUpper());
            options.Add("Do Nothing");
            int choice = fight.Display.AskForMenuOption("?> ", options, "Perform which action?");
			if (choice >= acting.Skills.Actions.Count)
			{
				target = acting;
				return doNothing;
			}
			else
			{
				target = fight.GetEnemyTeam(acting)?.Fighters[0];
				return new AttackAction(acting.Skills.Actions[choice]);
			}
		}
	}
}
