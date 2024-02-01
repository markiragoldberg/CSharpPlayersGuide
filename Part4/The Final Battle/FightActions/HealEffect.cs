namespace The_Final_Battle
{
	public class HealEffect : ISkillEffect
	{
		public Dice HealDice { get; }
		public string HealMessageFormat { get; }
		public double AverageResult => HealDice.AverageResult;

		public HealEffect(Dice healDice, string? healMessageFormat = null)
		{
			HealDice = healDice;
			HealMessageFormat = healMessageFormat ?? "{0} healed {1} for {2} health.";
		}
		public void Apply(Messaging.Log log, Creature user, Creature target)
		{
			int healing = HealDice.Roll();
			target.AdjustHealth(-healing);
			log.AddMessage(String.Format(HealMessageFormat, user.Name, target.Name, healing),
						   Messaging.MessageCategory.Good);
		}
	}
}
