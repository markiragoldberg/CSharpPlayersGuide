using The_Final_Battle.FightActions;

namespace The_Final_Battle
{
	public class SkillTracker
	{
		public List<SkillAction> Actions { get; } = [];
		public void AddSkill(AbilityDef def)
		{
			Actions.Add(new SkillAction(def));
		}
	}
}
