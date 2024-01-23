namespace The_Final_Battle.Commanders
{
	public class WaitForInputAICommander : ICommander
	{
		private ICommander _aiCommander =  new MindlessAICommander();
		public IFightAction GetCombatAction(Fight fight, Messaging.Log log, Creature acting, out Creature target)
		{
			log.AddTemporaryMessage("Press any key...", Messaging.MessageCategory.Info);
			Screens.FightScreen.Display(fight);
			Screens.MessagesScreen.Display(log);
			Console.ReadKey(true);
			return _aiCommander.GetCombatAction(fight, log, acting, out target);
		}
	}
}
