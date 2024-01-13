using ConsoleIO;

namespace The_Final_Battle
{
    public class HumanCommander : ICommander
    {
        public IFightAction GetCombatAction(Fighter acting, out Fighter target, Fight fight)
        {
            int i = 0;
            while (i < acting.Actions.Count)
            {
                fight.Display.AddTemporaryMessage($"{i + 1} - {acting.Actions[i].Name}", MessageCategory.Info);
                i += 1;
            }
            var doNothing = new DoNothingFightAction();
            fight.Display.AddTemporaryMessage($"{i + 1} - {doNothing.Name}", MessageCategory.Warning);
            fight.Display.UpdateDisplay(fight);
            int choice = fight.Display.AskForTypeInRange<int>("Perform which action? ", 1, acting.Actions.Count + 1);

            if(choice == acting.Actions.Count+1)
            {
                target = acting;
                return doNothing;
            }
            else
            {
                target = fight.GetEnemyTeam(acting).Fighters[0];
                return acting.Actions[choice - 1];
            }
        }
	}
}
