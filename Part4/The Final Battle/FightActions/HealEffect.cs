using static System.Net.Mime.MediaTypeNames;

namespace The_Final_Battle
{
	public class HealEffect : ISkillEffect
	{
		public Dice HealDice { get; }
		public string HealMessageFormat { get; }
		public void Apply(TFBConsole display, Creature user, Creature target)
		{
			int healing = HealDice.Roll();
			target.AdjustHealth(-healing);
			display.AddMessage(
				String.Format(HealMessageFormat, user.Name, target.Name, healing), MessageCategory.Good);
		}
		public HealEffect(Dice healDice, string? healMessageFormat = null)
		{
			HealDice = healDice;
			HealMessageFormat = healMessageFormat ?? "{0} healed {1} for {2} health.";
		}
	}
}
