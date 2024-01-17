namespace The_Final_Battle
{
	public class SkillTracker
	{
		public HashSet<SkillDef> Actions { get; } = new();
		public void AddSkill(SkillDef def)
		{
			Actions.Add(def);
		}
	}
}
