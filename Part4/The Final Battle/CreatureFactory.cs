namespace The_Final_Battle
{
	public class CreatureFactory
	{
		private Dictionary<string, CreatureDef> _creatureDefs;
		private Dictionary<string, SkillDef> _skillDefs;
		public Creature CreateCreature(string defName, string? name = null)
		{
			Creature creature = new Creature(_creatureDefs[defName], name);
			return creature;
		}
		public CreatureFactory()
		{
			string defName;
			_skillDefs = new();

			defName = "punch";
			_skillDefs[defName] = new SkillDef(defName, damageDice: new(1, 1, 0), hitChance: 1.0);
			defName = "quick shot";
			_skillDefs[defName] = new SkillDef(defName, damageDice: new(3, 3, 0), hitChance: 0.5);
			defName = "bone crunch";
			_skillDefs[defName] = new SkillDef(defName, damageDice: new(0, 1, 0), hitChance: 1.0);
			defName = "unravel";
			_skillDefs[defName] = new SkillDef(defName, damageDice: new(0, 2, 0), hitChance: 1.0);

			defName = "heal";
			_skillDefs[defName] = new SkillDef(
				defName, damageDice: new(0, 2, 0), hitChance: 1.0, target: SkillTarget.AllyUnconditional);
			_skillDefs[defName].Target.Conditions.Add(new TargetIsInjured());

			_creatureDefs = new();

			defName = "hero";
			_creatureDefs[defName] = new CreatureDef(defName, 25, null, _skillDefs["punch"]);

			defName = "fletcher";
			_creatureDefs[defName] = new CreatureDef(defName, 15, null, _skillDefs["punch"], _skillDefs["quick shot"]);

			defName = "skeleton";
			_creatureDefs[defName] = new CreatureDef(defName, 4, defaultName: "Skeleton", _skillDefs["bone crunch"]);

			defName = "boss";
			_creatureDefs[defName] = new CreatureDef(defName, 25, defaultName: "The Uncoded One",  _skillDefs["unravel"]);
		}
	}
}